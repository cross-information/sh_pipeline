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
    public class GxYsxmxxBusiness
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public void AddEntity(GX_XMYSXX gxXmysxx)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(unitOfWork);
            gxXmysxxRepository.AddEntity(gxXmysxx);
        }

        public void UpdateEntity(GX_XMYSXX gxXmysxx)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(unitOfWork);
            gxXmysxxRepository.UpdateEntity(gxXmysxx);
        }

        public void DeleteEntity(GX_XMYSXX gxXmysxx)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(unitOfWork);
            gxXmysxxRepository.DeleteEntity(gxXmysxx);
        }

        public GX_XMYSXX FindEntity(decimal id)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(unitOfWork);
            return gxXmysxxRepository.FindEntity(id);
        }

        public List<GX_XMYSXX> FindEntity()
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(unitOfWork);
            return gxXmysxxRepository.FindAllEntity();
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_XMYSXX FindGxXmysxx()
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(unitOfWork);
            return gxXmysxxRepository.FindRjxx();
        }

        /// <summary>
        /// 返回所有管线项目数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_XMYSXX> FindAllProject(string htbh, string gcbh, string gcmc, int pageIndex, int pageSize, out int totalRecord)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(unitOfWork);
            return gxXmysxxRepository.FindAllProject(htbh, gcbh, gcmc, pageIndex, pageSize, out totalRecord);
        }

        public List<GX_XMYSXX> QueryProject(int pageIndex, int pageSize, out int totalRecord)
        {
            GxXmysxxRepository gxXmysxxRepository = new GxXmysxxRepository(unitOfWork);
            return gxXmysxxRepository.QueryProject(pageIndex, pageSize, out totalRecord);
        }
    }
}
