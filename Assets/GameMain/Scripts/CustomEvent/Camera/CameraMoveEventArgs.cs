
using GameFramework.Event;
using GameFramework;
using UnityEngine;
namespace Project.TofuGirl.Event
{
    public class CameraMoveEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(CameraMoveEventArgs).GetHashCode();
        public override int Id => EventId;

        public Vector3 AimPosition { get; private set; }

        public override void Clear()
        {

        }
        public static CameraMoveEventArgs Create(Vector3 position)
        {
            CameraMoveEventArgs args = ReferencePool.Acquire<CameraMoveEventArgs>();
            args.AimPosition = position;
            return args;
        }
    }
}
