

using GameFramework.Event;
using GameFramework;

namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 女孩触发相机移动的方式
    /// </summary>
    public enum EnumGirlTriggerCameraMove
    {
        /// <summary>
        /// 跳跃
        /// </summary>
        Jump,
        /// <summary>
        /// 死亡
        /// </summary>
        Died
    }
    /// <summary>
    /// 女孩触发相机移动事件
    /// </summary>
    public class GirlTriggerCameraMoveEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(GirlTriggerCameraMoveEventArgs).GetHashCode();
        public override int Id => EventId;

        public EnumGirlTriggerCameraMove TriggerType;
        public override void Clear()
        {

        }
        public static GirlTriggerCameraMoveEventArgs Create(EnumGirlTriggerCameraMove triggerType)
        {
            GirlTriggerCameraMoveEventArgs args = ReferencePool.Acquire<GirlTriggerCameraMoveEventArgs>();
            args.TriggerType = triggerType;
            return args;
        }
    }
}
