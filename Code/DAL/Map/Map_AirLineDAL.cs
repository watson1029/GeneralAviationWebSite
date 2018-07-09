using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Map
{
    public class Map_AirLineDAL : DBHelper<Map_AirLine>
    {
        public List<string> GetAirlineList()
        {
            var linq = from t in context.Map_AirLine
                       select t.LineID;
            return linq.Distinct().ToList();
        }

        public List<Map_AirLine> GetAirLine(string lineID)
        {
            var linq = from t in context.Map_AirLine
                       where t.LineID == lineID
                       orderby t.Sort
                       select t;
            return linq.ToList();
        }
    }
}
