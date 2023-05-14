using CashFlow.Data;
using CashFlow.Models;
using System.Globalization;
using static Android.Text.Method.TextKeyListener;

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

    private async void Cancel(object sender, EventArgs e)
    {
        bool respuesta = await DisplayAlert("Título", "¿Desea continuar?", "Sí", "No");
        if (respuesta)
        {
            await Navigation.PopAsync();
        }
    }

    private async void Save(object sender, EventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(surnames.Text) && !mensualEarning.Text.StartsWith("-") && !string.IsNullOrWhiteSpace(mensualEarning.Text))
        {
            User oldUser = await database.GetUserAsync();
            string nombreEncriptado = RSAUtils.encriptar(name.Text.Trim());
            RegisterScreen.namePrivKey = RSAUtils.privKeyStr;
            string apellidosEncriptado = RSAUtils.encriptar(surnames.Text.Trim());
            RegisterScreen.surnamesPrivKey = RSAUtils.privKeyStr;
            double menE = Convert.ToDouble(mensualEarning.Text, CultureInfo.InvariantCulture);
            User user = new User()
            {
                Id = oldUser.Id,
                Name = nombreEncriptado,
                Surnames = apellidosEncriptado,
                InitCapital = oldUser.InitCapital,
                Capital = oldUser.Capital,
                MensualEarning = (float)Math.Round(menE, 2)
            };
            await database.UpdateUserAsync(user);
            await DisplayAlert("Éxito", "Modificado el movimiento correctamente", "Aceptar");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Error, al actualizar, compruebe los datos", "Aceptar");
        }
    }
}