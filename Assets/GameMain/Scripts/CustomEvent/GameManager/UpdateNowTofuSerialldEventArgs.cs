



using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 更新当前豆腐Id事件
    /// </summary>
    public class UpdateNowTofuSerialldEventArgs :GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(UpdateNowTofuSerialldEventArgs).GetHashCode();
        public override int Id => EventId;
        public int EntityId { get; private set; }
        public override void Clear()
        {

        }
        public static UpdateNowTofuSerialldEventArgs Create(int entityId)
        {
            UpdateNowTofuSerialldEventArgs args = ReferencePool.Acquire<UpdateNowTofuSerialldEventArgs>();
            args.EntityId = entityId;
            return args;
        }
    }
}
