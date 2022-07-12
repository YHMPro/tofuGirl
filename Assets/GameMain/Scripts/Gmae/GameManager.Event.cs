


using GameFramework.Event;
using Project.TofuGirl.Event;
using UnityEngine;
using Project.TofuGirl.Entity;
using UnityGameFramework.Runtime;
namespace Project.TofuGirl
{
    /// <summary>
    /// 游戏管理器事件(监听或派发)
    /// </summary>
    public partial class GameManager
    {
        private void GMEventSubscribe()
        {
            //火箭与女孩的绑定或解除
            GameEntry.Event.Subscribe(RocketWithGirlBindEventArgs.EventId, OnRocketWithGirlBind);
            GameEntry.Event.Subscribe(RocketWithGirlDetachEventArgs.EventId, OnRocketWithGirlDetach);
            //女孩触发相机移动
            GameEntry.Event.Subscribe(GirlTriggerCameraMoveEventArgs.EventId, OnGirlTriggerCameraMove);
            //火箭触发相机移动
            GameEntry.Event.Subscribe(RocketTriggerCameraMoveEventArgs.EventId, OnRocketTriggerCameraMove);
            //顶部豆腐Id更新
            GameEntry.Event.Subscribe(TopTofuIdUpdateEventArgs.EventId, OnTopTofuIdUpdate);
            //当前豆腐Id更新
            GameEntry.Event.Subscribe(NowTofuIdUpdateEventArgs.EventId, OnNowTofuIdUpdate);
            //开启台阶构建
            GameEntry.Event.Subscribe(OpenStairGenerateEventArgs.EventId, OnOpenStairGenerate);
            //关闭台阶构建
            GameEntry.Event.Subscribe(CloseStairGenerateEventArgs.EventId, OnCloseStairGenerate);

        }

        private void GMEventUnsubscribe()
        {
            //火箭与女孩的绑定或解除
            GameEntry.Event.Unsubscribe(RocketWithGirlBindEventArgs.EventId, OnRocketWithGirlBind);
            GameEntry.Event.Unsubscribe(RocketWithGirlDetachEventArgs.EventId, OnRocketWithGirlDetach);
            //女孩触发相机移动
            GameEntry.Event.Unsubscribe(GirlTriggerCameraMoveEventArgs.EventId, OnGirlTriggerCameraMove);
            //火箭触发相机移动
            GameEntry.Event.Unsubscribe(RocketTriggerCameraMoveEventArgs.EventId, OnRocketTriggerCameraMove);
            //顶部豆腐Id更新
            GameEntry.Event.Unsubscribe(TopTofuIdUpdateEventArgs.EventId, OnTopTofuIdUpdate);
            //当前豆腐Id更新
            GameEntry.Event.Unsubscribe(NowTofuIdUpdateEventArgs.EventId, OnNowTofuIdUpdate);
            //开启台阶构建
            GameEntry.Event.Unsubscribe(OpenStairGenerateEventArgs.EventId, OnOpenStairGenerate);
            //关闭台阶构建
            GameEntry.Event.Unsubscribe(CloseStairGenerateEventArgs.EventId, OnCloseStairGenerate);

        }
        #region 监听台阶开启或关闭构建事件
        private void OnOpenStairGenerate(object sneder, GameEventArgs gEArgs)
        {
            OpenStairGenerateEventArgs args = gEArgs as OpenStairGenerateEventArgs;
            if (args == null)
            {
                return;
            }
            m_StairGenerate = true;
            Debug.Log("开启构建");
        }
        private void OnCloseStairGenerate(object sneder, GameEventArgs gEArgs)
        {
            CloseStairGenerateEventArgs args = gEArgs as CloseStairGenerateEventArgs;
            if (args == null)
            {
                return;
            }
            m_StairGenerate = false;
            Debug.Log("关闭构建");
        }
        #endregion

        #region 监听豆腐Id更新事件
        /// <summary>
        /// 顶部豆腐Id更新事件
        /// </summary>
        /// <param name="sneder"></param>
        /// <param name="gEArgs"></param>
        private void OnTopTofuIdUpdate(object sneder,GameEventArgs gEArgs)
        {
            TopTofuIdUpdateEventArgs args = gEArgs as TopTofuIdUpdateEventArgs;
            if(args==null)
            {
                return;
            }
            if(TopTofuSerialId== args.EntityId)
            {
                return;
            }
            Log.Info("顶部豆腐更新:Entity{0}====>{1}", TopTofuSerialId, args.EntityId);
            TopTofuSerialId = args.EntityId;
        }
        /// <summary>
        /// 监听当前豆腐Id更新事件
        /// </summary>
        /// <param name="sneder"></param>
        /// <param name="gEArgs"></param>
        private void OnNowTofuIdUpdate(object sneder, GameEventArgs gEArgs)
        {
            NowTofuIdUpdateEventArgs args = gEArgs as NowTofuIdUpdateEventArgs;
            if(args==null)
            {
                return;
            }
            if (NowTofuSerialId == args.EntityId)
            {
                return;
            }
            Log.Info("当前豆腐更新:Entity{0}====>{1}", NowTofuSerialId, args.EntityId);
            NowTofuSerialId = args.EntityId;
        }
        #endregion

