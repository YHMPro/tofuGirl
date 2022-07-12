//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-07-12 12:33:48.021
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
    /// 豆腐配置表。
    /// </summary>
    public class DTTofu : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取豆腐配置Id。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取普通豆腐概率。
        /// </summary>
        public int PuTong
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具豆腐概率。
        /// </summary>
        public int DaoJu
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取特殊豆腐触发最小值。
        /// </summary>
        public int TeShuMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取特殊豆腐触发临界值。
        /// </summary>
        public int TeShuMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取金色特殊豆腐触发最小值。
        /// </summary>
        public int JinSheTeShuMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取金色特殊豆腐触发临界值。
        /// </summary>
        public int JinSheTeShuMax
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
            PuTong = int.Parse(columnStrings[index++]);
            DaoJu = int.Parse(columnStrings[index++]);
            TeShuMin = int.Parse(columnStrings[index++]);
            TeShuMax = int.Parse(columnStrings[index++]);
            JinSheTeShuMin = int.Parse(columnStrings[index++]);
            JinSheTeShuMax = int.Parse(columnStrings[index++]);

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
                    PuTong = binaryReader.Read7BitEncodedInt32();
                    DaoJu = binaryReader.Read7BitEncodedInt32();
                    TeShuMin = binaryReader.Read7BitEncodedInt32();
                    TeShuMax = binaryReader.Read7BitEncodedInt32();
                    JinSheTeShuMin = binaryReader.Read7BitEncodedInt32();
                    JinSheTeShuMax = binaryReader.Read7BitEncodedInt32();
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
