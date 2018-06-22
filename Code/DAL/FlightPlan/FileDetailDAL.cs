using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FlightPlan
{
   public class FileDetailDAL : DBHelper<File_Detail>
    {
        public List<ViewModel.FlightPlan.Location> GetByMasterID(string masterID)
        {
            var linq = from t in context.File_Detail
                       where t.MasterID == masterID
                       orderby t.Sort ascending
                       select new ViewModel.FlightPlan.Location
                       {
                           PointName = t.PointName,
                           Longitude = t.Longitude,
                           Latitude = t.Latitude
                       };
            return linq.ToList();
        }
    }
}
