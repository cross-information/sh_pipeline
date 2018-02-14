using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.DataAccess;
using Gx.DataAccess.Repository;
using Gx.Models;
using Gx.Models.ObjectBiz;

namespace Gx.Business
{
    public class GxSysRoleMenuBusniess
    {
        private UnitOfWork uw = UnitOfWork.Instance;

        public void Add(GX_SYS_ROLEMENU gxSysMenu)
        {
            GxSysRoleMenuRepository gxSysMenuRepository = new GxSysRoleMenuRepository(uw);
            gxSysMenuRepository.AddEntity(gxSysMenu);
        }

        public void Update(GX_SYS_ROLEMENU gxSysMenu)
        {
            GxSysRoleMenuRepository gxSysMenuRepository = new GxSysRoleMenuRepository(uw);
            gxSysMenuRepository.UpdateEntity(gxSysMenu);
        }

        public void Delete(GX_SYS_ROLEMENU gxSysMenu)
        {
            GxSysRoleMenuRepository gxSysMenuRepository = new GxSysRoleMenuRepository(uw);
            gxSysMenuRepository.DeleteEntity(gxSysMenu);
        }

        public GX_SYS_ROLEMENU Find(decimal id)
        {
            GxSysRoleMenuRepository gxSysMenuRepository = new GxSysRoleMenuRepository(uw);
            return gxSysMenuRepository.Find(t => t.ID == id);
        }

        public int SaveChange()
        {
            return uw.Save();
        }

        /// <summary>
        /// 返回菜单分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_SYS_ROLEMENU> FindAlMenuList(int pageIndex, int pageSize, out int totalRecord)
        {
            GxSysRoleMenuRepository gxSysMenuRepository = new GxSysRoleMenuRepository(uw);
            return gxSysMenuRepository.FindAllGxSysMenu(pageIndex, pageSize, out totalRecord);
        }

        /// <summary>
        /// 返回所有用户角色所对应的菜单数据集合
        /// </summary>
        /// <returns></returns>
        public List<RoleMenuBo> FindAllRoleMenuList(decimal roleId, int page, int rows, out int totalRecord)
        {
            GxSysRoleMenuRepository gxSysMenuRepository = new GxSysRoleMenuRepository(uw);
            return gxSysMenuRepository.FindAllRoleMenuList(roleId,page,rows,out totalRecord);
        }


        /// <summary>
        /// 获取首页显示的菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleMenuBo> GetMenuRole(int roleId)
        {
            GxSysRoleMenuRepository gxSysMenuRepository = new GxSysRoleMenuRepository(uw);
            return gxSysMenuRepository.GetMenuRole(roleId);
        }
    }
}
