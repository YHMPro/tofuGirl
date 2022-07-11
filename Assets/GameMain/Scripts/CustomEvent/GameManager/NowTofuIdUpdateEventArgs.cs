
using GameFramework.Event;
using GameFramework;

namespace Project.TofuGirl.Event
{
    public class NowTofuIdUpdateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(NowTofuIdUpdateEventArgs).GetHashCode();
        public override int Id => EventId;

        public int EntityId { get; private set; }

        public override void Clear()
        {

        }
        public static NowTofuIdUpdateEventArgs Create(int entityId)
        {
            NowTofuIdUpdateEventArgs args = ReferencePool.Acquire<NowTofuIdUpdateEventArgs>();
            args.EntityId = entityId;
            return args;
        }
    }
}
