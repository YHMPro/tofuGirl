


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

        public EnumEntity EntityType { get; private set; }

        public float Speed;

        public override void Clear()
        {

        }
        public static CameraFollowEventArgs Create(Vector3 aimPosition,EnumEntity entityType,float speed)
        { 
            CameraFollowEventArgs args = ReferencePool.Acquire<CameraFollowEventArgs>();
            args.AimPosition = aimPosition;
            args.EntityType = entityType;
            args.Speed = speed;
            return args;
        }
    }
}
