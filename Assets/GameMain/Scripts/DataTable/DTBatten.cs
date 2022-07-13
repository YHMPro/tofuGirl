//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-07-13 15:34:15.750
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Project.TofuGirl
{
    /// <summary>
    /// 木条配置表。
    /// </summary>
    public class DTBatten : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取木条配置Id。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取基础移动速度。
        /// </summary>
        public float SpeedBase
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取火箭附加的速度。
        /// </summary>
        public float RocketAppendSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取左方向概率。
        /// </summary>
        public int LeftDir
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取右方向概率。
        /// </summary>
        public int RightDir
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础创建时间(s)。
        /// </summary>
        public float CreateTimeBase
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取火箭影响下的创建时间。
        /// </summary>
        public float RocketCreateTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取木条之间的创建间隔。
        /// </summary>
        public float Interval
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            SpeedBase = float.Parse(columnStrings[index++]);
            RocketAppendSpeed = float.Parse(columnStrings[index++]);
            LeftDir = int.Parse(columnStrings[index++]);
            RightDir = int.Parse(columnStrings[index++]);
            CreateTimeBase = float.Parse(columnStrings[index++]);
            RocketCreateTime = float.Parse(columnStrings[index++]);
            Interval = float.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    SpeedBase = binaryReader.ReadSingle();
                    RocketAppendSpeed = binaryReader.ReadSingle();
                    LeftDir = binaryReader.Read7BitEncodedInt32();
                    RightDir = binaryReader.Read7BitEncodedInt32();
                    CreateTimeBase = binaryReader.ReadSingle();
                    RocketCreateTime = binaryReader.ReadSingle();
                    Interval = binaryReader.ReadSingle();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
