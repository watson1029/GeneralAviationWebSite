using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;
using RIPS.Util.Collections;
public class DBHelper<T> where T : class
{
    protected ZHCC_GAPlanEntities context;

    public DBHelper()
    {
        this.context = new ZHCC_GAPlanEntities();
    }
    /// <summary>
    /// 新增单个实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual int Add(T entity)
    {
        Create(entity); //设置字段默认值
        //第一种方式
        context.Entry(entity).State = EntityState.Added;
        //第二种方式
        //context.Set<T>().Add(entity);

        // return context.SaveChanges();
        var result = 0;
        SaveAction(() =>
        {
            result = context.SaveChanges();
        });
        return result;
    }
    /// <summary>
    /// 批量新增实体
    /// </summary>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    public int AddList(List<T> entities)
    {
        foreach (var item in entities)
        {
            if (item == null) continue;
            Create(item); //设置字段默认值
            context.Entry(item).State = EntityState.Added;
        }
        return context.SaveChanges();
    }
    /// <summary>
    /// 删除单个实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual int Delete(T entity)
    {
        //第一种方式
        context.Entry(entity).State = EntityState.Deleted;

        //第二种方式
        //context.Set<T>().Attach(entity);
        //context.Set<T>().Remove(entity);

        return context.SaveChanges();
    }
    /// <summary>
    /// 按条件删除多个实体
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public virtual int BatchDelete(Expression<Func<T, bool>> where)
    {
        var list = context.Set<T>().Where(where).AsNoTracking().ToList();
        foreach (var item in list)
        {
            context.Entry(item).State = EntityState.Deleted;
        }
        return context.SaveChanges();
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="IDs"></param>
    /// <returns></returns>
    public virtual int BatchDelete(string IDs)
    {
        if (string.IsNullOrEmpty(IDs)) return 0;

        string[] ids = IDs.Split(',');
        int id = -1;
        T temp;
        foreach (var item in ids)
        {
            id = int.Parse(item);
            temp = context.Set<T>().Find(id);
            if (temp != null) context.Entry(temp).State = EntityState.Deleted;
        }
        return context.SaveChanges();
    }
    /// <summary>
    /// 修改单个实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public int Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        return context.SaveChanges();
    }
    /// <summary>
    /// 修改单个实体，可修改指定属性
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="propertyNames"></param>
    /// <returns></returns>
    public int Update(T entity, params string[] propertyNames)
    {
        //除去上下文管理
        RemoveHoldingEntityInContext(entity);
        DbEntityEntry entry = context.Entry(entity);
        entry.State = EntityState.Unchanged;
        foreach (string propertyName in propertyNames)
        {
            entry.Property(propertyName).IsModified = true;
        }
        context.Configuration.ValidateOnSaveEnabled = false;
        return context.SaveChanges();
    }
    public void RemoveHoldingEntityInContext(T entity)
    {
        var objContext = ((IObjectContextAdapter)context).ObjectContext;
        var objSet = objContext.CreateObjectSet<T>();
        var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);
        object foundEntity;
        var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
        if (exists)
        {
            objContext.Detach(foundEntity);
        }
    }
    /// <summary>
    /// 按条件查询，返回单个实体
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public T Find(Expression<Func<T, bool>> where)
    {
        return context.Set<T>().Where(where).AsNoTracking().FirstOrDefault();
    }
    /// <summary>
    /// 查询全部
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public List<T> FindList<S>(Expression<Func<T, S>> orderBy, bool isAsc)
    {
        if (isAsc)
            return context.Set<T>().OrderBy(orderBy).AsNoTracking().ToList();
        else
            return context.Set<T>().OrderByDescending(orderBy).AsNoTracking().ToList();
    }
    /// <summary>
    /// 按条件查询，排序
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public List<T> FindList<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderBy, bool isAsc)
    {
        if (isAsc)
            return context.Set<T>().Where(where).OrderBy(orderBy).AsNoTracking().ToList();
        else
            return context.Set<T>().Where(where).OrderByDescending(orderBy).AsNoTracking().ToList();
    }
    /// <summary>
    /// 分页，排序
    /// </summary>
    /// <typeparam name="S"><peparam>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="rowCount"></param>
    /// <param name="orderBy"></param>
    /// <param name="isAsc"></param>
    /// <returns></returns>
    public List<T> FindPagedList<S>(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<T, S>> orderBy, bool isAsc)
    {
        var list = context.Set<T>().AsQueryable();
        rowCount = list.Count();

        pageCount = rowCount / pageSize;
        if (rowCount % pageSize > 0)
            pageCount++;

        if (isAsc)
            list = list.OrderBy(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        else
            list = list.OrderByDescending(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        return list.AsNoTracking().ToList();
    }
    /// <summary>
    /// 按条件查询，分页，排序
    /// </summary>
    /// <typeparam name="S"><peparam>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="rowCount"></param>
    /// <param name="where"></param>
    /// <param name="orderBy"></param>
    /// <param name="isAsc"></param>
    /// <returns></returns>
    public List<T> FindPagedList<S>(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<T, bool>> where,
        Expression<Func<T, S>> orderBy, bool isAsc)
    {
        var list = context.Set<T>().Where(where);
        rowCount = list.Count();

        pageCount = rowCount / pageSize;
        if (rowCount % pageSize > 0)
            pageCount++;

        if (isAsc)
            list = list.OrderBy(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        else
            list = list.OrderByDescending(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        return list.AsNoTracking().ToList();
    }
    public List<T> FindPagedList<S>(Pagination pg, Expression<Func<T, S>> orderBy, bool isAsc)
    {
        int pageCount;
        int rowCount;

        var list = FindPagedList(pg.page, pg.rows, out pageCount, out rowCount, orderBy, isAsc);
        pg.rows = rowCount;

        return list;
    }
    public List<T> FindPagedList<S>(Pagination pg, Expression<Func<T, bool>> where, Expression<Func<T, S>> orderBy, bool isAsc)
    {
        int pageCount;
        int rowCount;

        var list = FindPagedList(pg.page, pg.rows, out pageCount, out rowCount, where, orderBy, isAsc);
        pg.rows = rowCount;

        return list;
    }
    /// <summary>
    /// 捕获异常，抛出详细信息（字段）
    /// </summary>
    /// <param name="act"></param>
    public void SaveAction(Action act)
    {
        try
        {
            act();
        }
        catch (DbEntityValidationException ex)
        {
            StringBuilder errors = new StringBuilder();
            IEnumerable<DbEntityValidationResult> validationResult = ex.EntityValidationErrors;
            foreach (DbEntityValidationResult result in validationResult)
            {
                ICollection<DbValidationError> validationError = result.ValidationErrors;
                foreach (DbValidationError err in validationError)
                {
                    errors.Append(err.PropertyName + ":" + err.ErrorMessage + "\r\n");
                }
            }
            throw new Exception(errors.ToString());
        }
    }
    /// <summary>
    /// 更新数据时候默认更新的字段
    /// </summary>
    /// <param name="entity"></param>
    private void Modify(T entity)
    {
        //var loginInfo = new OperatorProvider().GetCurrent();
        //var userId = loginInfo == null ? null : loginInfo.UserId;
        //userId = userId == null ? "Not logged in" : userId;
        var userId = 0;

        foreach (var pro in entity.GetType().GetProperties())
        {
            if (pro.Name.Equals("ModifiedTime"))
            {
                if (pro.GetValue(entity, null) == null) pro.SetValue(entity, DateTime.Now);
            }
            if (pro.Name.Equals("ModifiedUserId"))
            {
                if (pro.GetValue(entity, null) == null) pro.SetValue(entity, userId);
            }
            if (pro.Name.Equals("CreateTime"))
            {
                context.Entry(entity).Property("CreateTime").IsModified = false;
            }
            if (pro.Name.Equals("CreateUserId"))
            {
                context.Entry(entity).Property("CreateUserId").IsModified = false;
            }
            //if (pro.Name.Equals("DeletedFlag"))
            //{
            //    if (pro.GetValue(entity, null) == null) context.Entry(entity).Property("DeletedFlag").IsModified = false;
            //}
        }
    }
    /// <summary>
    /// /更新实体，指定需要更新或者不需要更新的字段
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="ismodified">是否需要更新</param>
    /// <param name="fileds">字段参数</param>
    /// <returns></returns>
    public int Update(T entity, bool ismodified, params string[] fileds)
    {
        int result = 0;
        if (entity != null)
        {
            RemoveHoldingEntityInContext(entity);
            DbEntityEntry entry = context.Entry(entity);
            if (ismodified == true)
            {
                entry.State = EntityState.Unchanged;
            }
            else
            {
                entry.State = EntityState.Modified;
            }
            foreach (string propertyName in fileds)
            {
                entry.Property(propertyName).IsModified = ismodified;
            }
            context.Configuration.ValidateOnSaveEnabled = false;
            Modify(entity);
            SaveAction(() =>
            {
                result = context.SaveChanges();
            });
        }
        return result;
    }
    protected void Create(T entity)
    {
        //var loginInfo = new OperatorProvider().GetCurrent();
        //var userId = loginInfo == null ? null : loginInfo.UserId;
        //userId = userId == null ? "Not logged in" : userId;
        var userId = 0;

        foreach (var pro in entity.GetType().GetProperties())
        {
            //if (pro.Name.Equals("Id"))
            //{
            //    if (pro.GetValue(entity) == null)
            //    {
            //        pro.SetValue(entity, Common.GuId().ToUpper());
            //    }
            //}
            if (pro.Name.Equals("CreateTime"))
            {
                if (pro.GetValue(entity, null) == null) pro.SetValue(entity, DateTime.Now);
                else if ((DateTime)pro.GetValue(entity, null) == DateTime.MinValue) pro.SetValue(entity, DateTime.Now);
            }
            if (pro.Name.Equals("CreateUserId"))
            {
                if (pro.GetValue(entity, null) == null) pro.SetValue(entity, userId);
            }
            if (pro.Name.Equals("LastModifiedTime"))
            {
                if (pro.GetValue(entity, null) == null) pro.SetValue(entity, DateTime.Now);
            }
            if (pro.Name.Equals("LastModifiedUserId"))
            {
                if (pro.GetValue(entity, null) == null) pro.SetValue(entity, userId);
            }
            if (pro.Name.Equals("DeletedFlag"))
            {
                if (pro.GetValue(entity) == null) pro.SetValue(entity, 0);
            }
        }
    }
}