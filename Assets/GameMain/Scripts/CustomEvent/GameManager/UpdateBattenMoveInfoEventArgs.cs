using GameFramework;
using GameFramework.Event;
using UnityEngine;


namespace Project.TofuGirl.Event
{
    public class UpdateBattenMoveInfoEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(UpdateBattenMoveInfoEventArgs).GetHashCode();
        public override int Id => EventId;     
        
        public bool Move { get; private set; }
        public override void Clear()
        {

        }

        public static UpdateBattenMoveInfoEventArgs Create(bool move)
        {
            UpdateBattenMoveInfoEventArgs args = ReferencePool.Acquire<UpdateBattenMoveInfoEventArgs>();
            args.Move = move;
            return args;
        }
    }
}