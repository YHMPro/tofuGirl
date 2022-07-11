


using GameFramework.Event;
using GameFramework;

namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 火箭与女孩解除事件
    /// </summary>
    public class RocketWithGirlDetachEventArgs : GameEventArgs 
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(RocketWithGirlDetachEventArgs).GetHashCode();
        public override int Id => EventId;
        public override void Clear()
        {

        }
        public static RocketWithGirlDetachEventArgs Create()
        {
            RocketWithGirlDetachEventArgs args = ReferencePool.Acquire<RocketWithGirlDetachEventArgs>();
            return args;
        }
    }
}
