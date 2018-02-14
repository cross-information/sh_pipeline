using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.DataAccess;
using Gx.DataAccess.Repository;
using Gx.Models;
using Gx.Models.ViewModels;

namespace Gx.Business
{
    public class GXSysRoleBusiness
    {
        private UnitOfWork uw = UnitOfWork.Instance;


        public void Add(GX_SYS_ROLE gxSysRole)
        {
            GxSysRoleRepository gxSysRoleRepository = new GxSysRoleRepository(uw);
            gxSysRoleRepository.AddEntity(gxSysRole);
        }

        public void Update(GX_SYS_ROLE gxSysRole)
        {
            GxSysRoleRepository gxSysRoleRepository = new GxSysRoleRepository(uw);
            gxSysRoleRepository.UpdateEntity(gxSysRole);
        }

        public void Delete(GX_SYS_ROLE gxSysRole)
        {
            GxSysRoleRepository gxSysRoleRepository = new GxSysRoleRepository(uw);
            gxSysRoleRepository.DeleteEntity(gxSysRole);
        }

        public List<GX_SYS_ROLE> FindAll()
        {
            GxSysRoleRepository gxSysRoleRepository = new GxSysRoleRepository(uw);
            return gxSysRoleRepository.FindAllRole().ToList();
        }

        public int SaveChange()
        {
            return uw.Save();
        }

        public GX_SYS_ROLE Find(decimal? id)
        {
            GxSysRoleRepository gxSysRoleRepository = new GxSysRoleRepository(uw);
            return gxSysRoleRepository.FindEntity(id);
        }

        /// <summary>
        /// 返回菜单分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_SYS_ROLE> FindAllGxSysRole(int pageIndex, int pageSize, out int totalRecord)
        {
            GxSysRoleRepository gxSysRoleRepository = new GxSysRoleRepository(uw);
            return gxSysRoleRepository.FindAllGxSysRole(pageIndex, pageSize, out totalRecord);
        }

        /// <summary>
        /// 根据用户编号获取所以角色
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<UserRoleModels> FindAllUserRoleByUserId(decimal userid, int page , int rows ,out int totalCount)
        {
            GxSysUserrolerelate relation = new GxSysUserrolerelate(uw);
            return relation.FindAllUserrolerelate(userid, page, rows, out totalCount);
        }
    }
}
