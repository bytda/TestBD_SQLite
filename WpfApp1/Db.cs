using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Db
    {
        
        public List<PipingFluid> Fluids { set; get; } = new List<PipingFluid>();
        public List<PipingPhysical> Physical { set; get; } = new List<PipingPhysical>();
        public List<RunToPhysical> ToPhysical { set; get; } = new List<RunToPhysical>();
        public List<PipingRun> Run { set; get; } = new List<PipingRun>();
        public List<RunToFluid> ToFluid { set; get; } = new List<RunToFluid>();
        
    }
}
