using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models.ObjectBiz;

namespace Gx.DataAccess.Repository
{
    public class GxSysMenuRepository : RepositoryBase<GX_SYS_MENU>
    {
        public GxSysMenuRepository() : base()
        {

        }

        public GxSysMenuRepository(UnitOfWork uw)
        {
            base.context = uw.Context;
        }
        public void AddEntity(GX_SYS_MENU gxsysuser)
        {
            this.Add(gxsysuser);
        }

        public void UpdateEntity(GX_SYS_MENU gxsysuser)
        {
            this.Update(gxsysuser);
        }

        public void DeleteEntity(GX_SYS_MENU gxsysuser)
        {
            this.Delete(gxsysuser);
        }

        public GX_SYS_MENU FindEntity(GX_SYS_MENU gxsysmenu)
        {
            GX_SYS_MENU menuEntity = new GX_SYS_MENU();
            if (gxsysmenu.ID > 0)
                menuEntity = this.Find(t => t.ID == gxsysmenu.ID);
            if (!string.IsNullOrEmpty(gxsysmenu.MENUNAMC))
                menuEntity = this.Find(t => t.MENUNAMC.Contains(gxsysmenu.MENUNAMC));
            return menuEntity;
        }

        /// <summary>
        /// 返回所有菜单数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_SYS_MENU> FindAllGxSysMenu() {
            return this.FindAll();
        }

        /// <summary>
        /// 返回分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_SYS_MENU> FindAllGxSysMenu(int pageIndex, int pageSize, out int totalRecord)
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
            var models = context.Database.SqlQuery<RoleMenuBo>(string.Format(@"select gx_sys_rolemenu.Id as MenuId,gx_sys_menu.MENUNAMC as MenuName from gx_sys_menu
                                left join gx_sys_rolemenu on gx_sys_menu.id = gx_sys_rolemenu.menuid
                                where gx_sys_rolemenu.roleid = " + roleId)).ToList();
            totalRecord = models.Count;
            return models.Skip((page - 1) * rows).Take(rows).ToList();
        }

        public List<RoleMenuBo> GetMenuRole(decimal userid)
        {
            string SQLCOmmand = string.Format(@"select * from (
                            select distinct GX_SYS_MENU.Id as MenuId,GX_SYS_MENU.MENUNAMC as MenuName,GX_SYS_MENU.MENUURL as LinkUrl,GX_SYS_MENU.Modelid as ParaMenuID,GX_SYS_MODELS.Id as ModelId,GX_SYS_MODELS.NAME as ModelName from GX_SYS_ROLEMENU
                            left join GX_SYS_MENU on GX_SYS_MENU.ID = GX_SYS_ROLEMENU.Menuid
                            left join GX_SYS_MODELS on GX_SYS_MODELS.ID = GX_SYS_MENU.MODELID
                            left join GX_SYS_USERROLERELATE on GX_SYS_USERROLERELATE.ROLEID = GX_SYS_ROLEMENU.ROLEID
                            where GX_SYS_USERROLERELATE.USERID = " + userid + " ) temp where MenuName is not null ORDER BY modelId desc");
            return context.Database.SqlQuery<RoleMenuBo>(SQLCOmmand).ToList();
        }

        /// <summary>
        /// 得到未选择的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleMenuBo> GetNotSelectMenuRole(int roleId)
        {
            string SQLCOmmand = string.Format(@"select GX_SYS_MENU.Id as MenuId,GX_SYS_MENU.MENUNAMC as MenuName,GX_SYS_MENU.MENUURL as LinkUrl,GX_SYS_MENU.Modelid as ParaMenuID, "+ roleId + " as ModelId,'' as ModelName from GX_SYS_MENU  where GX_SYS_MENU.Id not in (select Menuid from GX_SYS_ROLEMENU where  ROLEID =" + roleId+")");
            return context.Database.SqlQuery<RoleMenuBo>(SQLCOmmand).ToList();
        }
    }
}
