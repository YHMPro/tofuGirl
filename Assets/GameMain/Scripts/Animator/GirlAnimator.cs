using Spine;
using Spine.Unity;
using UnityEngine;
namespace Project.TofuGirl.Animator
{
    
    /// <summary>
    /// 女孩动画控制器
    /// </summary>
    public class GirlAnimator : GOAnimator
    {

        protected override void OnInit()
        {
            base.OnInit();

            //动画混合设置
        }

        protected override void OnTrackEntry_Start(TrackEntry trackEntry)
        {
            base.OnTrackEntry_Start(trackEntry);
            switch (trackEntry.Animation.Name)
            {
                case "up":
                    {
                        m_EntityLogic.SkeAnim.timeScale = 1 / (m_EntityLogic.Rig2D.velocity.magnitude / Physics2D.gravity.magnitude * 2);
                        break;
                    }
                case "idle":
                    {
                        m_EntityLogic.SkeAnim.timeScale = 1;
                        break;
                    }
                case "ready":
                    {
                        m_EntityLogic.SkeAnim.timeScale = 0.75f;
                        break;
                    }
                case "huang":
                    {
                        m_EntityLogic.SkeAnim.timeScale = 1.5f;
                        break;
                    }
                case "huang2":
                    {
                        m_EntityLogic.SkeAnim.timeScale = 1.5f;
                        break;
                    }
            }
        }
        protected override void OnTrackEntry_End(TrackEntry trackEntry)
        {
            base.OnTrackEntry_End(trackEntry);
            //switch (trackEntry.Animation.Name)
            //{
            //    case "up":
            //        {
            //            Debug.Log("up被中断");
            //            break;
            //        }
            //    case "ready":
            //        {
            //            Debug.Log("ready被中断");
            //            break;
            //        }
            //}
        }

        protected override void OnTrackEntry_Complete(TrackEntry trackEntry)
        {
            base.OnTrackEntry_Complete(trackEntry);
            switch (trackEntry.Animation.Name)
            {
                case "ready":
                    {
                        Play("idle",true);
                        break;
                    }                
                case "huang":
                    {
                        Play("idle", true);
                        break;
                    }
                case "huang2":
                    {
                        Play("idle", true);
                        break;
                    }
            }
        }   
        #region 废弃
        //public GirlAnimationAIData AIData => m_AIData as GirlAnimationAIData;
        //public GirlAnimationController(SkeletonAnimation anim) : base(anim)
        //{
        //    m_AIData = new GirlAnimationAIData();

        //}
        //public override void AnimationAI()
        //{
        //    if (AIData == null)
        //    {
        //        Debug.Log("动画AI数据无效");
        //        return;
        //    }
        //    if (!AIData.IsCollision)
        //    {
        //        SetAnimation("up", false);
        //        return;
        //    }
        //    float f = AIData.GirlPosition.x - AIData.TufuPosition.x;
        //    if (Mathf.Abs(f) < (AIData.TufuWith / 3))
        //    {
        //        //落在中间
        //        return;
        //    }
        //    if (Mathf.Abs(f) > (AIData.TufuWith / 2))
        //    {
        //        //落在外边
        //        SetAnimation("bad", false);
        //        return;
        //    }
        //    if (f > 0)
        //    {
        //        //播放左晃
        //        SetAnimation("huang", false);
        //    }
        //    else
        //    {
        //        //播放右晃
        //        SetAnimation("huang2", false);
        //    }
        //}

        //protected override void State_Start(TrackEntry trackEntry)
        //{
        //    float length = trackEntry.Animation.Duration;
        //    base.State_Start(trackEntry);
        //    switch (trackEntry.Animation.Name)
        //    {
        //        case "up":
        //            {
        //                m_Anim.timeScale =1/((AIData.GirlVelocity.magnitude / Physics2D.gravity.magnitude) * 2f * length);
        //                m_Anim.loop = false;
        //                break;
        //            }
        //        case "huang"://左晃
        //            {
        //                m_Anim.timeScale = 1;
        //                m_Anim.loop = false;
        //                break;
        //            }
        //        case "huang2"://右晃
        //            {
        //                m_Anim.timeScale = 1;
        //                m_Anim.loop = false;
        //                break;
        //            }
        //        case "fly"://飞
        //            {

        //                break;
        //            }
        //        case "bad":
        //            {

        //                break;
        //            }
        //        case "go":
        //            {

        //                break;
        //            }
        //        case "idle":
        //            {
        //                m_Anim.loop = true;
        //                m_Anim.timeScale = 1;
        //                break;
        //            }
        //        case "idle2":
        //            {

        //                break;
        //            }
        //    }

        //}

        //protected override void State_End(TrackEntry trackEntry)
        //{
        //    float length = trackEntry.Animation.Duration;

        //    base.State_End(trackEntry);
        //    switch (trackEntry.Animation.Name)
        //    {
        //        case "up"://跳起
        //            {

        //                break;
        //            }
        //        case "huang"://左晃
        //            {

        //                break;
        //            }
        //        case "huang2"://右晃
        //            {

        //                break;
        //            }
        //        case "fly":
        //            {

        //                break;
        //            }
        //        case "bad":
        //            {

        //                break;
        //            }
        //        case "go":
        //            {

        //                break;
        //            }
        //        case "idle":
        //            {

        //                break;
        //            }
        //        case "idle2":
        //            {

        //                break;
        //            }
        //    }
        //}
        //protected override void State_Complete(TrackEntry trackEntry)
        //{
        //    float length = trackEntry.Animation.Duration;

        //    base.State_Complete(trackEntry);
        //    switch (trackEntry.Animation.Name)
        //    {
        //        case "up"://跳起
        //            {
        //                SetAnimation("idle", true);
        //                break;
        //            }
        //        case "huang"://左晃
        //            {
        //                SetAnimation("idle", true);                    
        //                break;
        //            }
        //        case "huang2"://右晃
        //            {
        //                SetAnimation("idle", true);
        //                break;
        //            }
        //        case "fly":
        //            {

        //                break;
        //            }
        //        case "bad":
        //            {

        //                break;
        //            }
        //        case "go":
        //            {

        //                break;
        //            }
        //        case "idle":
        //            {
        //                break;
        //            }
        //        case "idle2":
        //            {
        //                break;
        //            }
        //    }      
        //}
        #endregion
    }
}
