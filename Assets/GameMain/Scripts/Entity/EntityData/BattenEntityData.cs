

using GameFramework;
using UnityEngine;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Entity
{

    public class BattenEntityData : GOEntityData
    {
        /// <summary>
        /// 目标点
        /// </summary>
        public Vector3 AimPosition { get; private set; }
        /// <summary>
        /// 移动速度
        /// </summary>
        public float Speed { get; private set; }
        /// <summary>
        /// 移动类型
        /// </summary>
        public EnumBattenMove MoveType { get; private set; }

        public static BattenEntityData Create(BattenBridgeData bridgeData,object userData=null)
        {
            BattenEntityData battenEntityData = ReferencePool.Acquire<BattenEntityData>();
            battenEntityData.UserData = userData;
            battenEntityData.Position = bridgeData.InitPosition;
            battenEntityData.Rotation = bridgeData.InitRotation;
            battenEntityData.AimPosition = bridgeData.AimPosition;
            battenEntityData.Speed = bridgeData.Speed;
            battenEntityData.EntityType = EnumEntity.Batten;
            battenEntityData.MoveType = bridgeData.MoveType;
            return battenEntityData;
        }

        public static BattenEntityData Create(Vector3 position, Vector3 rotation,Vector3 aimPosition)
        {
            BattenEntityData battenEntityData = ReferencePool.Acquire<BattenEntityData>();
            battenEntityData.Position = position;
            battenEntityData.Rotation = rotation;
            battenEntityData.AimPosition = aimPosition;
            battenEntityData.MoveType = ((position-battenEntityData.AimPosition).x )> 0 ? EnumBattenMove.Left: EnumBattenMove.Right;
            battenEntityData.EntityType = EnumEntity.Batten;
            return battenEntityData;
        }

    }
}
