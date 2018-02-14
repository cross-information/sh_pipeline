using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models;

namespace Gx.DataAccess.Repository
{
    public class GxXlzxRepository : RepositoryBase<GX_XLZX>
    {
        #region
        public GxXlzxRepository()
            : base()
        {

        }

        public GxXlzxRepository(UnitOfWork uw)
        {
            base.context = uw.Context;
        }
        #endregion

        public void AddEntity(GX_XLZX gxXlzx)
        {
            this.Add(gxXlzx);
        }

        public void UpdateEntity(GX_XLZX gxXlzx)
        {
            this.Update(gxXlzx);
        }

        public void DeleteEntity(GX_XLZX gxXlzx)
        {
            this.Delete(gxXlzx);
        }

        public GX_XLZX FindEntity(decimal id)
        {
            return this.Find(t => t.ID == id);
        }

        public List<GX_XLZX> FindAllGxXlzx()
        {
            return this.FindAll();
        }

        /// <summary>
        /// 返回所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<GX_XLZX> FindAllGxXlzx(decimal roleid, int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().Where(t => t.ROLEID == roleid).ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList();
        }
    }
}
