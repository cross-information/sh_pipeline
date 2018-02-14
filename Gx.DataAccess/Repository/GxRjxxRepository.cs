using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.DataAccess.Repository
{
    public class GxRjxxRepository:RepositoryBase<GX_RJXX>
    {
        public GxRjxxRepository() : base()
        {

        }

        public GxRjxxRepository(UnitOfWork uw)
        {
            base.context = uw.Context;
        }
        public void AddEntity(GX_RJXX rjxx)
        {
            this.Add(rjxx);
        }

        public void UpdateEntity(GX_RJXX rjxx)
        {
            this.Update(rjxx);
        }

        public void DeleteEntity(GX_RJXX rjxx)
        {
            this.Delete(rjxx);
        }

        public GX_RJXX FindEntity(GX_RJXX rjxx)
        {
            GX_RJXX gxxxEntity = new GX_RJXX();
            if (rjxx.ID > 0)
                gxxxEntity = this.Find(t => t.ID == rjxx.ID);
            return gxxxEntity;
        }

        /// <summary>
        /// 根据ID获取人井信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GX_RJXX FindRjxx(decimal id)
        {
            return this.FindAll().OrderByDescending(t => t.CREATETIME).FirstOrDefault(t => t.ID == id);
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_RJXX FindRjxx()
        {
            return this.FindAll().OrderByDescending(t=>t.CREATETIME).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_RJXX> FindAllRjxx()
        {
            return this.FindAll();
        }

        /// <summary>
        /// 返回分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_RJXX> FindAllGXRJXX(decimal gcxmid, string rjbh, int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().Where(p => p.XMYSXXID == gcxmid).ToList();
            if (!string.IsNullOrEmpty(rjbh))
                allUser = allUser.Where(t => t.RJBH.Contains(rjbh)).ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 返回分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_RJXX> FindAllGxrjxxByYsXmIdRjmx(decimal ysxmid, string rjmc, int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().Where(p => p.XMYSXXID == ysxmid).ToList();
            if (!string.IsNullOrEmpty(rjmc))
                allUser = allUser.Where(t => t.RJMC.Contains(rjmc)).ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 添加人井图片信息
        /// </summary>
        /// <param name="photogallery"></param>
        /// <param name="unitOfWork"></param>
        public void AddRjxxPhoto(GX_PHOTOGALLERY photogallery,UnitOfWork unitOfWork)
        {
            RepositoryBase<GX_PHOTOGALLERY> repositoryBase = new RepositoryBase<GX_PHOTOGALLERY>(unitOfWork);
            repositoryBase.Add(photogallery);
        }

        /// <summary>
        /// 删除人井图片信息
        /// </summary>
        /// <param name="photogallery"></param>
        /// <param name="unitOfWork"></param>
        public void DelRjxxPhoto(GX_PHOTOGALLERY photogallery, UnitOfWork unitOfWork)
        {
            RepositoryBase<GX_PHOTOGALLERY> repositoryBase = new RepositoryBase<GX_PHOTOGALLERY>(unitOfWork);
            repositoryBase.Delete(photogallery);
        }

        /// <summary>
        /// 获取人井图片信息
        /// </summary>
        /// <param name="photogallery"></param>
        /// <param name="unitOfWork"></param>
        public List<GX_PHOTOGALLERY> FindAllRjxxPhoto(UnitOfWork unitOfWork)
        {
            RepositoryBase<GX_PHOTOGALLERY> repositoryBase = new RepositoryBase<GX_PHOTOGALLERY>(unitOfWork);
            return repositoryBase.FindAll();
        }
    }
}
