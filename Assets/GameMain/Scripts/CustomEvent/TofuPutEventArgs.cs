




using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    public class TofuPutEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(UpdateTopTofuSerialldEventArgs).GetHashCode();
        public override int Id => EventId;
        public bool Prefect { get; private set; }
        public int EntityId { get; private set; }
        public override void Clear()
        {

        }
        public static TofuPutEventArgs Create(int entityId,bool prefect)
        {
            TofuPutEventArgs args = ReferencePool.Acquire<TofuPutEventArgs>();
            args.Prefect = prefect;
            args.EntityId = entityId;
            return args;
        }
    }
}
