

using GameFramework;
using UnityEngine;
namespace Project.TofuGirl.Data
{
    public class BattenBridgeData : BridgeData
    {     
        public Vector3 AimPosition;

        public EnumBattenMove MoveType;

        public float Speed;

        public static BattenBridgeData Create()
        {
            return ReferencePool.Acquire<BattenBridgeData>();
        }
    }
}
