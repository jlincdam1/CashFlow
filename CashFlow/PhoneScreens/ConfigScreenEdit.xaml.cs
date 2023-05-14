using CashFlow.Data;
using CashFlow.Models;
using System.Globalization;

namespace CashFlow.PhoneScreens;

public partial class ConfigScreenEdit : ContentPage
{
    private readonly CashFlowDatabase database;
    public ConfigScreenEdit()
	{
		InitializeComponent();
        database = new CashFlowDatabase();
        LoadData();
    }

    private async void LoadData()
    {
        User user = await database.GetUserAsync();
        name.Text = RSAUtils.Desencriptar(RegisterScreen.namePrivKey, user.Name);
        surnames.Text = RSAUtils.Desencriptar(RegisterScreen.surnamesPrivKey, user.Surnames);
        mensualEarning.Text = user.MensualEarning.ToString(CultureInfo.InvariantCulture);
    }
}