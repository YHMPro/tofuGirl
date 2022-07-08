using GameFramework;
using GameFramework.Event;


namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 更新顶部豆腐Id
    /// </summary>
    public class UpdateTopTofuSerialldEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(UpdateTopTofuSerialldEventArgs).GetHashCode();
        public override int Id => EventId;
        public int EntityId { get; private set; }
        public override void Clear()
        {

        }
        public static UpdateTopTofuSerialldEventArgs Create(int entityId)
        {
            UpdateTopTofuSerialldEventArgs args = ReferencePool.Acquire<UpdateTopTofuSerialldEventArgs>();
            args.EntityId = entityId;
            return args;
        }
    }
}
