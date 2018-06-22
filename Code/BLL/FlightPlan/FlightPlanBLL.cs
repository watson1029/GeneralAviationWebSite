using DAL.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Model.EF;
using System.Text;
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
