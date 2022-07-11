

using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 女孩解除与火箭的绑定关系
    /// </summary>
    public class GirlReliefRocketEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(GirlReliefRocketEventArgs).GetHashCode();
        public override int Id => EventId;
        public bool StairGenerate { get; private set; }
        public EnumSender SenderType { get; private set; }
        public override void Clear()
        {

        }
        public static GirlReliefRocketEventArgs Create(EnumSender senderType)
        {
            GirlReliefRocketEventArgs args = ReferencePool.Acquire<GirlReliefRocketEventArgs>();
            args.SenderType = senderType;
            return args;
        }
    }
}
