using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FlightPlan
{
   public class WorkflowSubTplNodeDAL : DBHelper<SubTWFSteps>
    {
        public List<SubTWFSteps> GetSubNodeByTWFID(int twfId)
        {
       
            return FindList(a => a.ParentTWFID == twfId, a => a.ID, true);
        }
    }
}
