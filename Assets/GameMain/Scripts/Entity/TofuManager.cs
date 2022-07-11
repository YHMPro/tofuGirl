using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.TofuGirl
{
    public class TofuCacheData
    {
        public int Id;
        public bool Prefect;
        public Vector3 Position;
        public Vector3 Rotation;
        
    }
    // 数据 : Id、位置、旋转、Spine状态(可优化成一张Sprite)
    public class TofuManager : MonoBehaviour
    {
        private static Stack<TofuCacheData> m_Caches = new Stack<TofuCacheData>();
        /// <summary>
        /// 最近连续完美的数量
        /// </summary>
        public int LatelySeriesPerfectCount
        {
            get
            {
                return InquireLatelySeriesPerfectNum();
            }
        }
        /// <summary>
        /// 缓存数量
        /// </summary>
        public int Count 
        {
            get
            {
                return m_Caches.Count;
            }
        }

        public static void AddCache(TofuCacheData cacheData)
        {          
            m_Caches.Push(cacheData);
        }

        public static TofuCacheData GetCache()
        {
            return m_Caches.Pop();
        }
        

        private static int InquireLatelySeriesPerfectNum()
        {
            int num = 0;
            foreach(var item in m_Caches)
            {
                if(!item.Prefect)
                {
                    return num;
                }
                num++;
            }
            return num;
        }

    }
}
