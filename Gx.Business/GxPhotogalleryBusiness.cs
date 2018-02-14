using Gx.DataAccess;
using Gx.DataAccess.Repository;
using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.Business
{
    public class GxPhotogalleryBusiness
    {
        private UnitOfWork uw = UnitOfWork.Instance;

        public void Add(GX_PHOTOGALLERY gxXmysxx)
        {
            GxPhotogalleryRepository gxXmysxxRepository = new GxPhotogalleryRepository(uw);
            gxXmysxxRepository.AddEntity(gxXmysxx);
        }

        public void Update(GX_PHOTOGALLERY gxXmysxx)
        {
            GxPhotogalleryRepository gxXmysxxRepository = new GxPhotogalleryRepository(uw);
            gxXmysxxRepository.UpdateEntity(gxXmysxx);
        }

        public void Delete(GX_PHOTOGALLERY gxXmysxx)
        {
            GxPhotogalleryRepository gxXmysxxRepository = new GxPhotogalleryRepository(uw);
            gxXmysxxRepository.DeleteEntity(gxXmysxx);
        }

        public int SaveChange()
        {
            return uw.Save();
        }

        public GX_PHOTOGALLERY Find(decimal id)
        {
            GxPhotogalleryRepository gxXmysxxRepository = new GxPhotogalleryRepository(uw);
            return gxXmysxxRepository.FindEntity(id);
        }

        public List<GX_PHOTOGALLERY> FindAll(decimal id)
        {
            GxPhotogalleryRepository gxXmysxxRepository = new GxPhotogalleryRepository(uw);
            return gxXmysxxRepository.FindAll(t => t.PARENTID == id);
        }

        /// <summary>
        /// 返回菜单分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_PHOTOGALLERY> FindAllGxProject(string htbh, string gcbh, string gcmc, int pageIndex, int pageSize, out int totalRecord)
        {
            GxPhotogalleryRepository gxSysRoleRepository = new GxPhotogalleryRepository(uw);
            return gxSysRoleRepository.FindAllProject(htbh, gcbh, gcmc, pageIndex, pageSize, out totalRecord);
        }
    }
}
