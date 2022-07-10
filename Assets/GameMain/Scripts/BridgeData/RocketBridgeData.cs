

using UnityEngine;
using GameFramework;
namespace Project.TofuGirl.Data
{
    public class RocketBridgeData : BridgeData
    {
        public Vector3 AimPosition;

        public float Speed;

        public static RocketBridgeData Create()
        {
            return ReferencePool.Acquire<RocketBridgeData>();
        }
    }
}
