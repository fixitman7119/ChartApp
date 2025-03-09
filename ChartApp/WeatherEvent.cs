using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ChartApp
{
    public class WeatherEvent
    {
        public int id {  get; set; }
        public string day { get; set; }

        public int temp { get; set; }

        public override string? ToString()
        {
            return $"{id} - {day} - {temp}";
        }
    }
}
