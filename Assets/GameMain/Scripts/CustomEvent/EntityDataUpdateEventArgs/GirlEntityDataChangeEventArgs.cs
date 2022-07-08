
using GameFramework;
using GameFramework.Event;
using Project.TofuGirl.Entity;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 女孩实体数据更新事件
    /// </summary>
    public sealed class GirlEntityDataUpdateEventArgs : GameEventArgs 
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(GirlEntityDataUpdateEventArgs).GetHashCode();
        public override int Id => EventId;
        /// <summary>
        /// 实体数据
        /// </summary>
        public GirlEntityData EntityData { get; private set; }

        public override void Clear()
        {

        }

        public static GirlEntityDataUpdateEventArgs Create(GirlBridgeData data)
        {
            GirlEntityDataUpdateEventArgs args = ReferencePool.Acquire<GirlEntityDataUpdateEventArgs>();
            args.EntityData = GirlEntityData.Create(data);
            return args;
        }
    }
}
