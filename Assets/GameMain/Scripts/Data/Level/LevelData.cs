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
        /// <summary>
        /// 女孩数据
        /// </summary>
        public GirlData GData { get; private set; }
        /// <summary>
        /// 木条数据
        /// </summary>
        public BattenData BData { get; private set; }

        public LevelData(DTLevel dTLevel, GirlData girlData, BattenData battenData)
        {
            m_Level = dTLevel;
            GData = girlData;
            BData = battenData;
        }
    }
}