        #region 监听女孩触发相机移动事件
        /// <summary>
        /// 监听女孩触发相机移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gEArgs"></param>
        private void OnGirlTriggerCameraMove(object sender, GameEventArgs gEArgs)
        {
            GirlTriggerCameraMoveEventArgs args = gEArgs as GirlTriggerCameraMoveEventArgs;
            if(args==null)
            {
                return;
            }
            Vector3 cameraAimPosition = Vector3.zero;
            float speed = 0;
            float delay = 0;
            CameraEntityLogic logic = (GameEntry.Entity.GetEntity(CameraSerialId).Logic as CameraEntityLogic);
            switch (args.TriggerType)
            {
                case EnumGirlTriggerCameraMove.Jump:
                    {
                        //速度
                        speed = 5;
                        //位置
                        cameraAimPosition.y = GameEntry.Entity.GetEntity(TopTofuSerialId).transform.position.y+ m_LData.BData.Interval;
                        cameraAimPosition.x = 0;
                        cameraAimPosition.z = -10;
                        //延迟
                        delay = 0;
                        if(cameraAimPosition.y<=0)
                        {
                            return;
                        }
                        break;
                    }
                case EnumGirlTriggerCameraMove.Died:
                    {
                        //速度
                        speed = 10;
                        //位置
                        cameraAimPosition.y = (GameEntry.Entity.GetEntity(StageSerialId).Logic as StageEntityLogic).DownTran.position.y;
                        cameraAimPosition.z = -10;
                        cameraAimPosition.x = 0;
                        //延迟
                        delay = 2;
                        
                        break;
                    }
            }
            //派发事件
            logic.GirlTriggerCameraMove(delay, speed, cameraAimPosition, args.TriggerType);
        }
        #endregion

        #region 监听火箭触发相机移动事件
        private void OnRocketTriggerCameraMove(object sender, GameEventArgs gEArgs)
        {
            RocketTriggerCameraMoveEventArgs args = gEArgs as RocketTriggerCameraMoveEventArgs;
            if(args==null)
            {
                return;
            }
            Vector3 cameraAimPosition = m_RBData.AimPosition;
            cameraAimPosition.z = -10;
            cameraAimPosition.x = 0;
            float speed = m_RBData.Speed;
            float delay = 0;
            CameraEntityLogic logic = (GameEntry.Entity.GetEntity(CameraSerialId).Logic as CameraEntityLogic);
            logic.RocketTriggerCameraMove(delay, speed, cameraAimPosition);
        }
        #endregion

        #region 监听火箭与女孩的绑定或解除事件
        /// <summary>
        /// 监听火箭与女孩绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gEArgs"></param>
        private void OnRocketWithGirlBind(object sender, GameEventArgs gEArgs)
        {
            RocketWithGirlBindEventArgs args = gEArgs as RocketWithGirlBindEventArgs;
            if(args==null)
            {
                return;
            }
            m_RocketBindGirl = true;
            Log.Info("火箭与女孩的绑定");
            //更新台阶创建时间
            StairGenerateTimeUpdate();
            //更新火箭桥接数据
            RocketBridgeDataUpdate();
            //创建火箭
            BuilderRocketEntity();
            //设置构建时间
            m_StairGenerateTime = -1.7f;
        }
        /// <summary>
        /// 监听火箭与女孩解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gEArgs"></param>
        private void OnRocketWithGirlDetach(object sender,GameEventArgs gEArgs)
        {
            RocketWithGirlDetachEventArgs args = gEArgs as RocketWithGirlDetachEventArgs;
            if(args==null)
            {
                return;
            }
            m_RocketBindGirl = false;
            Log.Info("火箭与女孩的解除");
            //更新火箭桥接数据
            RocketBridgeDataUpdate();
            //开启台阶构建
            m_StairGenerate = true;
            //重置累计时间
            m_ElapseSeconds = 0;
            //
            m_StairGenerateTime = 0;
        }
        #endregion
    }
}
