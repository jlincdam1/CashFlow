﻿namespace CashFlow;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	async void Registrarse(object sender, EventArgs e)
	{
        await btnRegistro.ScaleTo(0.8, 50);
        await btnRegistro.ScaleTo(1, 50);
    }
}

