using CashFlow.Data;
using CashFlow.Models;

namespace CashFlow.PhoneScreens;

public partial class LoadingScreen : ContentPage
{
    private readonly CashFlowDatabase database;
    public LoadingScreen()
	{
		InitializeComponent();
        database = new CashFlowDatabase();
        NavigationPage.SetHasNavigationBar(this, false);
        Bienvenida();
        Animacion();
        NextPage();
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void Animacion()
	{
        await Task.WhenAll(
                this.FadeTo(0, 0)
        );
        await Task.WhenAll(
            this.FadeTo(1, 500)
        );
    }

    private async void Bienvenida()
    {
        User user = await database.GetUserAsync();
        string nombreDesencriptado = RSAUtils.Desencriptar(user.NamePrivkey, user.Name);
        bienvenida.Text = "¡BIENVENIDO " + nombreDesencriptado.ToUpper() + "!";
    }

    private async void NextPage()
    {
        await Task.Delay(2000);
        await Task.WhenAll(
            this.FadeTo(0, 1000)
        );
        await Navigation.PushAsync(new AppShell());
    }
}