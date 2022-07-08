using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
namespace Project.TofuGirl.Data
{
    /// <summary>
    /// 女孩桥接数据   
    /// </summary>
    public sealed class GirlBridgeData :BridgeData
    {
        public float Speed;

        public float Gravity;
        public static GirlBridgeData Create()
        {
            return ReferencePool.Acquire<GirlBridgeData>();
        }
    }
}
