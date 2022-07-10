using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
namespace Project.TofuGirl.Data
{
    public class TofuBridgeData : BridgeData
    {
        public EnumTofu TofuType;

        public float Gravity;

        public bool FirstTofu;

        public int PrevId;

        public static TofuBridgeData Create()
        {
            return ReferencePool.Acquire<TofuBridgeData>();
        }

    }
}
