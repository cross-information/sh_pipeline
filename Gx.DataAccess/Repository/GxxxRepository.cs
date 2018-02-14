using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.DataAccess.Repository
{
    public class GxxxRepository : RepositoryBase<GX_GXXX>
    {
        public GxxxRepository() : base()
        {

        }

        public GxxxRepository(UnitOfWork uw)
        {
            base.context = uw.Context;
        }
        public void AddEntity(GX_GXXX gxxx)
        {
            this.Add(gxxx);
        }

        public void UpdateEntity(GX_GXXX gxxx)
        {
            this.Update(gxxx);
        }

        public void DeleteEntity(GX_GXXX gxxx)
        {
            this.Delete(gxxx);
        }

        public GX_GXXX FindEntity(GX_GXXX gxxx)
        {
            GX_GXXX gxxxEntity = new GX_GXXX();
            if (gxxx.ID > 0)
                gxxxEntity = this.Find(t => t.ID == gxxx.ID);
            return gxxxEntity;
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_GXXX> FindAllGxxx()
        {
            return this.FindAll();
        }

        /// <summary>
        /// 添加管孔信息
        /// </summary>
        /// <param name="gkxx"></param>
        public void AddGxGkxx(GX_GKXX gkxx)
        {
            this.Add(gkxx);
        }

        /// <summary>
        /// 修改管孔信息
        /// </summary>
        /// <param name="gkxx"></param>
        public void UpdateGkxx(GX_GKXX gkxx) {
            this.Update(gkxx);
        }

        /// <summary>
        /// 删除管孔信息
        /// </summary>
        /// <param name="gkxx"></param>
        public void DeleteGkxx(GX_GKXX gkxx) {
            this.Delete(gkxx);
        }

        /// <summary>
        /// 查找管孔实体信息
        /// </summary>
        /// <param name="gkxx"></param>
        /// <returns></returns>
        public GX_GKXX FindGkxx(GX_GKXX gkxx) {
            return this.Find<GX_GKXX>(t => t.ID == gkxx.ID);
        }

        /// <summary>
        /// 返回管线信息下所有的管孔信息
        /// </summary>
        /// <param name="gxxxid"></param>
        /// <returns></returns>
        public List<GX_GKXX> FindAllGkxx()
        {
            return context.Set<GX_GKXX>().ToList();
        }

        /// <summary>
        /// 返回管线信息下所有的管孔信息
        /// </summary>
        /// <param name="gxxxid"></param>
        /// <returns></returns>
        public List<GX_GKXX> FindAllGkxx(decimal? gxxxid) {
            return this.FindAll<GX_GKXX>(t => t.GXXXID == gxxxid).ToList();
        }

        /// <summary>
        /// 返回分页数据列表
        /// </summary>
        /// <param name="xmgcid"></param>
        /// <param name="gcbh"></param>
        /// <param name="stqk"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_GXXX> FindAllGxDictConfig(decimal xmgcid, string gcbh, string stqk, int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().Where(t => t.XMYSXXID == xmgcid).ToList();
            if (!string.IsNullOrEmpty(gcbh))
                allUser = allUser.Where(t => t.GCBH == gcbh).ToList();
            if (!string.IsNullOrEmpty(stqk))
                allUser = allUser.Where(t => t.STQK == stqk).ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
