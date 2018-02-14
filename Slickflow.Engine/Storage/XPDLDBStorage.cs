using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Threading;
using Slickflow.Engine.Core;
using Slickflow.Engine.Common;
using Slickflow.Engine.Utility;
using Slickflow.Data;
using Slickflow.Engine.Xpdl;
using Slickflow.Engine.Business.Entity;
using Slickflow.Engine.Business.Manager;
using Slickflow.Engine.Core.Result;
using Slickflow.Engine.Core.Event;
using Slickflow.Engine.Parser;

namespace Slickflow.Engine.Storage
{
    /// <summary>
    /// 数据库实现
    /// </summary>
    public class XPDLDBStorage : IXPDLStorage
    {
        /// <summary>
        /// 读取数据库中的Content
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public XmlDocument Read(ProcessEntity entity)
        {
            XmlDocument xmlDoc = null;
            if (entity != null)
            { 
                xmlDoc = new XmlDocument();
                xmlDoc.Load(entity.XmlContent);
            }
            return xmlDoc;
        }

        /// <summary>
        /// XML文件数据库文件储存
        /// </summary>
        /// <param name="filePath">文件存储路径</param>
        /// <param name="xmlDoc">XML文档</param>
        public void Save(string filePath, XmlDocument xmlDoc)
        {
            //DoNothing
        }
    }
}
