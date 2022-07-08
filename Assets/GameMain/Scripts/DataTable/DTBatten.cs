//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-07-07 17:25:29.500
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
        /// 获取叠加速度最小值。
        /// </summary>
        public float SpeedMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取叠加速度最大值。
        /// </summary>
        public float SpeedMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础方向概率(默认向左)。
        /// </summary>
        public int DirBase
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取叠加方向概率最小值。
        /// </summary>
        public int DirMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取叠加概率最大值。
        /// </summary>
        public int DirMax
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
        /// 获取叠加创建时间最小值。
        /// </summary>
        public float CreateTimeMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取叠加创建时间最大值。
        /// </summary>
        public float CreateTimeMax
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
            SpeedMin = float.Parse(columnStrings[index++]);
            SpeedMax = float.Parse(columnStrings[index++]);
            DirBase = int.Parse(columnStrings[index++]);
            DirMin = int.Parse(columnStrings[index++]);
            DirMax = int.Parse(columnStrings[index++]);
            CreateTimeBase = float.Parse(columnStrings[index++]);
            CreateTimeMin = float.Parse(columnStrings[index++]);
            CreateTimeMax = float.Parse(columnStrings[index++]);

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
                    SpeedMin = binaryReader.ReadSingle();
                    SpeedMax = binaryReader.ReadSingle();
                    DirBase = binaryReader.Read7BitEncodedInt32();
                    DirMin = binaryReader.Read7BitEncodedInt32();
                    DirMax = binaryReader.Read7BitEncodedInt32();
                    CreateTimeBase = binaryReader.ReadSingle();
                    CreateTimeMin = binaryReader.ReadSingle();
                    CreateTimeMax = binaryReader.ReadSingle();
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
