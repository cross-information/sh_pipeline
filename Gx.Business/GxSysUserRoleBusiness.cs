using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.DataAccess;
using Gx.DataAccess.Repository;
using Gx.Models;

namespace Gx.Business
{
    public class GxSysUserRoleBusiness
    {
        private UnitOfWork uw = UnitOfWork.Instance;

        /// <summary>
        /// 保存用户角色关系表
        /// </summary>
        /// <param name="gxSys"></param>
        public void AddEntity(GX_SYS_USERROLERELATE gxSys)
        {
            GxSysUserrolerelate relation = new GxSysUserrolerelate(uw);
            relation.AddEntity(gxSys);
        }

        /// <summary>
        /// 删除用户角色关系表
        /// </summary>
        /// <param name="gxSys"></param>
        public void DeleteEntity(GX_SYS_USERROLERELATE gxSys)
        {
            GxSysUserrolerelate relation = new GxSysUserrolerelate(uw);
            relation.DeleteEntity(gxSys);
        }

        public GX_SYS_USERROLERELATE FindEntity(decimal id)
        {
            GxSysUserrolerelate relation = new GxSysUserrolerelate(uw);
            return relation.Find(t => t.ID == id);
        }

        public GX_SYS_USERROLERELATE FindRoleEntity(decimal id)
        {
            GxSysUserrolerelate relation = new GxSysUserrolerelate(uw);
            return relation.Find(t => t.USERID == id && t.ROLEID > 0 & t.USERID > 0);
        }

        /// <summary>
        /// 根据用户id编号和角色名称获取用户角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public GX_SYS_ROLE FindRoleByUserIdAndRleName(string roleName, decimal userId)
        {
            GxSysUserrolerelate relation = new GxSysUserrolerelate(uw);
            return relation.FindRoleByUserIdAndRleName(roleName, userId);
        }
    }
}
