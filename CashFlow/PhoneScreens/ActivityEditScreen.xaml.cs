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
}