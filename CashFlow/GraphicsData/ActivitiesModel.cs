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
        }

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
}
