using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models;

namespace Gx.DataAccess.Repository
{
    public class GxDictConfigRepository : RepositoryBase<GX_DICT_CONFIG>
    {
         public GxDictConfigRepository() : base()
        {

        }

        public GxDictConfigRepository(UnitOfWork uw)
        {
            base.context = uw.Context;
        }
        public void AddEntity(GX_DICT_CONFIG dictConfig)
        {
            this.Add(dictConfig);
        }

        public void UpdateEntity(GX_DICT_CONFIG dictConfig)
        {
            this.Update(dictConfig);
        }

        public void DeleteEntity(GX_DICT_CONFIG dictConfig)
        {
            this.Delete(dictConfig);
        }

        /// <summary>
        /// 根据ID获取人井信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GX_DICT_CONFIG FindRjxx(decimal id)
        {
            return this.FindAll().OrderByDescending(t => t.CREATETIME).FirstOrDefault(t => t.ID == id);
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_DICT_CONFIG FindRjxx()
        {
            return this.FindAll().OrderByDescending(t=>t.CREATETIME).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_DICT_CONFIG> FindAllRjxx()
        {
            return this.FindAll();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_DICT_CONFIG> FindAllRjxx(decimal id)
        {
            return this.FindAll().Where(t => t.PARENTID == id).ToList();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_DICT_CONFIG> FindAllRjxx(string name)
        {
            var SQLCommand = string.Format(@"select p.* from gx_dict_config p
                        left join gx_dict_config d on d.id = p.parentid
                        where d.name = '" + name + "'");
            return context.Database.SqlQuery<GX_DICT_CONFIG>(SQLCommand).ToList();
        }

        /// <summary>
        /// 返回分页数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_DICT_CONFIG> FindAllGxDictConfig(string name,decimal parentId,int pageIndex, int pageSize, out int totalRecord)
        {
            var allUser = this.FindAll().ToList();
            if (!string.IsNullOrEmpty(name))
                allUser = allUser.Where(t => t.NAME == name).ToList();
            if (parentId > 0)
                allUser = allUser.Where(t => t.PARENTID == parentId).ToList();
            totalRecord = allUser.Count;
            return allUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
