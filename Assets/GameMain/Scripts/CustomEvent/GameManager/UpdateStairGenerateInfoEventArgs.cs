using GameFramework;
using GameFramework.Event;
namespace Project.TofuGirl.Event
{
    public class UpdateStairGenerateInfoEventArgs :GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(UpdateStairGenerateInfoEventArgs).GetHashCode();
        public override int Id => EventId;
        public bool StairGenerate { get; private set; }
        public EnumSender SenderType { get; private set; }
        public override void Clear()
        {

        }
        public static UpdateStairGenerateInfoEventArgs Create(EnumSender senderType,bool stairGenerate=true)
        {
            UpdateStairGenerateInfoEventArgs args = ReferencePool.Acquire<UpdateStairGenerateInfoEventArgs>();
            args.SenderType = senderType;
            args.StairGenerate = stairGenerate;
            return args;
        }
    }
}
