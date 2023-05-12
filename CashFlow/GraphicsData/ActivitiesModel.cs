using CashFlow.Data;
using CashFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.GraphicsData
{
    public class ActivitiesModel
    {
        public readonly CashFlowDatabase database;
        List<Activities> activities = new List<Activities>();
        public float gastos;
        public float inversiones;
        readonly Color[] palette;

        public IReadOnlyList<Movimiento> MovimientosPie { get; }
        public ActivitiesModel() 
        {
            gastos = 0;
            inversiones = 0;
            database = new CashFlowDatabase();
            LoadActivitiesAsync();

            if (activities.Count > 0)
            {
                foreach (Activities activity in activities)
                {
                    if(activity.ActType == "Inversión")
                    {
                        inversiones += activity.Quantity;
                    }
                    else
                    {
                        gastos += activity.Quantity;
                    }
                }
            }

            MovimientosPie = new List<Movimiento>()
            {
                new Movimiento("Gastos", gastos),
                new Movimiento("Inversiones", inversiones)
            };

            palette = PaletteLoader.LoadPalette("#f45a4e", "#25a966");
        }

        public Color[] Palette => palette;

        private async void LoadActivitiesAsync()
        {
            activities = await database.GetActivitiesAsync();
        }
    }
    public class Movimiento
    {
        public string MovName { get; }
        public float Quantity { get; }

        public Movimiento(string actName, float quant)
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
}
