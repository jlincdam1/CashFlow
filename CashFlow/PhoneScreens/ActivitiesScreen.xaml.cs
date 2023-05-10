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

        private async void AddInvestAsync(object sender, EventArgs e)
        {
            if (float.TryParse(invest.Text, out float result) && !invest.Text.StartsWith("-") && !string.IsNullOrWhiteSpace(invest.Text))
            {
                double inv = Convert.ToDouble(invest.Text, CultureInfo.InvariantCulture);
                Activities activity = new Activities
                {
                    ActType = "Inversión",
                    Quantity = (float)Math.Round(inv, 2),
                    ActivityDate = DateTime.Now
                };
                await database.AddActivityAsync(activity);
                AddElement(activity);
                await DisplayAlert("Éxito", "Nueva inversión añadida correctamente", "Aceptar");
                invest.Text = "";
                outlay.Text = "";
            }
            else
            {
                await DisplayAlert("Error", "Error, introduzca la inversión de forma correcta", "Aceptar");
            }
        }

        private async void AddOutlayAsync(object sender, EventArgs e)
        {
            if (float.TryParse(outlay.Text, out float result) && !outlay.Text.StartsWith("-") && !string.IsNullOrWhiteSpace(outlay
                .Text))
            {
                double outl = Convert.ToDouble(outlay.Text, CultureInfo.InvariantCulture);
                Activities activity = new Activities
                {
                    ActType = "Gasto",
                    Quantity = (float)Math.Round(outl, 2),
                    ActivityDate = DateTime.Now
                };
                await database.AddActivityAsync(activity);
                AddElement(activity);
                await DisplayAlert("Éxito", "Nuevo gasto añadida correctamente", "Aceptar");
                invest.Text = "";
                outlay.Text = "";
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
        }

        private void AddElement(Activities activity)
        {
            string texto = "";
            if (activity.ActType == "Inversión")
            {
                texto = "+ " + activity.Quantity + "€";
                texto = texto.PadRight(50) + activity.ActivityDate.ToShortDateString();
            }
            else
            {
                texto = "- " + activity.Quantity + "€";
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
            string opcion = await DisplayActionSheet("Elija una opción", "Salir", null, "Editar", "Eliminar");
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
                for (int i = content.Children.Count - 1; i >= 6; i--)
                {
                    content.Children.RemoveAt(i);
                }
                Load();
                await DisplayAlert("Éxito", "Movimiento eliminado correctamente", "Aceptar");
                invest.Text = "";
                outlay.Text = "";
            }
        }


        protected override void OnAppearing()
        {
            var content = (StackLayout)FindByName("layout");
            for (int i = content.Children.Count - 1; i >= 6; i--)
            {
                content.Children.RemoveAt(i);
            }
            Load();
        }
    }
}