using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.TofuGirl.Data
{
    public sealed class PoolParamData 
    {
        private DTPoolParam m_PoolParam;
        /// <summary>
        /// 获取对象池参数Id。
        /// </summary>
        public int Id
        {
            get
            {
                return m_PoolParam.Id;
            }
        }

        /// <summary>
        /// 获取组名字。
        /// </summary>
        public string GroupName
        {
            get { return m_PoolParam.GroupName; }
        }

        /// <summary>
        /// 获取自动释放可释放对象的间隔秒数。
        /// </summary>
        public float InstanceAutoReleaseInterval
        {
            get { return m_PoolParam.InstanceAutoReleaseInterval; }
        }

        /// <summary>
        /// 获取实例对象池的容量。
        /// </summary>
        public int InstanceCapacity
        {
            get { return m_PoolParam.InstanceCapacity; }
        }

        /// <summary>
        /// 获取对象池对象过期秒数。
        /// </summary>
        public float InstanceExpireTime
        {
            get { return m_PoolParam.InstanceExpireTime; }
        }

        /// <summary>
        /// 获取实体组实例对象池的优先级。
        /// </summary>
        public int InstancePriority
        {
            get { return m_PoolParam.InstancePriority; }
        }
        public PoolParamData(DTPoolParam dTPoolParam)
        {
            m_PoolParam = dTPoolParam;
        }
    }
}
