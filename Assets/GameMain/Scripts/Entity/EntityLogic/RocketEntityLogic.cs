

using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.Events;
using Project.TofuGirl.Event;
using DG.Tweening;
using GameFramework.Event;
namespace Project.TofuGirl.Entity
{
    public class RocketEntityLogic : GOAnimatorEntityLogic
    {
        private RocketEntityData m_EntityData;
        private UnityAction<float, float> m_MoveAction;
        private int m_BindGirlEntityId;
        private int m_BindCameraEntityId;
        public float Speed => m_EntityData.Speed;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            GameEntry.Event.Subscribe(RocketTriggerStairGenerateEventArgs.EventId, OnRocketTriggerStairGenerate);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            Rig2D.isKinematic = true;
            m_EntityData = userData as RocketEntityData;
            if(m_EntityData==null)
            {
                return;
            }
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            if(isShutdown)
            {
                GameEntry.Event.Unsubscribe(RocketTriggerStairGenerateEventArgs.EventId, OnRocketTriggerStairGenerate);

            }
            base.OnHide(isShutdown, userData);

        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_MoveAction?.Invoke(elapseSeconds, realElapseSeconds);
        }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
            if(childEntity is GirlEntityLogic)
            {
                m_BindGirlEntityId = childEntity.Entity.Id;
                //开启第一阶段动画
                m_Anim.Play("animation", true);
                //女孩挂起一定高度
                childEntity.transform.AddLocalPositionY(0.3f);
                //两秒后起飞
                GameEntry.Coroutine.Delay(2f, () =>
                {
                    //开启第二阶段动画
                    m_Anim.Play("animation2");
                    //开启台阶构建
                    GameEntry.Event.Fire(this, OpenStairGenerateEventArgs.Create());
                    //开启相机移动
                    //GameEntry.Event.Fire(this, RocketTriggerCameraMoveEventArgs.Create());
                    //开启飞行
                    m_MoveAction = Move;
                });
            }
            if(childEntity is CameraEntityLogic)
            {
                m_BindCameraEntityId = childEntity.Entity.Id;
            }
        }

        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
            m_MoveAction = null;
        }

        private void OnRocketTriggerStairGenerate(object sender,GameEventArgs gEArgs)
        {
            RocketTriggerStairGenerateEventArgs args = gEArgs as RocketTriggerStairGenerateEventArgs;
            if(args==null)
            {
                return;
            }
            if(args.RocketTriggerTofuNum>= m_EntityData.TofuNum)
            {
                m_MoveAction = null;
                //关闭台阶构建
                GameEntry.Event.Fire(this, CloseStairGenerateEventArgs.Create());
                //解除女孩子实体          
                GameEntry.Entity.DetachEntity(m_BindGirlEntityId);
                //解除相机子实体
                GameEntry.Entity.DetachEntity(m_BindCameraEntityId);
                //往右抛出两秒后回收
                Rig2D.isKinematic = false;
                Rig2D.velocity = new Vector3(3f, 1, 0);
                GameEntry.Coroutine.Delay(1.75f, () =>
                {
                    GameEntry.Entity.HideEntity(Entity);
                });
            }
        }
        private void Move(float elapseSeconds, float realElapseSeconds)//0.6999999   20  
        {
            transform.position = Vector3.MoveTowards(transform.position, m_EntityData.AimPosition, m_EntityData.Speed * realElapseSeconds);//移动速度读取关卡配置表
            //if (m_EntityData.AimPosition == transform.position)
            //{
            //    m_MoveAction = null;
            //    //关闭台阶构建
            //    GameEntry.Event.Fire(this, CloseStairGenerateEventArgs.Create());
            //    //解除女孩子实体          
            //    GameEntry.Entity.DetachEntity(m_BindGirlEntityId);
            //    //解除相机子实体          
            //    GameEntry.Entity.DetachEntity(m_BindCameraEntityId);
            //    //往右抛出两秒后回收
            //    Rig2D.isKinematic = false;
            //    Rig2D.velocity = new Vector3(3f, 1, 0);
            //    GameEntry.Coroutine.Delay(1.75f, () =>
            //    {
            //        GameEntry.Entity.HideEntity(Entity);
            //    });
            //}
        }
    }
}
