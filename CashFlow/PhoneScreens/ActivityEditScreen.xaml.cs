using CashFlow.Data;
using CashFlow.Models;

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
		if(activity.ActType == "Inversi�n")
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
        bool respuesta = await DisplayAlert("T�tulo", "�Desea continuar?", "S�", "No");
        if(respuesta)
        {
            await Navigation.PopAsync();
        }
    }

    private void GuardarCambios(object sender, EventArgs e)
    {

    }
}