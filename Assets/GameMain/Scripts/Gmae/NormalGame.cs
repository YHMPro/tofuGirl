using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.TofuGirl.Entity;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Data;
namespace Project.TofuGirl
{
    public class NormalGame : GameBase
    {
        Vector3 InitPosition;

        private LevelData m_LevelData;

        private EntityLoader m_ELoader;

        protected override void OnInit()
        {
            base.OnInit();
            m_ELoader = EntityLoader.Create();
        }

        protected override void OnPreload()
        {                 
            base.OnPreload();
            m_LevelData = GameEntry.Data.GetData<DataLevel>().GetLevelData(1001);//加载第一关的配置数据
            InitPosition = new Vector3(-5.5f, -3.2f, 0);
            CameraSerialId = m_ELoader.ShowEntity<CameraEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.CameraId, (entity) => 
            {
               GameEntry.Entity.ShowEntity<StageEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.StageId_1, StageEntityData.Create(Vector3.zero, Vector3.zero));
            }, CameraEntityData.Create(new Vector3(0, 0, -10), Vector3.zero));
            m_ELoader.ShowEntity<GirlEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.GirlId, (entity) => 
            {
               
            }, GirlEntityData.Create(new Vector3(0, -2.4f, 0), Vector3.zero));
            NowTufoSerialId =m_ELoader.ShowEntity<TofuEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.TufoId, (entity) =>
            {

            }, TofuEntityData.Create(new Vector3(0, InitPosition.y, 0), Vector3.zero,this));
           
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            
            m_ElapseSeconds += elapseSeconds;
            if (m_ElapseSeconds > m_LevelData.BData.CreateTimeBase)
            {           
                m_ElapseSeconds = 0;
                InitPosition.y += 0.7f;
                NowBattenSerialId = m_ELoader.ShowEntity<BattenEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.BattemId, (battenEntity) =>
                {
                   Vector3 tufoPosition = Vector3.zero;
                   switch((battenEntity.Logic as BattenEntityLogic).MoveType)
                    {
                        case EnumBattenMove.Left:
                            {
                                tufoPosition = (battenEntity.Logic as BattenEntityLogic).LeftPoint.position;
                                break;
                            }
                        case EnumBattenMove.Right:
                            {
                                tufoPosition = (battenEntity.Logic as BattenEntityLogic).RightPoint.position;
                                break;
                            }
                    }
                    m_ELoader.ShowEntity<TofuEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.TufoId, (tofuEntity) =>
                    {
                       NowTufoSerialId = tofuEntity.Id;
                       GameEntry.Entity.AttachEntity(tofuEntity, battenEntity);
                    }, TofuEntityData.Create(tufoPosition, Vector3.zero));
                },  BattenEntityData.Create(InitPosition, Vector3.zero, new Vector3(0, InitPosition.y, 0)));             
            }
        }
        protected override void OnUnload()
        {
            m_ELoader.Clear();
            base.OnUnload();
        }


        


    }
}
