using DAL.FlightPlan;
using Model.EF;
using Model.FlightPlan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Untity;
using ViewModel.FlightPlan;

namespace BLL.FlightPlan
{
public class RepetitivePlanBLL
    {
        RepetitivePlanDAL dal = new RepetitivePlanDAL();
        vRepetitivePlanAirportDAL airdal = new vRepetitivePlanAirportDAL();
        FileAirportDAL airportdal = new FileAirportDAL();
        FileDetailDAL detaildal = new FileDetailDAL();
        FileMasterDAL masterdal = new FileMasterDAL();
        public bool Delete(string id)
        {
            var context = new ZHCC_GAPlanEntities();
                var entity = context.RepetitivePlan.Where(u => u.RepetPlanID.ToString().Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    context.RepetitivePlan.Remove(entity);
                }
                var airlist = context.File_Airport.Where(u => u.RepetPlanID.Equals(id));
                foreach (var item in airlist)
                {
                    context.File_Airport.Remove(item);
                }
                var masterlist = context.File_Master.Where(u => u.RepetPlanID.Equals(id));
                foreach (var item in masterlist)
                {
                    context.File_Master.Remove(item);
                }
                var detaillist = context.File_Detail.Where(u => u.RepetPlanID.Equals(id));
                foreach (var item in detaillist)
                {
                    context.File_Detail.Remove(item);

                }
            return context.SaveChanges() > 0;

            
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(RepetitivePlan model)
        {
            return dal.Add(model) > 0;
        }
        public bool AddFileAirport(File_Airport model)
        {
            return airportdal.Add(model) > 0;

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(RepetitivePlan model)
        {
            return dal.Update(model) > 0;
        }
        public bool Update(RepetitivePlan model, params string[] propertyNames)
        {
            return dal.Update(model, propertyNames) > 0;
        }

        public List<RepetitivePlan> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<RepetitivePlan, bool>> where)
        {
            return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.RepetPlanID, true);
        }

