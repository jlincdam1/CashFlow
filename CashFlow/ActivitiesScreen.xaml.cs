using CashFlow.Data;
using CashFlow.Models;
using System.Globalization;

namespace CashFlow;

public partial class ActivitiesScreen : ContentPage
{
    private readonly CashFlowDatabase database;
    public ActivitiesScreen()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        database = new CashFlowDatabase();
    }

    private async Task AddInvestAsync(object sender, EventArgs e)
    {
        double inv = Convert.ToDouble(invest.Text, CultureInfo.InvariantCulture);
        if (float.TryParse(invest.Text, out float result)) 
        {
            Activities activity = new Activities
            {
                UserId = 1,
                ActType = "Inversión",
                Quantity = (float)Math.Round(inv, 2),
                ActivityDate = DateTime.Now
            };
            await database.AddActivityAsync(activity);
            AbsoluteLayout element = new AbsoluteLayout { Margin = new Thickness(0, 25, 0, 5), AutomationId="Movimiento" + activity.Id};
            var stackLayout = (StackLayout)this.Content;
            Button button = new Button { FontFamily= "Montserrat-Medium", BackgroundColor= Color.FromArgb("#DEDEDE"), TextColor= Color.FromRgb(0, 0, 0),
            Text="+ " + activity.Quantity + "€                                      " + activity.ActivityDate.Date, CornerRadius = 10, Padding = new Thickness(0, 20, 0, 0)};
            button.Shadow = new Shadow()
            {
                Brush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Offset = new Point(10, 10),
                Radius = 10,
                Opacity = (float)0.3
            };
            AbsoluteLayout.SetLayoutBounds(button, new Rect(0.5, 0, 320, 67));
            AbsoluteLayout.SetLayoutFlags(button, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
            element.Children.Add(button);
            Label label = new Label { Text = activity.ActType, FontAttributes = FontAttributes.Bold, FontFamily="Montserrat-Medium"};
            AbsoluteLayout.SetLayoutBounds(label, new Rect(8, 0.4, 330, 40));
            AbsoluteLayout.SetLayoutFlags(label, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
            element.Children.Add(label);
            stackLayout.Children.Add(element);
        }
        else
        {
            await DisplayAlert("Error", "Error, introduzca la inversión de forma correcta", "Aceptar");
        }
    }

    private async Task AddOutlayAsync(object sender, EventArgs e)
    {
        double outl = Convert.ToDouble(outlay.Text, CultureInfo.InvariantCulture);
        if (float.TryParse(outlay.Text, out float result))
        {
            Activities activity = new Activities
            {
                UserId = 1,
                ActType = "Gasto",
                Quantity = (float)Math.Round(outl, 2),
                ActivityDate = DateTime.Now
            };
            await database.AddActivityAsync(activity);
            AbsoluteLayout element = new AbsoluteLayout { Margin = new Thickness(0, 25, 0, 5), AutomationId = "Movimiento" + activity.Id };
            var stackLayout = (StackLayout)this.Content;
            Button button = new Button
            {
                FontFamily = "Montserrat-Medium",
                BackgroundColor = Color.FromArgb("#DEDEDE"),
                TextColor = Color.FromRgb(0, 0, 0),
                Text = "+ " + activity.Quantity + "€                                      " + activity.ActivityDate.Date,
                CornerRadius = 10,
                Padding = new Thickness(0, 20, 0, 0)
            };
            button.Shadow = new Shadow()
            {
                Brush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Offset = new Point(10, 10),
                Radius = 10,
                Opacity = (float)0.3
            };
            AbsoluteLayout.SetLayoutBounds(button, new Rect(0.5, 0, 320, 67));
            AbsoluteLayout.SetLayoutFlags(button, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
            element.Children.Add(button);
            Label label = new Label { Text = activity.ActType, FontAttributes = FontAttributes.Bold, FontFamily = "Montserrat-Medium" };
            AbsoluteLayout.SetLayoutBounds(label, new Rect(8, 0.4, 330, 40));
            AbsoluteLayout.SetLayoutFlags(label, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
            element.Children.Add(label);
            stackLayout.Children.Add(element);
        }
        else
        {
            await DisplayAlert("Error", "Error, introduzca el gasto de forma correcta", "Aceptar");
        }
    }
}