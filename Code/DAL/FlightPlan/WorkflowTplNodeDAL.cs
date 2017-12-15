using Model.EF;
using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Untity.DB;

namespace DAL.FlightPlan
{
    public class WorkflowTplNodeDAL:DBHelper<TWFSteps>
    {
        /// <summary>
        /// 根据模板获取节点
        /// </summary>
        /// <param name="twfId"></param>
        /// <returns></returns>
        public List<WorkflowTplNode> GetNodeByTWFID(int twfId)
        {
            List<WorkflowTplNode> _WorkflowTplNodeList = new List<WorkflowTplNode>();

            var _TWFStepsList = FindList(a => a.TWFID == twfId.ToString(), a => a.StepID, true);
            foreach (var item in _TWFStepsList)
            {
                _WorkflowTplNodeList.Add(ExecReader(item));
            }
            return _WorkflowTplNodeList;
        }

        private WorkflowTplNode ExecReader(TWFSteps entity)
        {
            WorkflowTplNode wfNode = new WorkflowTplNode();
            if (!entity.StepID.Equals(DBNull.Value))
                wfNode.StepId = Convert.ToInt32(entity.StepID);
            if (!entity.TWFID.Equals(DBNull.Value))
                wfNode.TWFID = Convert.ToInt32(entity.TWFID);
            if (!entity.StepName.Equals(DBNull.Value))
                wfNode.StepName = Convert.ToString(entity.StepName);
            if (!entity.PrevID.Equals(DBNull.Value))
                wfNode.PrevId = Convert.ToInt32(entity.PrevID);
            if (!entity.NextID.Equals(DBNull.Value))
                wfNode.NextId = Convert.ToInt32(entity.NextID);
            if (!entity.AuthorType.Equals(DBNull.Value))
                wfNode.AuthorType = Convert.ToString(entity.AuthorType);
            return wfNode;
        }
       /// <summary>
       /// 创建流程实例
       /// </summary>
       /// <param name="tnode"></param>
       /// <param name="applyId"></param>
       /// <returns></returns>
        public WorkflowNodeInstance CreateNodeInstance(WorkflowTplNode tnode, int planId)
        {
            var guid=Guid.NewGuid();
            var date = DateTime.Now;
            WorkflowNodeInstance nodeInst = new WorkflowNodeInstance();

            ActualSteps _instance = new ActualSteps
            {
                ID = guid,
                PlanID = planId,
                StepID = tnode.StepId,
                TWFID = tnode.TWFID,
                State = 0,
                PrevID = Guid.Empty,
                NextID = Guid.Empty,
                CreateTime = DateTime.Now
            };
            context.ActualSteps.Add(_instance);
            if (context.SaveChanges() > 0)
            {
                nodeInst.Id = guid;
                nodeInst.PlanID = planId;
                nodeInst.StepId = tnode.StepId;
                nodeInst.TWFID = tnode.TWFID;
                nodeInst.State = WorkflowNodeInstance.StepStateType.Initialized;
                nodeInst.PrevId = Guid.Empty;
                nodeInst.NextId = Guid.Empty;
                nodeInst.CreateTime = date;
            }
            return nodeInst;
        }
        public WorkflowTplNode GetNode(int stepId)
        {
            TWFSteps _instance = Find(a => a.StepID == stepId);
            return ExecReader(_instance);
        }
    }
}
