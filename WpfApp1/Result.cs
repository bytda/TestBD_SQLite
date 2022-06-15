using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Result
    {
        public string FluidCode { get; set; }
        public double PressureRating { get; set; }
        public double Temp { get; set; }
        public double RunLength { get; set; }
        public double LineWeight { get; set; }
        public double RunDiam { get; set; }
        public double WallThickness { get; set; }
        public string NPD { get; set; } = "";
    }
}
