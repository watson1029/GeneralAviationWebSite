using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FlightPlan
{
    public class SuperMapBLL
    {
        private DAL.FlightPlan.FileMasterDAL masterdal = new DAL.FlightPlan.FileMasterDAL();
        private DAL.FlightPlan.FileDetailDAL detaildal = new DAL.FlightPlan.FileDetailDAL();
        private DAL.FlightPlan.FileFlightPlanMasterDAL flightplandal = new DAL.FlightPlan.FileFlightPlanMasterDAL();
        public List<ViewModel.FlightPlan.SuperMapVM> GetRepetMapData(string planID)
        {
            var superMap = new List<ViewModel.FlightPlan.SuperMapVM>();
            var masters = masterdal.GetByPlanID(planID);
            foreach (var master in masters)
            {
                var map = new ViewModel.FlightPlan.SuperMapVM();
                map.MasterID = master.ID;
                map.WorkType = master.WorkType;
                map.RaidusMile = master.RaidusMile;
                map.Location = detaildal.GetByMasterID(map.MasterID.ToString());
                // 满足条件确定类型为：航线左右范围
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
                        if (!string.IsNullOrEmpty(map.Location[i + 1].Latitude)&& !string.IsNullOrEmpty(map.Location[i + 1].Longitude) && !string.IsNullOrEmpty(map.Location[i].Latitude) && !string.IsNullOrEmpty(map.Location[i].Longitude))
                        {
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
                }
                else
                    superMap.Add(map);
            }
            return superMap;
        }

        public List<ViewModel.FlightPlan.SuperMapVM> GetFlyMapData(string planID)
        {
            var superMap = new List<ViewModel.FlightPlan.SuperMapVM>();
            var masters = masterdal.GetByMasterID(flightplandal.GetMasterList(planID));
            foreach (var master in masters)
            {
                var map = new ViewModel.FlightPlan.SuperMapVM();
                map.MasterID = master.ID;
                map.WorkType = master.WorkType;
                map.RaidusMile = master.RaidusMile;
                map.Location = detaildal.GetByMasterID(map.MasterID.ToString());
                // 满足条件确定类型为：航线左右范围
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
            return superMap;
        }
    }
}
