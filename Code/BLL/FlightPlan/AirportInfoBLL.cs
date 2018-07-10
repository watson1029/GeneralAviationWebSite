using DAL.FlightPlan;
using Model.EF;
using RIPS.Util.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity;
using ViewModel.FlightPlan;

namespace BLL.FlightPlan
{
    public class AirportInfoBLL
    {
        private AirportInfoDAL _dal = new AirportInfoDAL();

        public void Delete(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                AirportInfo airportInfo = new AirportInfo();
                airportInfo.Id = keyValue;
                airportInfo.DeletedFlag = true;
                _dal.Update(airportInfo, true, "DeletedFlag");
            }
        }
        public List<AirportInfo> GetList(Pagination pag, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return _dal.FindPagedList(pag, t => t.DeletedFlag == false, o => o.CreateTime, false);
            }
            else
            {
                return _dal.FindPagedList(pag, t => t.DeletedFlag == false && t.Name.Equals(keyValue), o => o.CreateTime, false);
            }
        }

        public List<AirportInfo> GetList()
        {
            return _dal.FindList(w => w.DeletedFlag == false, o => o.CreateTime, false);
        }
        public AirportInfo Get(string id)
        {
            return _dal.Find(w => w.Id.Equals(id));
        }
        public AirportInfo GetAirport(string name)
        {
            return _dal.Find(w => w.Name.Contains(name));
        }
        public AirportInfo Add(AirportInfo entity)
        {
            entity.DeletedFlag = false;
            entity.CreateTime = DateTime.Now;
            entity.CreateUserId = 0;
            entity.ModifiedTime = DateTime.Now;
            entity.ModifiedUserId = 0;

            _dal.Add(entity);

            return entity;
        }
        public void Update(AirportInfo entity)
        {
            _dal.Update(entity, false, "DeletedFlag");
        }
        /// <summary>
        /// 机场起降点基础数据维护
        /// </summary>
        /// <param name="airportArray"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<string> AddOrUpdateAirport(List<AirportFillText> airportArray, int userID, ref string airportText)
        {

            List<string> airportIDs = new List<string>();
            StringBuilder sb = new StringBuilder("");
            foreach (var item in airportArray)
            {
                var airportid = string.Empty;
                var data = GetAirport(item.AirportName);
                var splitmodel = SpecialFunctions.latLongSplit(item.LatLong);
                if (data == null)
                {
                    airportid = Guid.NewGuid().ToString();
                    data = new AirportInfo()
                    {
                        Id = airportid,
                        Name = item.AirportName,
                        Type = "TH",
                        Code4= item.CodeF,
                        Longitude = splitmodel.Longitude ?? "",
                        Latitude = splitmodel.Latitude ?? "",
                        FrameOfAxes = "",
                        RunwayLength = "",
                        AreaParentCode = "",
                        AreaCode = "",
                        DeletedFlag = false,
                        CreateTime = DateTime.Now,
                        ModifiedTime = DateTime.Now,
                        ModifiedUserId = userID,
                        CreateUserId = userID,
                        CreateSource = ""
                    };
                    this.Add(data);
                }
                else
                {
                    airportid = data.Id;
                    data.Name = item.AirportName;
                    if (!string.IsNullOrEmpty(splitmodel.Latitude))
                        data.Latitude = splitmodel.Latitude;
                    if (!string.IsNullOrEmpty(splitmodel.Longitude))
                        data.Longitude = splitmodel.Longitude;
                    _dal.Update(data, "Id", "Name", "Longitude", "Latitude");
                }
                sb.AppendLine(item.AirportName + "；");
                airportIDs.Add(airportid);
            }
            airportText = sb.ToString();
            return airportIDs;

        }


    }
}
