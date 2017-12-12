﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public int Add(T entity)
    {
        //第一种方式
        context.Entry<T>(entity).State = EntityState.Added;
        //第二种方式
        //context.Set<T>().Add(entity);

        return context.SaveChanges();        
    }
    /// <summary>
    /// 批量新增实体
    /// </summary>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    public int AddList(params T[] entities)
    {
        int result = 0;
        for (int i = 0; i < entities.Count(); i++)
        {
            if (entities[i] == null)
                continue;
            context.Entry<T>(entities[i]).State = EntityState.Added;
            //每20个记录提交一次
            if (i != 0 && i % 20 == 0)
            {
                result += context.SaveChanges();
            }
        }
        if (entities.Count() > 0)
            result += context.SaveChanges();
        return result;
    }
    /// <summary>
    /// 删除单个实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public int Delete(T entity)
    {
        //第一种方式
        context.Entry<T>(entity).State = EntityState.Deleted;

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
    public int BatchDelete(Expression<Func<T,bool>> where)
    {
        var list = context.Set<T>().Where(where).AsNoTracking().ToList();
        foreach (var item in list)
        {
            context.Entry<T>(item).State = EntityState.Deleted;
        }
        return context.SaveChanges();
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="IDs"></param>
    /// <returns></returns>
    public int BatchDelete(string IDs)
    {
        if (string.IsNullOrEmpty(IDs)) return 0;

        string[] ids = IDs.Split(',');
        int id = -1;
        T temp;
        foreach (var item in ids)
        {
            id = int.Parse(item);
            temp = context.Set<T>().Find(id);
            if(temp!= null)context.Entry<T>(temp).State = EntityState.Deleted;
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
        context.Entry<T>(entity).State = EntityState.Modified;
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
        DbEntityEntry entry = context.Entry<T>(entity);
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
        var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name,entity);
        object foundEntity;
        var exists = objContext.TryGetObjectByKey(entityKey,out foundEntity);
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
    public List<T> FindList<S>(Expression<Func<T, S>> orderBy,bool isAsc)
    {
        if(isAsc)
            return context.Set<T>().OrderBy(orderBy).AsNoTracking().ToList();
        else
            return context.Set<T>().OrderByDescending(orderBy).AsNoTracking().ToList();
    }
    /// <summary>
    /// 按条件查询，排序
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public List<T> FindList<S>(Expression<Func<T, bool>> where,Expression<Func<T, S>> orderBy, bool isAsc)
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
            list = list.OrderBy<T, S>(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        else
            list = list.OrderByDescending<T, S>(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
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
            list = list.OrderBy<T, S>(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        else
            list = list.OrderByDescending<T, S>(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        return list.AsNoTracking().ToList();
    }
}