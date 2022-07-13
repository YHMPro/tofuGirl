
using GameFramework;

namespace Project.TofuGirl.Data
{
    /// <summary>
    /// 护盾桥接数据
    /// </summary>
    public class ShieldBridgeData : BridgeData
    {



        public static ShieldBridgeData Create()
        {
            return ReferencePool.Acquire<ShieldBridgeData>();
        }
    }
}
