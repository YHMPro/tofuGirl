

using GameFramework;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Entity;
using Spine;
using System.Collections.Generic;
namespace Project.TofuGirl.Animator
{   
    public class GOAnimator:Animator
    {
        protected GOAnimatorEntityLogic m_EntityLogic;

        private float m_PauseAnimTimeScale = 0;
        
        public sealed override void Init(object userData)
        {
            m_EntityLogic = userData as GOAnimatorEntityLogic;
            if(m_EntityLogic==null)
            {
                return;
            }
            m_PauseAnimTimeScale = m_EntityLogic.SkeAnim.timeScale;
            m_EntityLogic.SkeAnim.state.Start += TrackEntry_Start;
            m_EntityLogic.SkeAnim.state.End += TrackEntry_End;
            m_EntityLogic.SkeAnim.state.Complete += TrackEntry_Complete;
            OnInit();
        }

        public sealed override void Play(string animName, bool loop = false)
        {
            m_EntityLogic.SkeAnim.Skeleton.SetToSetupPose();
            m_EntityLogic.SkeAnim.state.ClearTrack(0);
            TrackEntry_Start(m_EntityLogic.SkeAnim.state.SetAnimation(0, animName, loop));
            OnPlay();
        }

        public sealed override void Reset()
        {
            m_EntityLogic.SkeAnim.Skeleton.SetToSetupPose();
            m_EntityLogic.SkeAnim.state.ClearTrack(0);
            OnReset();
        }

        private void TrackEntry_Start(TrackEntry trackEntry)
        {
            if (trackEntry != null)
            {
                OnTrackEntry_Start(trackEntry);
            }
        }

        private void TrackEntry_End(TrackEntry trackEntry)
        {
            if (trackEntry != null)
            {
                OnTrackEntry_End(trackEntry);
            }
        }

        private void TrackEntry_Complete(TrackEntry trackEntry)
        {        
            if (trackEntry != null)
            {
                OnTrackEntry_Complete(trackEntry);
            }
        }

        public sealed override void ContinuePlay()
        {
            m_EntityLogic.SkeAnim.timeScale = m_PauseAnimTimeScale;
            OnContinuePlay();
        }

        public sealed override void Pause()
        {
            if (m_EntityLogic.SkeAnim.timeScale==0)
            {
                return;
            }
            m_PauseAnimTimeScale = m_EntityLogic.SkeAnim.timeScale;
            m_EntityLogic.SkeAnim.timeScale = 0;
            OnPause();
        }

        public sealed override void SetSkin(string skinName)
        {
            m_EntityLogic.SkeAnim.skeleton.SetSkin(skinName);
            OnSetSkin();
        }
        protected virtual void OnInit()
        {

        }

        protected virtual void OnPlay()
        {

        }

        protected virtual void OnPause()
        {

        }
        
        protected virtual void OnContinuePlay()
        {

        }

        protected virtual void OnSetSkin()
        {

        }

        protected virtual void OnReset()
        {

        }
        protected virtual void OnTrackEntry_Start(TrackEntry trackEntry)
        {

        }

        protected virtual void OnTrackEntry_End(TrackEntry trackEntry)
        {

        }

        protected virtual void OnTrackEntry_Complete(TrackEntry trackEntry)
        {

        }

        //public void PlayAnim()
        //{
        //    m_Anim.state.TimeScale = 1;

        //}
        ///// <summary>
        ///// 设置皮肤(需要初始化才有用)
        ///// </summary>
        ///// <param name="skinName"></param>
        //protected virtual void SetSkin(string skinName)
        //{
        //    m_Anim.Skeleton.SetSkin(skinName);
        //    //m_Anim.initialSkinName = skinName;
        //}
        ///// <summary>
        ///// 设置动画
        ///// </summary>
        ///// <param name="animationName"></param>
        //protected virtual void SetAnimation(string animationName, bool loop)
        //{
        //    m_Anim.Skeleton.SetToSetupPose();
        //    m_Anim.state.ClearTrack(0);
        //    RegisterAnimationEvent(m_Anim.state.SetAnimation(0, animationName, loop));
        //}

        //#region 监听的动画状态事件
        //private void RegisterAnimationEvent(TrackEntry trackEntry)
        //{
        //    this.State_Start(trackEntry);
        //    if (!m_TrackEntryLi.Contains(trackEntry))
        //    {
        //        trackEntry.End += this.State_End;
        //        trackEntry.Complete += this.State_Complete;
        //    }
        //}
        ///// <summary>
        ///// 动画播放开始(重复播放不会触发)
        ///// </summary>
        ///// <param name="trackEntry"></param>
        //protected virtual void State_Start(TrackEntry trackEntry)
        //{
        //    Debug.Log(m_Anim.name + ":" + trackEntry.Animation.Name + "动画开始");
        //}
        ///// <summary>
        ///// 动画播放中断
        ///// </summary>
        ///// <param name="trackEntry"></param>
        //protected virtual void State_End(TrackEntry trackEntry)
        //{
        //    Debug.Log(m_Anim.name + ":" + trackEntry.Animation.Name + "动画中断");
        //}
        ///// <summary>
        ///// 动画播放完成
        ///// </summary>
        ///// <param name="trackEntry"></param>
        //protected virtual void State_Complete(TrackEntry trackEntry)
        //{
        //    Debug.Log(m_Anim.name + ":" + trackEntry.Animation.Name + "动画完成");
        //}
        //#endregion


        ///// <summary>
        ///// 动画AI
        ///// </summary>
        //public virtual void AnimationAI()
        //{

        //}
        //protected void RefreshAnim(UnityAction callback)
        //{

        //    m_Anim.Initialize(true, true);
        //    callback?.Invoke();
        //}

        //protected virtual void MixAnim(string fromAnimName, string toAnimName, float mixTime)
        //{
        //    //m_Anim.state.Data.SetMix(fromAnimName, toAnimName, mixTime);
        //}


    }
}
