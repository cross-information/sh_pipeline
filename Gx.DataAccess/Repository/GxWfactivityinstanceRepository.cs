using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models;

namespace Gx.DataAccess.Repository
{
    /// <summary>
    /// 活动实例表
    /// </summary>
    public class GxWfactivityinstanceRepository : RepositoryBase<GX_WFACTIVITYINSTANCE>
    {
        public GxWfactivityinstanceRepository() : base() { }
        public GxWfactivityinstanceRepository(UnitOfWork obj) { this.context = obj.Context; }

        public void Add(GX_WFACTIVITYINSTANCE models)
        {
            this.Add(models);
        }

        public void Update(GX_WFACTIVITYINSTANCE models)
        {
            this.Update(models);
        }

        public void Delete(GX_WFACTIVITYINSTANCE models)
        {
            this.Delete(models);
        }

        public GX_WFACTIVITYINSTANCE FindEntity(GX_WFACTIVITYINSTANCE models)
        {
            GX_WFACTIVITYINSTANCE modelsEntity = new GX_WFACTIVITYINSTANCE();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_WFACTIVITYINSTANCE FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_WFACTIVITYINSTANCE> FindAllRjxx()
        {
            return this.FindAll();
        }
    }
}
