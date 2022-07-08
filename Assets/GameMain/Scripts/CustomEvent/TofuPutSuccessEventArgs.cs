using GameFramework;
using GameFramework.Event;


namespace Project.TofuGirl.Event
{
    public class TofuPutSuccessEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(GirlDiedEventArgs).GetHashCode();
        public override int Id => EventId;

        public override void Clear()
        {

        }
        public static GirlDiedEventArgs Create()
        {
            GirlDiedEventArgs args = ReferencePool.Acquire<GirlDiedEventArgs>();
            return args;
        }

    }
}
