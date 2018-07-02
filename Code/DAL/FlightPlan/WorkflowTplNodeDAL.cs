using Model.EF;
using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Untity.DB;

namespace DAL.FlightPlan
{
    public class WorkflowTplNodeDAL : DBHelper<TWFSteps>
    {
        /// <summary>
        /// 根据模板获取节点
        /// </summary>
        /// <param name="twfId"></param>
        /// <returns></returns>
        public List<WorkflowTplNode> GetNodeByTWFID(int twfId)
        {
            List<WorkflowTplNode> _WorkflowTplNodeList = new List<WorkflowTplNode>();

            var _TWFStepsList = FindList(a => a.TWFID == twfId, a => a.StepID, true);
            foreach (var item in _TWFStepsList)
            {
                _WorkflowTplNodeList.Add(ExecReader(item));
            }
            return _WorkflowTplNodeList;
        }
        private WorkflowTplNode ExecReader(TWFSteps entity)
        {
            WorkflowTplNode wfNode = new WorkflowTplNode();
            wfNode.StepId = entity.StepID;
            wfNode.TWFID = entity.TWFID;
            wfNode.StepName = entity.StepName ?? "";
            if (entity.PrevID.HasValue)
                wfNode.PrevId = entity.PrevID.Value;
            if (entity.NextID.HasValue)
                wfNode.NextId = entity.NextID.Value;
            wfNode.AuthorType = entity.AuthorType ?? "";
            if (entity.IsParallel.HasValue)
                wfNode.IsParallel = entity.IsParallel.Value;
            wfNode.SubTWFStepsList = context.Set<SubTWFSteps>().Where(u => u.ParentTWFID == entity.StepID).OrderBy(u => u.ID).AsNoTracking().ToList();
            return wfNode;
        }
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="tnode"></param>
        /// <param name="applyId"></param>
        /// <returns></returns>
        public WorkflowNodeInstance CreateNodeInstance(WorkflowTplNode tnode, Guid planId)
        {
            var guid = Guid.NewGuid();
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
                CreateTime = DateTime.Now,
               IsParallel= tnode.IsParallel
            };
            context.ActualSteps.Add(_instance);
            foreach (var item in tnode.SubTWFStepsList)
            {
            SubActualSteps _subinstance = new SubActualSteps
                {
                    ID = Guid.NewGuid(),
                    ParentStepID = guid.ToString(),
                    State = 0,
                    CreateTime = DateTime.Now,
                    ActorName= item.AuthorType
            };
                context.SubActualSteps.Add(_subinstance);
                nodeInst.SubActualStepsList.Add(_subinstance);
            }
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
