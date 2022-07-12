

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
        private event UnityAction<float,float> m_MoveAction;
        /// <summary>
        /// 与之绑定的豆腐Id
        /// </summary>
        private int m_BindTofuId;    
        /// <summary>
        /// 移动类型
        /// </summary>
        public EnumBattenMove MoveType => m_EntityData.MoveType;     
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_SRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            GameEntry.Event.Subscribe(SetBattenMoveInfoEventArgs.EventId, OnBattenMoveInfo);
        }

        protected override void OnShow(object userData)
        {           
            base.OnShow(userData);
            m_EntityData = userData as BattenEntityData;
            if (m_EntityData==null)
            {
                return;
            }         
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_MoveAction?.Invoke(elapseSeconds, realElapseSeconds);
        }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
            m_BindTofuId = childEntity.Entity.Id;
            m_SRenderer.sortingOrder = (childEntity as TofuEntityLogic).OrderInLayer - 1;
            m_MoveAction = Move;
        }

        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
            GameEntry.Entity.HideEntity(Entity);
            m_MoveAction = null;
        }

        protected override void OnHide(bool isShutdown, object userData)
        {   
            if(isShutdown)
            {
                GameEntry.Event.Unsubscribe(SetBattenMoveInfoEventArgs.EventId, OnBattenMoveInfo);
            }
            base.OnHide(isShutdown, userData);
        }

        private void OnBattenMoveInfo(object sender, GameEventArgs gEArgs)
        {
            SetBattenMoveInfoEventArgs args = gEArgs as SetBattenMoveInfoEventArgs;
            if (args == null)
            {
                return;
            }
            if (args.Move)
            {
                m_MoveAction = Move;
            }
            else
            {
                m_MoveAction = null;
            }
            Log.Info(1);
        }
        public override void Pause(object userData)
        {
           
        }
    
        private void Move(float elapseSeconds, float realElapseSeconds)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_EntityData.AimPosition, m_EntityData.Speed * realElapseSeconds);//移动速度读取关卡配置表
            if (m_EntityData.AimPosition == transform.position)
            {
                m_MoveAction = null;
                //更新顶部豆腐Id
                GameEntry.Event.Fire(this, TopTofuIdUpdateEventArgs.Create(m_BindTofuId));
                //触发摆放完美事件
                GameEntry.Event.Fire(this, TofuPutEventArgs.Create(m_BindTofuId, true));
                //解除子实体          
                GameEntry.Entity.DetachEntity(m_BindTofuId);
            }
        }
    }
}
