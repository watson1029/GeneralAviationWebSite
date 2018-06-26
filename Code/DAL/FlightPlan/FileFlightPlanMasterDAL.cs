using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.EF;
namespace DAL.FlightPlan
{
    public class FileFlightPlanMasterDAL : DBHelper<File_FlightPlanMaster>
    {
        public List<string> GetMasterList(string flyPlanId)
        {
            var linq = from t in context.File_FlightPlanMaster
                       where t.FlightPlanID == flyPlanId
                       select t.MasterID;
            return linq.ToList();
        }
    }
}
