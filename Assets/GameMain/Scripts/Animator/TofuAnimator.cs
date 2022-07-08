
using Spine;
using Spine.Unity;
using UnityEngine;
namespace Project.TofuGirl.Animator
{
    
    /// <summary>
    /// 豆腐动画控制器
    /// </summary>
    public class TofuAnimator: GOAnimator
    {

        protected override void OnTrackEntry_Start(TrackEntry trackEntry)
        {
            base.OnTrackEntry_Start(trackEntry);

            switch (trackEntry.Animation.Name)
            {
                case "up":
                    {
                        m_EntityLogic.SkeAnim.loop = false;
                        m_EntityLogic.SkeAnim.timeScale = 2.5f;
                        break;
                    }
            }
        }
        protected override void OnTrackEntry_End(TrackEntry trackEntry)
        {
            base.OnTrackEntry_End(trackEntry);
        }
        protected override void OnTrackEntry_Complete(TrackEntry trackEntry)
        {
            base.OnTrackEntry_Complete(trackEntry);
            switch (trackEntry.Animation.Name)
            {
                case "up":
                    {
                        m_EntityLogic.SkeAnim.timeScale =1f;
                        break;
                    }
            }
        }
      
        #region 废弃
        /// <summary>
        /// 豆腐动画AI数据
        /// </summary>
        //public TufuAnimationAIData AIData => m_AIData as TufuAnimationAIData;
        //public TofuAnimatorController(SkeletonAnimation anim) : base(anim)
        //{
        //    m_AIData = new TufuAnimationAIData();
        //}

        //public override void AnimationAI()
        //{
        //    if (AIData == null)
        //    {
        //        Debug.Log("动画AI数据无效");
        //        return;
        //    }
        //    float f = AIData.GirlPosition.x - AIData.TufuPosition.x;
        //    if (Mathf.Abs(f) < (AIData.TufuWith / 3))
        //    {
        //        播放中间变形动画
        //        SetAnimation("up", false);
        //        return;
        //    }
        //    if (f > 0)
        //    {
        //        播放左边变形动画
        //        SetAnimation("up2", false);
        //    }
        //    else
        //    {
        //        播放右边变形动画
        //        SetAnimation("up3", false);
        //    }

        //}
        #endregion
    }
}
