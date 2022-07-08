


using GameFramework;
using GameFramework.Event;


namespace Project.TofuGirl.Event
{
    public class TofuWithGirlCollisionEventArgs :GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(TofuWithGirlCollisionEventArgs).GetHashCode();
        public override int Id => EventId;

        public override void Clear()
        {

        }
        public static TofuWithGirlCollisionEventArgs Create()
        {
            TofuWithGirlCollisionEventArgs args = ReferencePool.Acquire<TofuWithGirlCollisionEventArgs>();
            return args;
        }
    }
}
