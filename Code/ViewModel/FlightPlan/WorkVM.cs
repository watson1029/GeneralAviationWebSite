using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.FlightPlan
{
  public class WorkVM
    {
        public WorkVM()
        {
            pointList = new List<PointVM>();
        }
        public List<PointVM> pointList { get; set; }
        public string FlyHeight { get; set; }
        public string Raidus { get; set; }
    }
}
