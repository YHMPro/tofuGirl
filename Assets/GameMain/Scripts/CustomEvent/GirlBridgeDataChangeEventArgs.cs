using GameFramework;
using GameFramework.Event;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Event
{
    public class GirlBridgeDataChangeEventArgs :GameEventArgs
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public static readonly int EventId = typeof(GirlBridgeDataChangeEventArgs).GetHashCode();
        public override int Id => EventId;

        public GirlBridgeData BirdgeData { get; private set; }

        public override void Clear()
        {

        }
        public static GirlBridgeDataChangeEventArgs Create(GirlBridgeData bridgeData)
        {
            GirlBridgeDataChangeEventArgs args = ReferencePool.Acquire<GirlBridgeDataChangeEventArgs>();
            args.BirdgeData = bridgeData;
            return args;
        }
    }
}
