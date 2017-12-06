﻿using DAL.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.FlightPlan
{ 
    public class CurrentPlanBLL
    {
        FlightPlanDAL dal = new FlightPlanDAL(); 

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public  bool Update(Model.EF.FlightPlan model)
        {
            return dal.Update(model)>0;
        }
        /// <summary>
        /// 更新某些字段
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public bool Update(Model.EF.FlightPlan model, params string[] propertyNames)
        {
            return dal.Update(model, propertyNames) > 0;
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Submit(int planid,int userid,string username)
        {
            try
            {
                WorkflowTemplateBLL.CreateWorkflowInstance((int)TWFTypeEnum.CurrentPlan, planid, userid, username);
                WorkflowNodeInstanceDAL.Submit(planid, "", workPlan => {
                    dal.Update(new Model.EF.FlightPlan { ActorID = workPlan.Actor, PlanState = workPlan.PlanState, FlightPlanID = workPlan.PlanID }, "ActorID", "PlanState");
                });
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
        /// <summary>
        /// 按分页获取记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <param name="rowCount"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Model.EF.FlightPlan> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<Model.EF.FlightPlan, bool>> where)
        {
            return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.FlightPlanID, true);
        }
        /// <summary>
        /// 获取单行记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.EF.FlightPlan Get(int id)
        {
            return dal.Find(u => u.FlightPlanID == id);
        }
    }
}
