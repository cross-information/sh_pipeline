using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.DataAccess.Repository
{
    /// <summary>
    /// 角色对应的用户角色组（角色对应的部门信息）
    /// </summary>
    public class GXUserDeptRepository : RepositoryBase<GX_XLZX>
    {
        public GXUserDeptRepository() : base() { }
        public GXUserDeptRepository(UnitOfWork obj) { this.context = obj.Context; }

        public void Add(GX_XLZX models)
        {
            this.Add(models);
        }

        public void Update(GX_XLZX models)
        {
            this.Update(models);
        }

        public void Delete(GX_XLZX models)
        {
            this.Delete(models);
        }

        public GX_XLZX FindEntity(GX_XLZX models)
        {
            GX_XLZX modelsEntity = new GX_XLZX();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_XLZX FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_XLZX> FindAllRjxx()
        {
            return this.FindAll();
        }
    }
}
