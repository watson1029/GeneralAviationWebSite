using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FlightPlan
{
    public class WorkflowNodeInstance
    {
        public WorkflowNodeInstance() {
            SubActualStepsList = new List<SubActualSteps>();
        }
        public enum StepStateType
        {
            Initialized, 
            Processing, 
            Processed, 
            Deserted,
            NoValid
        };//前1-4是申请单生成，审核中，审核通过，审核不通过,无效。 
        public Guid Id { get; set; }
        public Guid PlanID { get; set; }
        public int StepId { get; set; }
        public int TWFID { get; set; }
        public StepStateType State { get; set; }
        public Guid PrevId { get; set; }
        public Guid NextId { get; set; }
        public int? ActorID { get; set; }
        public string RoleName { get; set; }
        public string ActorName { get; set; }
        public DateTime? ActorTime { get; set; }
        public string Comments { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ApplyTime { get; set; }
        public bool? IsParallel { get; set; }
        public List<SubActualSteps> SubActualStepsList { get; set; }
    }
}
