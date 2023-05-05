﻿using CashFlow.Data;
using CashFlow.Models;
using System.Globalization;

namespace CashFlow;

public partial class RegisterScreen : ContentPage
{
    private readonly CashFlowDatabase database;
    public static string namePrivKey;
    public static string surnamesPrivKey;
	public RegisterScreen()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        database = new CashFlowDatabase();
	}

	async void Registrarse(object sender, EventArgs e)
	{
        await btnRegistro.ScaleTo(0.8, 50);
        await btnRegistro.ScaleTo(1, 50);
        User user;

        double capI = Convert.ToDouble(capitalI.Text, CultureInfo.InvariantCulture);
        string nombreEncriptado = RSAUtils.encriptar(nombre.Text.Trim());
        namePrivKey = RSAUtils.privKeyStr;
        string apellidosEncriptado = RSAUtils.encriptar(apellidos.Text.Trim());
        surnamesPrivKey = RSAUtils.privKeyStr;

        if (string.IsNullOrWhiteSpace(gananciaM.Text))
        {
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
            double menE = Convert.ToDouble(gananciaM.Text, CultureInfo.InvariantCulture);
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
            await DisplayAlert("Éxito", "Usuario registrado correctamente", "Aceptar");
            await Task.WhenAll(
                this.FadeTo(0, 1000)
            );
            await Navigation.PushAsync(new LoadingScreen());
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

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await database.DeleteUserAsync();
    }
}

