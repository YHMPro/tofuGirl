


using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 木条撤销事件
    /// </summary>
    public class BattenCancleEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(BattenCancleEventArgs).GetHashCode();
        public override int Id => EventId;

        public override void Clear()
        {

        }
        public static BattenCancleEventArgs Create()
        {
            BattenCancleEventArgs args = ReferencePool.Acquire<BattenCancleEventArgs>();
            return args;
        }
    }
}
