

using GameFramework;
using UnityEngine;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Entity
{
    public class TofuEntityData : GOEntityData
    {
        private static int m_OrderInLayer = -999;

        public bool FirstTofu { get; private set; }

        public int PrevId { get; private set; }

        public static TofuEntityData Create(TofuBridgeData bridgeData,object userData=null)
        {
            TofuEntityData tofuEntityData = ReferencePool.Acquire<TofuEntityData>();
            m_OrderInLayer += 2;
            tofuEntityData.UserData = userData;
            tofuEntityData.Position = bridgeData.InitPosition;
            tofuEntityData.Rotation = bridgeData.InitRotation;
            tofuEntityData.EntityType = EnumEntity.Tofu;     
            tofuEntityData.OrderInLayer = m_OrderInLayer;
            tofuEntityData.FirstTofu = bridgeData.FirstTofu;
            tofuEntityData.PrevId = bridgeData.PrevId;
            return tofuEntityData;
        }

        public static TofuEntityData Create(Vector3 position,Vector3 rotation,object userData=null)
        {
            TofuEntityData tofuEntityData = ReferencePool.Acquire<TofuEntityData>();
            m_OrderInLayer+=2;
            tofuEntityData.Position = position;
            tofuEntityData.Rotation = rotation;
            tofuEntityData.EntityType = EnumEntity.Tofu;
            tofuEntityData.UserData = userData;
            tofuEntityData.OrderInLayer = m_OrderInLayer;
            return tofuEntityData;
        }
    }
}
