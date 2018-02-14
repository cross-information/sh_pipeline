using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models.ObjectBiz;

namespace Gx.DataAccess.Repository
{
    public class GxSysRoleMenuRepository : RepositoryBase<GX_SYS_ROLEMENU>
    {
        public GxSysRoleMenuRepository() : base()
        {

        }

        public GxSysRoleMenuRepository(UnitOfWork uw)
        {
            base.context = uw.Context;
        }
        public void AddEntity(GX_SYS_ROLEMENU gxsysuser)
        {
            this.Add(gxsysuser);
        }

        public void UpdateEntity(GX_SYS_ROLEMENU gxsysuser)
        {
            this.Update(gxsysuser);
        }

        public void DeleteEntity(GX_SYS_ROLEMENU gxsysuser)
        {
            this.Delete(gxsysuser);
        }

        public GX_SYS_ROLEMENU FindEntity(GX_SYS_ROLEMENU gxsysmenu)
        {
            GX_SYS_ROLEMENU menuEntity = new GX_SYS_ROLEMENU();
            if (gxsysmenu.ID > 0)
                menuEntity = this.Find(t => t.ID == gxsysmenu.ID);
            return menuEntity;
        }

        /// <summary>
        /// 返回所有菜单数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_SYS_ROLEMENU> FindAllGxSysMenu() {
            return this.FindAll();
        }

        /// <summary>
        /// 返回分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_SYS_ROLEMENU> FindAllGxSysMenu(int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 返回所有用户角色数据集合
        /// </summary>
        /// <returns></returns>
        public List<RoleMenuBo> FindAllRoleMenuList(decimal roleId, int page, int rows, out int totalRecord)
        {
            var models = (from p in context.Set<GX_SYS_MENU>().ToList()
                          from r in context.Set<GX_SYS_ROLEMENU>().ToList()
                          where p.ID == r.MENUID
                          where r.ROLEID == roleId
                          select new RoleMenuBo
                          {
                              MenuId = p.ID,
                              MenuName = p.MENUNAMC
                          }).ToList();
            totalRecord = models.Count;
            return models.Skip((page - 1) * rows).Take(rows).ToList();
        }

        public List<RoleMenuBo> GetMenuRole(int userid)
        {
            string SQLCOmmand = string.Format(@"select GX_SYS_MENU.Id as MenuId,GX_SYS_MENU.MENUNAMC as MenuName,GX_SYS_MENU.MENUURL as LinkUrl,GX_SYS_MENU.Modelid as ParaMenuID,GX_SYS_MODELS.Id as ModelId,GX_SYS_MODELS.NAME as ModelName from GX_SYS_ROLEMENU
                            left join GX_SYS_MENU on GX_SYS_MENU.ID = GX_SYS_ROLEMENU.Menuid
                            left join GX_SYS_MODELS on GX_SYS_MODELS.ID = GX_SYS_MENU.MODELID
                            left join GX_SYS_USERROLERELATE on GX_SYS_USERROLERELATE.ROLEID = GX_SYS_ROLEMENU.ROLEID
                            where GX_SYS_USERROLERELATE.USERID = '166662099'");
            return context.Database.SqlQuery<RoleMenuBo>(SQLCOmmand).ToList();
        }
    }
}
