




using GameFramework.Event;
using GameFramework;
using UnityEngine;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 豆腐回收事件
    /// </summary>
    public class TofuRecycleEventArgs :GameEventArgs 
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(TofuRecycleEventArgs).GetHashCode();
        public override int Id => EventId;

        public Vector3 ReferPosition;

        public override void Clear()
        {

        }
        public static TofuRecycleEventArgs Create(Vector3 referPosition)
        {
            TofuRecycleEventArgs args = ReferencePool.Acquire<TofuRecycleEventArgs>();
            args.ReferPosition = referPosition;
            return args;
        }
    }
}
