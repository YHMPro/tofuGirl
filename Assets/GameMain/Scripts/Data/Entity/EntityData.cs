using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.TofuGirl.Data
{
    public sealed class EntityData
    {
        /// <summary>
        /// 实体信息
        /// </summary>
        private DTEntity m_Entity;

        /// <summary>
        /// 获实体Id。
        /// </summary>
        public int Id
        {
            get
            {
                return m_Entity.Id;
            }
        }

        /// <summary>
        /// 获取实体名称。
        /// </summary>
        public string Name
        {
            get { return m_Entity.Name; }
        }

        /// <summary>
        /// 获取实体资源ID。
        /// </summary>
        public int AssetId
        {
            get { return m_Entity.AssetId;}
        }

        /// <summary>
        /// 获取实体组Id。
        /// </summary>
        public int EntityGroupId
        {
            get { return m_Entity.EntityGroupId;}
        }
        /// <summary>
        /// 资源路径数据
        /// </summary>
        public AssetsPathData APData { get; private set; }
        /// <summary>
        /// 实体组数据
        /// </summary>
        public EntityGroupData EGData { get; private set; }
       
        public EntityData(DTEntity dTEntity, AssetsPathData assetsPathData , EntityGroupData entityGroupData)
        {
            m_Entity = dTEntity;
            APData = assetsPathData;
            EGData = entityGroupData;
        }
    }
}
