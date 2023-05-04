using CashFlow.Data;
using CashFlow.Models;
using System.Globalization;

namespace CashFlow;

public partial class RegisterScreen : ContentPage
{
    private readonly CashFlowDatabase database;
	public RegisterScreen()
	{
		InitializeComponent();
        database = new CashFlowDatabase();
	}

	async void Registrarse(object sender, EventArgs e)
	{
        await btnRegistro.ScaleTo(0.8, 50);
        await btnRegistro.ScaleTo(1, 50);
        User user = new User();

        if (string.IsNullOrWhiteSpace(gananciaM.Text))
        {
            double capI = Convert.ToDouble(capitalI.Text, CultureInfo.InvariantCulture);
            string nombreEncriptado = RSAUtils.Encriptar(nombre.Text.Trim());
            string apellidosEncriptado = RSAUtils.Encriptar(apellidos.Text.Trim());
            user = new User
            {
                Id = 1,
                Name = nombreEncriptado,
                Surnames = apellidosEncriptado,
                InitCapital = (float)Math.Round(capI, 2),
                Capital = (float)Math.Round(capI, 2)
            };
        }
        else
        {
            double capI = Convert.ToDouble(capitalI.Text, CultureInfo.InvariantCulture);
            double menE = Convert.ToDouble(gananciaM.Text, CultureInfo.InvariantCulture);
            string nombreEncriptado = RSAUtils.Encriptar(nombre.Text.Trim());
            string apellidosEncriptado = RSAUtils.Encriptar(apellidos.Text.Trim());
            user = new User
            {
                Id = 1,
                Name = nombreEncriptado,
                Surnames = apellidosEncriptado,
                InitCapital = (float)Math.Round(capI, 2),
                Capital = (float)Math.Round(capI, 2),
                MensualEarning = (float)Math.Round(menE, 2)
            };
        }

        var response = await database.SaveUserAsync(user);

        if(response > 0)
        {
            await DisplayAlert("Éxito", "Añadido correctamente" + (user.Capital + 2), "Aceptar");
        }
        else
        {
            await DisplayAlert("Error", "Error al añadir", "Aceptar");
        }
    }

	private bool entriesRight()
	{
		return !string.IsNullOrWhiteSpace(nombre.Text) && !string.IsNullOrWhiteSpace(apellidos.Text) && 
            float.TryParse(capitalI.Text, out float result);
	}

    private void on_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (entriesRight() && string.IsNullOrWhiteSpace(gananciaM.Text))
        {
            btnRegistro.Opacity = 0.8;
            btnRegistro.IsEnabled = true;
        }
        else
        {
            if(entriesRight() && float.TryParse(gananciaM.Text, out float result))
            {
                btnRegistro.Opacity = 0.8;
                btnRegistro.IsEnabled = true;
            }
            else
            {
                btnRegistro.Opacity = 0.2;
                btnRegistro.IsEnabled = false;
            }
        }
    }
}