        public List<vGetRepetitivePlanNodeInstance> GetNodeInstanceList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<vGetRepetitivePlanNodeInstance, bool>> where)
        {
            var insdal = new DBHelper<vGetRepetitivePlanNodeInstance>();
            return insdal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.PlanID, true);
        }
        public List<vGetRepetitivePlanNodeInstance> GetNodeInstanceList(Expression<Func<vGetRepetitivePlanNodeInstance, bool>> where)
        {
            var insdal = new DBHelper<vGetRepetitivePlanNodeInstance>();
            return insdal.FindList(where, m => m.ID, true);
        }
        public List<RepetitivePlan> GetList(Expression<Func<RepetitivePlan, bool>> where)
        {
            return dal.FindList(where, m => m.RepetPlanID, true);
        }
        public RepetitivePlan Get(Guid id)
        {
            return dal.Find(u => u.RepetPlanID == id);
        }
        public List<vRepetitivePlanAirport> GetRepetitivePlanAirport(Guid id)
        {
            return airdal.FindList(u => u.RepetPlanID.Equals(id.ToString()), m => m.Sort, true);
        }
        public bool DeleteByRepetPlanID(string[] ids)
        {
            return airdal.BatchDelete(u => ids.Contains(u.RepetPlanID.ToString())) > 0;
        }
        public vGetRepetitivePlanNodeInstance GetRepetitivePlanNodeInstance(Expression<Func<vGetRepetitivePlanNodeInstance, bool>> where)
        {
            var insdal = new DBHelper<vGetRepetitivePlanNodeInstance>();
            return insdal.Find(where);
        }

        //public List<SelectItemModel> GetAllFlightTask()
        //{
        //    var list = CacheHelper.GetFromCache<List<SelectItemModel>>("SelectFlightTask", 360, () =>
        //    {
        //        return GetSelectFlightTask();
        //    });
        //    return list;
        //}
        //public List<SelectItemModel> GetSelectFlightTask()
        //{
        //    FlightTaskBLL fbll = new FlightTaskBLL();
        //    List<FlightTask> data = fbll.GetList();
        //    List<SelectItemModel> list = new List<SelectItemModel>();
        //    foreach (var item in data)
        //    {
        //        list.Add(new SelectItemModel { id = item.TaskCode, text = item.Description });
        //    }
        //    return list;
        //}

        public int GetRepetUnSubmitNum(int userId)
        {
            return dal.GetRepetUnSubmitNum(userId);
        }

        public int GetRepetUnAuditNum(int userId)
        {
            return dal.GetRepetUnAuditNum(userId);
        }

        public int GetRepetSubmitNum(int userId)
        {
            return dal.GetRepetSubmitNum(userId);
        }

        public int GetRepetAuditNum(int userId)
        {
            return dal.GetRepetAuditNum(userId);
        }

        public int GetRepetSubmitNum(int userId, DateTime beginTime, DateTime endTime)
        {
            return dal.GetRepetSubmitNum(userId, beginTime, endTime);
        }

        public int GetRepetAuditNum(int userId, DateTime beginTime, DateTime endTime)
        {
            return dal.GetRepetAuditNum(userId, beginTime, endTime);
        }
        /// <summary>
        /// 添加机场、航线、作业区
        /// </summary>
        /// <param name="airportidList"></param>
        /// <param name="airlineText"></param>
        /// <param name="repetPlanID"></param>
        public void AddRepetitivePlanOther(List<string> airportidList, string airlineText, string cworkText, string pworkText, string hworkText, string repetPlanID, string keyValue, ref string airlineworkText)
        {
            var context = new ZHCC_GAPlanEntities();
            StringBuilder sb = new StringBuilder("");
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        var airlist = context.File_Airport.Where(u => u.RepetPlanID.Equals(repetPlanID));
                        foreach (var item in airlist)
                        {
                            context.File_Airport.Remove(item);
                        }
                        var masterlist = context.File_Master.Where(u => u.RepetPlanID.Equals(repetPlanID));
                        foreach (var item in masterlist)
                        {
                            context.File_Master.Remove(item);
                        }
                        var detaillist = context.File_Detail.Where(u => u.RepetPlanID.Equals(repetPlanID));
                        foreach (var item in detaillist)
                        {
                            context.File_Detail.Remove(item);
                        }
                        context.SaveChanges();
                    }
                    #region 机场
                    var i = 1;
                    foreach (var item in airportidList)
                    {
                        File_Airport airport = new File_Airport()
                        {
                            RepetPlanID = repetPlanID,
                            AirportID = item,
                            Sort = i
                        };
                        i++;
                        context.File_Airport.Add(airport);  
                    } 
                    context.SaveChanges();
                    #endregion
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
                                    RepetPlanID = repetPlanID,
                                    WorkType = "airline",
                                    FlyHeight = item.FlyHeight,
                                };
                                var index = 1;
                                foreach (var pointItem in item.airlinePointList)
                                {
                                    if (string.IsNullOrEmpty(pointItem.PointName)) continue;
                                    sblinedesc.Append(pointItem.PointName);
                                    File_Detail point = new File_Detail()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        MasterID = master.ID,
                                        RepetPlanID = repetPlanID,
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
                            if (item.airlinePointList.Count() > 0&&!string.IsNullOrEmpty(item.Radius))
                            {
                                sblinedesc.Clear();
                                File_Master master = new File_Master()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    RepetPlanID = repetPlanID,
                                    FlyHeight = item.FlyHeight,
                                    WorkType = "circle"
                                };
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
                                        RepetPlanID = repetPlanID,
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
                                    RepetPlanID = repetPlanID,
                                    FlyHeight = item.FlyHeight,
                                    WorkType = "area"
                                };
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
                                        RepetPlanID = repetPlanID,
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
                                    RepetPlanID = repetPlanID,
                                    FlyHeight = item.FlyHeight,
                                    WorkType = "airlinelr"
                                };
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
                                        RepetPlanID = repetPlanID,
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

        public List<File_Master> GetFileMasterList(Expression<Func<File_Master, bool>> where)
        {
            return masterdal.FindList(where, m => m.ID, true);
        }
        public List<File_Detail> GetFileDetailList(Expression<Func<File_Detail, bool>> where)
        {
            return detaildal.FindList(where, m => m.Sort, true);
        }
        }
}
