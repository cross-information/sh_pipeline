using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gx.Models.ObjectBiz
{
    [Serializable()]
    public class UserEntity<T>
    {
        public T UserInfo;
        public DateTime TimeStamp;
        public string IP;

        public UserEntity<T> DeepClone()
        {
            //创建内存流     
            MemoryStream ms = new MemoryStream();
            //以二进制格式进行序列化          
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            //反序列化当前实例到一个object    
            ms.Seek(0, 0);
            object obj = bf.Deserialize(ms);
            //关闭内存流            
            ms.Close();
            return obj as UserEntity<T>;   
        }
    }
    public class SessionEntity
    {
        public string UserSession;
        public DateTime TimeStamp;
    }
}