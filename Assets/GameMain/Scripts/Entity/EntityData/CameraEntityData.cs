

using UnityEngine;
using GameFramework;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Entity
{
    public class CameraEntityData :GOEntityData
    {       
        public static CameraEntityData Create(CameraBridgeData bridgeData,object userData=null)
        {
            CameraEntityData cameraEntityData = ReferencePool.Acquire<CameraEntityData>();
            cameraEntityData.Position = bridgeData.InitPosition;
            cameraEntityData.Rotation = bridgeData.InitRotation;
            cameraEntityData.UserData = userData;
            return cameraEntityData;
        }
        public static CameraEntityData Create(Vector3 position,Vector3 rotation,object userData=null)
        {
            CameraEntityData cameraEntityData = ReferencePool.Acquire<CameraEntityData>();
            cameraEntityData.Position = position;
            cameraEntityData.Rotation = rotation;
            cameraEntityData.EntityType = EnumEntity.Camera;
            cameraEntityData.UserData = userData;
            return cameraEntityData;
        }
    }
}
