using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.DataAccess.Repository
{
    public class GxSysRoleperRelate : RepositoryBase<GX_SYS_ROLEPERRELATE>
    {
        public GxSysRoleperRelate() : base() { }
        public GxSysRoleperRelate(UnitOfWork obj) { this.context = obj.Context; }

        public void Add(GX_SYS_ROLEPERRELATE models)
        {
            this.Add(models);
        }

        public void Update(GX_SYS_ROLEPERRELATE models)
        {
            this.Update(models);
        }

        public void Delete(GX_SYS_ROLEPERRELATE models)
        {
            this.Delete(models);
        }

        public GX_SYS_ROLEPERRELATE FindEntity(GX_SYS_ROLEPERRELATE models)
        {
            GX_SYS_ROLEPERRELATE modelsEntity = new GX_SYS_ROLEPERRELATE();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_SYS_ROLEPERRELATE FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_SYS_ROLEPERRELATE> FindAllRjxx()
        {
            return this.FindAll();
        }
    }
}
