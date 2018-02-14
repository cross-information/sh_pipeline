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
    public class GxYszlfileBusiness
    {
        private UnitOfWork uw = UnitOfWork.Instance;


        public void AddEntity(GX_YSZLFILE yszlfile)
        {
            GxYszlfileRepository gxXlzxRepository = new GxYszlfileRepository(uw);
            gxXlzxRepository.AddEntity(yszlfile);
        }

        public void UpdateEntity(GX_YSZLFILE yszlfile)
        {
            GxYszlfileRepository gxXlzxRepository = new GxYszlfileRepository(uw);
            gxXlzxRepository.UpdateEntity(yszlfile);
        }

        public void DeleteEntity(GX_YSZLFILE yszlfile)
        {
            GxYszlfileRepository gxXlzxRepository = new GxYszlfileRepository(uw);
            gxXlzxRepository.DeleteEntity(yszlfile);
        }

        public int SaveChange()
        {
            return uw.Save();
        }

        public GX_YSZLFILE Find(decimal id)
        {
            GxYszlfileRepository gxXlzxRepository = new GxYszlfileRepository(uw);
            return gxXlzxRepository.FindEntity(id);
        }

        public List<GX_YSZLFILE> FindAllGxYsxmFileList(decimal id)
        {
            GxYszlfileRepository gxXlzxRepository = new GxYszlfileRepository(uw);
            return gxXlzxRepository.FindAllGxYsxmFileList(id);
        }

        public List<GX_YSZLFILE> FindAllModels()
        {
            GxYszlfileRepository gxXlzxRepository = new GxYszlfileRepository(uw);
            return gxXlzxRepository.FindAllGxXlzx().Where(t => t.FILEZT == "0").ToList();
        }

        /// <summary>
        /// 返回菜单分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_YSZLFILE> FindAlXlzxList(int type, decimal gcxmid, int pageIndex, int pageSize, out int totalRecord)
        {
            GxYszlfileRepository gxXlzxRepository = new GxYszlfileRepository(uw);
            return gxXlzxRepository.FindAllGxXlzx(type, gcxmid, pageIndex, pageSize, out totalRecord);
        }
    }
}
