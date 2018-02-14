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
    public class GxXlzxBusiness
    {
        private UnitOfWork uw = UnitOfWork.Instance;


        public void AddEntity(GX_XLZX gxXlzx)
        {
            GxXlzxRepository gxXlzxRepository = new GxXlzxRepository(uw);
            gxXlzxRepository.AddEntity(gxXlzx);
        }

        public void UpdateEntity(GX_XLZX gxXlzx)
        {
            GxXlzxRepository gxXlzxRepository = new GxXlzxRepository(uw);
            gxXlzxRepository.UpdateEntity(gxXlzx);
        }

        public void DeleteEntity(GX_XLZX gxXlzx)
        {
            GxXlzxRepository gxXlzxRepository = new GxXlzxRepository(uw);
            gxXlzxRepository.DeleteEntity(gxXlzx);
        }

        public int SaveChange()
        {
            return uw.Save();
        }

        public GX_XLZX Find(decimal id)
        {
            GxXlzxRepository gxXlzxRepository = new GxXlzxRepository(uw);
            return gxXlzxRepository.FindEntity(id);
        }

        public List<GX_XLZX> FindAllModels()
        {
            GxXlzxRepository gxXlzxRepository = new GxXlzxRepository(uw);
            return gxXlzxRepository.FindAllGxXlzx().Where(t => t.STATUS == 0).ToList();
        }

        /// <summary>
        /// 返回菜单分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_XLZX> FindAlXlzxList(decimal roleid, int pageIndex, int pageSize, out int totalRecord)
        {
            GxXlzxRepository gxXlzxRepository = new GxXlzxRepository(uw);
            return gxXlzxRepository.FindAllGxXlzx(roleid, pageIndex, pageSize, out totalRecord);
        }
    }
}
