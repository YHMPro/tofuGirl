


using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    /// <summary>
    /// 更新台阶构建事件
    /// </summary>
    public class UpdateStairGenerateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(UpdateStairGenerateEventArgs).GetHashCode();
        public override int Id => EventId;

        public bool StairGenerate { get; private set; }
        public override void Clear()
        {

        }
        public static UpdateStairGenerateEventArgs Create( bool stairGenerate)
        {
            UpdateStairGenerateEventArgs args = ReferencePool.Acquire<UpdateStairGenerateEventArgs>();

            args.StairGenerate = stairGenerate;
            return args;
        }
    }
}
