using DevExpress.Maui.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.PhoneScreens
{
    public class AxisLabelTextFormatter : IAxisLabelTextFormatter
    {
        public string Format(object value)
        {
            return ((DateTime)value).ToString("MM");
        }
    }
}
