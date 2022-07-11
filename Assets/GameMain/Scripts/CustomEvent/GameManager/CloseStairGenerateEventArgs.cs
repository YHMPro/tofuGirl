





using GameFramework.Event;
using GameFramework;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 关闭台阶构建事件
    /// </summary>
    public class CloseStairGenerateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(CloseStairGenerateEventArgs).GetHashCode();
        public override int Id => EventId;
        public override void Clear()
        {

        }
        public static CloseStairGenerateEventArgs Create()
        {
            CloseStairGenerateEventArgs args = ReferencePool.Acquire<CloseStairGenerateEventArgs>();
            return args;
        }
    }
}