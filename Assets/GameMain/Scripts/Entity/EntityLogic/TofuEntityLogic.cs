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
        /// <summary>
        /// 豆腐类型
        /// </summary>
        public EnumTofu TofuType => m_EntityData.TofuType;
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
            Prefect=false;
            m_EntityData = userData as TofuEntityData;
            if(m_EntityData==null)
            {
                return;
            }
            if(m_EntityData.FirstTofu)
            {
                Prefect =true;
                //更新顶部豆腐Id
                GameEntry.Event.Fire(this, TopTofuIdUpdateEventArgs.Create(Entity.Id));
            }
            switch (m_EntityData.TofuType)
            {
                case EnumTofu.DaoJu: 
                    {
                        m_Anim.SetSkin("2");
                        break;
                    }
                default:
                    {
                        m_Anim.SetSkin("1");
                        break;
                    }
            }
            SkeAnim.GetComponent<MeshRenderer>().sortingOrder = m_EntityData.OrderInLayer;
            //更新当前豆腐Id事件
            GameEntry.Event.Fire(this, NowTofuIdUpdateEventArgs.Create(Entity.Id));
        }

        protected override void OnDetachFrom(EntityLogic parentEntity, object userData)
        {
            base.OnDetachFrom(parentEntity, userData);
            //设置自己的层级
            gameObject.SetLayerRecursively(9);
        }
        protected override void OnHide(bool isShutdown, object userData)
        {
            if(isShutdown)
            {
               GameEntry.Event.Unsubscribe(TofuPutEventArgs.EventId, OnTofuPut);
            }
            base.OnHide(isShutdown, userData);
        }
        public void PrefectAnim()
        {
            m_Anim.Play("up");
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {        
            if(m_Tread)
            {
                return;
            }
            if(collision.collider.CompareTag("Shield"))
            {
                
                return;
            }
            if (collision.relativeVelocity.magnitude > 1f)//防止生成时的碰撞导致播放抖动动画
            {
                CollisionLogic(collision.transform.position);
            }           
            m_Tread = true;
            //更新顶部豆腐Id
            GameEntry.Event.Fire(this, TopTofuIdUpdateEventArgs.Create(Entity.Id));
            //解除自身的父级实体           
            GameEntry.Entity.DetachEntity(Entity);
            //GameEntry.Event.Fire(this, TofuWithGirlCollisionEventArgs.Create());
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            m_Tread = false;
        }

       

        #region 碰撞判定
        private void CollisionLogic(Vector3 girlPosition)
        {
            float selfX = transform.position.x;
            float girlX = girlPosition.x;
            float disX = girlX - selfX;

            if(Mathf.Abs(disX)< (Box2D.size.x / 3))
            {
                m_Anim.Play("up");
                if(disX==0)
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
                m_Anim.Play((disX>0)?"up2":"up3");
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
            Log.Info("摆放完美TofuId{0}", Entity.Id);
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
