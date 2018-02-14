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
    public class GxRjxxBusiness
    {
        private UnitOfWork uw = UnitOfWork.Instance;

        public void Add(GX_RJXX gxRjxx)
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository(uw);
            gxRjxxRepository.AddEntity(gxRjxx);
        }

        public void Update(GX_RJXX gxRjxx)
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository(uw);
            gxRjxxRepository.UpdateEntity(gxRjxx);
        }

        public void Delete(GX_RJXX gxRjxx)
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository(uw);
            gxRjxxRepository.DeleteEntity(gxRjxx);
        }

        public int SaveChange()
        {
            return uw.Save();
        }

        public GX_RJXX Find(decimal id)
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository(uw);
            return gxRjxxRepository.FindRjxx(id);
        }

        public List<GX_RJXX> FindAllModels()
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository(uw);
            return gxRjxxRepository.FindAllRjxx().Where(t => t.RJZT == "0").ToList();
        }

        /// <summary>
        /// 返回菜单分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_RJXX> FindAllGXRJXX(decimal gcxmid, string rjbh, int pageIndex, int pageSize, out int totalRecord)
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository(uw);
            return gxRjxxRepository.FindAllGXRJXX(gcxmid, rjbh, pageIndex, pageSize, out totalRecord);
        }

        /// <summary>
        /// 添加人井图片信息
        /// </summary>
        /// <param name="photogallery"></param>
        /// <param name="unitOfWork"></param>
        public void AddRjxxPhoto(GX_PHOTOGALLERY photogallery)
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository();
            gxRjxxRepository.AddRjxxPhoto(photogallery, uw);
        }

        /// <summary>
        /// 删除人井图片信息
        /// </summary>
        /// <param name="photogallery"></param>
        /// <param name="unitOfWork"></param>
        public void DelRjxxPhoto(GX_PHOTOGALLERY photogallery)
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository();
            gxRjxxRepository.DelRjxxPhoto(photogallery, uw);
        }

        /// <summary>
        /// 删除人井图片信息
        /// </summary>
        /// <param name="photogallery"></param>
        /// <param name="unitOfWork"></param>
        public List<GX_PHOTOGALLERY> FindAllRjxxPhoto()
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository();
            return gxRjxxRepository.FindAllRjxxPhoto(uw);
        }

        /// <summary>
        /// 返回菜单分页数据列表
        /// </summary>
        /// <param name="ysxmid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_RJXX> FindAllGxrjxxByYsXmIdRjmx(decimal ysxmid, string rjmc, int pageIndex, int pageSize, out int totalRecord)
        {
            GxRjxxRepository gxRjxxRepository = new GxRjxxRepository(uw);
            return gxRjxxRepository.FindAllGxrjxxByYsXmIdRjmx(ysxmid, rjmc, pageIndex, pageSize, out totalRecord);
        }
    }
}
