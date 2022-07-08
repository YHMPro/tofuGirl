


using GameFramework;
using GameFramework.Event;
using UnityEngine;
namespace Project.TofuGirl.Event
{
    public class CameraFollowEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(CameraFollowEventArgs).GetHashCode();
        public override int Id => EventId;
        public Vector3 AimPosition { get; private set; }
        public override void Clear()
        {

        }
        public static CameraFollowEventArgs Create(Vector3 aimPosition)
        {
            CameraFollowEventArgs args = ReferencePool.Acquire<CameraFollowEventArgs>();
            args.AimPosition = aimPosition;
            return args;
        }
    }
}
