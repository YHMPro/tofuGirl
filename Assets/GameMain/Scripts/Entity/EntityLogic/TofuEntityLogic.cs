using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.TofuGirl.Animator;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Event;
using GameFramework.Event;
namespace Project.TofuGirl.Entity
{
    public class TofuEntityLogic : GOAnimatorEntityLogic
    {
        private TofuEntityData m_EntityData;
        private bool m_Tread;
        /// <summary>
        /// 排序层级
        /// </summary>
        public int OrderInLayer => m_EntityData.OrderInLayer;
        /// <summary>
        /// 是否完美
        /// </summary>
        public bool Prefect { get; private set; }
        /// <summary>
        /// 上一个豆腐的Id
        /// </summary>
        public int PrevId => m_EntityData.PrevId;
        protected override void OnInit(object userData)
        {
            m_Anim = new TofuAnimator();
            base.OnInit(userData);

            GameEntry.Event.Subscribe(TofuPutEventArgs.EventId, OnTofuPut);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_Tread = false;
            m_EntityData = userData as TofuEntityData;
            if(m_EntityData==null)
            {
                return;
            }            
            SkeAnim.GetComponent<MeshRenderer>().sortingOrder = m_EntityData.OrderInLayer;

            //更新当前豆腐Id事件
            GameEntry.Event.Fire(this, UpdateNowTofuSerialldEventArgs.Create(Entity.Id));
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
            switch ((parentEntity as BattenEntityLogic).MoveType)
            {
                case EnumBattenMove.Left:
                    {
                        transform.position = (parentEntity as BattenEntityLogic).LeftPoint.position;
                        break;
                    }
                case EnumBattenMove.Right:
                    {
                        transform.position = (parentEntity as BattenEntityLogic).RightPoint.position;
                        break;
                    }
            }        
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            if(isShutdown)
            {

            }
            //将动画重置
            m_Anim.Play("up");
            base.OnHide(isShutdown, userData);
        }
        public void PrefectAnim()
        {
            m_Anim.Play("up");
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {        
            if ((!m_Tread)&&collision.relativeVelocity.magnitude > 1f)//防止生成时的碰撞导致播放抖动动画
            {
                CollisionLogic(collision.transform.position);
            }
            m_Tread = true;
            //更新顶部豆腐Id事件
            GameEntry.Event.Fire(this, UpdateTopTofuSerialldEventArgs.Create(Entity.Id));     
            //解除自身的父级实体           
            GameEntry.Entity.DetachEntity(Entity);         
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            m_Tread = false;
        }

        #region 碰撞判定
        private void CollisionLogic(Vector3 referPos)
        {
            //判断碰撞时女孩相对于豆腐的位置
            float f = (referPos - transform.position).x;
            if (Mathf.Abs(f) < (Box2D.size.x / 3))
            {
                //播放中间变形动画
                m_Anim.Play("up");
                if(Mathf.Abs(f) < (Box2D.size.x / 100))
                {
                   int toFuEntityId = m_EntityData.PrevId;
                   TofuEntityLogic tofuEntityLogic;
                    while (GameEntry.Entity.HasEntity(toFuEntityId))
                    {                      
                        tofuEntityLogic = GameEntry.Entity.GetEntity(toFuEntityId).Logic as TofuEntityLogic;
                        if (!tofuEntityLogic.Prefect)
                        {
                            break;
                        }
                        tofuEntityLogic.PrefectAnim();
                        toFuEntityId = tofuEntityLogic.PrevId;
                    }                               
                }             
            }
            else
            {
                if (f > 0)
                {
                    //播放左边变形动画
                    m_Anim.Play("up2");
                }
                else
                {
                    //播放右边变形动画
                    m_Anim.Play("up3");
                }
            }        
        }

        private void OnTofuPut(object sender,GameEventArgs gEArgs)
        {
            TofuPutEventArgs args = gEArgs as TofuPutEventArgs;
            if (args == null)
            {
                return;
            }
            if(args.EntityId!=Entity.Id)
            {
                return;
            }
            Log.Info("摆放完美:{0}", Entity.Id);
            Vector3 pos = transform.position;
            pos.x = 0;
            transform.position = pos;
            Prefect = args.Prefect;
        }
        #endregion
        protected override void OnPause()
        {
            base.OnPause();

        }


    }
}
