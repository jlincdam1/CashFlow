﻿using CashFlow.Models;
using CashFlow.Services;

namespace CashFlow;

public partial class RegisterScreen : ContentPage
{
    private readonly IUserService _userService;
	public RegisterScreen()
	{
		InitializeComponent();
        _userService = new UserService();
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

        var response = await _userService.AddUser(user);

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
}

