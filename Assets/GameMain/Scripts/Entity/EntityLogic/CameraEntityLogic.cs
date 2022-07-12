

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

        private float m_Speed = 0;
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
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_FollowAction = null;
            SelfCamera.orthographicSize = 5;
            StaticCamera.orthographicSize = 5;
            StaticCamera.transform.position = transform.position;
            StaticCamera.clearFlags = CameraClearFlags.SolidColor;            
            StaticCamera.cullingMask = LayerMask.GetMask("StaticCamera");
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
            }
            base.OnHide(isShutdown, userData);
        }
        public override void Pause(object userData)
        {
            
        }
        public void GirlTriggerCameraMove(float delayTime, float speed,Vector3 aimPosition,EnumGirlTriggerCameraMove triggerType)
        {
            GameEntry.Coroutine.Delay(delayTime, () =>
            {
                m_Speed = speed;
                m_AimPosition = aimPosition;
                switch (triggerType)
                {
                    case EnumGirlTriggerCameraMove.Jump:
                        {
                            m_FollowAction = (elapseSeconds, realElapseSeconds) =>
                            {
                                transform.position = Vector3.MoveTowards(transform.position, m_AimPosition, m_Speed * realElapseSeconds);
                                if (transform.position == m_AimPosition)
                                {
                                    m_FollowAction = null;
                                    //触发豆腐回收事件
                                    Vector3 selfPosition = Vector3.zero;
                                    float screenRate = (float)Screen.width / Screen.height;
                                    selfPosition = transform.position;
                                    selfPosition.y -= SelfCamera.orthographicSize / screenRate / 2;
                                    GameEntry.Event.Fire(this, TofuRecycleEventArgs.Create(selfPosition));
                                }
                            };
                            break;
                        }
                    case EnumGirlTriggerCameraMove.Died:
                        {
                            Vector3 selfPosition = Vector3.zero;
                            float screenRate = (float)Screen.width / Screen.height;
                            m_FollowAction = (elapseSeconds, realElapseSeconds) =>
                            {
                                transform.position = Vector3.MoveTowards(transform.position, m_AimPosition, m_Speed * realElapseSeconds);
                                SelfCamera.orthographicSize += (m_Speed + 2) * realElapseSeconds;
                                selfPosition = transform.position;
                                selfPosition.y -= SelfCamera.orthographicSize / screenRate / 2;
                                if ((selfPosition - m_AimPosition).y <= 0)
                                {
                                    selfPosition = m_AimPosition;
                                    selfPosition.y += SelfCamera.orthographicSize / screenRate / 2;
                                    transform.position = selfPosition;
                                    m_FollowAction = null;
                                    //触发游戏结束事件
                                    GameEntry.Event.Fire(this, GameOverEventArgs.Create());
                                }
                            };
                            break;
                        }
                }
            });
        }

        public void RocketTriggerCameraMove(float delayTime, float speed, Vector3 aimPosition)
        {
            m_Speed = speed;
            m_AimPosition = aimPosition;
            GameEntry.Coroutine.Delay(delayTime, () => 
            {
                m_FollowAction = (elapseSeconds, realElapseSeconds) =>
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_AimPosition, m_Speed * realElapseSeconds);
                    if (transform.position == m_AimPosition)
                    {
                        m_FollowAction = null;
                        Vector3 selfPosition = Vector3.zero;
                        float screenRate = (float)Screen.width / Screen.height;
                        selfPosition = transform.position;
                        selfPosition.y -= SelfCamera.orthographicSize / screenRate / 2;
                        //触发豆腐回收事件
                        GameEntry.Event.Fire(this, TofuRecycleEventArgs.Create(selfPosition));
                    }
                };
            });
        }

      
    }
}
