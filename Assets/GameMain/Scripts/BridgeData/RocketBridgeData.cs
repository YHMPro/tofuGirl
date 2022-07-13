

using UnityEngine;
using GameFramework;
namespace Project.TofuGirl.Data
{
    public class RocketBridgeData : BridgeData
    {
        public Vector3 AimPosition;

        public float Speed;
        public int TofuNum;

        public static RocketBridgeData Create()
        {
            return ReferencePool.Acquire<RocketBridgeData>();
        }
    }
}
