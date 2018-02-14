using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models;

namespace Gx.DataAccess.Repository
{
    public class GxPhotogalleryRepository : RepositoryBase<GX_PHOTOGALLERY>
    {
        public GxPhotogalleryRepository() : base()
        {

        }

        public GxPhotogalleryRepository(UnitOfWork uw) {
            this.context = uw.Context;
        }

        public void AddEntity(GX_PHOTOGALLERY gxXmysxx)
        {
            this.Add(gxXmysxx);
        }

        public void UpdateEntity(GX_PHOTOGALLERY gxXmysxx)
        {
            this.Update(gxXmysxx);
        }

        public void DeleteEntity(GX_PHOTOGALLERY gxXmysxx)
        {
            this.Delete(gxXmysxx);
        }

        public GX_PHOTOGALLERY FindEntity(decimal id)
        {
            GX_PHOTOGALLERY gxxxEntity = new GX_PHOTOGALLERY();
            if (id > 0)
                gxxxEntity = this.Find(t => t.ID == id);
            return gxxxEntity;
        }

        public List<GX_PHOTOGALLERY> FindAllEntity()
        { 
            return this.FindAll();
        }

  

        /// <summary>
        /// 返回所有管线项目数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_PHOTOGALLERY> FindAllProject(string htbh, string gcbh, string gcmc, int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().ToList();
            //if (!string.IsNullOrEmpty(htbh))
            //    allUser = allUser.Where(t => t.HTBH == htbh).ToList();
            //if (!string.IsNullOrEmpty(gcbh))
            //    allUser = allUser.Where(t => t.GCBH == gcbh).ToList();
            //if (!string.IsNullOrEmpty(gcmc))
            //    allUser = allUser.Where(t => t.GCMC.Contains(gcmc)).ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<GX_PHOTOGALLERY> QueryProject( int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        } 
    }
}
