



using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    public class LevelStartEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(LevelStartEventArgs).GetHashCode();
        public override int Id => EventId;
        
        public override void Clear()
        {

        }
        public static LevelStartEventArgs Create()
        {
            LevelStartEventArgs args = ReferencePool.Acquire<LevelStartEventArgs>();

            return args;
        }
    }
}
