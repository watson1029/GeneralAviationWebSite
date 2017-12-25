using DAL.Log;
using Model.EF;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace BLL.Log
{
    public class LoginLogBLL
    {

        private LoginLogDAL _dal = new LoginLogDAL();

        public LoginLog Get(int id)
        {
            return _dal.Find(m => m.ID == id);
        }
        public List<LoginLog> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<LoginLog, bool>> where)
        {
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.ID, true);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LoginLog model)
        {
            return _dal.Add(model);
        }
    }
}
