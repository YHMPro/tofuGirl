
using GameFramework;
using UnityEngine;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Entity
{
    public class RocketEntityData : GOAnimatorEntityData
    {
        public Vector3 AimPosition { get; private set; }
        public float Speed { get; private set; }
        public static RocketEntityData Create(RocketBridgeData bridgeData, object userData=null)
        {
            RocketEntityData rocketEntityData = ReferencePool.Acquire<RocketEntityData>();

            rocketEntityData.UserData = userData;
            rocketEntityData.Position = bridgeData.InitPosition;
            rocketEntityData.Rotation = bridgeData.InitPosition;
            rocketEntityData.AimPosition = bridgeData.AimPosition;
            rocketEntityData.Speed = bridgeData.Speed;
            rocketEntityData.EntityType = EnumEntity.Rocket;

            return rocketEntityData;
        }
    }
}
