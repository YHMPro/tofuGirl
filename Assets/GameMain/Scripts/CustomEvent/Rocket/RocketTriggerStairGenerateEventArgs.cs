

using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 火箭触发的台阶构建事件
    /// </summary>
    public class RocketTriggerStairGenerateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(RocketTriggerStairGenerateEventArgs).GetHashCode();
        public override int Id => EventId;

        public int RocketTriggerTofuNum { get; private set; }
        public override void Clear()
        {

        }
        public static RocketTriggerStairGenerateEventArgs Create(int rocketTriggerTofuNum)
        {
            RocketTriggerStairGenerateEventArgs args = ReferencePool.Acquire<RocketTriggerStairGenerateEventArgs>();
            args.RocketTriggerTofuNum = rocketTriggerTofuNum;
            return args;
        }
    }
}
