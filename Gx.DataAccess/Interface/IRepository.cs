using System.Collections.Generic;
using CommonUnits.Specifications;

namespace Gx.DataAccess.Interface
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, new()
    {
        /// <summary>
        /// 创建用于查询的Object
        /// </summary>
        /// <returns></returns>
        T Create();
        /// <summary>
        /// 更新指定Object到实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNoNeedChangeModifiedDate">不需要修改Last Modified Date</param>
        /// <returns></returns>
        T Update(T entity, bool isNoNeedChangeModifiedDate = false);
        /// <summary>
        /// 插入新Object到实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);
        /// <summary>
        /// 从实体删除指定Object
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 根据主键查询数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        T Find(params object[] keyValues);
        /// <summary>
        ///  返回所有数据
        /// </summary>
        /// <returns></returns>
        List<T> FindAll(ISpecification<T> spec, bool noTracking = false, params string[] includes);

        List<T> FindAll(ISpecification<T> spec, string order, int pageIndex, int pageSize, out int totalCount, bool noTracking = false);
    }
}
