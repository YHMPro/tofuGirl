

using Spine.Unity;
using UnityGameFramework.Runtime;
using UnityEngine;
using Project.TofuGirl.Animator;
namespace Project.TofuGirl.Entity
{
    public abstract class GOAnimatorEntityLogic : GOEntityLogic
    {
        private GOAnimatorEntityData m_EntityData;
        /// <summary>
        /// 状态控制器
        /// </summary>
        protected GOAnimator m_Anim;
        /// <summary>
        /// 骨骼动画
        /// </summary>
        public SkeletonAnimation SkeAnim { get; private set; }
        /// <summary>
        /// 碰撞盒子
        /// </summary>
        public BoxCollider2D Box2D { get; private set; }
        /// <summary>
        /// 2D刚体
        /// </summary>
        public Rigidbody2D Rig2D { get; private set; }


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            SkeAnim = GetComponentInChildren<SkeletonAnimation>(true);

            Box2D = GetComponent<BoxCollider2D>();

            Rig2D = GetComponent<Rigidbody2D>();

            if (m_Anim == null)
            {
                m_Anim = new GOAnimator();
            }
            m_Anim.Init(this);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_Anim.ContinuePlay();
            m_Anim.Reset();
            m_EntityData = userData as GOAnimatorEntityData;
            if(m_EntityData==null)
            {
                return;
            }        
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

        }
        public override void Pause(object userData)
        {
            Rig2D.constraints = RigidbodyConstraints2D.FreezeAll;
            m_Anim.Pause();
            OnPause();
        }

        protected virtual void OnPause()
        {

        }
    }
}
