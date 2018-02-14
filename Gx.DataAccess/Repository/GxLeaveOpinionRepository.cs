using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.Models;

namespace Gx.DataAccess.Repository
{
    public class GxLeaveOpinionRepository : RepositoryBase<GX_LEAVEOPINION>
    {
        public GxLeaveOpinionRepository() : base() { }
        public GxLeaveOpinionRepository(UnitOfWork obj) { this.context = obj.Context; }

        public void AddEntity(GX_LEAVEOPINION models)
        {
            this.Add(models);
        }

        public void UpdateEntity(GX_LEAVEOPINION models)
        {
            this.Update(models);
        }

        public void DeleteEntity(GX_LEAVEOPINION models)
        {
            this.Delete(models);
        }

        public GX_LEAVEOPINION FindEntity(decimal id)
        {
            return this.Find(t => t.ID == id); ;
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_LEAVEOPINION FindRjxx()
        {
            return this.FindAll().OrderByDescending(t => t.ID).FirstOrDefault();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_LEAVEOPINION> FindAllLeaveoption()
        {
            return this.FindAll();
        }
        
        /// <summary>
        /// 根据应用id 获取流程信息
        /// </summary>
        /// <param name="appInstanceId"></param>
        /// <returns></returns>
        public GX_LEAVEOPINION FindAllLeaveoption(decimal appInstanceId)
        {
            return this.FindAll(t => t.APPINSTANCEID == appInstanceId).FirstOrDefault();
        }
    }
}
