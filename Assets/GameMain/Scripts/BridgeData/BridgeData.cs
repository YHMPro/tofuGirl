
using GameFramework;
using UnityEngine;
namespace Project.TofuGirl.Data
{
    /// <summary>
    /// 桥接数据     LevelData(动态)+??Data(静态) => ??EntityData(实体逻辑数据)
    /// </summary>
    public abstract class BridgeData : IReference
    {
        public Vector3 InitPosition;

        public Vector3 InitRotation;
        public virtual void Clear()
        {
            
        }
    }
}
