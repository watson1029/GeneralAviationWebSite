using DAL.FlightPlan;
using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FlightPlan
{
   public class WorkflowTemplateBLL
    {
        public static Guid CreateWorkflowInstance(int twfId, int planId,int userID, string userName)
        {
            Guid firstStepId = Guid.Empty;
            List<WorkflowNodeInstance> tempNodesInst = new List<WorkflowNodeInstance>();
            List<WorkflowNodeInstance> nodesInstance = new List<WorkflowNodeInstance>();
            WorkflowNodeInstance firstNodeInst = new WorkflowNodeInstance();

            //找出指定流程模板中所有的流程节点，并创建流程节点实例
            List<WorkflowTplNode> tnodeList = WorkflowTplNodeDAL.GetNodeByTWFID(twfId);
            foreach (WorkflowTplNode tnode in tnodeList)
            {
                WorkflowNodeInstance ninst = WorkflowTplNodeDAL.CreateNodeInstance(tnode, planId);
                tempNodesInst.Add(ninst);
            }

            //当流程节点实例创建完成后，给实例加上流程链，让实例节点串联起来
            WorkflowTplNode firstTnode = new WorkflowTplNode();
            var query1 = from c in tnodeList where c.PrevId == 0 select c;
            if (query1.Count() != 1)
            {
                throw new Exception("由于没有或者存在多个流程初始节点，导致无法创建流程。");
            }
            else
            {
                firstTnode = query1.First();
                firstNodeInst = tempNodesInst.First(item => item.StepId == firstTnode.StepId && item.PlanID == planId);
                firstStepId = firstNodeInst.Id;
            }
            var query2 = from c in tnodeList where c.NextId == 0 select c;
            if (query2.Count() != 1)
            {
                throw new Exception("由于没有或者存在多个流程结束节点，导致无法创建流程。");
            }
            WorkflowTplNode prevTnode = firstTnode;
            WorkflowNodeInstance prevNodeInst = firstNodeInst;
            while (prevTnode.NextId != 0)
            {
                var nextTnode = tnodeList.First(item => item.PrevId == prevTnode.StepId);
                var nextNodeInst = tempNodesInst.First(item => item.StepId == nextTnode.StepId && item.PlanID == planId);
                nextNodeInst.PrevId = prevNodeInst.Id;
                prevNodeInst.NextId = nextNodeInst.Id;
                if (nodesInstance.Count <= 0)
                {
                    nodesInstance.Add(prevNodeInst);  //第一次循环的时候，需要添加开始节点
                }
                nodesInstance.Add(nextNodeInst);
                prevTnode = nextTnode;
                prevNodeInst = nextNodeInst;
            }

            //完成流程节点实例的串联工作，更新到数据库
            WorkflowNodeInstanceDAL.UpdateNodeInstance(nodesInstance);

            // 将第一个节点的状态改为Processing，并设置活动所有者
            WorkflowNodeInstanceDAL.UpdateFirstNode(firstNodeInst,userID,userName);
            return firstStepId;
        }

    }
}
