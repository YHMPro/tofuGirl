
using UnityGameFramework.Runtime;
namespace Project.TofuGirl.Data
{
 
    public sealed class EntityGroupData
    {
        private DTEntityGroup m_EntityGroup;
        /// <summary>
        /// 实体组Id
        /// </summary>
        public int Id => m_EntityGroup.Id;
        /// <summary>
        /// 实体组名称
        /// </summary>
        public string Name => m_EntityGroup.Name;
        /// <summary>
        /// 对象池参数Id
        /// </summary>
        public int PoolParamId => m_EntityGroup.PoolParamId;
        /// <summary>
        /// 对象池参数数据
        /// </summary>
        public PoolParamData PPData { get; private set; }
        public EntityGroupData (DTEntityGroup dTEntityGroup, PoolParamData poolParamData)
        {
            m_EntityGroup = dTEntityGroup;
            PPData = poolParamData;
        }
    }
}
