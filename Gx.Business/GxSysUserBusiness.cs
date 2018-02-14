using System.Web;
using System.Web.Security;
using Gx.CommonUnits;
using Gx.CommonUnits.Tools;
using Gx.DataAccess;
using Gx.DataAccess.Repository;
using Gx.Models;
using Gx.Models.ObjectBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.Business
{
    public class GxSysUserBusiness
    {
        UnitOfWork uw = UnitOfWork.Instance;
        public void Add(GX_SYS_USER gxSysUser) {
            GxSysUserRepository gxSysUserRepository = new GxSysUserRepository(uw);
            gxSysUserRepository.AddEntity(gxSysUser);
        }

        public void Update(GX_SYS_USER gxSysUser)
        {
            GxSysUserRepository gxSysUserRepository = new GxSysUserRepository(uw);
            gxSysUserRepository.UpdateEntity(gxSysUser);
        }

        public void Delete(GX_SYS_USER gxSysUser)
        {
            GxSysUserRepository gxSysUserRepository = new GxSysUserRepository(uw);
            gxSysUserRepository.DeleteEntity(gxSysUser);
        }

        public GX_SYS_USER FindGxSysUser(GX_SYS_USER gxSysUser)
        {
            GxSysUserRepository gxSysUserRepository = new GxSysUserRepository(uw);
            return gxSysUserRepository.FindEntity(gxSysUser);
        }

        public List<GX_SYS_USER> FindGxSysUser()
        {
            GxSysUserRepository gxSysUserRepository = new GxSysUserRepository(uw);
            return gxSysUserRepository.FindAllGxSysUser();
        }

        public GX_SYS_USER FindGxSysUser(decimal id)
        {
            GxSysUserRepository gxSysUserRepository = new GxSysUserRepository(uw);
            return gxSysUserRepository.FindEntity(id);
        }

        public List<GX_SYS_USER> FindAllGxSysUser(int pageIndex, int pageSize, out int totalRecord)
        {
            GxSysUserRepository gxSysUserRepository = new GxSysUserRepository(uw);
            return gxSysUserRepository.FindAllGxSysUser(pageIndex, pageSize, out totalRecord);
        }

        /// <summary>
        /// 返回用户登录信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public OperationResult FindGxSysUser(string userId, string pass)
        {
            if (!string.IsNullOrEmpty(pass))
            {
                //pass = Encrypt.MD5(Encrypt.MD5(pass + Encrypt.DesKey));
            }
            GxSysUserRepository gxSysUserRepository = new GxSysUserRepository(uw);
            var models = gxSysUserRepository.FindAllGxSysUser().Find(t => t.USERID == userId && t.USERPWD == pass && t.USERSTATUS == 10);
            if (models != null)
            {
                UserInfo userinfo = new UserInfo();
                userinfo.Id = models.ID;
                userinfo.UserId = models.USERID;
                userinfo.UserName = models.USERNAME;
                userinfo.Mobile = models.USERMOBILE;
                UserEntity<UserInfo> userEntity = new UserEntity<UserInfo>();
                userEntity.IP = HttpContext.Current.Request.UserHostAddress;
                userEntity.UserInfo = userinfo;
                userEntity.TimeStamp = DateTime.Now;
                //userinfo.RoleId=models.
                string guid = Guid.NewGuid().ToString("N");
                UserFormsPrincipal<UserEntity<UserInfo>>.SignIn(guid, userEntity, FormsAuthentication.Timeout);
                HttpCookie cookieForBC = new HttpCookie("SSOUserCookie", guid);
                HttpContext.Current.Response.Cookies.Add(cookieForBC);
                HttpCookie cookieForIP = new HttpCookie("SSOUserIP", userEntity.IP);
                HttpContext.Current.Response.Cookies.Add(cookieForIP);
                return new OperationResult(OperationResultType.Success, "登录成功,正在跳转！");
            }
            else
            {
                var model = gxSysUserRepository.FindAllGxSysUser().Find(t => t.USERID == userId);
                if (model != null)
                    return new OperationResult(OperationResultType.QueryNull, "请输入正确的登录密码！");
                else
                    return new OperationResult(OperationResultType.QueryNull, "未找到当前登录用户信息！");
            }
        }

        /// <summary>
        /// 根据角色编号获取用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<GX_SYS_USER> FindAllUserByRoleId(decimal roleId)
        {
            GxSysUserRepository gxSysUserRepository = new GxSysUserRepository(uw);
            return gxSysUserRepository.FindAllUserByRoleId(roleId);
        }

        /// <summary>
        /// 执行数据操作保存
        /// </summary>
        /// <returns></returns>
        public int SaveChage()
        {
            return uw.Save();
        }
    }
}
