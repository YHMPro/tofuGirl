
using GameFramework;
using Project.TofuGirl.Data;
namespace Project.TofuGirl.Entity
{
    /// <summary>
    /// 盾牌实体数据
    /// </summary>
    public class ShieldEntityData : GOAnimatorEntityData
    {
        public static ShieldEntityData Create(ShieldBridgeData bridgeData, object userData=null)
        {
            ShieldEntityData shieldEntityData = ReferencePool.Acquire<ShieldEntityData>();
            shieldEntityData.Position = bridgeData.InitPosition;
            shieldEntityData.Rotation = bridgeData.InitRotation;

            shieldEntityData.UserData = userData;
            return shieldEntityData;
        }
    }
}
