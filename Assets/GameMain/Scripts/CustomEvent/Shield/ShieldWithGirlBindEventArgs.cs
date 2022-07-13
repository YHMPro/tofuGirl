using GameFramework.Event;
using GameFramework;

namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 护盾与女孩绑定事件
    /// </summary>
    public class ShieldWithGirlBindEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(ShieldWithGirlBindEventArgs).GetHashCode();
        public override int Id => EventId;
        public override void Clear()
        {

        }
        public static ShieldWithGirlBindEventArgs Create()
        {
            ShieldWithGirlBindEventArgs args = ReferencePool.Acquire<ShieldWithGirlBindEventArgs>();
            return args;
        }
    }
}
