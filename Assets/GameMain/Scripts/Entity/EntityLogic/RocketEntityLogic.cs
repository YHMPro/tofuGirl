

using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.Events;
using Project.TofuGirl.Event;
namespace Project.TofuGirl.Entity
{
    public class RocketEntityLogic : GOAnimatorEntityLogic
    {
        private RocketEntityData m_EntityData;
        private UnityAction<float, float> m_MoveAction;
        private int m_BindGirlEntityId;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_EntityData = userData as RocketEntityData;
            if(m_EntityData==null)
            {
                return;
            }
            m_BindGirlEntityId = (GameEntry.Procedure.CurrentProcedure as ProcedureLevel).GM.GirlSerialId;
            GameEntry.Entity.AttachEntity(m_BindGirlEntityId, Entity);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_MoveAction?.Invoke(elapseSeconds, realElapseSeconds);
        }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
            //女孩挂起一定高度
            childEntity.transform.AddLocalPositionY(0.2f);
            //两秒后起飞
            GameEntry.Coroutine.Delay(2f, () =>
            {
                //开启台阶构建
                GameEntry.Event.Fire(this, UpdateStairGenerateEventArgs.Create(true));
                //开启相机移动
                GameEntry.Event.Fire(this, UpdateCameraFollowInfoEventArgs.Create(EnumCameraFollow.Rocket));
                //开启飞行
                m_MoveAction = Move;
            });
            
        }

        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
            m_MoveAction = null;
        }


        private void Move(float elapseSeconds, float realElapseSeconds)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_EntityData.AimPosition, m_EntityData.Speed * elapseSeconds);//移动速度读取关卡配置表
            if (m_EntityData.AimPosition == transform.position)
            {
                m_MoveAction = null;
                //解除子实体          
                GameEntry.Entity.DetachEntity(m_BindGirlEntityId);
            }
        }
    }
}
