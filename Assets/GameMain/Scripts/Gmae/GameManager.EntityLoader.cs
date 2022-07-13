


using UnityEngine.Events;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Data;
using Project.TofuGirl.Entity;
using UnityEngine;
using System.Collections.Generic;
namespace Project.TofuGirl
{
    /// <summary>
    /// 游戏管理器(实体)
    /// </summary>
    public partial class GameManager
    {
        /// <summary>
        /// 实体加载器
        /// </summary>
        private EntityLoader m_ELoader;


        #region 实体初始化
        /// <summary>
        /// 实体初始化
        /// </summary>
        private void EntityInit()
        {
            m_ELoader = EntityLoader.Create();
            BuilderCameraEntity(()=> 
            {
                BuilderStageEntity();//舞台
            });//相机     
            BuilderTofuEntity();//豆腐
            BuilderGirlEntity();//女孩
            //游戏开始
            GameStart = true;
            Log.Info("暂时写这里，后续通过事件调控游戏开始:{0}", GameStart);
        }
        #endregion
        #region 实体加载   
        /// <summary>
        /// 构建楼梯实体
        /// </summary>
        private void BuilderStairEntity()
        {
            m_ELoader.ShowEntity<BattenEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.BattemId, (battenEntity) =>
            {
                m_ELoader.ShowEntity<TofuEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.TufoId, (tofuEntity) =>
                {
                    GameEntry.Entity.AttachEntity(tofuEntity, battenEntity);
                }, TofuEntityData.Create(m_TBData));
            }, BattenEntityData.Create(m_BBData));
        }
        /// <summary>
        /// 构建豆腐实体
        /// </summary>
        private void BuilderTofuEntity()
        {
            m_ELoader.ShowEntity<TofuEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.TufoId, (tofuEntity) =>
            {
            }, TofuEntityData.Create(m_TBData));
        }
        
        private void BuilderGirlEntity()
        {
           m_ELoader.ShowEntity<GirlEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.GirlId, (girlEntity) =>
            {
               GirlSerialId = girlEntity.Id;
            }, GirlEntityData.Create(m_GBData));
        }
        /// <summary>
        /// 构建相机实体
        /// </summary>
        private void BuilderCameraEntity(UnityAction callback)
        {
            m_ELoader.ShowEntity<CameraEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.CameraId, (cameraEntity) =>
            {
                CameraSerialId = cameraEntity.Id;
                m_SBData.CameraOrthographicSize = (cameraEntity.Logic as CameraEntityLogic).SelfCamera.orthographicSize;
                callback?.Invoke();
            }, CameraEntityData.Create(m_CBData));
           
        }
        /// <summary>
        /// 构建舞台实体
        /// </summary>
        private void BuilderStageEntity()
        {
            m_ELoader.ShowEntity<StageEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.StageId_1, (stageEntity) =>
            {
                StageSerialId = stageEntity.Id;
            }, StageEntityData.Create(m_SBData));

        }
        /// <summary>
        /// 构建火箭实体
        /// </summary>
        private void BuilderRocketEntity()
        {
            m_ELoader.ShowEntity<RocketEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.RocketId, (rocketEntity) =>
            {
               //与女孩绑定
               if(GameEntry.Entity.HasEntity(GirlSerialId))
                {                     
                    GameEntry.Entity.AttachEntity(GirlSerialId, rocketEntity);
                }
                if (GameEntry.Entity.HasEntity(CameraSerialId))
                {
                    GameEntry.Entity.AttachEntity(CameraSerialId, rocketEntity);
                }
            }, RocketEntityData.Create(m_RBData));
        }
        /// <summary>
        /// 构建护盾实体
        /// </summary>
        private void BuilderShieldEntity()
        {
            m_ELoader.ShowEntity<ShieldEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.Shield, (shieldEntity) =>
            {
                //与女孩绑定
                if (GameEntry.Entity.HasEntity(GirlSerialId))
                {
                    GameEntry.Entity.AttachEntity(shieldEntity, GirlSerialId);
                }               
            }, ShieldEntityData.Create(m_ShieldBData));
        }
        #endregion

        #region 实体回收
        
        /// <summary>
        /// 豆腐实体自动回收
        /// </summary>
        private void TofuEntityAutoRecycle()
        {
            //规则  依据当前的豆腐向下遍历判断每个豆腐与相机的距离是否大于等于相机的高的一半            
            int dofuId = NowTofuSerialId;
            CameraEntityLogic cameraLogic = GameEntry.Entity.GetEntity(CameraSerialId).Logic as CameraEntityLogic;
            float referDis = cameraLogic.SelfCamera.orthographicSize / ((float)Screen.width / Screen.height) / 2 + m_LData.BData.Interval;            
            while (GameEntry.Entity.HasEntity(dofuId))
            {               
                TofuEntityLogic tofuLogic = GameEntry.Entity.GetEntity(dofuId).Logic as TofuEntityLogic;
                if (Vector2.Distance(cameraLogic.transform.position, tofuLogic.transform.position) >= referDis)
                {
                    //回收豆腐  
                    TofuCacheData tofuCacheData = TofuCacheData.Create();
                    tofuCacheData.Position = tofuLogic.transform.position;
                    tofuCacheData.Rotation = tofuLogic.transform.eulerAngles;
                    tofuCacheData.Id = dofuId;
                    tofuCacheData.Prefect = tofuLogic.Prefect;
                    //将豆腐缓存数据添加到托管队列中
                    TofuManager.Enqueue(tofuCacheData);
                    //隐藏实体
                    GameEntry.Entity.HideEntity(dofuId);
                }
                dofuId = tofuLogic.PrevId;   
                
            }
        }
        #endregion
    }
}
