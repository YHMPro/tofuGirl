


using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace Project.TofuGirl.Event
{
    public class TofuWithGirlCollisionEventArgs :GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(TofuWithGirlCollisionEventArgs).GetHashCode();
        public override int Id => EventId;

        public EntityLogic Logic;
        public override void Clear()
        {

        }
        public static TofuWithGirlCollisionEventArgs Create(EntityLogic logic=null)
        {
            TofuWithGirlCollisionEventArgs args = ReferencePool.Acquire<TofuWithGirlCollisionEventArgs>();
            args.Logic = logic;
            return args;
        }
    }
}
