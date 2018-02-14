using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.DataAccess.Repository
{
    public class GxSysUserRepository: RepositoryBase<GX_SYS_USER>
    {
        public GxSysUserRepository() : base()
        {

        }

        public GxSysUserRepository(UnitOfWork uw) {
            base.context = uw.Context;
        }
        public void AddEntity(GX_SYS_USER gxsysuser)
        {
            this.Add(gxsysuser);
        }

        public void UpdateEntity(GX_SYS_USER gxsysuser)
        {
            this.Update(gxsysuser);
        }

        public void DeleteEntity(GX_SYS_USER gxsysuser)
        {
            this.Delete(gxsysuser);
        }

        public GX_SYS_USER FindEntity(decimal id)
        {
            return this.Find(t => t.ID == id);
        }

        public GX_SYS_USER FindEntity(GX_SYS_USER gxsysuser)
        {
            GX_SYS_USER userEntity = new GX_SYS_USER();
            if (gxsysuser.ID > 0)
                userEntity = this.Find(t => t.ID == gxsysuser.ID);
            if (!string.IsNullOrEmpty(gxsysuser.USERID))
                userEntity = this.Find(t => t.USERID == gxsysuser.USERID);
            if (!string.IsNullOrEmpty(gxsysuser.USERNAME))
                userEntity = this.Find(t => t.USERNAME.Contains(gxsysuser.USERNAME));
            return userEntity;
        }

        public List<GX_SYS_USER> FindAllGxSysUser()
        {
            return this.FindAll();
        }

        /// <summary>
        /// 返回所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<GX_SYS_USER> FindAllGxSysUser(int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据角色编号获取用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<GX_SYS_USER> FindAllUserByRoleId(decimal roleId)
        {
            var list = (from p in context.Set<GX_SYS_USER>()
                        from r in context.Set<GX_SYS_USERROLERELATE>()
                        where p.ID == r.USERID && r.ROLEID == roleId
                        select p).ToList();
            return list;
        }
    }
}
