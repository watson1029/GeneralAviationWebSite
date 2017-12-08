using DAL.Log;
using Model.EF;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace BLL.Log
{
    public class OperationLogBLL
    {

        private OperationLogDAL _dal = new OperationLogDAL();

        public OperationLog Get(int id)
        {
            return _dal.Find(m => m.ID == id);
        }
        public List<OperationLog> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<OperationLog, bool>> where)
        {
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.ID, true);
        }
    }
}
