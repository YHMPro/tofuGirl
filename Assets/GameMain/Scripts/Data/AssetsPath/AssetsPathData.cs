using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.TofuGirl.Data
{
    public sealed class AssetsPathData
    {
        private DTAssetsPath m_AssetsPath;
        /// <summary>
        /// 实体资源Id
        /// </summary>
        public int Id => m_AssetsPath.Id;
        /// <summary>
        /// 实体资源名称
        /// </summary>
        public string Name => m_AssetsPath.Name;
        /// <summary>
        /// 资源组索引
        /// </summary>
        public string ResourceGroupIndexs => m_AssetsPath.ResourceGroupIndexs;

        public AssetsPathData(DTAssetsPath dTAssetsPath)
        {
            m_AssetsPath = dTAssetsPath;
        }
    }
}
