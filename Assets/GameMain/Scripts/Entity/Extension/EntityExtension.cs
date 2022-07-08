


using GameFramework.Entity;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Data;
using Project.TofuGirl;
using Project.TofuGirl.Entity;
namespace Project
{
    public static class EntityExtension 
    {
        private static int m_SerialId = 0;
   
        public static void ShowStageEntity(this EntityComponent entityComponent, int serialId,int stageIndex, object userData = null)
        {
            ////EntityData entityData = GameEntry.Data.GetData<DataEntity>().GetEntityData(Constant.EntityId.StageId);
            ////if (entityData == null)
            ////{
            ////    return;
            ////}
            ////if (!entityComponent.HasEntityGroup(entityData.EGData.Name))
            ////{
            ////    PoolParamData ppData = entityData.EGData.PPData;
            ////    entityComponent.AddEntityGroup(entityData.EGData.Name, ppData.InstanceAutoReleaseInterval, ppData.InstanceCapacity, ppData.InstanceExpireTime, ppData.InstancePriority);
            ////}
            ////entityComponent.ShowEntity<StageEntityLogic>(serialId, entityData.APData.Name+stageIndex+".prefab", entityData.EGData.Name, Constant.AssetPriority.EntityAsset, userData);
        }
        public static void ShowEntity<T>(this EntityComponent entityComponent,int serialId,int entityId,object userData=null) where T:EntityLogic
        {
           EntityData entityData = GameEntry.Data.GetData<DataEntity>().GetEntityData(entityId);
            if (entityData == null)
            {
                return;
            }
            if (!entityComponent.HasEntityGroup(entityData.EGData.Name))
            {
                PoolParamData ppData = entityData.EGData.PPData;
                entityComponent.AddEntityGroup(entityData.EGData.Name, ppData.InstanceAutoReleaseInterval, ppData.InstanceCapacity, ppData.InstanceExpireTime, ppData.InstancePriority);
            }
            entityComponent.ShowEntity<T>(serialId, entityData.APData.Name, entityData.EGData.Name, Constant.AssetPriority.EntityAsset, userData);
        }
        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return ++m_SerialId;
        }



    }
}
