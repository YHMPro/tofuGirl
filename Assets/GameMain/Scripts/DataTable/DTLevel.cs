//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-07-12 12:33:48.016
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
    /// 关卡配置表。
    /// </summary>
    public class DTLevel : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取关卡Id。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取女孩配置数据Id。
        /// </summary>
        public int GirlDataId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取木条配置数据Id。
        /// </summary>
        public int BattenDataId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取火箭配置Id。
        /// </summary>
        public int RocketDataId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取豆腐配置Id。
        /// </summary>
        public int TofuDataId
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
            GirlDataId = int.Parse(columnStrings[index++]);
            BattenDataId = int.Parse(columnStrings[index++]);
            RocketDataId = int.Parse(columnStrings[index++]);
            TofuDataId = int.Parse(columnStrings[index++]);

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
                    GirlDataId = binaryReader.Read7BitEncodedInt32();
                    BattenDataId = binaryReader.Read7BitEncodedInt32();
                    RocketDataId = binaryReader.Read7BitEncodedInt32();
                    TofuDataId = binaryReader.Read7BitEncodedInt32();
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
