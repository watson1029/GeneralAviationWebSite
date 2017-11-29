
using System;
using System.Data;
using System.Data.SqlClient;
using Untity.DB;
using Untity;
using Model.BasicData;

namespace DAL.BasicData
{

    public class AircraftDAL
    {

        private static SqlDbHelper dao = new SqlDbHelper();


        public static bool Delete(string ids)
        {
            var sql = string.Format("delete from Aircraft WHERE  (ID IN ({0}))", ids);

            return dao.ExecNonQuery(sql) > 0;
        }
        public static bool Add(Aircraft model)
        {
            var sql = @"insert into Aircraft(AircraftID,FuelCapacity,AcfType,Range,AcfNo,ASdate,AcfClass,CruiseAltd,Manufacturer,CruiseSpeed,
                                             WakeTurbulance,MaxSpeed,FueledWeight,MinSpeed,CreateTime)
                          values (@AircraftID,@FuelCapacity,@AcfType,@Range,@AcfNo,@ASdate,@AcfClass,@CruiseAltd,@Manufacturer,@CruiseSpeed,
                                  @WakeTurbulance,@MaxSpeed,@FueledWeight,@MinSpeed,@CreateTime)";
            SqlParameter[] parameters = {
                    new SqlParameter("@AircraftID",  model.AircraftID),
                    new SqlParameter("@FuelCapacity", model.FuelCapacity),
                    new SqlParameter("@AcfType", model.AcfType),
                    new SqlParameter("@Range", model.Range),
                    new SqlParameter("@AcfNo", model.AcfNo),
                    new SqlParameter("@ASdate", model.ASdate),
                    new SqlParameter("@AcfClass",  model.AcfClass),
                    new SqlParameter("@CruiseAltd", model.CruiseAltd),
                    new SqlParameter("@Manufacturer", model.Manufacturer),
                    new SqlParameter("@CruiseSpeed", model.CruiseSpeed),
                    new SqlParameter("@WakeTurbulance", model.WakeTurbulance),
                    new SqlParameter("@MaxSpeed", model.MaxSpeed),
                    new SqlParameter("@FueledWeight",  model.FueledWeight),
                    new SqlParameter("@MinSpeed", model.MinSpeed),
                    new SqlParameter("@CreateTime", model.CreateTime),
            };
            return dao.ExecNonQuery(sql, parameters) > 0;

        }
        public static bool Update(Aircraft  model)
        {
            var sql = @"update Aircraft set AircraftID=@AircraftID,FuelCapacity=@FuelCapacity,AcfType=@AcfType,Range=@Range,AcfNo=@AcfNo
                                            ASdate=@ASdate,AcfClass=@AcfClass,CruiseAltd=@CruiseAltd,
                                            Manufacturer=@Manufacturer,CruiseSpeed=@CruiseSpeed,WakeTurbulance=@WakeTurbulance,MaxSpeed=@MaxSpeed,
                                            FueledWeight=@FueledWeight,MinSpeed=@MinSpeed,CreateTime=@CreateTime where ID=@ID";
            SqlParameter[] parameters = {
                    new SqlParameter("@AircraftID",  model.AircraftID),
                    new SqlParameter("@FuelCapacity", model.FuelCapacity),
                    new SqlParameter("@AcfType", model.AcfType),
                    new SqlParameter("@Range", model.Range),
                    new SqlParameter("@AcfNo", model.AcfNo),
                    new SqlParameter("@ASdate", model.ASdate),
                    new SqlParameter("@AcfClass",  model.AcfClass),
                    new SqlParameter("@CruiseAltd", model.CruiseAltd),
                    new SqlParameter("@Manufacturer", model.Manufacturer),
                    new SqlParameter("@CruiseSpeed", model.CruiseSpeed),
                    new SqlParameter("@WakeTurbulance", model.WakeTurbulance),
                    new SqlParameter("@MaxSpeed", model.MaxSpeed),
                    new SqlParameter("@Weight",  model.FueledWeight),
                    new SqlParameter("@MinSpeed", model.MinSpeed),
                    new SqlParameter("@CreateTime", model.CreateTime),
            };
            return dao.ExecNonQuery(sql, parameters) > 0;

        }


        public static PagedList<Aircraft> GetList(int pageSize, int pageIndex, string strWhere)
        {
            var sql = string.Format("select * from Aircraft where {0}", strWhere);
            return dao.ExecSelectCmd(ExecReader, sql).ToPagedList<Aircraft>(pageIndex, pageSize);

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Aircraft Get(int id)
        {
            var sql = "select  top 1 * from Aircraft where ID=@ID";
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            return dao.ExecSelectSingleCmd<Aircraft>(ExecReader, sql, parameters);
        }

        private static Aircraft ExecReader(SqlDataReader dr)
        {
            Aircraft aircraft = new Aircraft();
            aircraft.AircraftID = Convert.ToString(dr["AircraftID"]);
            aircraft.ID = Convert.ToInt32(dr["ID"]);
            aircraft.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            aircraft.FuelCapacity = Convert.ToInt32(dr["FuelCapacity"]);
            aircraft.AcfType = Convert.ToString(dr["AcfType"]);
            aircraft.Range = Convert.ToInt32(dr["Range"]);
            aircraft.AcfNo = Convert.ToString(dr["AcfNo"]);
            aircraft.ASdate = Convert.ToInt32(dr["ASdate"]);
            aircraft.AcfClass = Convert.ToString(dr["AcfClass"]);
            aircraft.CruiseAltd = Convert.ToInt32(dr["CruiseAltd"]);
            aircraft.Manufacturer = Convert.ToString(dr["Manufacturer"]);
            aircraft.CruiseSpeed = Convert.ToInt32(dr["CruiseSpeed"]);
            aircraft.WakeTurbulance = Convert.ToString(dr["WakeTurbulance"]);
            aircraft.MaxSpeed = Convert.ToInt32(dr["MaxSpeed"]);
            aircraft.FueledWeight = Convert.ToInt32(dr["FueledWeight "]);
            aircraft.MinSpeed = Convert.ToInt32(dr["MinSpeed"]);
            return aircraft;
        }
    }
}
