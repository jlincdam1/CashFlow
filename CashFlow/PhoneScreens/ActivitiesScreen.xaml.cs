using CashFlow.Data;
using CashFlow.Models;
using System;
using System.Diagnostics;
using System.Globalization;

namespace CashFlow.PhoneScreens
{
    public partial class ActivitiesScreen : ContentPage
    {
        private readonly CashFlowDatabase database;
        public static string buttonId;
        private User user;

        public ActivitiesScreen()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            database = new CashFlowDatabase();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnAppearing()
        {
            var content = (StackLayout)FindByName("layout");
            for (int i = content.Children.Count - 1; i >= 7; i--)
            {
                content.Children.RemoveAt(i);
            }
            Load();
        }

        private async void AddInvestAsync(object sender, EventArgs e)
        {
            if (float.TryParse(invest.Text, out float result) && !invest.Text.StartsWith("-") && !string.IsNullOrWhiteSpace(invest.Text))
            {
                double inv = Convert.ToDouble(invest.Text, CultureInfo.InvariantCulture);
                Activities activity = new Activities
                {
                    ActType = "Inversi�n",
                    Quantity = (float)Math.Round(inv, 2),
                    ActivityDate = DateTime.Now
                };
                await database.AddActivityAsync(activity);
                UpdateCapital(activity.Quantity, activity);
                AddElement(activity);
                await DisplayAlert("�xito", "Nueva inversi�n a�adida correctamente", "Aceptar");
                invest.Text = "";
                outlay.Text = "";
                user = await database.GetUserAsync();
                capActual.Text = user.Capital.ToString() + " �";
            }
            else
            {
                await DisplayAlert("Error", "Error, introduzca la inversi�n de forma correcta", "Aceptar");
            }
        }

        private async void AddOutlayAsync(object sender, EventArgs e)
        {
            if (float.TryParse(outlay.Text, out float result) && !outlay.Text.StartsWith("-") && !string.IsNullOrWhiteSpace(outlay
                .Text))
            {
                double outl = Convert.ToDouble(outlay.Text, CultureInfo.InvariantCulture);
                user = await database.GetUserAsync();
                if (outl < user.Capital)
                {
                    Activities activity = new Activities
                    {
                        ActType = "Gasto",
                        Quantity = (float)Math.Round(outl, 2),
                        ActivityDate = DateTime.Now
                    };
                    await database.AddActivityAsync(activity);
                    UpdateCapital(activity.Quantity, activity);
                    AddElement(activity);
                    await DisplayAlert("�xito", "Nuevo gasto a�adida correctamente", "Aceptar");
                    invest.Text = "";
                    outlay.Text = "";
                    user = await database.GetUserAsync();
                    capActual.Text = user.Capital.ToString() + " �";
                }
                else
                {
                    await DisplayAlert("Error", "Error, no le queda suficiente capital", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error, introduzca el gasto de forma correcta", "Aceptar");
            }
        }

        private async void Load()
        {
            List<Activities> activities = await database.GetActivitiesAsync();
            if (activities.Count > 0)
            {
                foreach (Activities activity in activities)
                {
                    AddElement(activity);
                }
            }
            user = await database.GetUserAsync();
            capActual.Text = user.Capital.ToString() + " �";
        }

        private void AddElement(Activities activity)
        {
            string texto = "";
            if (activity.ActType == "Inversi�n")
            {
                texto = "+ " + activity.Quantity + "�";
                texto = texto.PadRight(50) + activity.ActivityDate.ToShortDateString();
            }
            else
            {
                texto = "- " + activity.Quantity + "�";
                texto = texto.PadRight(50) + activity.ActivityDate.ToShortDateString();
            }
            AbsoluteLayout element = new AbsoluteLayout { Margin = new Thickness(0, 20, 0, 5)};
            var content = (StackLayout)FindByName("layout");
            Button button = new Button
            {
                AutomationId = activity.Id.ToString(),
                FontFamily = "Montserrat-Medium",
                BackgroundColor = Color.FromArgb("#DEDEDE"),
                TextColor = Color.FromRgb(0, 0, 0),
                Text = texto,
                CornerRadius = 10,
                Padding = new Thickness(0, 20, 0, 0),
            };
            button.Clicked += Acciones;
            button.Shadow = new Shadow()
            {
                Brush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Offset = new Point(10, 10),
                Radius = 10,
                Opacity = (float)0.3
            };
            AbsoluteLayout.SetLayoutBounds(button, new Rect(0.5, 0, 320, 67));
            AbsoluteLayout.SetLayoutFlags(button, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
            element.Children.Add(button);
            Label label = new Label { Text = activity.ActType, FontAttributes = FontAttributes.Bold, FontFamily = "Montserrat-Medium" };
            AbsoluteLayout.SetLayoutBounds(label, new Rect(8, 0.4, 330, 40));
            AbsoluteLayout.SetLayoutFlags(label, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
            element.Children.Add(label);
            content.Children.Add(element);
        }

        private async void Acciones(object sender, EventArgs e)
        {
            string opcion = await DisplayActionSheet("Elija una opci�n", "Salir", null, "Editar", "Eliminar");
            Button btn = (Button)sender;

            if (opcion == "Editar")
            {
                buttonId = btn.AutomationId;
                await Navigation.PushAsync(new ActivityEditScreen());
            }
            else if (opcion == "Eliminar")
            {
                Activities activity = await database.GetActivityAsync(int.Parse(btn.AutomationId));     
                await database.DeleteActivityAsync(activity);
                var content = (StackLayout)FindByName("layout");
                for (int i = content.Children.Count - 1; i >= 7; i--)
                {
                    content.Children.RemoveAt(i);
                }
                if (activity.ActType == "Inversi�n")
                {
                    activity.ActType = "Gasto";
                    UpdateCapital(activity.Quantity, activity);
                }
                else
                {
                    activity.ActType = "Inversi�n";
                    UpdateCapital(activity.Quantity, activity);
                }
                await DisplayAlert("�xito", "Movimiento eliminado correctamente", "Aceptar");
                invest.Text = "";
                outlay.Text = "";
                Load();
            }
        }

        private async void UpdateCapital(float cap, Activities act)
        {
            user = await database.GetUserAsync();
            if(act.ActType == "Inversi�n")
            {
                User editedUser = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surnames = user.Surnames,
                    InitCapital = user.InitCapital,
                    Capital = user.Capital + cap,
                    MensualEarning = user.MensualEarning,
                    NamePrivkey = user.NamePrivkey,
                    SurnamesPrivKey = user.SurnamesPrivKey
                };
                await database.UpdateUserAsync(editedUser);
            }
            else
            {
                User editedUser = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surnames = user.Surnames,
                    InitCapital = user.InitCapital,
                    Capital = user.Capital - cap,
                    MensualEarning = user.MensualEarning,
                    NamePrivkey = user.NamePrivkey,
                    SurnamesPrivKey = user.SurnamesPrivKey
                };
                await database.UpdateUserAsync(editedUser);
            }
        }
    }
}