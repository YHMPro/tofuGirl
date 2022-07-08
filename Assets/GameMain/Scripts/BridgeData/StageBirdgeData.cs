


using GameFramework;
using UnityEngine;
namespace Project.TofuGirl.Data
{
    public class StageBirdgeData : BridgeData
    {
        public float CameraOrthographicSize;

        public static StageBirdgeData Create()
        {
            return ReferencePool.Acquire<StageBirdgeData>();
        }
    }
}
