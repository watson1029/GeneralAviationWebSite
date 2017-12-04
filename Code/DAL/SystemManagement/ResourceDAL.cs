﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity;
using System.Data.SqlClient;
using Untity.DB;
using Model;
using System.Linq.Expressions;
using Model.EF;
namespace DAL.SystemManagement
{
    public class ResourceDAL : DBHelper<Resource>
    {
        //   private static SqlDbHelper dao = new SqlDbHelper();
        //删除资料
        public int Delete(int ID)
        {
            return Delete(new Resource() { ID = ID });
        }
        //新增资料
        public int Add(Resource res)
        {
            return Add(res);
        }
        //   //更新资料信息
        //   public static bool Update(Resource resource)
        //   {
        //       var sql = @"update Resource set Title=@Title ,DealUser=@DealUser
        //               ,ResourceType=@ResourceType,UsefulTime=@UsefulTime,FilePath=@FilePath,Status=@Status";
        //       SqlParameter[] parameters = {
        //new SqlParameter("@Title",  resource.Title),
        //new SqlParameter("@DealUser", resource.DealUser),
        //new SqlParameter("@ResourceType", resource.ResourceType),
        //new SqlParameter("@UsefulTime", resource.UsefulTime),
        //               new SqlParameter("@FilePath",resource.FilePath),
        //               new SqlParameter("@Status",resource.Status),
        //                                   };
        //       return dao.ExecNonQuery(sql, parameters) > 0;
        //   }
        //根据条件查询资料
        public List<Resource> GetList(int resourceType, int status, int pageIndex, int pageSize)
        {
            Expression<Func<Resource, bool>> predicate = PredicateBuilder.True<Resource>();
            if (resourceType != 0)
            {
                predicate = predicate.And(m => m.ResourceType==resourceType);
            }
            if (status != 0)
            {
                predicate = predicate.And(m => m.Status == status);
            }
            predicate = predicate.And(m => m.IsDeleted == 0);
            List<Model.EF.Resource> list = FindPagedList(pageIndex, pageSize,out pageIndex,out pageSize, predicate, m => m.ID, true);
            return list;
        }
        //   public static List<Resource> GetList(int resourceType,int status,int pageIndex,int pageSize)
        //   {
        //       StringBuilder sb = new StringBuilder();
        //       sb.Append("IsDeleted=0");
        //       if (resourceType != 0)
        //       {
        //           sb.Append(" and ResourceType=" + resourceType);
        //       }
        //       if (status != 0)
        //       {
        //           sb.Append(" and Status="+status);
        //       }
        //       var sql = string.Format("select * from Resource where {0}", sb.ToString());
        //       return dao.ExecSelectCmd(ExecReader, sql).ToPagedList<Resource>(pageIndex, pageSize); 
        //   }
           //根据条件查询资料总数
           public int GetCount(int resourceType,int status)
           {
               Expression<Func<Resource, bool>> predicate = PredicateBuilder.True<Resource>();
               if (resourceType != 0)
               {
                   predicate = predicate.And(m => m.ResourceType == resourceType);
               }
               if (status != 0)
               {
                   predicate = predicate.And(m => m.Status == status);
               }
               predicate = predicate.And(m => m.IsDeleted == 0);
               List<Model.EF.Resource> list = FindList(predicate, m => m.ID, true);
               return list.Count;
           }
        //   //将数据库返回的数据转换成Resource对象
        //   private static Resource ExecReader(SqlDataReader dr)
        //   {
        //       Resource resource = new Resource();
        //       resource.ID = Convert.ToInt32(dr["ID"]);
        //       resource.Title = Convert.ToString(dr["Title"]);
        //       resource.DealUser = Convert.ToString(dr["DealUser"]);
        //       resource.ResourceType = Convert.ToInt16(dr["ResourceType"]);
        //       resource.UsefulTime = Convert.ToString(dr["UsefulTime"]);
        //       resource.FilePath = Convert.ToString(dr["FilePath"]);
        //       resource.Created = Convert.ToDateTime(dr["Created"]);
        //       resource.IsDeleted = Convert.ToByte(dr["IsDeleted"]);
        //       resource.Status = Convert.ToInt16(dr["Status"]);
        //       resource.SenderId = Convert.ToInt16(dr["SenderId"]);
        //       return resource;
        //   }
    }
}
