

using UnityEngine.Events;
using UnityEngine;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Event;
namespace Project.TofuGirl.Entity
{
    public class CameraEntityLogic : GOEntityLogic
    {
        private CameraEntityData m_EntityData;

        private event UnityAction<float,float> m_FollowAction;

        private Vector3 m_AimPosition;

        /// <summary>
        /// 是否移动
        /// </summary>
        public bool Move { get; private set; }
        /// <summary>
        /// 自身相机
        /// </summary>
        public Camera SelfCamera { get; private set; }
        /// <summary>
        /// 静态相机
        /// </summary>
        public Camera StaticCamera { get; private set; }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            SelfCamera = GetComponent<Camera>();
            StaticCamera = new GameObject("StaticCamera").AddComponent<Camera>();
            GameEntry.Event.Subscribe(CameraFollowEventArgs.EventId, OnCameraFollow);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_FollowAction = null;
            SelfCamera.orthographicSize = 7;
            StaticCamera.orthographicSize = 7;
            StaticCamera.transform.position = transform.position;
            StaticCamera.clearFlags = CameraClearFlags.SolidColor;
            StaticCamera.cullingMask = 1;
            StaticCamera.orthographic = true;
            StaticCamera.orthographicSize = SelfCamera.orthographicSize;
            StaticCamera.depth = SelfCamera.depth - 1;
            m_EntityData = userData as CameraEntityData;        
            if(m_EntityData==null)
            {
                return;
            }        
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_FollowAction?.Invoke(elapseSeconds, realElapseSeconds);         
        }     
        protected override void OnHide(bool isShutdown, object userData)
        {
            if (isShutdown)
            {
                GameEntry.Event.Unsubscribe(CameraFollowEventArgs.EventId, OnCameraFollow);
            }
            base.OnHide(isShutdown, userData);
        }
        public override void Pause(object userData)
        {
            
        }
        private void OnCameraFollow(object sender,GameEventArgs gEArgs)
        {
            CameraFollowEventArgs args = gEArgs as CameraFollowEventArgs;
            if(args==null)
            {
                return;
            }
            m_AimPosition = args.AimPosition;
            m_AimPosition.z = SelfCamera.transform.position.z;
            if (sender is GirlEntityLogic)
            {
                if(m_AimPosition.y<= transform.position.y)
                {
                    return;
                }
                m_FollowAction = (elapseSeconds, realElapseSeconds) =>
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_AimPosition, 5 * elapseSeconds);
                    if ((transform.position - m_AimPosition).y >= 0)
                    {
                        transform.position = m_AimPosition;
                        m_FollowAction = null;
                    }
                };
                return;
            }
            if(sender is GameManager)
            {
                //判断一次女孩是否死亡(严谨的话)
                Vector3 selfPosition = Vector3.zero;
                float screenRate= (float)Screen.width / Screen.height;
                m_FollowAction = (elapseSeconds, realElapseSeconds) =>
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_AimPosition, 4 * elapseSeconds);
                    SelfCamera.orthographicSize += 6 * elapseSeconds;
                    selfPosition = transform.position;
                    selfPosition.y -= SelfCamera.orthographicSize / screenRate/ 2;
                    if ((selfPosition - m_AimPosition).y <= 0)
                    {
                        selfPosition = m_AimPosition;
                        selfPosition.y+= SelfCamera.orthographicSize / screenRate / 2;
                        transform.position = selfPosition;
                        m_FollowAction = null;
                        //触发游戏结束事件
                        GameEntry.Event.Fire(this, GameOverEventArgs.Create());
                    }
                };
                return;
            }

        }   
    }
}
