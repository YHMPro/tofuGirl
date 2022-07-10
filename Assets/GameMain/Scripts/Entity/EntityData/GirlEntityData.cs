
using GameFramework;
using System;
using UnityEngine;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Entity
{
    public sealed class GirlEntityData : GOEntityData
    {
        public float Speed { get; private set; }
        public float Gravity { get; private set; }

        public static GirlEntityData Create(GirlBridgeData bridgeData, object userData = null)
        {
            GirlEntityData girlEntityData = ReferencePool.Acquire<GirlEntityData>();
            girlEntityData.Position = bridgeData.InitPosition;
            girlEntityData.Rotation = bridgeData.InitRotation;
            girlEntityData.UserData = userData;
            girlEntityData.Speed = bridgeData.Speed;
            girlEntityData.Gravity = bridgeData.Gravity;
            girlEntityData.EntityType = EnumEntity.Girl;
            return girlEntityData;
        }
        public static GirlEntityData Create(Vector3 position, Vector3 rotation,object userData=null)
        {
            GirlEntityData girlEntityData = ReferencePool.Acquire<GirlEntityData>();
            girlEntityData.Position = position;
            girlEntityData.Rotation = rotation;
            girlEntityData.EntityType = EnumEntity.Girl;
            return girlEntityData;
        }

    }
}
