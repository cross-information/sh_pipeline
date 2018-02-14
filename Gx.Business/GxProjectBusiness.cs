using Gx.DataAccess;
using Gx.DataAccess.Repository;
using Gx.Models;
using Gx.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.Business
{
    public class GxProjectBusiness
    {
        private UnitOfWork uw = UnitOfWork.Instance;


        public void Add(GX_XMYSXX gxXmysxx)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(uw);
            gxXmysxxRepository.AddEntity(gxXmysxx);
        }

        public void Update(GX_XMYSXX gxXmysxx)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(uw);
            gxXmysxxRepository.UpdateEntity(gxXmysxx);
        }

        public void Delete(GX_XMYSXX gxXmysxx)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(uw);
            gxXmysxxRepository.DeleteEntity(gxXmysxx);
        }

        public int SaveChange()
        {
            return uw.Save();
        }

        public GX_XMYSXX Find(decimal id)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(uw);
            return gxXmysxxRepository.FindEntity(id);
        }

        /// <summary>
        /// 返回菜单分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_XMYSXX> FindAllGxProject(string htbh, string gcbh, string gcmc, int pageIndex, int pageSize, out int totalRecord)
        {
            GxXmysxxRepository gxSysRoleRepository = new GxXmysxxRepository(uw);
            return gxSysRoleRepository.FindAllProject(htbh, gcbh, gcmc, pageIndex, pageSize, out totalRecord);
        }

        /// <summary>
        /// 根据角色名称返回用户
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<UserModels> GetUserList(string roleName)
        {
            GxSysUserrolerelate gxSysUserrolerelate = new GxSysUserrolerelate(uw);
            return gxSysUserrolerelate.GetRoleUserList(roleName);
        }
    }
}
