namespace CashFlow.PhoneScreens;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
    }

    private async void Salir(object sender, EventArgs e)
    {
        bool respuesta = await DisplayAlert("Salir", "¿Desea salir de CashFlow?", "Sí", "No");
        if (respuesta)
        {
            Application.Current.Quit();
        }
    }
}
