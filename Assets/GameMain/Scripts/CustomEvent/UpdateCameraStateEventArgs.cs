

using GameFramework.Event;
using UnityEngine;
using GameFramework;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 更新相机状态事件
    /// </summary>
    public class UpdateCameraStateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 更新相机状态事件
        /// </summary>
        public static readonly int EventId = typeof(UpdateCameraStateEventArgs).GetHashCode();
        public override int Id => EventId;
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 Position { get; private set; }
        /// <summary>
        /// 是否拉伸
        /// </summary>
        public bool Stretch { get; private set; }
        public override void Clear()
        {
            
        }
        public static UpdateCameraStateEventArgs Create (Vector3 position,bool stretch=false)
        {
            UpdateCameraStateEventArgs args = ReferencePool.Acquire<UpdateCameraStateEventArgs>();
            args.Position = position;
            args.Stretch = stretch;
            return args;
        }
    }
}
