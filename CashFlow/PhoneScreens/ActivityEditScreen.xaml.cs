using CashFlow.Data;
using CashFlow.Models;
using System.Globalization;

namespace CashFlow.PhoneScreens;

public partial class ActivityEditScreen : ContentPage
{
    private readonly CashFlowDatabase database;
    public ActivityEditScreen()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        database = new CashFlowDatabase();
        fechaMov.MaximumDate = DateTime.Today;
		CargarDatos();
    }

	private async void CargarDatos()
	{
		Activities activity = await database.GetActivityAsync(int.Parse(ActivitiesScreen.buttonId));
		if(activity.ActType == "Inversión")
		{
			tipoMovimiento.SelectedIndex = 0;
		}
		else
		{
            tipoMovimiento.SelectedIndex = 1;
        }
		invest.Text = activity.Quantity.ToString();
		fechaMov.Date = activity.ActivityDate;
	}

    private async void CancelAsync(object sender, EventArgs e)
    {
        bool respuesta = await DisplayAlert("Título", "¿Desea continuar?", "Sí", "No");
        if(respuesta)
        {
            await Navigation.PopAsync();
        }
    }

    private async void GuardarCambios(object sender, EventArgs e)
    {
        if(float.TryParse(invest.Text, out float result) && !invest.Text.StartsWith("-") && !string.IsNullOrWhiteSpace(invest.Text))
        {
            double inv = Convert.ToDouble(invest.Text, CultureInfo.InvariantCulture);
            User user = await database.GetUserAsync();
            Activities oldActivity = await database.GetActivityAsync(int.Parse(ActivitiesScreen.buttonId));
            Activities activity = new Activities
            {
                Id = int.Parse(ActivitiesScreen.buttonId),
                ActType = tipoMovimiento.SelectedItem.ToString(),
                Quantity = (float)Math.Round(inv, 2),
                ActivityDate = fechaMov.Date
            };
            if (activity.ActType == "Gasto" && oldActivity.ActType == "Inversión")
            {
                User editedUser = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surnames = user.Surnames,
                    InitCapital = user.InitCapital,
                    Capital = user.Capital - activity.Quantity - oldActivity.Quantity,
                    MensualEarning = user.MensualEarning
                };
                await database.UpdateUserAsync(editedUser);
            }
            else if (activity.ActType == "Inversión" && oldActivity.ActType == "Gasto")
            {
                User editedUser = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surnames = user.Surnames,
                    InitCapital = user.InitCapital,
                    Capital = user.Capital + activity.Quantity + oldActivity.Quantity,
                    MensualEarning = user.MensualEarning
                };
                await database.UpdateUserAsync(editedUser);
            }
            else
            {
                if(activity.ActType == "Inversión")
                {
                    User editedUser = new User
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surnames = user.Surnames,
                        InitCapital = user.InitCapital,
                        Capital = user.Capital - oldActivity.Quantity + activity.Quantity,
                        MensualEarning = user.MensualEarning
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
                        Capital = user.Capital + oldActivity.Quantity - activity.Quantity,
                        MensualEarning = user.MensualEarning
                    };
                    await database.UpdateUserAsync(editedUser);
                }
            }
            await database.UpdateActivity(activity);
            await DisplayAlert("Éxito", "Modificado el movimiento correctamente", "Aceptar");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Error, al actualizar, compruebe los datos", "Aceptar");
        }
    }
}