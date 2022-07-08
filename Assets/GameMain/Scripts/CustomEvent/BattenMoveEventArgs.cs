

using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{

    public class BattenMoveEventArgs :GameEventArgs 
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(BattenMoveEventArgs).GetHashCode();
        public override int Id => EventId;

        public bool Move { get; private set; }

        public override void Clear()
        {

        }
        public static BattenMoveEventArgs Create(bool move)
        {
            BattenMoveEventArgs args = ReferencePool.Acquire<BattenMoveEventArgs>();
            args.Move = move;
            return args;
        }
    }
}
