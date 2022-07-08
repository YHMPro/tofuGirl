
using GameFramework;
using GameFramework.Event;

namespace Project.TofuGirl.Event
{
    public class GameOverEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(GameOverEventArgs).GetHashCode();
        public override int Id => EventId;

        public override void Clear()
        {

        }
        public static GameOverEventArgs Create()
        {
            GameOverEventArgs args = ReferencePool.Acquire<GameOverEventArgs>();
            return args;
        }
    }
}
