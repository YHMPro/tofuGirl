

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
            GameEntry.Event.Subscribe(BattenCancleEventArgs.EventId, OnBattenCancle);
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
            //发送台阶构建成功事件
            GameEntry.Event.Fire(this, StairGenerateSuccessEventArgs.Create());
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
                GameEntry.Event.Unsubscribe(BattenCancleEventArgs.EventId, OnBattenCancle);
            }
            base.OnHide(isShutdown, userData);
        }

      
        public override void Pause(object userData)
        {
           
        }
        private void OnBattenCancle(object sender,GameEventArgs gEArgs)
        {
            BattenCancleEventArgs args = gEArgs as BattenCancleEventArgs;
            if(args==null)
            {
                return;
            }
            //平移移出屏幕后执行一下操作

            //解除子实体          
            GameEntry.Entity.DetachEntity(m_BindTofuId);
            //豆腐管理中弹出第一个  待考虑

            //隐藏子实体
            GameEntry.Entity.HideEntity(m_BindTofuId);
            //隐藏自身
            GameEntry.Entity.HideEntity(Entity);
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
