using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models;

namespace Gx.DataAccess.Repository
{
    public class GxYszlfileRepository : RepositoryBase<GX_YSZLFILE>
    {
        #region
        public GxYszlfileRepository()
            : base()
        {

        }

        public GxYszlfileRepository(UnitOfWork uw)
        {
            base.context = uw.Context;
        }
        #endregion

        public void AddEntity(GX_YSZLFILE yszlfile)
        {
            this.Add(yszlfile);
        }

        public void UpdateEntity(GX_YSZLFILE yszlfile)
        {
            this.Update(yszlfile);
        }

        public void DeleteEntity(GX_YSZLFILE yszlfile)
        {
            this.Delete(yszlfile);
        }

        public GX_YSZLFILE FindEntity(decimal id)
        {
            return this.Find(t => t.ID == id);
        }

        public List<GX_YSZLFILE> FindAllGxXlzx()
        {
            return this.FindAll();
        }

        public List<GX_YSZLFILE> FindAllGxYsxmFileList(decimal id)
        {
            return this.FindAll(t => t.XMYSXXID == id);
        }

        /// <summary>
        /// 返回所有工程项目下数据
        /// </summary>
        /// <returns></returns>
        public List<GX_YSZLFILE> FindAllGxXlzx(int type, decimal gcxmid, int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().Where(t => t.XMYSXXID == gcxmid && t.TYPE == type).ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList();
        }
    }
}
