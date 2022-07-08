
using GameFramework;
using GameFramework.Event;
using Project.TofuGirl.Entity;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 豆腐实体数据
    /// </summary>
    public sealed class TufoEntityDataUpdateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(TufoEntityDataUpdateEventArgs).GetHashCode();
        public override int Id => EventId;
        /// <summary>
        /// 实体数据
        /// </summary>
        public TofuEntityData EntityData { get; private set; }

        public override void Clear()
        {

        }

        public static TufoEntityDataUpdateEventArgs Create(TofuBridgeData data)
        {
            TufoEntityDataUpdateEventArgs args = ReferencePool.Acquire<TufoEntityDataUpdateEventArgs>();
            args.EntityData = TofuEntityData.Create(data);
            return args;
        }
    }
}
