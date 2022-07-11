
using GameFramework.Event;
using GameFramework;



namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 火箭触发相机移动事件
    /// </summary>
    public class RocketTriggerCameraMoveEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(RocketTriggerCameraMoveEventArgs).GetHashCode();
        public override int Id => EventId;

        public override void Clear()
        {

        }
        public static RocketTriggerCameraMoveEventArgs Create()
        {
            RocketTriggerCameraMoveEventArgs args = ReferencePool.Acquire<RocketTriggerCameraMoveEventArgs>();
            return args;
        }
    }
}
