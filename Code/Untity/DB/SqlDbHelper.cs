using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untity.DB
{
    public class SqlDbHelper
    {
        //这里的W只能是代表着本函数的的类型,一到被其它函数调用的时候要根据函数的返回值来确定的
        public delegate W objectHandler<W>(SqlDataReader dr);//委托,函数类型,参数为游标
        SqlConnection conn = null;      //连接对象
        SqlTransaction tran = null;     //事务
        ETransactionState TransactionState { get; set; }

        public enum ETransactionState
        {
            /// <summary>
            /// 若调用SqlDBHelper.CommitTran()方法，将保留事务，暂不提交
            /// </summary>
            Retain = 0,
            /// <summary>
            /// 若调用SqlDBHelper.CommitTran()方法，将提交事务
            /// </summary>
            Commit = 1
        }

        //工程分开后,命名空间就可以区分开来了
        public SqlDbHelper()
        {
            ConnectionStringSettings set = ConfigurationManager.ConnectionStrings["SqlConnection"];//连接sqlserver的字符串
            if (set == null)
                throw new ConfigurationErrorsException("配置文件key有误。应该为:ConnString");
            string strSql = set.ConnectionString;
            Conn = new SqlConnection(strSql);//连接数据库
            TransactionState = ETransactionState.Commit;
        }

        /// <summary>
        /// 带连接字符串的构造函数
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        public SqlDbHelper(string connStr)
        {
            Conn = new SqlConnection(connStr);
        }

        public SqlConnection Conn
        {
            get { return this.conn; }
            private set { this.conn = value; }
        }

        public SqlTransaction Tran
        {
            get { return this.tran; }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            if (tran == null && conn.State == ConnectionState.Closed)//事务为null,连接处于关闭状态
            {
                conn.Open();
                tran = conn.BeginTransaction();
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="tranState">事务状态</param>
        public void BeginTran(ETransactionState tranState)
        {
            this.TransactionState = tranState;
            this.BeginTran();
        }

        /// <summary>
        /// Command执行操作准备
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="cmdParams">SqlParameter[]</param>
        /// <param name="cmdText">CommandText</param>
        /// <param name="isProc">是否为存储过程</param>
        public void PrepareCommand(SqlCommand cmd, SqlParameter[] cmdParams, string cmdText, bool isProc)
        {
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.Transaction = tran;
            cmd.CommandType = isProc ? CommandType.StoredProcedure : CommandType.Text;
            if (tran == null && conn.State == ConnectionState.Closed)
                conn.Open();

            if (cmdParams != null)
                foreach (SqlParameter cmdParam in cmdParams)
                {
                    cmd.Parameters.Add(cmdParam);
                }
        }

        /// <summary>
        /// 清除参数
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        private void ClearParameters(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
        }

        /// <summary>
        /// Command执行操作准备
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="cmdParams">SqlParameter[]</param>
        /// <param name="cmdText">CommandText</param>
        public void PrepareCommand(SqlCommand cmd, SqlParameter[] cmdParams, string cmdText)
        {
            this.PrepareCommand(cmd, cmdParams, cmdText, false);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns>执行成功返回true，否则false</returns>
        public bool CommitTran()
        {
            if (this.TransactionState == ETransactionState.Retain)
                return false;

            try
            {
                tran.Commit();//执行事务
                tran = null;  //设置事务为空,下次执行事务时能正常
                return true;
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="tranState">事务状态</param>
        /// <returns>执行成功返回true，否则false</returns>
        public bool CommitTran(ETransactionState tranState)
        {
            this.TransactionState = tranState;
            return this.CommitTran();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollBackTran()
        {
            try
            {
                if (tran != null && conn.State == ConnectionState.Open)
                    tran.Rollback();//回滚事务
                tran = null;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseConnection()
        {
            if (tran == null && conn.State == ConnectionState.Open)
                conn.Close();
        }

        /// <summary>
        /// 关闭连接，在此之前若存在事务，事务将回滚并置为null
        /// </summary>
        public void CloseConnectionWithTran()
        {
            RollBackTran();
        }

        /// <summary>
        /// 此方法适合用于增、删、改
        /// </summary>
        /// <param name="cmd">执行Sql语句</param>
        /// <returns>执行行数</returns>
        public int ExecNonQuery(SqlCommand cmd, string cmdText, params SqlParameter[] cmdParams)
        {
            try
            {
                PrepareCommand(cmd, cmdParams, cmdText);
                int line = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return line;
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// 重构的增、删、改方法
        /// </summary>
        /// <param name="cmdText">执行的sql字符串</param>
        /// <returns>执行行数</returns>
        public int ExecNonQuery(string cmdText, params SqlParameter[] cmdParams)
        {

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    int line = this.ExecNonQuery(cmd, cmdText, cmdParams);
                    ClearParameters(cmd);
                    return line;
                }
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 批量记录插入，此次操作包含事务控制
        /// </summary>
        /// <param name="dt">记录集</param>
        /// <param name="targetTableName">目标表字符串</param>
        /// <returns>执行结果布尔值</returns>
        public bool BulkInsertWithTransaction(DataTable dt, string targetTableName)
        {
            if (dt != null && dt.Rows.Count != 0)
            {
                try
                {
                    BeginTran();

                    BulkInsert(dt, targetTableName);

                    CommitTran();
                    return true;
                }
                catch (SqlException ex)
                {
                    RollBackTran();
                    throw ex;
                }
            }

            return false;
        }

        /// <summary>
        /// 批量记录插入，此次操作包含事务控制并且删除原来旧的数据
        /// </summary>
        /// <param name="dt">记录集</param>
        /// <param name="targetTableName">目标表字符串</param>
        /// <param name="deleteTargetData">删除原来数据语句</param>
        /// <returns>执行结果布尔值</returns>
        public bool BulkInsertWithTransaction(DataTable dt, string targetTableName, string deleteTargetData, string insertTargetData)
        {
            if (dt != null && dt.Rows.Count != 0)
            {
                try
                {
                    BeginTran();

                    ExecNonQuery(deleteTargetData, null);

                    BulkInsert(dt, targetTableName);

                    if (!string.IsNullOrEmpty(insertTargetData))
                    {
                        ExecNonQuery(insertTargetData, null);
                    }

                    CommitTran();
                    return true;
                }
                catch (SqlException ex)
                {
                    RollBackTran();
                    throw ex;
                }
            }

            return false;
        }

        /// <summary>
        /// 批量记录插入
        /// </summary>
        /// <param name="dt">记录集</param>
        /// <param name="targetTableName">目标表字符串</param>
        /// <returns>执行结果布尔值</returns>
        public bool BulkInsert(DataTable dt, string targetTableName)
        {
            if (dt != null && dt.Rows.Count != 0)
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
                {
                    bulkCopy.DestinationTableName = targetTableName;
                    bulkCopy.BatchSize = dt.Rows.Count;
                    bulkCopy.WriteToServer(dt);
                    bulkCopy.Close();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="dt">记录集</param>
        public void BulkUpdate(DataTable dt, string selectCmdText)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(selectCmdText, conn);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
            adapter.Update(dt);
            adapter.Dispose();
        }

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="cmdText">查询字符串</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns>
        public DataTable ExecSelectCmd(string cmdText, params SqlParameter[] cmdParams)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    DataTable db = new DataTable();
                    PrepareCommand(cmd, cmdParams, cmdText);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(db);
                    ClearParameters(cmd);
                    return db;
                }
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="cmdText">查询字符串</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns>
        public DataTable ExecSelectCmd(SqlCommand cmd, string cmdText, params SqlParameter[] cmdParams)
        {
            try
            {
                DataTable db = new DataTable();
                PrepareCommand(cmd, cmdParams, cmdText);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(db);
                return db;
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }


        /// <summary>
        /// 执行查询语句的泛型方法
        /// </summary>
        /// <typeparam name="T">返回实体对象的类型</typeparam>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="objHandler">委托方法</param>
        /// <param name="cmdText">查询字符串</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns>
        public List<T> ExecSelectCmd<T>(SqlCommand cmd, objectHandler<T> objHandler, string cmdText, params SqlParameter[] cmdParams)
        {

            List<T> objList = null;  //T代表不确定的类型
            try
            {
                PrepareCommand(cmd, cmdParams, cmdText);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (objList == null)
                            objList = new List<T>();
                        T obj = objHandler(dr); //调用委托方法,传递游标参数
                        objList.Add(obj); //添加到集合中
                    }
                    dr.Close();
                }
                return objList;
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 执行查询语句的泛型方法
        /// </summary>
        /// <typeparam name="T">返回实体对象的类型</typeparam>
        /// <param name="objHandler">委托方法</param>
        /// <param name="cmdText">查询字符串</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns>
        public List<T> ExecSelectCmd<T>(objectHandler<T> objHandler, string cmdText, params SqlParameter[] cmdParams)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                return ExecSelectCmd<T>(cmd, objHandler, cmdText, cmdParams);
            }
        }

        /// <summary>
        /// 执行查询语句的泛型方法，返回单个实体类
        /// </summary>
        /// <typeparam name="T">返回实体对象的类型</typeparam>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="objSinleHandler">委托方法</param>
        /// <param name="cmdText">查询字符串</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns> 
        public T ExecSelectSingleCmd<T>(SqlCommand cmd, objectHandler<T> objSinleHandler, string cmdText, params SqlParameter[] cmdParams)
        {
            T obj = default(T);  //T代表不确定的类型

            try
            {
                PrepareCommand(cmd, cmdParams, cmdText);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    ClearParameters(cmd);

                    if (dr.Read())
                    {
                        obj = objSinleHandler(dr); //调用委托方法,传递游标参数
                    }
                    dr.Close();
                }
                return obj;
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 执行查询语句的泛型方法，返回单个实体类
        /// </summary>
        /// <typeparam name="T">返回实体对象的类型</typeparam>
        /// <param name="objSinleHandler">委托方法</param>
        /// <param name="cmdText">查询字符串</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns>
        public T ExecSelectSingleCmd<T>(objectHandler<T> objSinleHandler, string cmdText, params SqlParameter[] cmdParams)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                return ExecSelectSingleCmd<T>(cmd, objSinleHandler, cmdText, cmdParams);
            }
        }

        /// <summary>
        /// 返回单行单列数据
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="cmdText">查询字符串</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns>
        public object ExecScalar(SqlCommand cmd, string cmdText, params SqlParameter[] cmdParams)
        {
            try
            {
                PrepareCommand(cmd, cmdParams, cmdText);
                object obj = cmd.ExecuteScalar();
                ClearParameters(cmd);
                return obj;
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 返回单行单列数据
        /// </summary>
        /// <param name="cmdText">查询字符串</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns>
        public object ExecScalar(string cmdText, params SqlParameter[] cmdParams)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                object obj = ExecScalar(cmd, cmdText, cmdParams);
                ClearParameters(cmd);
                return obj;
            }
        }

        /// <summary>
        /// 执行存储过程，返回结果记录表
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns>
        public DataTable ExecProcedureSelect(string procName, params SqlParameter[] cmdParams)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    DataTable db = new DataTable();
                    PrepareCommand(cmd, cmdParams, procName, true);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(db);
                    ClearParameters(cmd);
                    return db;
                }
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 执行存储过程，返回执行影响行数
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns>执行影响行数</returns>
        public int ExecProcedureNonquery(string procName, params SqlParameter[] cmdParams)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, cmdParams, procName, true);
                    int count = cmd.ExecuteNonQuery();
                    ClearParameters(cmd);
                    return count;
                }
            }
            catch (SqlException ex)
            {
                RollBackTran();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 执行查询，返回SqlDataReader
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <param name="cmdParams">查询参数</param>
        /// 在事务存在的情况下，须使用此方法重载；
        /// 此方法重载默认情况下会判断当前事务是否存在，
        /// 如果事务存在，则SqlCommand.ExecuteReader()的参数为CommandBehavior.Default
        /// 否则为CommandBehavior.Default。
        /// <returns></returns>
        public SqlDataReader ExecReader(string cmdText, params SqlParameter[] cmdParams)
        {
            CommandBehavior cmdBehavior = tran == null ? CommandBehavior.CloseConnection : CommandBehavior.Default;
            return ExecReader(cmdText, cmdBehavior, cmdParams);
        }

        /// <summary>
        /// 执行查询，返回SqlDataReader
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns></returns>
        public SqlDataReader ExecReader(string cmdText, CommandBehavior cmdBehavior, params SqlParameter[] cmdParams)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, cmdParams, cmdText);
                SqlDataReader rdr = cmd.ExecuteReader(cmdBehavior);
                ClearParameters(cmd);
                return rdr;
            }
            catch (SqlException ex)
            {
                RollBackTran();
                CloseConnection();
                throw ex;
            }
        }

        /// <summary>
        /// 返回记录编号
        /// </summary>
        /// <param name="tableName">与数据库对应的表名</param>
        /// <param name="prefixName">编号前缀</param>
        /// <returns>前缀+编号</returns>
        public string GetSerialNum(string tableName, string prefixName)
        {
            string ident_current = this.ExecScalar(string.Format("SELECT IDENT_CURRENT('{0}') + 1", tableName)).ToString();

            if (ident_current.Length < 4)
                return string.Format("{0}{1}", prefixName, ident_current.PadLeft(4, '0'));
            return string.Format("{0}{1}", prefixName, ident_current);
        }
    }
}
