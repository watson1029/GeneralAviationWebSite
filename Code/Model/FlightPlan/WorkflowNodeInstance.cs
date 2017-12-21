using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FlightPlan
{
    public class WorkflowNodeInstance
    {
        public enum StepStateType
        {
            Initialized, 
            Processing, 
            Processed, 
            Deserted
        };//前1-5是申请单生成，审核中，审核过，取消。 
        public Guid Id { get; set; }
        public int PlanID { get; set; }
        public int StepId { get; set; }
        public int TWFID { get; set; }
        public StepStateType State { get; set; }
        public Guid PrevId { get; set; }
        public Guid NextId { get; set; }
        public int ActorID { get; set; }
        public string ActorName { get; set; }
        public DateTime? ActorTime { get; set; }
        public string Comments { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ApplyTime { get; set; }
    }
}
