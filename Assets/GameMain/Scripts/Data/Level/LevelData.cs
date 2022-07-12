using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.TofuGirl.Data
{
    public sealed class LevelData 
    {
        private DTLevel m_Level;

        public int Id => m_Level.Id;

        public int GirlDataId => m_Level.GirlDataId;

        public int BattenDataId => m_Level.BattenDataId;

        public int RocketDataId => m_Level.RocketDataId;

        public int TofuDataId => m_Level.TofuDataId;
        /// <summary>
        /// 女孩数据
        /// </summary>
        public GirlData GData { get; private set; }
        /// <summary>
        /// 木条数据
        /// </summary>
        public BattenData BData { get; private set; }
        /// <summary>
        /// 火箭数据
        /// </summary>
        public RocketData RData { get; private set; }
        /// <summary>
        /// 豆腐数据
        /// </summary>
        public TofuData TDData { get; private set; }

        public LevelData(DTLevel dTLevel, GirlData girlData, BattenData battenData ,RocketData rocketData, TofuData tofuData)
        {
            m_Level = dTLevel;
            GData = girlData;
            BData = battenData;
            RData = rocketData;
            TDData = tofuData;
        }
    }
}
