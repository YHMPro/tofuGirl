


using GameFramework.Event;
using GameFramework;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 开启台阶构建
    /// </summary>
    public class OpenStairGenerateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(OpenStairGenerateEventArgs).GetHashCode();
        public override int Id => EventId;
        public override void Clear()
        {

        }
        public static OpenStairGenerateEventArgs Create()
        {
            OpenStairGenerateEventArgs args = ReferencePool.Acquire<OpenStairGenerateEventArgs>();
            return args;
        }
    }
}
