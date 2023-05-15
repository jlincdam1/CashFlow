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
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void LoadData()
    {
        User user = await database.GetUserAsync();
        nombre.Text = RSAUtils.Desencriptar(user.NamePrivkey, user.Name);
        apellidos.Text = RSAUtils.Desencriptar(user.SurnamesPrivKey, user.Surnames);
        capital.Text = user.Capital.ToString(CultureInfo.InvariantCulture);
        gananciaM.Text = user.MensualEarning.ToString(CultureInfo.InvariantCulture);
    }

    protected override void OnAppearing()
    {
        LoadData();
    }

    private async void EditProfile(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ConfigScreenEdit());
    }

    private async void DeleteUser(object sender, EventArgs e)
    {
        bool respuesta = await DisplayAlert("Eliminar usuario", "Al eliminar el usuario también se eliminarán todos sus movimientos. ¿Quiere continuear?", "Sí", "No");
        if (respuesta)
        {
            await database.DeleteUserAsync();
            await database.DeleteAllActivities();
            await DisplayAlert("Elminado todo", "Se ha eliminado el usuario junto a todos sus movimientos", "Aceptar");
            await Navigation.PushAsync(new RegisterScreen());
        }
    }
}