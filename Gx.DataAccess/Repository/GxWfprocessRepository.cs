using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.DataAccess.Repository
{
    /// <summary>
    /// 流程定义表
    /// </summary>
    public class GxWfprocessRepository : RepositoryBase<GX_WFPROCESS>
    {
        public GxWfprocessRepository() : base() { }
        public GxWfprocessRepository(UnitOfWork obj) { this.context = obj.Context; }

        public void Add(GX_WFPROCESS models)
        {
            this.Add(models);
        }

        public void Update(GX_WFPROCESS models)
        {
            this.Update(models);
        }

        public void Delete(GX_WFPROCESS models)
        {
            this.Delete(models);
        }

        public GX_WFPROCESS FindEntity(GX_WFPROCESS models)
        {
            GX_WFPROCESS modelsEntity = new GX_WFPROCESS();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_WFPROCESS FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_WFPROCESS> FindAllRjxx()
        {
            return this.FindAll();
        }
    }
}
