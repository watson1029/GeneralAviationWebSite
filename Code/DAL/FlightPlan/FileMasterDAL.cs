using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FlightPlan
{
   public class FileMasterDAL : DBHelper<File_Master>
    {
        public List<File_Master> GetByPlanID(string planID)
        {
            var linq = from t in context.File_Master
                       where t.RepetPlanID == planID
                       orderby t.ID ascending
                       select t;
            return linq.ToList();
        }

        public List<File_Master> GetByMasterID(List<string> masterId)
        {
            var linq = from t in context.File_Master
                       where masterId.Contains(t.ID)
                       orderby t.ID ascending
                       select t;
            return linq.ToList();
        }
    }
}
