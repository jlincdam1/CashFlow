﻿using CashFlow.PhoneScreens;

namespace CashFlow;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new RegisterScreen());
	}
}
