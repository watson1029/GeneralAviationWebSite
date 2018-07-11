using DAL.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Model.EF;
using System.Transactions;
using Untity;
using RIPS.Util.Collections;
using Newtonsoft.Json;
using Model.FlightPlan;
using ViewModel.FlightPlan;
using System.Data.Entity.Spatial;

namespace BLL.FlightPlan
{
    public class CurrentPlanBLL
    {
        CurrentFlightPlanDAL dal = new CurrentFlightPlanDAL();
        private DAL.FlightPlan.FileCurrentPlanMasterDAL masterdal = new DAL.FlightPlan.FileCurrentPlanMasterDAL();
        vCurrentPlanDAL vdal = new vCurrentPlanDAL();
        WorkflowTemplateBLL wftbll = new WorkflowTemplateBLL();
        WorkflowNodeInstanceDAL instal = new WorkflowNodeInstanceDAL();

        public void ActionWithTrans(Action act)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
                try
                {
                    act();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }
        public bool Add(CurrentFlightPlan model)
        {
            return dal.Add(model) > 0;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(CurrentFlightPlan model)
        {
            return dal.Update(model, false, "DeleteFlag") > 0;
        }
        /// <summary>
        /// 更新某些字段
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public bool Update(CurrentFlightPlan model, params string[] propertyNames)
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
        public bool Submit(Guid planid, int userid, string username,string rolename, CurrentFlightPlanVM model)
        {
            //ActionWithTrans(() =>
            //{
            CurrentFlightPlan entity = new CurrentFlightPlan();
            entity.CurrentFlightPlanID = Guid.NewGuid();
            entity.FlightPlanID = planid.ToString();
            entity.ActorID = userid;
            entity.PlanState = "";
            //entity.Pilot = model.Pilot;
            //entity.ContractWay = model.ContractWay;
            entity.AircraftNum = model.AircraftNum;
            entity.ActualStartTime = model.ActualStartTime;
            entity.ActualEndTime = model.ActualEndTime;
            entity.Creator = userid;
            entity.CreatorName = username;
            dal.Add(entity);

            var currPlanId = entity.CurrentFlightPlanID;
            wftbll.CreateWorkflowInstance((int)TWFTypeEnum.CurrentPlan, currPlanId, userid, username);
            instal.Submit(currPlanId, (int)TWFTypeEnum.CurrentPlan, userid, username, rolename,"", workPlan =>
            {
                dal.Update(new CurrentFlightPlan { ActorName = workPlan.ActorName, PlanState = workPlan.PlanState, CurrentFlightPlanID = workPlan.PlanID }, "ActorID", "PlanState");
            });
            //});

            return true;
        }
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Audit(Guid planid, string comment, int userid, string userName,string roleName)
        {
            instal.Submit(planid, (int)TWFTypeEnum.CurrentPlan, userid, userName, roleName, comment, workPlan =>
            {
                dal.Update(new CurrentFlightPlan { ActorID = workPlan.Actor, PlanState = workPlan.PlanState, CurrentFlightPlanID = workPlan.PlanID, Creator = userid, CreateTime = DateTime.Now }, "ActorID", "PlanState", "CreateUserId", "CreateTime");
            });
            return true;
        }
        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Terminate(Guid planid, string comment, int userid, string userName,string roleName)
        {
            instal.Terminate(planid, (int)TWFTypeEnum.CurrentPlan, userid, userName, roleName, comment, workPlan =>
            {
                dal.Update(new CurrentFlightPlan { ActorID = workPlan.Actor, PlanState = workPlan.PlanState, CurrentFlightPlanID = workPlan.PlanID, Creator = userid, CreateTime = DateTime.Now }, "ActorID", "PlanState", "CreateUserId", "CreateTime");
            });
            return true;
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
        public List<CurrentFlightPlan> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<CurrentFlightPlan, bool>> where)
        {
            return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.CreateTime, true);
        }
        public vGetCurrentPlanNodeInstance GetCurrentFlightPlanNodeInstance(Expression<Func<vGetCurrentPlanNodeInstance, bool>> where)
        {
            var insdal = new DBHelper<vGetCurrentPlanNodeInstance>();
            return insdal.Find(where);
        }
        /// <summary>
        /// 按条件获取记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<vCurrentPlan> GetList(Expression<Func<vCurrentPlan, bool>> where)
        {
            return vdal.FindList(where, m => m.CreateTime, true);
        }
        /// <summary>
        /// 获取单行记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public vCurrentPlan Get(Guid id)
        {
            return vdal.Find(u => u.CurrentFlightPlanID == id);
        }
        public CurrentFlightPlan GetCurrentFlightPlan(Guid id)
        {
            return dal.Find(u => u.CurrentFlightPlanID == id);
        }
        public vCurrentPlan GetByCurrid(Guid id)
        {
            return vdal.Find(u => u.CurrentFlightPlanID == id);
        }
        public CurrentFlightPlan GetEntity(Guid id)
        {
            return dal.Find(u => u.CurrentFlightPlanID == id);
        }

