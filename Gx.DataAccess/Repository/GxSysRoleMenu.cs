using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models.ObjectBiz;

namespace Gx.DataAccess.Repository
{
    public class GxSysRoleMenu : RepositoryBase<GX_SYS_ROLEMENU>
    {
        public GxSysRoleMenu() : base() { }
        public GxSysRoleMenu(UnitOfWork obj) { this.context = obj.Context; }

        public void Add(GX_SYS_ROLEMENU models)
        {
            this.Add(models);
        }

        public void Update(GX_SYS_ROLEMENU models)
        {
            this.Update(models);
        }

        public void Delete(GX_SYS_ROLEMENU models)
        {
            this.Delete(models);
        }

        public GX_SYS_ROLEMENU FindEntity(GX_SYS_ROLEMENU models)
        {
            GX_SYS_ROLEMENU modelsEntity = new GX_SYS_ROLEMENU();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_SYS_ROLEMENU FindRoleMenu()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_SYS_ROLEMENU> FindAllRoleMenu()
        {
            return this.FindAll();
        }
    }
}
