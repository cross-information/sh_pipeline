using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models;

namespace Gx.DataAccess.Repository
{
    public class GxXmysxxRepository:RepositoryBase<GX_XMYSXX>
    {
        public GxXmysxxRepository() : base()
        {

        }

        public GxXmysxxRepository(UnitOfWork uw) {
            this.context = uw.Context;
        }

        public void AddEntity(GX_XMYSXX gxXmysxx)
        {
            this.Add(gxXmysxx);
        }

        public void UpdateEntity(GX_XMYSXX gxXmysxx)
        {
            this.Update(gxXmysxx);
        }

        public void DeleteEntity(GX_XMYSXX gxXmysxx)
        {
            this.Delete(gxXmysxx);
        }

        public GX_XMYSXX FindEntity(decimal id)
        {
            GX_XMYSXX gxxxEntity = new GX_XMYSXX();
            if (id > 0)
                gxxxEntity = this.Find(t => t.ID == id);
            return gxxxEntity;
        }

        public List<GX_XMYSXX> FindAllEntity()
        { 
            return this.FindAll();
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_XMYSXX FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.CREATETIME).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线项目数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_XMYSXX> FindAllProject(string htbh, string gcbh, string gcmc, int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().ToList();
            if (!string.IsNullOrEmpty(htbh))
                allUser = allUser.Where(t => t.HTBH == htbh).ToList();
            if (!string.IsNullOrEmpty(gcbh))
                allUser = allUser.Where(t => t.GCBH == gcbh).ToList();
            if (!string.IsNullOrEmpty(gcmc))
                allUser = allUser.Where(t => t.GCMC.Contains(gcmc)).ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<GX_XMYSXX> QueryProject( int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        } 
    }
}
