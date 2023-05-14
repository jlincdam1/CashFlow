using DevExpress.Maui.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.PhoneScreens
{
    public class CustomColorizer : ICustomPointColorizer
    {
        Color ICustomPointColorizer.GetColor(ColoredPointInfo info)
        {
            return Color.FromArgb("#CC2626");
        }
        public ILegendItemProvider GetLegendItemProvider()
        {
            return null;
        }
    }
}
