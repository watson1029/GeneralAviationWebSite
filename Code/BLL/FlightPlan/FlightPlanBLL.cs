using DAL.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Model.EF;
using System.Text;
using System.Data.Entity.Spatial;
using ViewModel.FlightPlan;
using Newtonsoft.Json;
using Untity;

namespace BLL.FlightPlan
{
    public class FlightPlanBLL
    {
        FlightPlanDAL dal = new FlightPlanDAL();
        private DAL.FlightPlan.FileFlightPlanMasterDAL masterdal = new DAL.FlightPlan.FileFlightPlanMasterDAL();
        private DAL.FlightPlan.FileDetailDAL detaildal = new DAL.FlightPlan.FileDetailDAL();
        public bool Delete(string id)
        {
            var context = new ZHCC_GAPlanEntities();
            var entity = context.FlightPlan.Where(u => u.FlightPlanID.ToString().Equals(id)).FirstOrDefault();
            if (entity != null)
            {
                context.FlightPlan.Remove(entity);
            }

            var masterlist = context.File_FlightPlanMaster.Where(u => u.FlightPlanID.Equals(id));
            foreach (var item in masterlist)
            {
                context.File_FlightPlanMaster.Remove(item);
            }
            return context.SaveChanges() > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.EF.FlightPlan model)
        {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.EF.FlightPlan model)
        {
            return dal.Update(model) > 0;
        }


