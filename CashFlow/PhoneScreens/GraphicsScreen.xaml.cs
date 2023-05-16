using CashFlow.Data;
using CashFlow.Models;

namespace CashFlow.PhoneScreens;

public partial class GraphicsScreen : ContentPage
{
    public readonly CashFlowDatabase database;
    public List<Movimiento> MovimientosPie { get; set; }
    public List<Mov> Gastos { get; set; }

    public List<Mov> Inversiones { get; set; }

    public Act MovIngresos { get; set; }
    public Act MovGastos { get; set; }

    public GraphicsScreen()
	{
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        Animacion();
        mesPie.SelectedItem = DateTime.Now.Month;
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
        MovimientosPie = new List<Movimiento>()
        {
            new Movimiento("Gastos", 0),
            new Movimiento("Inversiones", 0)
        };
        if (activities.Any())
        {
            foreach (Activities activity in activities)
            {
                if (activity.ActivityDate.Month.ToString() == mesPie.SelectedItem.ToString() && activity.ActivityDate.Year == DateTime.Now.Year)
                {
                    if (activity.ActType == "Inversión")
                    {
                        MovimientosPie[1].Quantity += activity.Quantity;
                    }
                    else
                    {
                        MovimientosPie[0].Quantity += activity.Quantity;
                    }
                }
            }
            if (MovimientosPie[1].Quantity > 0 || MovimientosPie[0].Quantity > 0)
            {
                datosPie.DataSource = MovimientosPie;
                coloresPie.Palette = Palette;
            }
            else
            {
                datosPie.DataSource = null;
            }

        }
        else
        {
            datosPie.DataSource = null;
        }

    }

    private async void LoadMovimientosSeries()
    {
        Gastos = new List<Mov>()
        {
            new Mov() { Fecha = new DateTime(2023, 1, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 2, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 3, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 4, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 5, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 6, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 7, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 8, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 9, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 10, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 11, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 12, 1), Quantity = 0 },
        };
        Inversiones = new List<Mov>()
        {
            new Mov() { Fecha = new DateTime(2023, 1, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 2, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 3, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 4, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 5, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 6, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 7, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 8, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 9, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 10, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 11, 1), Quantity = 0 },
            new Mov() { Fecha = new DateTime(2023, 12, 1), Quantity = 0 },
        };
        List<Activities> activities = await database.GetActivitiesAsync();
        if (activities.Count > 0)
        {
            foreach (Activities activity in activities)
            {
                if(activity.ActivityDate.Year == DateTime.Now.Year)
                {
                    int mes = activity.ActivityDate.Month;
                    if (activity.ActType == "Gasto")
                    {
                        Gastos[mes - 1].Quantity += activity.Quantity;
                    }
                    else
                    {
                        Inversiones[mes - 1].Quantity += activity.Quantity;
                    }
                }
            }
        }
        gastosSeries.DataSource = Gastos;
        inversionesSeries.DataSource = Inversiones;
    }

    private async void LoadComparacion()
    {
        MovIngresos = new Act(
                "Ingresos",
                new ActValues(new DateTime(2023, 1, 1), 0),
                new ActValues(new DateTime(2023, 2, 1), 0),
                new ActValues(new DateTime(2023, 3, 1), 0),
                new ActValues(new DateTime(2023, 4, 1), 0),
                new ActValues(new DateTime(2023, 5, 1), 0),
                new ActValues(new DateTime(2023, 6, 1), 0),
                new ActValues(new DateTime(2023, 7, 1), 0),
                new ActValues(new DateTime(2023, 8, 1), 0),
                new ActValues(new DateTime(2023, 9, 1), 0),
                new ActValues(new DateTime(2023, 10, 1), 0),
                new ActValues(new DateTime(2023, 11, 1), 0),
                new ActValues(new DateTime(2023, 12, 1), 0));

        MovGastos = new Act(
            "Gastos",
            new ActValues(new DateTime(2023, 1, 1), 0),
            new ActValues(new DateTime(2023, 2, 1), 0),
            new ActValues(new DateTime(2023, 3, 1), 0),
            new ActValues(new DateTime(2023, 4, 1), 0),
            new ActValues(new DateTime(2023, 5, 1), 0),
            new ActValues(new DateTime(2023, 6, 1), 0),
            new ActValues(new DateTime(2023, 7, 1), 0),
            new ActValues(new DateTime(2023, 8, 1), 0),
            new ActValues(new DateTime(2023, 9, 1), 0),
            new ActValues(new DateTime(2023, 10, 1), 0),
            new ActValues(new DateTime(2023, 11, 1), 0),
            new ActValues(new DateTime(2023, 12, 1), 0));
        List<Activities> activities = await database.GetActivitiesAsync();
        if (activities.Count > 0)
        {
            foreach (Activities activity in activities)
            {
                if (activity.ActivityDate.Year == DateTime.Now.Year)
                {
                    int mes = activity.ActivityDate.Month;
                    foreach (ActValues a in MovGastos.Values)
                    {
                        if (activity.ActType == "Gasto" && a.Date.Month == mes)
                        {
                            a.Quantity += activity.Quantity;
                        }
                    }
                    foreach (ActValues a in MovIngresos.Values)
                    {
                        if (activity.ActType == "Inversión" && a.Date.Month == mes)
                        {
                            a.Quantity += activity.Quantity;
                        }
                    }
                }
            }
        }
        nombreIngresos.DisplayName = MovIngresos.Tipo;
        ingresosDatos.DataSource = MovIngresos.Values;
        nombreGastos.DisplayName = MovGastos.Tipo;
        gastosDatos.DataSource = MovGastos.Values;
    }

    protected override void OnAppearing()
    {
        LoadActivities();
        LoadMovimientosSeries();
        LoadComparacion();
        mesPie.SelectedIndexChanged += mesPie_SelectedIndexChanged;
    }

    private void mesPie_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadActivities();
    }
}

public class Movimiento
{
    public string MovName { get; set; }
    public double Quantity { get; set; }

    public Movimiento(string actName, double quant)
    {
        this.MovName = actName;
        this.Quantity = quant;
    }
}

public class Mov
{
    public DateTime Fecha { get; set; }
    public double Quantity { get; set; }
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

// Para la gráfica de comparación
public class Act
{
    public string Tipo { get; }
    public IList<ActValues> Values { get; }

    public Act(string mov, params ActValues[] values)
    {
        this.Tipo = mov;
        this.Values = new List<ActValues>(values);
    }
}

public class ActValues
{
    public DateTime Date { get; }
    public double Quantity { get; set; }

    public ActValues(DateTime month, double value)
    {
        this.Date = month;
        this.Quantity = value;
    }
}