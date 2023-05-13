using CashFlow.Data;
using CashFlow.Models;

namespace CashFlow.PhoneScreens;

public partial class GraphicsScreen : ContentPage
{
    public readonly CashFlowDatabase database;

    public double gastos;
    public double inversiones;
    public List<Movimiento> MovimientosPie { get; set; }
    public GraphicsScreen()
	{
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        Animacion();
        mesPie.SelectedItem = DateTime.Now.Month;
        gastos = 0;
        inversiones = 0;
        database = new CashFlowDatabase();
        MovimientosPie = new List<Movimiento>();
        palette = PaletteLoader.LoadPalette("#f45a4e", "#25a966");
    }

    readonly Color[] palette;
    public Color[] Palette => palette;

    protected override bool OnBackButtonPressed()
    {
        return true;
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

    private async void LoadActivities()
    {
        List<Activities> activities = await database.GetActivitiesAsync();
        gastos = 0;
        inversiones = 0;
        if (activities.Count > 0)
        {
            foreach (Activities activity in activities)
            {
                if(activity.ActivityDate.Month.ToString() == mesPie.SelectedItem.ToString())
                {
                    if (activity.ActType == "Inversión")
                    {
                        inversiones += activity.Quantity;
                    }
                    else
                    {
                        gastos += activity.Quantity;
                    }
                }
            }
        }
        MovimientosPie = new List<Movimiento>()
        {
            new Movimiento("Gastos", gastos),
            new Movimiento("Inversiones", inversiones)
        };
        datosPie.DataSource = MovimientosPie;
        coloresPie.Palette = Palette;
    }

    protected override void OnAppearing()
    {
        LoadActivities();
        mesPie.SelectedIndexChanged += mesPie_SelectedIndexChanged;
    }

    private void mesPie_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadActivities();
    }
}

public class Movimiento
{
    public string MovName { get; }
    public double Quantity { get; }

    public Movimiento(string actName, double quant)
    {
        this.MovName = actName;
        this.Quantity = quant;
    }
}

public static class PaletteLoader
{
    public static Color[] LoadPalette(params string[] values)
    {
        Color[] colors = new Color[values.Length];
        for (int i = 0; i < values.Length; i++)
            colors[i] = Color.FromArgb(values[i]);
        return colors;
    }
}