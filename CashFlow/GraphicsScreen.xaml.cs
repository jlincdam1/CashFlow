namespace CashFlow;

public partial class GraphicsScreen : ContentPage
{
	public GraphicsScreen()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        Animacion();
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
}