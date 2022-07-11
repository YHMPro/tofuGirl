using GameFramework.Event;
using GameFramework;
namespace Project.TofuGirl
{
    /// <summary>
    /// 火箭与女孩绑定事件
    /// </summary>
    public class RocketWithGirlBindEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(RocketWithGirlBindEventArgs).GetHashCode();
        public override int Id => EventId;
        public override void Clear()
        {

        }
        public static RocketWithGirlBindEventArgs Create()
        {
            RocketWithGirlBindEventArgs args = ReferencePool.Acquire<RocketWithGirlBindEventArgs>();
            return args;
        }
    }
}