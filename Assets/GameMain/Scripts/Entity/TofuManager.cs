using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
namespace Project.TofuGirl
{
    public class TofuCacheData :IReference
    {
        public int Id;
        public bool Prefect;
        public Vector3 Position;
        public Vector3 Rotation;
        
        public static TofuCacheData Create()
        {
            TofuCacheData tofuCacheData = ReferencePool.Acquire<TofuCacheData>();
            return tofuCacheData;
        }

        public void Clear()
        {
            
        }
    }
    // 数据 : Id、位置、旋转
    public class TofuManager
    {
        private static List<TofuCacheData> m_CacheLi = new List<TofuCacheData>();
        private static Stack<TofuCacheData> m_Caches = new Stack<TofuCacheData>();
        /// <summary>
        /// 最近连续完美的数量
        /// </summary>
        public static int LatelySeriesPerfectCount
        {
            get
            {
                return InquireLatelySeriesPerfectNum();
            }
        }
        /// <summary>
        /// 缓存数量
        /// </summary>
        public static int Count 
        {
            get
            {
                return m_Caches.Count;
            }
        }

        public static void AddCache(TofuCacheData cacheData)
        {          
            m_CacheLi.Add(cacheData);
        }

        public static TofuCacheData GetCache(int index)
        {
            return m_Caches.Pop();
        }
        

        private static int InquireLatelySeriesPerfectNum()
        {
            int num = 0;
            //排序一次  依照id  从小到大排序
            m_CacheLi.Sort((a, b) =>
            {
                if (a.Id > b.Id)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
            foreach(TofuCacheData data in m_CacheLi) 
            {
                m_Caches.Push(data);
            }
            m_CacheLi.Clear();

            foreach (TofuCacheData data in m_Caches)
            {
                Log.Warning("Id:{0}", data.Id);
                if (!data.Prefect)
                {
                    return num;
                }
                num++;
            }     
            return num;
        }

    }
}
