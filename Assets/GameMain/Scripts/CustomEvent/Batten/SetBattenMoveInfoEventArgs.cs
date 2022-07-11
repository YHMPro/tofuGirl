using GameFramework;
using GameFramework.Event;
using UnityEngine;


namespace Project.TofuGirl.Event
{
    public class SetBattenMoveInfoEventArgs: GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(SetBattenMoveInfoEventArgs).GetHashCode();
        public override int Id => EventId;

        public bool Move { get; private set; }
        public override void Clear()
        {

        }
        public static SetBattenMoveInfoEventArgs Create(bool move)
        {
            SetBattenMoveInfoEventArgs args = ReferencePool.Acquire<SetBattenMoveInfoEventArgs>();
            args.Move = move;
            return args;
        }
    }
}
