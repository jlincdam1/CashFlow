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
            Activities activity = new Activities
            {
                Id = int.Parse(ActivitiesScreen.buttonId),
                ActType = tipoMovimiento.SelectedItem.ToString(),
                Quantity = (float)Math.Round(inv, 2),
                ActivityDate = fechaMov.Date
            };
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