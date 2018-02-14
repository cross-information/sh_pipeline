using Gx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.DataAccess.Repository
{
    public class GxSysModelsRepository:RepositoryBase<GX_SYS_MODELS>
    {
        public GxSysModelsRepository() : base() { }
        public GxSysModelsRepository(UnitOfWork obj) { this.context = obj.Context; }

        public void AddEntity(GX_SYS_MODELS models)
        {
            this.Add(models);
        }

        public void UpdateEntity(GX_SYS_MODELS models)
        {
            this.Update(models);
        }

        public void DeleteEntity(GX_SYS_MODELS models)
        {
            this.Delete(models);
        }

        public GX_SYS_MODELS FindEntity(GX_SYS_MODELS models)
        {
            GX_SYS_MODELS modelsEntity = new GX_SYS_MODELS();
            if (models.ID > 0)
                modelsEntity = this.Find(t => t.ID == models.ID);
            return modelsEntity;
        }

        public GX_SYS_MODELS FindEntity(decimal id)
        {
            GX_SYS_MODELS modelsEntity = new GX_SYS_MODELS();
            if (id > 0)
                modelsEntity = this.Find(t => t.ID == id);
            return modelsEntity;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_SYS_MODELS FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.CREATETIME).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_SYS_MODELS> FindAllRjxx()
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
        public List<GX_SYS_MODELS> FindAllGxSysModels(int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