        public List<Model.EF.FlightPlan> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<Model.EF.FlightPlan, bool>> where)
        {
            return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.FlightPlanID, true);
        }
        public List<Model.EF.FlightPlan> GetList(Expression<Func<Model.EF.FlightPlan, bool>> where)
        {
            return dal.FindList(where, m => m.FlightPlanID, true);
        }
        public Model.EF.FlightPlan Get(Guid id)
        {
            return dal.Find(u => u.FlightPlanID == id);
        }
        public Model.EF.vFlightPlan GetvFlightPlan(Guid id)
        {
            var context = new ZHCC_GAPlanEntities();
            return context.Set<vFlightPlan>().Where(u => u.FlightPlanID == id).FirstOrDefault();
        }

        public List<vGetFlightPlanNodeInstance> GetNodeInstanceList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<vGetFlightPlanNodeInstance, bool>> where)
        {
            var insdal = new DBHelper<vGetFlightPlanNodeInstance>();
            return insdal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.PlanID, true);
        }
        public vGetFlightPlanNodeInstance GetFlightPlanNodeInstance(Expression<Func<vGetFlightPlanNodeInstance, bool>> where)
        {
            var insdal = new DBHelper<vGetFlightPlanNodeInstance>();
            return insdal.Find(where);
        }

        public int GetFlyUnSubmitNum(int userId)
        {
            return dal.GetFlyUnSubmitNum(userId);
        }

        public int GetFlyUnAuditNum(int userId)
        {
            return dal.GetFlyUnAuditNum(userId);
        }

        public int GetFlySubmitNum(int userId)
        {
            return dal.GetFlySubmitNum(userId);
        }

        public int GetFlyAuditNum(int userId)
        {
            return dal.GetFlyAuditNum(userId);
        }

        public int GetFlySubmitNum(int userId, DateTime beginTime, DateTime endTime)
        {
            return dal.GetFlySubmitNum(userId, beginTime, endTime);
        }

        public int GetFlyAuditNum(int userId, DateTime beginTime, DateTime endTime)
        {
            return dal.GetFlyAuditNum(userId, beginTime, endTime);
        }
        public List<ViewModel.FlightPlan.SuperMapVM> GetAreaByCode(string code)
        {
            var context = new ZHCC_GAPlanEntities();
            var superMap = new List<ViewModel.FlightPlan.SuperMapVM>();
            var masters = (
                        from a in context.FlightPlan
                        from b in context.File_FlightPlanMaster
                        from c in context.File_Master
                        where a.FlightPlanID.ToString().Equals(b.FlightPlanID) && a.Code.Equals(code) && b.MasterID == c.ID
                        select c).ToList();
            if (masters != null && masters.Any())
            {
                foreach (var master in masters)
                {
                    var map = new ViewModel.FlightPlan.SuperMapVM();
                    map.MasterID = master.ID;
                    map.WorkType = master.WorkType;
                    map.RaidusMile = master.RaidusMile;
                    map.Location = detaildal.GetByMasterID(map.MasterID.ToString());
                    // 满足这3个条件确定类型为：航线左右范围
                    if (map.WorkType.Equals("airlinelr"))
                    {
                        for (int i = 0; i < map.Location.Count; i++)
                        {
                            // 添加点的圆形范围
                            var circle = new ViewModel.FlightPlan.SuperMapVM();
                            circle.WorkType = ViewModel.FlightPlan.DrawType.airlineCircle.ToString();
                            circle.RaidusMile = map.RaidusMile;
                            circle.Location = new List<ViewModel.FlightPlan.Location>();
                            circle.Location.Add(map.Location[i]);
                            superMap.Add(circle);
                            if (i + 1 == map.Location.Count)
                                break;
                            // 计算两点间航线左右范围形成的矩形4点
                            var area = new ViewModel.FlightPlan.SuperMapVM();
                            area.WorkType = ViewModel.FlightPlan.DrawType.airlineRectangle.ToString();
                            area.Location = new List<ViewModel.FlightPlan.Location>();
                            // 计算两点航线与正x轴行程的夹角
                            double angle = Math.Atan2
                            (
                                double.Parse(map.Location[i + 1].Latitude) - double.Parse(map.Location[i].Latitude),
                                double.Parse(map.Location[i + 1].Longitude) - double.Parse(map.Location[i].Longitude)
                            ) * 180 / Math.PI;
                            // 计算范围的宽度
                            double raidus = (double)map.RaidusMile / 111;
                            // 计算point1坐标
                            var point1 = new ViewModel.FlightPlan.Location();
                            point1.Longitude = (double.Parse(map.Location[i].Longitude) - Math.Cos(Math.PI / (180 / (90 - angle))) * raidus).ToString();
                            point1.Latitude = (double.Parse(map.Location[i].Latitude) + Math.Sin(Math.PI / (180 / (90 - angle))) * raidus).ToString();
                            area.Location.Add(point1);
                            // 计算point2坐标
                            var point2 = new ViewModel.FlightPlan.Location();
                            point2.Longitude = (double.Parse(map.Location[i].Longitude) + Math.Cos(Math.PI / (180 / (90 - angle))) * raidus).ToString();
                            point2.Latitude = (double.Parse(map.Location[i].Latitude) - Math.Sin(Math.PI / (180 / (90 - angle))) * raidus).ToString();
                            area.Location.Add(point2);
                            // 计算point3坐标
                            var point3 = new ViewModel.FlightPlan.Location();
                            point3.Longitude = (double.Parse(map.Location[i + 1].Longitude) + Math.Sin(Math.PI / (180 / angle)) * raidus).ToString();
                            point3.Latitude = (double.Parse(map.Location[i + 1].Latitude) - Math.Cos(Math.PI / (180 / angle)) * raidus).ToString();
                            area.Location.Add(point3);
                            // 计算point4坐标
                            var point4 = new ViewModel.FlightPlan.Location();
                            point4.Longitude = (double.Parse(map.Location[i + 1].Longitude) - Math.Sin(Math.PI / (180 / angle)) * raidus).ToString();
                            point4.Latitude = (double.Parse(map.Location[i + 1].Latitude) + Math.Cos(Math.PI / (180 / angle)) * raidus).ToString();
                            area.Location.Add(point4);

                            superMap.Add(area);
                        }
                    }
                    else
                        superMap.Add(map);
                }
            }
            return superMap;
        }
        public List<ViewModel.FlightPlan.SuperMapVM> GetAreaByCallSign(string callSign)
        {
            var context = new ZHCC_GAPlanEntities();
            var superMap = new List<ViewModel.FlightPlan.SuperMapVM>();
            var flyId = (from t in context.FlightPlan
                         where t.CallSign.Equals(callSign)
                         orderby t.CreateTime descending
                         select t.FlightPlanID).FirstOrDefault();
            var masters = (
                        from b in context.File_FlightPlanMaster
                        from c in context.File_Master
                        where b.FlightPlanID == flyId.ToString() && b.MasterID == c.ID
                        select c).ToList();
            if (masters != null && masters.Any())
            {
                foreach (var master in masters)
                {
                    var map = new ViewModel.FlightPlan.SuperMapVM();
                    map.MasterID = master.ID;
                    map.WorkType = master.WorkType;
                    map.RaidusMile = master.RaidusMile;
                    map.Location = detaildal.GetByMasterID(map.MasterID.ToString());
                    // 满足这3个条件确定类型为：航线左右范围
                    if (map.WorkType.Equals("airlinelr"))
                    {
                        for (int i = 0; i < map.Location.Count; i++)
                        {
                            // 添加点的圆形范围
                            var circle = new ViewModel.FlightPlan.SuperMapVM();
                            circle.WorkType = ViewModel.FlightPlan.DrawType.airlineCircle.ToString();
                            circle.RaidusMile = map.RaidusMile;
                            circle.Location = new List<ViewModel.FlightPlan.Location>();
                            circle.Location.Add(map.Location[i]);
                            superMap.Add(circle);
                            if (i + 1 == map.Location.Count)
                                break;
                            // 计算两点间航线左右范围形成的矩形4点
                            var area = new ViewModel.FlightPlan.SuperMapVM();
                            area.WorkType = ViewModel.FlightPlan.DrawType.airlineRectangle.ToString();
                            area.Location = new List<ViewModel.FlightPlan.Location>();
                            // 计算两点航线与正x轴行程的夹角
                            double angle = Math.Atan2
                            (
                                double.Parse(map.Location[i + 1].Latitude) - double.Parse(map.Location[i].Latitude),
                                double.Parse(map.Location[i + 1].Longitude) - double.Parse(map.Location[i].Longitude)
                            ) * 180 / Math.PI;
                            // 计算范围的宽度
                            double raidus = (double)map.RaidusMile / 111;
                            // 计算point1坐标
                            var point1 = new ViewModel.FlightPlan.Location();
                            point1.Longitude = (double.Parse(map.Location[i].Longitude) - Math.Cos(Math.PI / (180 / (90 - angle))) * raidus).ToString();
                            point1.Latitude = (double.Parse(map.Location[i].Latitude) + Math.Sin(Math.PI / (180 / (90 - angle))) * raidus).ToString();
                            area.Location.Add(point1);
                            // 计算point2坐标
                            var point2 = new ViewModel.FlightPlan.Location();
                            point2.Longitude = (double.Parse(map.Location[i].Longitude) + Math.Cos(Math.PI / (180 / (90 - angle))) * raidus).ToString();
                            point2.Latitude = (double.Parse(map.Location[i].Latitude) - Math.Sin(Math.PI / (180 / (90 - angle))) * raidus).ToString();
                            area.Location.Add(point2);
                            // 计算point3坐标
                            var point3 = new ViewModel.FlightPlan.Location();
                            point3.Longitude = (double.Parse(map.Location[i + 1].Longitude) + Math.Sin(Math.PI / (180 / angle)) * raidus).ToString();
                            point3.Latitude = (double.Parse(map.Location[i + 1].Latitude) - Math.Cos(Math.PI / (180 / angle)) * raidus).ToString();
                            area.Location.Add(point3);
                            // 计算point4坐标
                            var point4 = new ViewModel.FlightPlan.Location();
                            point4.Longitude = (double.Parse(map.Location[i + 1].Longitude) - Math.Sin(Math.PI / (180 / angle)) * raidus).ToString();
                            point4.Latitude = (double.Parse(map.Location[i + 1].Latitude) + Math.Cos(Math.PI / (180 / angle)) * raidus).ToString();
                            area.Location.Add(point4);

                            superMap.Add(area);
                        }
                    }
                    else
                        superMap.Add(map);
                }
            }
            return superMap;
        }
        public void AddFlightPlanOther(string masterIDs, string flightPlanID, string keyValue, ref string airlineworkText)
        {
            var context = new ZHCC_GAPlanEntities();
            StringBuilder sb = new StringBuilder("");
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        var masterlist = context.File_FlightPlanMaster.Where(u => u.FlightPlanID.Equals(keyValue));
                        foreach (var item in masterlist)
                        {
                            context.File_FlightPlanMaster.Remove(item);
                        }
                        context.SaveChanges();
                    }
                    var masterArray = masterIDs.Split(',');
                    foreach (var item in masterArray)
                    {
                        File_FlightPlanMaster master = new File_FlightPlanMaster()
                        {
                            MasterID = item,
                            FlightPlanID = flightPlanID
                        };
                        context.File_FlightPlanMaster.Add(master);
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
        /// <summary>
        /// 添加机场、航线、作业区
        /// </summary>
        /// <param name="airportidList"></param>
        /// <param name="airlineText"></param>
        /// <param name="repetPlanID"></param>
        public void AddFlightPlanTempOther(string airlineText, string cworkText, string pworkText, string hworkText, string flightPlanID, string keyValue, ref string airlineworkText)
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
                        var flightmasterlist = context.File_FlightPlanMaster.Where(u => u.FlightPlanID.Equals(flightPlanID));
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
                        File_FlightPlanMaster master = new File_FlightPlanMaster()
                        {
                            MasterID = item,
                            FlightPlanID = flightPlanID
                        };
                        context.File_FlightPlanMaster.Add(master);
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
        public List<File_FlightPlanMaster> GetFileFlightPlanMasterList(Expression<Func<File_FlightPlanMaster, bool>> where)
        {
            return masterdal.FindList(where, m => m.ID, true);
        }
        public List<vGetFlightPlanNodeInstance> GetNodeInstanceList(Expression<Func<vGetFlightPlanNodeInstance, bool>> where)
        {
            var insdal = new DBHelper<vGetFlightPlanNodeInstance>();
            return insdal.FindList(where, m => m.PlanID, true);
        }

    }
}
