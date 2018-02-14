using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.DataAccess.Repository
{
    public class GxSysPerMissionsRepository : RepositoryBase<GX_SYS_PERMISSIONS>
    {
        public GxSysPerMissionsRepository() : base() { }
        public GxSysPerMissionsRepository(UnitOfWork obj) { this.context = obj.Context; }

        public void Add(GX_SYS_PERMISSIONS models)
        {
            this.Add(models);
        }

        public void Update(GX_SYS_PERMISSIONS models)
        {
            this.Update(models);
        }

        public void Delete(GX_SYS_PERMISSIONS models)
        {
            this.Delete(models);
        }

        public GX_SYS_PERMISSIONS FindEntity(GX_SYS_PERMISSIONS models)
        {
            GX_SYS_PERMISSIONS modelsEntity = new GX_SYS_PERMISSIONS();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_SYS_PERMISSIONS FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.CREATETIME).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_SYS_PERMISSIONS> FindAllRjxx()
        {
            return this.FindAll();
        }
    }
}
