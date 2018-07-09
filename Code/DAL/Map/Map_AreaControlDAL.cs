using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Map
{
    public class Map_AreaControlDAL : DBHelper<Map_AreaControl>
    {
        public List<string> GetAreaList()
        {
            var linq = from t in context.Map_AreaControl
                       select t.AreaID;
            return linq.Distinct().ToList();
        }

        public List<Map_AreaControl> GetArea(string areaID)
        {
            var linq = from t in context.Map_AreaControl
                       where t.AreaID == areaID
                       orderby t.Sort
                       select t;
            return linq.ToList();
        }
    }
}
