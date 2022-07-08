

using UnityGameFramework.Runtime;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;
using Project.TofuGirl.Event;
namespace Project.TofuGirl.Entity
{
    public class BattenEntityLogic : GOEntityLogic
    {
        private BattenEntityData m_EntityData;
        private SpriteRenderer m_SRenderer;
        private event UnityAction m_MoveEvent;
        private int m_BindTufoId;
        /// <summary>
        /// 右点
        /// </summary>
        public Transform RightPoint { get; private set; }
        //左点
        public Transform LeftPoint { get; private set; }
        /// <summary>
        /// 移动类型
        /// </summary>
        public EnumBattenMove MoveType => m_EntityData.MoveType;
        /// <summary>
        /// 是否移动
        /// </summary>
        public bool Move { get; private set; }
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            RightPoint = transform.GetChild(0);
            LeftPoint = transform.GetChild(1);
            m_SRenderer = transform.GetChild(2).GetComponent<SpriteRenderer>();
            GameEntry.Event.Subscribe(BattenMoveEventArgs.EventId, OnBattenMove);
        }

        protected override void OnShow(object userData)
        {           
            base.OnShow(userData);
            m_EntityData = userData as BattenEntityData;
            if (m_EntityData==null)
            {
                return;
            }
            switch(m_EntityData.MoveType)
            {
                case EnumBattenMove.Left:
                    {
                        m_MoveEvent = LeftMove;
                        break;
                    }
                case EnumBattenMove.Right:
                    {
                        m_MoveEvent = RightMove;
                        break;
                    }
            }
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            if (Move)
            {
                m_MoveEvent?.Invoke();
                transform.position = Vector3.MoveTowards(transform.position, m_EntityData.AimPosition, 1.5f * elapseSeconds);//移动速度读取关卡配置表
            }
        }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
            m_BindTufoId = childEntity.Entity.Id;
            m_SRenderer.sortingOrder = (childEntity as TofuEntityLogic).OrderInLayer - 1;
            Move = true;
        }

        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
            Move = false;          
            GameEntry.Entity.HideEntity(Entity);            
        }

        protected override void OnHide(bool isShutdown, object userData)
        {   
            if(isShutdown)
            {
                GameEntry.Event.Unsubscribe(BattenMoveEventArgs.EventId, OnBattenMove);
            }
            base.OnHide(isShutdown, userData);
        }

        private void LeftMove()
        {
            if ((m_EntityData.AimPosition - LeftPoint.position).x > 0)
            {
                MoveCommon();
            }
        }

        private void RightMove()
        {
            if ((m_EntityData.AimPosition - RightPoint.position).x<0)
            {
                MoveCommon();
            }
        }

        private void  MoveCommon()
        {
            Move = false;
            //触发摆放完美事件(立即触发)
            GameEntry.Event.Fire(this, TofuPutEventArgs.Create(m_BindTufoId, true));
            //解除子实体          
            GameEntry.Entity.DetachEntity(m_BindTufoId);
            
        }
        private void OnBattenMove(object sender,GameEventArgs gEArgs)
        {
            BattenMoveEventArgs args = gEArgs as BattenMoveEventArgs;
            if (args == null)
            {
                return;
            }
            Move = args.Move;
        }

        public override void Pause(object userData)
        {
           
        }
         
        
       
    }
}
