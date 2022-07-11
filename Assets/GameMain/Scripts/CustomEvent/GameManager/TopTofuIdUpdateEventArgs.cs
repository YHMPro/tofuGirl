
using GameFramework.Event;
using GameFramework;


namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 顶部豆腐Id更新事件
    /// </summary>
    public class TopTofuIdUpdateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(TopTofuIdUpdateEventArgs).GetHashCode();
        public override int Id => EventId;

        public int EntityId { get; private set; }

        public override void Clear()
        {

        }
        public static TopTofuIdUpdateEventArgs Create(int entityId)
        {
            TopTofuIdUpdateEventArgs args = ReferencePool.Acquire<TopTofuIdUpdateEventArgs>();
            args.EntityId = entityId;
            return args;
        }
    }
}