        public int GetCurrentUnSubmitNum(int userId)
        {
            return dal.GetCurrentUnSubmitNum(userId);
        }

        public int GetCurrentUnAuditNum(int userId)
        {
            return dal.GetCurrentUnAuditNum(userId);
        }

        public int GetCurrentSubmitNum(int userId)
        {
            return dal.GetCurrentSubmitNum(userId);
        }

        public int GetCurrentAuditNum(int userId)
        {
            return dal.GetCurrentAuditNum(userId);
        }

        public int GetCurrentSubmitNum(int userId, DateTime beginTime, DateTime endTime)
        {
            return dal.GetCurrentSubmitNum(userId, beginTime, endTime);
        }

        public int GetCurrentAuditNum(int userId, DateTime beginTime, DateTime endTime)
        {
            return dal.GetCurrentAuditNum(userId, beginTime, endTime);
        }
        public List<FlightPlanStatistics> GetList(int pageIndex, int pageSize, out int pageCount, DateTime started, DateTime ended)
        {
            try
            {
                List<FlightPlanStatistics> fplist = JsonConvert.DeserializeObject<List<FlightPlanStatistics>>(JsonConvert.SerializeObject(new FlightPlanDAL().GetFullTimeFlightStatistics(started, ended)));
                fplist = JsonConvert.DeserializeObject<List<FlightPlanStatistics>>(JsonConvert.SerializeObject(fplist.GroupBy(x => new { x.Creator, x.CompanyName }).Select(group => new {
                    Creator = group.Key.Creator,
                    CompanyName = group.Key.CompanyName,
                    AircraftNum = group.Sum(p => p.AircraftNum),
                    SecondDiff = group.Sum(p => p.SecondDiff)
                }).ToList()));
                pageCount = fplist.Count;
                fplist = fplist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return fplist;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }
        public List<vGetCurrentPlanNodeInstance> GetList(int pageIndex, int pageSize, out int rowCount, int Creator, DateTime started, DateTime ended)
        {
            return new FlightPlanDAL().GetCurrentPlanNodeInstanceList(pageIndex, pageSize, out rowCount, Creator, started, ended);
        }

        public int GetFlyNum(string company)
        {
            return dal.GetFlyNum(company);
        }

        public int GetFlyNum(string company, DateTime begin, DateTime end)
        {
            return dal.GetFlyNum(company, begin, end);
        }

        public int GetFlyTime(string company)
        {
            return dal.GetFlyTime(company);
        }

        public int GetFlyTime(string company, DateTime begin, DateTime end)
        {
            return dal.GetFlyTime(company, begin, end);
        }
        public List<File_CurrentPlanMaster> GetFileCurrentPlanMasterList(Expression<Func<File_CurrentPlanMaster, bool>> where)
        {
            return masterdal.FindList(where, m => m.ID, true);
        }
        /// <summary>
        /// 添加机场、航线、作业区
        /// </summary>
        /// <param name="airportidList"></param>
        /// <param name="airlineText"></param>
        /// <param name="repetPlanID"></param>
        public void AddCurrentPlanTempOther(string airlineText, string cworkText, string pworkText, string hworkText, string currentPlanID, string keyValue, ref string airlineworkText)
        {
            var context = new ZHCC_GAPlanEntities();
            StringBuilder sb = new StringBuilder("");
            List<string> masterIDs = new List<string>();
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        var flightmasterlist = context.File_CurrentPlanMaster.Where(u => u.CurrentPlanID.Equals(currentPlanID));
                        foreach (var item in flightmasterlist)
                        {
                            var master = context.File_Master.Find(item.MasterID);
                            context.File_Master.Remove(master);

                            var detaillist = context.File_Detail.Where(u => u.MasterID.Equals(item.MasterID));
                            foreach (var sitem in detaillist)
                            {
                                context.File_Detail.Remove(sitem);
                            }
                            context.SaveChanges();
                        }

                    }

                    #region 航线
                    if (!string.IsNullOrEmpty(airlineText))
                    {
                        var airlineList = (AirlineFillTotal)JsonConvert.DeserializeObject(airlineText, typeof(AirlineFillTotal));
                        var sblinedesc = new StringBuilder("");
                        foreach (var item in airlineList.airlineArray)
                        {
                            if (item.airlinePointList.Count() > 0)
                            {
                                sblinedesc.Clear();
                                File_Master master = new File_Master()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    WorkType = "airline",
                                    FlyHeight = item.FlyHeight,
                                };
                                masterIDs.Add(master.ID);
                                var index = 1;
                                foreach (var pointItem in item.airlinePointList)
                                {
                                    if (string.IsNullOrEmpty(pointItem.PointName)) continue;
                                    sblinedesc.Append(pointItem.PointName);
                                    File_Detail point = new File_Detail()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        MasterID = master.ID,
                                        PointName = pointItem.PointName,
                                        Sort = index
                                    };

                                    var splitmodel = SpecialFunctions.latLongSplit(pointItem.LatLong);
                                    point.Longitude = splitmodel.Longitude;
                                    point.Latitude = splitmodel.Latitude;
                                    context.File_Detail.Add(point);
                                    context.SaveChanges();
                                    sblinedesc.Append("(");
                                    sblinedesc.Append(pointItem.LatLong);
                                    sblinedesc.Append(")");
                                    sblinedesc.Append("-");
                                    index++;
                                }
                                if (sblinedesc.Length > 0)
                                {
                                    sblinedesc.Remove(sblinedesc.Length - 1, 1);
                                }
                                if (!string.IsNullOrEmpty(master.FlyHeight))
                                {
                                    sblinedesc.Append(",高度");
                                    sblinedesc.Append(master.FlyHeight);
                                    sblinedesc.Append("米（含）以下");
                                }
                                master.LineDescript = sblinedesc.ToString();
                                context.File_Master.Add(master);
                                context.SaveChanges();
                                sb.AppendLine(master.LineDescript + "；");
                            }
                        }
                    }
                    #endregion
                    #region 作业区(圆)
                    if (!string.IsNullOrEmpty(cworkText))
                    {
                        var workList = (AirlineFillTotal)JsonConvert.DeserializeObject(cworkText, typeof(AirlineFillTotal));
                        var sblinedesc = new StringBuilder("");
                        foreach (var item in workList.airlineArray)
                        {
                            if (item.airlinePointList.Count() > 0 && !string.IsNullOrEmpty(item.Radius))
                            {
                                sblinedesc.Clear();
                                File_Master master = new File_Master()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    FlyHeight = item.FlyHeight,
                                    WorkType = "circle"
                                };
                                masterIDs.Add(master.ID);
                                var customAreaStr = new StringBuilder("");
                                var index = 1;
                                foreach (var pointItem in item.airlinePointList)
                                {
                                    if (string.IsNullOrEmpty(pointItem.PointName)) continue;
                                    sblinedesc.Append(pointItem.PointName);
                                    File_Detail point = new File_Detail()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        MasterID = master.ID,
                                        PointName = pointItem.PointName,
                                        Sort = index
                                    };
                                    var splitmodel = SpecialFunctions.latLongSplit(pointItem.LatLong);
                                    point.Longitude = splitmodel.Longitude;
                                    point.Latitude = splitmodel.Latitude;
                                    context.File_Detail.Add(point);
                                    context.SaveChanges();
                                    sblinedesc.Append("(");
                                    sblinedesc.Append(pointItem.LatLong);
                                    sblinedesc.Append(")");
                                    if (!string.IsNullOrEmpty(point.Latitude) && !string.IsNullOrEmpty(point.Longitude))
                                    {
                                        customAreaStr.Append("N");
                                        customAreaStr.Append(point.Latitude);
                                        customAreaStr.Append("E");
                                        customAreaStr.Append(point.Longitude);
                                        customAreaStr.Append(",");
                                    }
                                    index++;
                                }
                                DbGeography geoArea = null;
                                try
                                {
                                    geoArea = context.f_GetGEOAreaByPointString(customAreaStr.ToString(), 4).First();
                                }
                                catch
                                {
                                }
                                //计算管制区
                                var customAreaList = context.CustomControlArea.Where(m => m.ControlAreaBoundary.Intersects(geoArea)).Select(m => m.ControlAreaName);
                                master.CustomArea = string.Join(",", customAreaList.ToArray());

                                Int16 _radius = 0;
                                if (!string.IsNullOrWhiteSpace(item.Radius) && Int16.TryParse(item.Radius, out _radius))
                                {
                                    master.RaidusMile = _radius;
                                    var tempraidus = "为圆心半径" + item.Radius + "公里范围内";
                                    sblinedesc.Append(tempraidus);
                                }
                                if (!string.IsNullOrEmpty(master.FlyHeight))
                                {
                                    sblinedesc.Append(",高度");
                                    sblinedesc.Append(master.FlyHeight);
                                    sblinedesc.Append("米（含）以下");
                                }
                                master.LineDescript = sblinedesc.ToString();
                                context.File_Master.Add(master);
                                context.SaveChanges();
                                sb.AppendLine(master.LineDescript + "；");
                            }
                        }
                    }
                    #endregion
                    #region 作业区(点)
                    if (!string.IsNullOrEmpty(pworkText))
                    {
                        var workList = (AirlineFillTotal)JsonConvert.DeserializeObject(pworkText, typeof(AirlineFillTotal));
                        var sblinedesc = new StringBuilder("");
                        foreach (var item in workList.airlineArray)
                        {
                            if (item.airlinePointList.Count() > 0)
                            {
                                sblinedesc.Clear();
                                File_Master master = new File_Master()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    FlyHeight = item.FlyHeight,
                                    WorkType = "area"
                                };
                                masterIDs.Add(master.ID);
                                var customAreaStr = new StringBuilder("");
                                var index = 1;
                                foreach (var pointItem in item.airlinePointList)
                                {
                                    if (string.IsNullOrEmpty(pointItem.PointName)) continue;
                                    sblinedesc.Append(pointItem.PointName);
                                    File_Detail point = new File_Detail()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        MasterID = master.ID,
                                        PointName = pointItem.PointName,
                                        Sort = index
                                    };
                                    var splitmodel = SpecialFunctions.latLongSplit(pointItem.LatLong);
                                    point.Longitude = splitmodel.Longitude;
                                    point.Latitude = splitmodel.Latitude;
                                    context.File_Detail.Add(point);
                                    context.SaveChanges();
                                    sblinedesc.Append("(");
                                    sblinedesc.Append(pointItem.LatLong);
                                    sblinedesc.Append(")");
                                    sblinedesc.Append("-");
                                    if (!string.IsNullOrEmpty(point.Latitude) && !string.IsNullOrEmpty(point.Longitude))
                                    {
                                        customAreaStr.Append("N");
                                        customAreaStr.Append(point.Latitude);
                                        customAreaStr.Append("E");
                                        customAreaStr.Append(point.Longitude);
                                        customAreaStr.Append(",");
                                    }
                                    index++;

                                }
                                if (customAreaStr.Length > 0)
                                {
                                    customAreaStr.Remove(customAreaStr.Length - 1, 1);
                                }
                                if (sblinedesc.Length > 0)
                                {
                                    sblinedesc.Remove(sblinedesc.Length - 1, 1);
                                }
                                DbGeography geoArea = null;
                                try
                                {
                                    geoArea = context.f_GetGEOAreaByPointString(customAreaStr.ToString(), 3).First();
                                }
                                catch
                                {
                                }
                                //计算管制区         
                                var customAreaList = context.CustomControlArea.Where(m => m.ControlAreaBoundary.Intersects(geoArea)).Select(m => m.ControlAreaName);
                                master.CustomArea = string.Join(",", customAreaList.ToArray());
                                var tempraidus = item.airlinePointList.Count() + "点连线范围内";
                                sblinedesc.Append(tempraidus);
                                if (!string.IsNullOrEmpty(master.FlyHeight))
                                {
                                    sblinedesc.Append(",高度");
                                    sblinedesc.Append(master.FlyHeight);
                                    sblinedesc.Append("米（含）以下");
                                }
                                master.LineDescript = sblinedesc.ToString();
                                context.File_Master.Add(master);
                                context.SaveChanges();
                                sb.AppendLine(master.LineDescript + "；");
                            }
                        }
                    }
                    #endregion
                    #region 作业区(线)
                    if (!string.IsNullOrEmpty(hworkText))
                    {
                        var workList = (AirlineFillTotal)JsonConvert.DeserializeObject(hworkText, typeof(AirlineFillTotal));
                        var sblinedesc = new StringBuilder("");
                        foreach (var item in workList.airlineArray)
                        {
                            if (item.airlinePointList.Count() > 0)
                            {
                                sblinedesc.Clear();
                                File_Master master = new File_Master()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    FlyHeight = item.FlyHeight,
                                    WorkType = "airlinelr"
                                };
                                masterIDs.Add(master.ID);
                                var customAreaStr = new StringBuilder("");
                                var index = 1;
                                foreach (var pointItem in item.airlinePointList)
                                {
                                    if (string.IsNullOrEmpty(pointItem.PointName)) continue;
                                    sblinedesc.Append(pointItem.PointName);
                                    File_Detail point = new File_Detail()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        MasterID = master.ID,
                                        PointName = pointItem.PointName,
                                        Sort = index
                                    };
                                    var splitmodel = SpecialFunctions.latLongSplit(pointItem.LatLong);
                                    point.Longitude = splitmodel.Longitude;
                                    point.Latitude = splitmodel.Latitude;
                                    context.File_Detail.Add(point);
                                    context.SaveChanges();
                                    sblinedesc.Append("(");
                                    sblinedesc.Append(pointItem.LatLong);
                                    sblinedesc.Append(")");
                                    sblinedesc.Append("-");
                                    if (!string.IsNullOrEmpty(point.Latitude) && !string.IsNullOrEmpty(point.Longitude))
                                    {
                                        customAreaStr.Append("N");
                                        customAreaStr.Append(point.Latitude);
                                        customAreaStr.Append("E");
                                        customAreaStr.Append(point.Longitude);
                                        customAreaStr.Append(",");
                                    }
                                    index++;
                                }
                                if (customAreaStr.Length > 0)
                                {
                                    customAreaStr.Remove(customAreaStr.Length - 1, 1);
                                }
                                if (sblinedesc.Length > 0)
                                {
                                    sblinedesc.Remove(sblinedesc.Length - 1, 1);
                                }
                                Int16 _radius = 0;
                                if (!string.IsNullOrWhiteSpace(item.Radius) && Int16.TryParse(item.Radius, out _radius))
                                {
                                    master.RaidusMile = _radius;
                                    var tempraidus = "航线左右" + item.Radius + "公里范围内";
                                    sblinedesc.Append(tempraidus);
                                }
                                if (!string.IsNullOrEmpty(master.FlyHeight))
                                {
                                    sblinedesc.Append(",高度");
                                    sblinedesc.Append(master.FlyHeight);
                                    sblinedesc.Append("米（含）以下");
                                }
                                DbGeography geoArea = null;
                                try
                                {
                                    geoArea = context.f_GetGEOAreaByPointString(customAreaStr.ToString(), 2).First();
                                }
                                catch
                                {
                                }
                                //计算管制区
                                var customAreaList = context.CustomControlArea.Where(m => m.ControlAreaBoundary.Intersects(geoArea)).Select(m => m.ControlAreaName);
                                master.CustomArea = string.Join(",", customAreaList.ToArray());
                                master.LineDescript = sblinedesc.ToString();
                                context.File_Master.Add(master);
                                context.SaveChanges();
                                sb.AppendLine(master.LineDescript + "；");
                            }
                        }
                    }
                    #endregion
                    foreach (var item in masterIDs)
                    {
                        File_CurrentPlanMaster master = new File_CurrentPlanMaster()
                        {
                            MasterID = item,
                            CurrentPlanID = currentPlanID
                        };
                        context.File_CurrentPlanMaster.Add(master);
                        context.SaveChanges();
                        var fileMaster = context.File_Master.Where(u => u.ID.Equals(item)).FirstOrDefault();
                        if (fileMaster != null)
                        {
                            sb.AppendLine(fileMaster.LineDescript + "；");
                        }
                    }
                    airlineworkText = sb.ToString();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }

        }
        public List<vGetCurrentPlanNodeInstance> GetNodeInstanceList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<vGetCurrentPlanNodeInstance, bool>> where)
        {
            var insdal = new DBHelper<vGetCurrentPlanNodeInstance>();
            return insdal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.PlanID, true);
        }
    }
}
