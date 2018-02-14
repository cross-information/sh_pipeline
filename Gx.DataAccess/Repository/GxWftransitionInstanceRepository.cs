using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.DataAccess.Repository
{
    public class GxWftransitionInstanceRepository : RepositoryBase<GX_WFTRANSITIONINSTANCE>
    {
        public GxWftransitionInstanceRepository() : base() { }
        public GxWftransitionInstanceRepository(UnitOfWork obj) { this.context = obj.Context; }

        public void Add(GX_WFTRANSITIONINSTANCE models)
        {
            this.Add(models);
        }

        public void Update(GX_WFTRANSITIONINSTANCE models)
        {
            this.Update(models);
        }

        public void Delete(GX_WFTRANSITIONINSTANCE models)
        {
            this.Delete(models);
        }

        public GX_WFTRANSITIONINSTANCE FindEntity(GX_WFTRANSITIONINSTANCE models)
        {
            GX_WFTRANSITIONINSTANCE modelsEntity = new GX_WFTRANSITIONINSTANCE();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_WFTRANSITIONINSTANCE FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_WFTRANSITIONINSTANCE> FindAllRjxx()
        {
            return this.FindAll();
        }
    }
}
