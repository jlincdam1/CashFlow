using CashFlow.Data;
using CashFlow.Models;

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
            user = new User
            {
                Id = 1,
                Name = nombre.Text,
                Surnames = apellidos.Text,
                InitCapital = float.Parse(capitalI.Text),
                Capital = float.Parse(capitalI.Text)
            };
        }
        else
        {
            user = new User
            {
                Id = 1,
                Name = nombre.Text,
                Surnames = apellidos.Text,
                InitCapital = float.Parse(capitalI.Text),
                Capital = float.Parse(capitalI.Text),
                MensualEarning = float.Parse(gananciaM.Text)
            };
        }

        var response = await database.SaveUserAsync(user);

        if(response > 0)
        {
            await DisplayAlert("Éxito", "Añadido correctamente", "Aceptar");
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

    async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            User usuario = await database.GetUserAsync();
            info.Text = usuario.Name + usuario.Surnames + usuario.InitCapital + usuario.Capital + usuario.MensualEarning + usuario.Id;
        }
        catch
        {
            await DisplayAlert("Error", "Al mostrar", "Aceptar");
        }
    }

    async void Button_Clicked_1(object sender, EventArgs e)
    {
        await database.DeleteUserAsync();
    }
}

