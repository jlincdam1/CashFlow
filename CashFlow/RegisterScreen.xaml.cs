namespace CashFlow;

public partial class RegisterScreen : ContentPage
{
	public RegisterScreen()
	{
		InitializeComponent();
	}

	async void Registrarse(object sender, EventArgs e)
	{
        await btnRegistro.ScaleTo(0.8, 50);
        await btnRegistro.ScaleTo(1, 50);
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

