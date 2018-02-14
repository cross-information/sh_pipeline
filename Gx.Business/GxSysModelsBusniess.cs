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
    public class GxSysModelsBusniess
    {
        private UnitOfWork uw = UnitOfWork.Instance;


        public void Add(GX_SYS_MODELS gxSysModels)
        {
            GxSysModelsRepository gxSysModelsRepository = new GxSysModelsRepository(uw);
            gxSysModelsRepository.AddEntity(gxSysModels);
        }

        public void Update(GX_SYS_MODELS gxSysModels)
        {
            GxSysModelsRepository gxSysModelsRepository = new GxSysModelsRepository(uw);
            gxSysModelsRepository.UpdateEntity(gxSysModels);
        }

        public void Delete(GX_SYS_MODELS gxSysModels)
        {
            GxSysModelsRepository gxSysModelsRepository = new GxSysModelsRepository(uw);
            gxSysModelsRepository.DeleteEntity(gxSysModels);
        } 

        public int SaveChange()
        {
            return uw.Save();
        }

        public GX_SYS_MODELS Find(decimal id)
        {
            GxSysModelsRepository gxSysModelsRepository = new GxSysModelsRepository(uw);
            return gxSysModelsRepository.FindEntity(id);
        }

        public List<GX_SYS_MODELS> FindAllModels()
        {
            GxSysModelsRepository gxSysModelsRepository = new GxSysModelsRepository(uw);
            return gxSysModelsRepository.FindAllRjxx().Where(t => t.FLAG == 0).ToList();
        }

        /// <summary>
        /// 返回菜单分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_SYS_MODELS> FindAlMenuList(int pageIndex, int pageSize, out int totalRecord)
        {
            GxSysModelsRepository gxSysModelsRepository = new GxSysModelsRepository(uw);
            return gxSysModelsRepository.FindAllGxSysModels(pageIndex, pageSize, out totalRecord);
        }
    }
}
