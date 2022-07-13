using GameFramework.Event;
using GameFramework;

namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 互动与女孩解除事件
    /// </summary>
    public class ShieldWithGirlDetachEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(ShieldWithGirlDetachEventArgs).GetHashCode();
        public override int Id => EventId;
        public override void Clear()
        {

        }
        public static ShieldWithGirlDetachEventArgs Create()
        {
            ShieldWithGirlDetachEventArgs args = ReferencePool.Acquire<ShieldWithGirlDetachEventArgs>();
            return args;
        }
    }
}
