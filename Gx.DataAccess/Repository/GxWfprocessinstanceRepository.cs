using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models;

namespace Gx.DataAccess.Repository
{
    public class GxWfprocessinstanceRepository : RepositoryBase<GX_WFPROCESSINSTANCE>
    {
        public GxWfprocessinstanceRepository() : base() { }
        public GxWfprocessinstanceRepository(UnitOfWork obj) { this.context = obj.Context; }

        public void Add(GX_WFPROCESSINSTANCE models)
        {
            this.Add(models);
        }

        public void Update(GX_WFPROCESSINSTANCE models)
        {
            this.Update(models);
        }

        public void Delete(GX_WFPROCESSINSTANCE models)
        {
            this.Delete(models);
        }

        public GX_WFPROCESSINSTANCE FindEntity(GX_WFPROCESSINSTANCE models)
        {
            GX_WFPROCESSINSTANCE modelsEntity = new GX_WFPROCESSINSTANCE();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_WFPROCESSINSTANCE FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_WFPROCESSINSTANCE> FindAllRjxx()
        {
            return this.FindAll();
        }
    }
}
