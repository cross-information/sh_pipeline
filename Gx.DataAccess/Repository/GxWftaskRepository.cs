using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models;

namespace Gx.DataAccess.Repository
{
    public class GxWftaskRepository: RepositoryBase<GX_WFTASK>
    {
        public GxWftaskRepository() : base() { }
        public GxWftaskRepository(UnitOfWork obj) { this.context = obj.Context; }

        public void Add(GX_WFTASK models)
        {
            this.Add(models);
        }

        public void Update(GX_WFTASK models)
        {
            this.Update(models);
        }

        public void Delete(GX_WFTASK models)
        {
            this.Delete(models);
        }

        public GX_WFTASK FindEntity(GX_WFTASK models)
        {
            GX_WFTASK modelsEntity = new GX_WFTASK();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_WFTASK FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_WFTASK> FindAllRjxx()
        {
            return this.FindAll();
        }
    }
}
