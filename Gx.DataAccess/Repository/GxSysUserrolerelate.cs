using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models.ViewModels;

namespace Gx.DataAccess.Repository
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    public class GxSysUserrolerelate : RepositoryBase<GX_SYS_USERROLERELATE>
    {
        public GxSysUserrolerelate() : base() { }
        public GxSysUserrolerelate(UnitOfWork obj) { this.context = obj.Context; }

        public void AddEntity(GX_SYS_USERROLERELATE models)
        {
            this.Add(models);
        }

        public void UpdateEntity(GX_SYS_USERROLERELATE models)
        {
            this.Update(models);
        }

        public void DeleteEntity(GX_SYS_USERROLERELATE models)
        {
            this.Delete(models);
        }

        public GX_SYS_USERROLERELATE FindEntity(GX_SYS_USERROLERELATE models)
        {
            GX_SYS_USERROLERELATE modelsEntity = new GX_SYS_USERROLERELATE();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_SYS_USERROLERELATE FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_SYS_USERROLERELATE> FindAllRjxx()
        {
            return this.FindAll();
        }

        /// <summary>
        /// 返回所有用户角色数据集合
        /// </summary>
        /// <returns></returns>
        public List<UserRoleModels> FindAllUserrolerelate(decimal userid, int page, int rows, out int totalRecord)
        {
            var models = (from p in context.Set<GX_SYS_USERROLERELATE>().ToList()
                          from r in context.Set<GX_SYS_ROLE>().ToList()
                          where p.ROLEID == r.ID
                          where p.USERID == userid
                          select new UserRoleModels
                              {
                                  Id = p.ID,
                                  RoleName = r.ROLENAME
                              }).ToList();
            totalRecord = models.Count;
            return models.Skip((page - 1) * rows).Take(rows).ToList();
        }


        /// <summary>
        /// 返回角色id下所有的用户数据列表
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<UserModels> GetRoleUserList(string roleName)
        {
            var models = (from m in context.Set<GX_SYS_USER>().ToList()
                          from p in context.Set<GX_SYS_USERROLERELATE>().ToList()
                          from r in context.Set<GX_SYS_ROLE>().ToList()
                          where m.ID == p.USERID
                          where p.ROLEID == r.ID
                          where r.ROLENAME == roleName
                          select new UserModels
                          {
                              Id = m.ID,
                              UserName = m.USERNAME
                          }).Distinct().ToList();
            //totalRecord = models.Count;
            return models;
        }


        /// <summary>
        /// 根据用户id编号和角色名称获取用户角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public GX_SYS_ROLE FindRoleByUserIdAndRleName(string roleName, decimal userId)
        {
            var model = from p in context.Set<GX_SYS_USERROLERELATE>()
                        from r in context.Set<GX_SYS_ROLE>()
                        where p.ROLEID == r.ID
                        where string.IsNullOrEmpty(roleName) ? true : r.ROLENAME == roleName
                        where userId > 0 ? p.USERID == userId : true
                        select r;
            return model.FirstOrDefault();
        }
    }
}
