

using GameFramework;
namespace Project.TofuGirl.Data
{
    public class CameraBridgeData : BridgeData
    {
        public static CameraBridgeData Create()
        {
            return ReferencePool.Acquire<CameraBridgeData>();
        }
    }
}
