using CashFlow.Data;
using CashFlow.Models;
using System.Globalization;

namespace CashFlow.PhoneScreens;

public partial class ConfigScreen : ContentPage
{
    private readonly CashFlowDatabase database;
    public ConfigScreen()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        database = new CashFlowDatabase();
        LoadData();
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void LoadData()
    {
        User user = await database.GetUserAsync();
        nombre.Text = RSAUtils.Desencriptar(RegisterScreen.namePrivKey, user.Name);
        apellidos.Text = RSAUtils.Desencriptar(RegisterScreen.surnamesPrivKey, user.Surnames);
        capital.Text = user.Capital.ToString(CultureInfo.InvariantCulture);
        gananciaM.Text = user.MensualEarning.ToString(CultureInfo.InvariantCulture);
    }

}