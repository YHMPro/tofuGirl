

using GameFramework.Event;
using GameFramework;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 更新相机跟随信息
    /// </summary>
    public class UpdateCameraFollowInfoEventArgs :GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(UpdateCameraFollowInfoEventArgs).GetHashCode();
        public override int Id => EventId;
        
        /// <summary>
        /// 发送类型
        /// </summary>
        public EnumSender SenderType { get; private set; }
        public override void Clear()
        {

        }
        public static UpdateCameraFollowInfoEventArgs Create(EnumSender senderType)
        {
            UpdateCameraFollowInfoEventArgs args = ReferencePool.Acquire<UpdateCameraFollowInfoEventArgs>();
            args.SenderType = senderType;
            return args;
        }
    }
}
