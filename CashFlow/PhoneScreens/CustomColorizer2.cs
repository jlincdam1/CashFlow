using DevExpress.Maui.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.PhoneScreens
{
    public class CustomColorizer2 : ICustomPointColorizer
    {
        Color ICustomPointColorizer.GetColor(ColoredPointInfo info)
        {
            return Color.FromHex("#25a966");
        }
        public ILegendItemProvider GetLegendItemProvider()
        {
            return null;
        }
    }
}
