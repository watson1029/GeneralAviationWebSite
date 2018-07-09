using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Map
{
    public class Map_AppControlDAL : DBHelper<Map_AppControl>
    {
        public List<string> GetAppList()
        {
            var linq = from t in context.Map_AppControl
                       select t.AppID;
            return linq.Distinct().ToList();
        }

        public List<Map_AppControl> GetApp(string appID)
        {
            var linq = from t in context.Map_AppControl
                       where t.AppID == appID
                       orderby t.Sort
                       select t;
            return linq.ToList();
        }
    }
}
