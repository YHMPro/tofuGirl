using GameFramework;
using GameFramework.Event;
using UnityEngine;
namespace Project.TofuGirl.Event
{

    public class SetCameraFollowInfoEventArgs : GameEventArgs 
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(SetCameraFollowInfoEventArgs).GetHashCode();
        public override int Id => EventId;
        /// <summary>
        /// 目标位置
        /// </summary>
        public Vector3 AimPosition { get; private set; }
        /// <summary>
        /// 跟随样式
        /// </summary>
        /// <summary>
        /// 发起类型
        /// </summary>
        public EnumSender SenderType
        {
            get;private set;
        }
        /// <summary>
        /// 跟随速度
        /// </summary>
        public float Speed;
        /// <summary>
        /// 延迟时间
        /// </summary>
        public float DelayTime;
        public override void Clear()
        {

        }
        public static SetCameraFollowInfoEventArgs Create(Vector3 aimPosition, EnumSender senderType, float speed,float delayTime=0)
        {
            SetCameraFollowInfoEventArgs args = ReferencePool.Acquire<SetCameraFollowInfoEventArgs>();
            args.AimPosition = aimPosition;
            args.SenderType = senderType;
            args.Speed = speed;
            args.DelayTime = delayTime;
            return args;
        }
    }
}
