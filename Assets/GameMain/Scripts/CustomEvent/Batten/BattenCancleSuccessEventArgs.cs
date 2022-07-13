
using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 木条撤销成功事件
    /// </summary>
    public class BattenCancleSuccessEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(BattenCancleSuccessEventArgs).GetHashCode();
        public override int Id => EventId;

        public override void Clear()
        {

        }
        public static BattenCancleSuccessEventArgs Create()
        {
            BattenCancleSuccessEventArgs args = ReferencePool.Acquire<BattenCancleSuccessEventArgs>();
            return args;
        }
    }
}
