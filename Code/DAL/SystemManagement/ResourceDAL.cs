using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity;
using System.Data.SqlClient;
using Untity.DB;
using Model;
using Model.EF;
using System.Linq.Expressions;

namespace DAL.SystemManagement
{
    public class ResourceDAL : DBHelper<Resource>
    {
        //   private static SqlDbHelper dao = new SqlDbHelper();
        //删除资料
        public int DeleteResource(int ID)
        {
            return base.Delete(new Resource() { ID = ID });
        }
        //新增资料
        public int AddResource(Resource res)
        {
            return base.Add(res);
        }
        /// <summary>
        /// 更新资料
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public int UpdateResource(Resource res)
        {
            string[] paramters;
            if (res.FilePath == null || res.FilePath.Equals(""))
            {
                paramters = new string[]{
                "Title","DealUser","ResourceType","UsefulTime"
                };
            }
            else
            {
                paramters = new string[]{
                "Title","DealUser","ResourceType","UsefulTime","FilePath"
                };
            }
            return base.Update(res, paramters);
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
                predicate = predicate.And(m => m.ResourceType == resourceType);
            }
            if (status != 0)
            {
                predicate = predicate.And(m => m.Status == status);
            }
            predicate = predicate.And(m => m.IsDeleted == 0);
            List<Model.EF.Resource> list = FindPagedList(pageIndex, pageSize, out pageIndex, out pageSize, predicate, m => m.Created, false);
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
        public int GetCount(int resourceType, int status)
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
    }
}
