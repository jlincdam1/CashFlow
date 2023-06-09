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

    protected override void OnAppearing()
    {
        LoadData();
    }

    private async void LoadData()
    {
        User user = await database.GetUserAsync();
        nombre.Text = RSAUtils.Desencriptar(user.NamePrivkey, user.Name);
        apellidos.Text = RSAUtils.Desencriptar(user.SurnamesPrivKey, user.Surnames);
        capital.Text = user.Capital.ToString(CultureInfo.InvariantCulture);
        gananciaM.Text = user.MensualEarning.ToString(CultureInfo.InvariantCulture);
    }

    private async void EditProfile(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ConfigScreenEdit());
    }

    private async void DeleteActivities(object sender, EventArgs e)
    {
        bool respuesta = await DisplayAlert("Eliminar movimientos", "Est� a punto de eliminar todos los movimientos de la cuenta. �Quiere continuear?", "S�", "No");
        if (respuesta)
        {
            await database.DeleteAllActivities();
            User oldUser = await database.GetUserAsync();
            User user = new User()
            {
                Id = oldUser.Id,
                Name = oldUser.Name,
                Surnames = oldUser.Surnames,
                InitCapital = oldUser.InitCapital,
                Capital = oldUser.InitCapital,
                NamePrivkey = oldUser.NamePrivkey,
                SurnamesPrivKey = oldUser.SurnamesPrivKey
            };
            await database.UpdateUserAsync(user);
            capital.Text = user.Capital.ToString(CultureInfo.InvariantCulture);
            await DisplayAlert("Elminado", "Se han eliminado todos los movimientos que exist�an", "Aceptar");
        }
    }

    private async void DeleteUser(object sender, EventArgs e)
    {
        bool respuesta = await DisplayAlert("Eliminar usuario", "Al eliminar el usuario tambi�n se eliminar�n todos sus movimientos. �Quiere continuear?", "S�", "No");
        if (respuesta)
        {
            await database.DeleteUserAsync();
            await database.DeleteAllActivities();
            await DisplayAlert("Elminado todo", "Se ha eliminado el usuario junto a todos sus movimientos", "Aceptar");
            Application.Current.Quit();
        }
    }
}