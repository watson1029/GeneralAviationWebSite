using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FlightPlan
{
    public class FileCurrentPlanMasterDAL : DBHelper<File_CurrentPlanMaster>
    {
        public List<string> GetMasterList(string flyPlanId)
        {
            var linq = from t in context.File_CurrentPlanMaster
                       where t.CurrentPlanID == flyPlanId
                       select t.MasterID;
            return linq.ToList();
        }
    }
}
