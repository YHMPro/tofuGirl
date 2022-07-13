


using GameFramework.Event;
using GameFramework;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 台阶构建成功事件
    /// </summary>
    public class StairGenerateSuccessEventArgs :GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(StairGenerateSuccessEventArgs).GetHashCode();
        public override int Id => EventId;
        public override void Clear()
        {

        }
        public static StairGenerateSuccessEventArgs Create()
        {
            StairGenerateSuccessEventArgs args = ReferencePool.Acquire<StairGenerateSuccessEventArgs>();
            return args;
        }
    }
}
