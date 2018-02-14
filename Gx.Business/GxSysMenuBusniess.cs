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
    public class GxSysMenuBusniess
    {
        private UnitOfWork uw = UnitOfWork.Instance;

        public void AddEntity(GX_SYS_MENU gxSysMenu)
        {
            GxSysMenuRepository gxSysMenuRepository = new GxSysMenuRepository(uw);
            gxSysMenuRepository.AddEntity(gxSysMenu);
        }

        public void UpdateEntity(GX_SYS_MENU gxSysMenu)
        {
            GxSysMenuRepository gxSysMenuRepository = new GxSysMenuRepository(uw);
            gxSysMenuRepository.UpdateEntity(gxSysMenu);
        }

        public void DeleteEntity(GX_SYS_MENU gxSysMenu)
        {
            GxSysMenuRepository gxSysMenuRepository = new GxSysMenuRepository(uw);
            gxSysMenuRepository.DeleteEntity(gxSysMenu);
        }

        public GX_SYS_MENU Find(decimal id)
        {
            GxSysMenuRepository gxSysMenuRepository = new GxSysMenuRepository(uw);
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
        public List<GX_SYS_MENU> FindAlMenuList(int pageIndex, int pageSize, out int totalRecord)
        {
            GxSysMenuRepository gxSysMenuRepository = new GxSysMenuRepository(uw);
            return gxSysMenuRepository.FindAllGxSysMenu(pageIndex, pageSize, out totalRecord);
        }

        /// <summary>
        /// 返回所有用户角色所对应的菜单数据集合
        /// </summary>
        /// <returns></returns>
        public List<RoleMenuBo> FindAllRoleMenuList(decimal roleId, int page, int rows, out int totalRecord)
        {
            GxSysMenuRepository gxSysMenuRepository = new GxSysMenuRepository(uw);
            return gxSysMenuRepository.FindAllRoleMenuList(roleId,page,rows,out totalRecord);
        }


        /// <summary>
        /// 获取首页显示的菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleMenuBo> GetMenuRole(decimal roleId)
        {
            GxSysMenuRepository gxSysMenuRepository = new GxSysMenuRepository(uw);
            return gxSysMenuRepository.GetMenuRole(roleId);
        }

        /// <summary>
        /// 得到未选择的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleMenuBo> GetNotSelectMenuRole(int roleId)
        {
            GxSysMenuRepository gxSysMenuRepository = new GxSysMenuRepository(uw);
            return gxSysMenuRepository.GetNotSelectMenuRole(roleId);
        }
        }
}
