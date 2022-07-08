


using UnityEngine;
using GameFramework;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Entity
{
    public class StageEntityData : GOEntityData
    {
        public float CameraOrthographicSize { get; private set; }

        public static StageEntityData Create(StageBirdgeData birdgeData,object userData=null)
        {
            StageEntityData stageEntityData = ReferencePool.Acquire<StageEntityData>();
            stageEntityData.CameraOrthographicSize = birdgeData.CameraOrthographicSize;
            stageEntityData.Position = birdgeData.InitPosition;
            stageEntityData.Rotation = birdgeData.InitRotation;
            stageEntityData.UserData = userData;
            return stageEntityData;
        }
        public static StageEntityData Create(Vector3 position, Vector3 rotation,object userData=null)
        {
            StageEntityData stageEntityData = ReferencePool.Acquire<StageEntityData>();
            stageEntityData.Position = position;
            stageEntityData.Rotation = rotation;
            stageEntityData.UserData = userData;
            return stageEntityData;
        }


    }
}
