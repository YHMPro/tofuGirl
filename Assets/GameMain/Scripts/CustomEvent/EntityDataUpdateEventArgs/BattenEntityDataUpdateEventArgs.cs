using GameFramework;
using GameFramework.Event;
using Project.TofuGirl.Entity;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 木条实体数据更新事件
    /// </summary>
    public sealed class BattenEntityDataUpdateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(BattenEntityDataUpdateEventArgs).GetHashCode();
        public override int Id => EventId;
        /// <summary>
        /// 实体数据
        /// </summary>
        public BattenEntityData EntityData { get; private set; }

        public override void Clear()
        {

        }

        public static BattenEntityDataUpdateEventArgs Create(BattenBridgeData data)
        {
            BattenEntityDataUpdateEventArgs args = ReferencePool.Acquire<BattenEntityDataUpdateEventArgs>();
            args.EntityData = BattenEntityData.Create(data);
            return args;
        }
    }
}
