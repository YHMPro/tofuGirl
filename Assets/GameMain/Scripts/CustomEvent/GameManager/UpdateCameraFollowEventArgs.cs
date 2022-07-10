




using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    public class UpdateCameraFollowEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(UpdateStairGenerateEventArgs).GetHashCode();
        public override int Id => EventId;


        public override void Clear()
        {

        }
        public static UpdateStairGenerateEventArgs Create()
        {
            UpdateStairGenerateEventArgs args = ReferencePool.Acquire<UpdateStairGenerateEventArgs>();

            return args;
        }
    }
}
