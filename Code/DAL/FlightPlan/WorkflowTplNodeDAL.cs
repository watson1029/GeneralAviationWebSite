using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Untity.DB;

namespace DAL.FlightPlan
{
    public class WorkflowTplNodeDAL
    {
        private static SqlDbHelper dao = new SqlDbHelper();

        /// <summary>
        /// 根据模板获取节点
        /// </summary>
        /// <param name="twfId"></param>
        /// <returns></returns>
        public static List<WorkflowTplNode> GetNodeByTWFID(int twfId)
        {
            var sql = "select * from TWFSteps where TWFID=@twfId";
            SqlParameter[] parameters = {
					new SqlParameter("@twfId",  twfId)};
            return dao.ExecSelectCmd(ExecReader, sql, parameters);
           
        }

        private static WorkflowTplNode ExecReader(SqlDataReader dr)
        {
            WorkflowTplNode wfNode = new WorkflowTplNode();
            if (!dr["StepId"].Equals(DBNull.Value))
                wfNode.StepId = Convert.ToInt32(dr["StepId"]);
            if (!dr["TWFID"].Equals(DBNull.Value))
                wfNode.TWFID = Convert.ToInt32(dr["TWFID"]);
            if (!dr["StepName"].Equals(DBNull.Value))
                wfNode.StepName = Convert.ToString(dr["StepName"]);
            if (!dr["PrevId"].Equals(DBNull.Value))
                wfNode.PrevId = Convert.ToInt32(dr["PrevId"]);
            if (!dr["NextId"].Equals(DBNull.Value))
                wfNode.NextId = Convert.ToInt32(dr["NextId"]);
            if (!dr["AuthorType"].Equals(DBNull.Value))
                wfNode.AuthorType = Convert.ToString(dr["AuthorType"]);
            return wfNode;
        }
       /// <summary>
       /// 创建流程实例
       /// </summary>
       /// <param name="tnode"></param>
       /// <param name="applyId"></param>
       /// <returns></returns>
        public static WorkflowNodeInstance CreateNodeInstance(WorkflowTplNode tnode, int planId)
        {
            var guid=Guid.NewGuid();
            var date = DateTime.Now;
            WorkflowNodeInstance nodeInst = new WorkflowNodeInstance();
            string sql = @"insert into ActualSteps (ID,PlanID,StepID,TWFID,State,PrevID,NextID,CreateTime) values (@id,@planId,@stepId,@twfId,@state,@prevId,@nextId,@createTime)";

            SqlParameter[] parameters = {
					new SqlParameter("@id", guid),
					new SqlParameter("@planId", planId),
					new SqlParameter("@stepId",tnode.StepId),
					new SqlParameter("@twfId", tnode.TWFID),
                    new SqlParameter("@state",byte.Parse("0")),
                    new SqlParameter("@prevId",Guid.Empty),
                    new SqlParameter("@nextId", Guid.Empty),
                    new SqlParameter("@createTime", DateTime.Now)
        };
            if (dao.ExecNonQuery(sql, parameters)>0)
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


        public static WorkflowTplNode GetNode(int stepId)
        {
            var sql = "select * from TWFSteps where StepID=@stepId";
            SqlParameter[] parameters = {
					new SqlParameter("@stepId",stepId)
			};
            return dao.ExecSelectSingleCmd<WorkflowTplNode>(ExecReader, sql, parameters);
        }
    }
}
