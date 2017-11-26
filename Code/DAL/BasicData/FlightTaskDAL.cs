using Model.BasicData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Untity.DB;

namespace DAL.BasicData
{
  public  class FlightTaskDAL
  {
      private static SqlDbHelper dao = new SqlDbHelper();
      public static List<FlightTask> GetAllList()
      {
          var sql = "select TaskCode,Abbreviation from FlightTask";
          return dao.ExecSelectCmd(ExecReader, sql);
      }
      private static FlightTask ExecReader(SqlDataReader dr)
      {
          FlightTask task = new FlightTask();
          task.Abbreviation = Convert.ToString(dr["Abbreviation"]);
          task.TaskCode = Convert.ToString(dr["TaskCode"]);
          return task;
      }
    }
}
