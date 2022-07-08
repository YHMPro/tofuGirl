
using GameFramework;
using GameFramework.Event;

namespace Project.TofuGirl.Event
{
    public class LevelEndEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(LevelEndEventArgs).GetHashCode();
        public override int Id => EventId;

        public override void Clear()
        {

        }
        public static LevelEndEventArgs Create()
        {
            LevelEndEventArgs args = ReferencePool.Acquire<LevelEndEventArgs>();


            return args;
        }
    }
}
