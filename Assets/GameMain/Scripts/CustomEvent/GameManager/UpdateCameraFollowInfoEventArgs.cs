

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
        /// 跟随样式
        /// </summary>
        public EnumCameraFollow FollowType { get; private set; }
        public override void Clear()
        {

        }
        public static UpdateCameraFollowInfoEventArgs Create(EnumCameraFollow followType)
        {
            UpdateCameraFollowInfoEventArgs args = ReferencePool.Acquire<UpdateCameraFollowInfoEventArgs>();
            args.FollowType = followType;
            return args;
        }
    }
}
