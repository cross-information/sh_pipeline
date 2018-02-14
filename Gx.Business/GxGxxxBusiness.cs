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
    public class GxGxxxBusiness
    {
        private UnitOfWork uw = UnitOfWork.Instance;

        public void AddEntity(GX_GXXX gxGxxx)
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            gxRjxxRepository.AddEntity(gxGxxx);
        }

        public void UpdateEntity(GX_GXXX gxGxxx)
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            gxRjxxRepository.UpdateEntity(gxGxxx);
        }

        public void DeleteEntity(GX_GXXX gxGxxx)
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            gxRjxxRepository.DeleteEntity(gxGxxx);
        }

        public int SaveChange()
        {
            return uw.Save();
        }

        public GX_GXXX Find(decimal id)
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            return gxRjxxRepository.Find(id);
        }

        public List<GX_GXXX> FindAllModels()
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            return gxRjxxRepository.FindAllGxxx();
        }

        //管孔信息维护

        public void AddGkxx(GX_GKXX gxGkxx)
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            gxRjxxRepository.AddGxGkxx(gxGkxx);
        }

        public void UpdateGkxx(GX_GKXX gxGkxx)
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            gxRjxxRepository.UpdateGkxx(gxGkxx);
        }

        public void DeleteGkxx(GX_GKXX gxGkxx)
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            gxRjxxRepository.DeleteGkxx(gxGkxx);
        }

        public GX_GKXX FindGkxx(decimal id)
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            return gxRjxxRepository.FindAllGkxx().FirstOrDefault(t => t.ID == id);
        }

        public List<GX_GKXX> FindAllGkxxModels()
        {
            GxxxRepository gxRjxxRepository = new GxxxRepository(uw);
            return gxRjxxRepository.FindAllGkxx();
        }

        /// <summary>
        /// 返回分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_GXXX> FindAllGxxxList(decimal xmgcid, string gxbh, string stqk, int pageIndex, int pageSize, out int totalRecord)
        {
            GxxxRepository gxSysRoleRepository = new GxxxRepository(uw);
            return gxSysRoleRepository.FindAllGxDictConfig(xmgcid, gxbh, stqk, pageIndex, pageSize, out totalRecord);
        }
    }
}
