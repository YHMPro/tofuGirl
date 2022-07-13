
using UnityEngine;
using Spine.Unity;
using Project.TofuGirl.Animator;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using Project.TofuGirl.Event;
namespace Project.TofuGirl.Entity
{
    public class GirlEntityLogic : GOAnimatorEntityLogic
    {
        private GirlEntityData m_EntityData;

        /// <summary>
        /// 绑定火箭
        /// </summary>
        private bool m_BindRocket = false;
        /// <summary>
        /// 是否死亡
        /// </summary>
        public bool Died { get; private set; }

        private bool m_Jump;

        protected override void OnInit(object userData)
        {
            m_Anim = new GirlAnimator();
            base.OnInit(userData);
            GameEntry.Event.Subscribe(GirlBridgeDataChangeEventArgs.EventId, OnGirlBridgeDataChange);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_Anim.Play("idle",true);
            m_Jump = false;
            Died = false;
            Rig2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            m_EntityData = userData as GirlEntityData;
            if(m_EntityData==null)
            {
                return;
            }
            Rig2D.gravityScale = m_EntityData.Gravity;
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
           
            m_BindRocket = true;
            Rig2D.isKinematic = true;
            m_Jump = true;
            m_Anim.Play("fly",true);
        }

        protected override void OnDetachFrom(EntityLogic parentEntity, object userData)
        {
            base.OnDetachFrom(parentEntity, userData);
            Rig2D.isKinematic = false;
            //添加一个速度
            Rig2D.velocity = new Vector3(0, (parentEntity as RocketEntityLogic).Speed, 0);
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            //暂时先这样写              
            if ((!m_Jump) && Input.GetKeyDown(KeyCode.Space))
            {
                m_Jump = true;
                Rig2D.velocity = new Vector3(0, m_EntityData.Speed, 0);//后续读取关卡配置数据
                m_Anim.Play("up");
            }
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            if(isShutdown)
            {               
                GameEntry.Event.Unsubscribe(GirlBridgeDataChangeEventArgs.EventId, OnGirlBridgeDataChange);
            }
            base.OnHide(isShutdown, userData);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(Died)
            {
                return;
            }
            //float d = (transform.position - collision.transform.position).y- (collision.collider as BoxCollider2D).size.y;
            //if (d< 0.01f)
            //{
            //    //Died = true;
            //    //Rig2D.constraints = RigidbodyConstraints2D.None;
            //    //Rig2D.velocity = ((((transform.position - collision.transform.position).normalized).x * Vector2.right) + Vector2.up) * 5f;
            //    //m_Anim.Play("bad");

            //    //GameEntry.Event.Fire(this, GirlDiedEventArgs.Create());//派发角色死亡事件
            //    //GameEntry.Event.Fire(this, UpdateBattenMoveInfoEventArgs.Create(false));
            //    //GameEntry.Event.Fire(this, UpdateCameraFollowInfoEventArgs.Create(EnumSender.Stage));
            //    //GameEntry.Coroutine.Delay(0.35f, () =>
            //    //{
            //    //    m_Anim.Pause();
            //    //    Rig2D.constraints = RigidbodyConstraints2D.FreezeAll;
            //    //});          
            //    return;
            //}
            if(m_BindRocket)
            {
                m_BindRocket = false;
                //派发火箭与女孩解除事件
                GameEntry.Event.Fire(this, RocketWithGirlDetachEventArgs.Create());
            }
            if (m_Jump&&collision.relativeVelocity.magnitude > 1)
            {
                m_Jump = false;
                CollisionLogic(collision.transform.position, collision.collider as BoxCollider2D);                                            
                GameEntry.Event.Fire(this, GirlTriggerCameraMoveEventArgs.Create(EnumGirlTriggerCameraMove.Jump));
                TofuEntityLogic logic = collision.gameObject.GetComponent<TofuEntityLogic>();
                if(logic!=null)
                {
                    switch(logic.TofuType)
                    {
                        case EnumTofu.DaoJu:
                            {
                                //目前只考虑火箭
                                GameEntry.Event.Fire(this, RocketWithGirlBindEventArgs.Create());
                                //GameEntry.Event.Fire(this, ShieldWithGirlBindEventArgs.Create());
                                //依照概率进行分配
                                break;
                            }
                      
                    }
                }
            }
        }
        #region 碰撞判定
        private void CollisionLogic(Vector3 referPos,BoxCollider2D referBox2D)
        {
            float f = (transform.position-referPos).x;
            if (Mathf.Abs(f) < (referBox2D.size.x / 3))
            {
                //播放中间变形动画
                m_Anim.Play("ready");
            }
            else
            {
                if (f > 0)
                {
                    //播放左边变形动画
                    m_Anim.Play("huang");
                }
                else
                {
                    //播放右边变形动画
                    m_Anim.Play("huang2");
                }
            }
        }
        #endregion
        protected override void OnPause()
        { 
            base.OnPause();

        }

        private void OnGirlBridgeDataChange(object sender,GameEventArgs gEArgs)
        {
            GirlBridgeDataChangeEventArgs args = gEArgs as GirlBridgeDataChangeEventArgs;
            if(args==null)
            {
                return;
            }
            m_EntityData = GirlEntityData.Create(args.BirdgeData);//修改女孩的实体数据
            Log.Info("女孩实体数据修改");
        }

        #region 事件回调
        private void OnGirlEntityDataChange(object sender,GirlEntityDataUpdateEventArgs args)
        {
            if(args!=null)
            {
                return;
            }
            Log.Info("女孩实体数据更新");
            m_EntityData = args.EntityData;
        }
        #endregion
    }
}
