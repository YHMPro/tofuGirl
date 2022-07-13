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
        /// <summary>
        /// 豆腐缓存数据队列
        /// </summary>
        private static Queue<TofuCacheData> m_TofuCacheDataQu = new Queue<TofuCacheData>();
        /// <summary>
        /// 连续完美的个数
        /// </summary>
        public static int PrefectCount
        {
            get
            {
                int prefectNum = 0;
                foreach (TofuCacheData data in m_TofuCacheDataQu)
                {
                    if(!data.Prefect)
                    {
                        return prefectNum;
                    }
                    prefectNum++;
                }
                return prefectNum;
            }
        }
        /// <summary>
        /// 豆腐缓存数据个数
        /// </summary>
        public static int Count => m_TofuCacheDataQu.Count;
        /// <summary>
        /// 添加豆腐缓存数据
        /// </summary>
        /// <param name="tofuCacheData"></param>
        public static void Enqueue(TofuCacheData tofuCacheData)
        {
            
            m_TofuCacheDataQu.Enqueue(tofuCacheData);
        }
        /// <summary>
        /// 取出首位豆腐缓存数据
        /// </summary>
        /// <returns></returns>
        public static TofuCacheData Dequeue()
        {
            return m_TofuCacheDataQu.Dequeue();
        }
        /// <summary>
        /// 查看首位豆腐缓存数据
        /// </summary>
        /// <returns></returns>
        public static TofuCacheData Peek()
        {
            return m_TofuCacheDataQu.Peek();
        }
    }
}
