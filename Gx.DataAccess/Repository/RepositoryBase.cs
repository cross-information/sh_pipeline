using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using CommonUnits.Specifications;
using Gx.DataAccess.Interface;
using LINQExtensions.CommonUnits;

namespace Gx.DataAccess.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class, new()
    {
        public DbContext context;

        /// <summary>
        /// 显式传入UnitOfWork
        /// </summary>
        /// <param name="obj"></param>
        public RepositoryBase(UnitOfWork obj)
        {
            this.context = obj.Context;
        }

        /// <summary>
        /// 默认构造函数，自动获取UnitOfWork
        /// </summary>
        /// <param name="isNewContext">是否需要新建连接</param>
        public RepositoryBase(bool isNewContext = false)
        {
            this.context = isNewContext ? new GxSysEntities() : UnitOfWork.Instance.Context;
        }

        public RepositoryBase()
        {
            this.context = UnitOfWork.Instance.Context;
        }

        /// <summary>
        /// 新增数据实体
        /// </summary>
        /// <returns></returns>
        public T Create()
        {
            return context.Set<T>().Create();
        }

        public void Detach(T entity)
        {
            context.Entry<T>(entity).State = System.Data.EntityState.Detached;
        }

        public void Attach(T entity)
        {
            context.Entry<T>(entity).State = System.Data.EntityState.Unchanged;
        }
        /// <summary>
        /// 更新指定Object到实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNoNeedChangeModifiedDate">不需要修改Last Modified Date</param>
        /// <returns></returns>
        public T Update(T entity, bool isNoNeedChangeModifiedDate = false)
        {
            if (context.Entry<T>(entity).State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
            }
            context.Entry<T>(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// 更新指定Object到实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public E Update<E>(E entity) where E : class , new()
        {
            if (context.Entry<E>(entity).State == EntityState.Detached)
            {
                context.Set<E>().Attach(entity);
            }
            context.Entry<E>(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// 插入新Object到实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public T Add(T entity)
        { 
            context.Set<T>().Add(entity);
            return entity;
        }

        /// <summary>
        /// 插入新Object到实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public E Add<E>(E entity) where E : class , new()
        { 
            context.Set<E>().Add(entity);
            return entity;
        }

        /// <summary>
        /// 从实体删除指定Object
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// 从实体删除指定Object
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Delete<E>(E entity) where E : class , new()
        {
            this.context.Set<E>().Remove(entity);
        }

        /// <summary>
        /// 将实体改动保存到数据库，在有unitofwork情况下此方法一般不需调用
        /// </summary>
        /// <returns>影响行数</returns>
        public int Save()
        {
            return this.context.SaveChanges();
        }

        /// <summary>
        /// 取第一条数据
        /// </summary>
        /// <returns></returns>
        public T FindFirstRow()
        {
            return context.Set<T>().FirstOrDefault();
        }

        /// <summary>
        /// 根据主键查询数据
        /// </summary>
        /// <param name="keyValues">主键</param>
        /// <returns></returns>
        public T Find(params object[] keyValues)
        {
            return context.Set<T>().Find(keyValues);
        }

        /// <summary>
        /// 查询满足条件的第一条记录
        /// </summary>
        /// <param name="predicate">查询条件（表达式形式）</param>
        /// <param name="noTracking">是否返回无跟踪实体</param>
        /// <param name="includes">满足条件的实体集合</param>
        /// <returns>满足条件的第一条记录</returns>
        public T Find(Expression<Func<T, bool>> predicate, bool noTracking = false, params string[] includes)
        {
            DbQuery<T> dbset = this.context.Set<T>();
            foreach (string include in includes)
            {
                dbset = dbset.Include(include);
            }
            if (noTracking)
            {
                return dbset.Where(predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return dbset.Where(predicate).FirstOrDefault();
            }
        }

        /// <summary>
        /// 查询满足条件的第一条记录
        /// </summary>
        /// <param name="predicate">查询条件（表达式形式）</param>
        /// <param name="noTracking">是否返回无跟踪实体</param>
        /// <param name="includes">满足条件的实体集合</param>
        /// <returns>满足条件的第一条记录</returns>
        public E Find<E>(Expression<Func<E, bool>> predicate, bool noTracking = false, params string[] includes) where E : class , new()
        {
            DbQuery<E> dbset = this.context.Set<E>();
            foreach (string include in includes)
            {
                dbset = dbset.Include(include);
            }
            if (noTracking)
            {
                return dbset.Where(predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return dbset.Where(predicate).FirstOrDefault();
            }
        }

        /// <summary>
        /// 返回满足条件的所有数据
        /// </summary>
        /// <param name="predicate">查询条件（表达式形式）</param>
        /// <param name="noTracking">是否返回无跟踪实体</param>
        /// <returns>满足条件的实体集合</returns>
        public virtual List<T> FindAll(Expression<Func<T, bool>> predicate, bool noTracking = false, params string[] includes)
        {
            DbQuery<T> dbset = this.context.Set<T>();
            foreach (string include in includes)
            {
                dbset = dbset.Include(include);
            }
            if (noTracking)
            {
                return dbset.Where(predicate).AsNoTracking().ToList();
            }
            else
            {
                return dbset.Where(predicate).ToList();
            }
        }

        /// <summary>
        /// 返回满足条件的所有数据
        /// </summary>
        /// <param name="predicate">查询条件（表达式形式）</param>
        /// <param name="noTracking">是否返回无跟踪实体</param>
        /// <returns>满足条件的实体集合</returns>
        public List<E> FindAll<E>(Expression<Func<E, bool>> predicate, bool noTracking = false, params string[] includes)where E : class , new()
        {
            DbQuery<E> dbset = this.context.Set<E>();
            foreach (string include in includes)
            {
                dbset = dbset.Include(include);
            }
            if (noTracking)
            {
                return dbset.Where(predicate).AsNoTracking().ToList();
            }
            else
            {
                return dbset.Where(predicate).ToList();
            }
        }

        /// <summary>
        /// 返回满足条件的所有数据
        /// </summary>
        /// <param name="spec">查询条件（规约形式）</param>
        /// <param name="noTracking">是否返回无跟踪实体</param>
        /// <returns>满足条件的实体集合</returns>
        public virtual List<T> FindAll(ISpecification<T> spec = null, bool noTracking = false, params string[] includes)
        {
            DbQuery<T> dbset = this.context.Set<T>();
            foreach (string include in includes)
            {
                dbset = dbset.Include(include);
            }
            if (noTracking)
            {
                if (spec == null)
                    return dbset.AsNoTracking().ToList();
                else
                    return dbset.Where(spec.SatisfiedBy()).AsNoTracking().ToList();
            }
            else
            {
                if (spec == null)
                    return dbset.ToList();
                else
                    return dbset.Where(spec.SatisfiedBy()).ToList();
            }
        }

        /// <summary>
        /// 返回满足条件的所有数据
        /// </summary>
        /// <param name="spec">查询条件（规约形式）</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="noTracking">是否返回无跟踪实体</param>
        /// <returns>满足条件的实体集合</returns>
        public virtual List<T> FindAll(ISpecification<T> spec, string order, int pageIndex, int pageSize, out int totalCount, bool noTracking = false)
        {
            if (noTracking)
            {
                totalCount = this.context.Set<T>().Where(spec.SatisfiedBy()).AsNoTracking().Count();
                return pageSize > 0 ? this.context.Set<T>().Where(spec.SatisfiedBy()).OrderUsingSortExpression(order).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToList()
                    : this.context.Set<T>().Where(spec.SatisfiedBy()).OrderUsingSortExpression(order).AsNoTracking().ToList();
            }
            else
            {
                totalCount = this.context.Set<T>().Where(spec.SatisfiedBy()).Count();
                return pageSize > 0 ? this.context.Set<T>().Where(spec.SatisfiedBy()).OrderUsingSortExpression(order).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
                    : this.context.Set<T>().Where(spec.SatisfiedBy()).OrderUsingSortExpression(order).ToList();
            }
#if DEBUG
            System.Diagnostics.Debug.WriteLine(this.context.Set<T>().Where(spec.SatisfiedBy()).ToString());
#endif
        }

        #region 执行返回值为标量的储存过程
        /// <summary>
        /// 执行返回值为标量的储存过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="functionName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteFunction<T>(string functionName, params System.Data.EntityClient.EntityParameter[] parameters)
        {
            System.Data.EntityClient.EntityCommand cmd = ((System.Data.EntityClient.EntityConnection)context.Database.Connection).CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var entityParameter in parameters)
            {
                if ((entityParameter.Direction == ParameterDirection.InputOutput || entityParameter.Direction == ParameterDirection.Input) && (entityParameter.Value == null))
                {
                    //获取参数的值
                    entityParameter.Value = DBNull.Value;
                }
                //添加参数
                cmd.Parameters.Add(new System.Data.EntityClient.EntityParameter(entityParameter.ParameterName, DbType.Decimal).Value);
            }
            cmd.Parameters.AddRange(parameters);
            cmd.CommandText = functionName;
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                var obj = cmd.ExecuteScalar();
                return (T)obj;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        #endregion
  
        /// <summary>
        /// 获取数据库当前时间，解决step提交时间不一致
        /// </summary>
        /// <returns>数据库时间</returns>
        public DateTime GetDBTime()
        {

            var time = this.context.Database.SqlQuery<DateTime>("select sysdate from dual").FirstOrDefault();
            if (time == null)
                return DateTime.Now;
            else
                return time;
        }

        public decimal GetSeq(string seq)
        {
            return this.context.Database.SqlQuery<decimal>("select " + seq + ".nextval from dual").FirstOrDefault();
        }
    }
}
