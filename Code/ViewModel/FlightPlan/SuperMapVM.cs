using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.FlightPlan
{
    public class SuperMapVM
    {
        public SuperMapVM() {
            Location = new List<FlightPlan.Location>();
        }
        public string MasterID { get; set; }
        public string WorkType { get; set; }
        public int? RaidusMile { get; set; }
        public List<Location> Location { get; set; }
    }

    public class Location
    {
        public string PointName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }

    public enum DrawType
    {
        /// <summary>
        /// 点间连线
        /// </summary>
        airline = 0,
        /// <summary>
        /// 点为圆心画圆
        /// </summary>
        circle = 1,
        /// <summary>
        /// 多点间形成闭合区间
        /// </summary>
        area = 2,
        /// <summary>
        /// 点为圆心画圆
        /// </summary>
        airlineCircle = 3,
        /// <summary>
        /// 点为圆心画圆，对连线做垂直线，画矩形
        /// </summary>
        airlineRectangle = 4
    }
}
