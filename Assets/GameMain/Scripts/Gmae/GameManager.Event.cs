


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
        /// <summary>
        /// 台阶构建
        /// </summary>
        private bool m_StairGenerate = false;
        /// <summary>
        /// 台阶构建成功
        /// </summary>
        private bool m_StairGenerateSuccess = false;
        /// <summary>
        /// 护盾绑定女孩
        /// </summary>
        private bool m_ShieldBindGirl = false;
        /// <summary>
        /// 火箭绑定女孩
        /// </summary>
        private bool m_RocketBindGirl = false;
        /// <summary>
        /// 木条撤销
        /// </summary>
        private bool m_BattenCancle = false;

        private void GMEventSubscribe()
        {
            //火箭与女孩的绑定或解除
            GameEntry.Event.Subscribe(RocketWithGirlBindEventArgs.EventId, OnRocketWithGirlBind);
            GameEntry.Event.Subscribe(RocketWithGirlDetachEventArgs.EventId, OnRocketWithGirlDetach);
            //女孩触发相机移动
            GameEntry.Event.Subscribe(GirlTriggerCameraMoveEventArgs.EventId, OnGirlTriggerCameraMove);
            //顶部豆腐Id更新
            GameEntry.Event.Subscribe(TopTofuIdUpdateEventArgs.EventId, OnTopTofuIdUpdate);
            //当前豆腐Id更新
            GameEntry.Event.Subscribe(NowTofuIdUpdateEventArgs.EventId, OnNowTofuIdUpdate);
            //开启台阶构建
            GameEntry.Event.Subscribe(OpenStairGenerateEventArgs.EventId, OnOpenStairGenerate);
            //关闭台阶构建
            GameEntry.Event.Subscribe(CloseStairGenerateEventArgs.EventId, OnCloseStairGenerate);
            //台阶构建成功
            GameEntry.Event.Subscribe(StairGenerateSuccessEventArgs.EventId, OnStairGenerateSuccess);
            //木条撤销事件
            //GameEntry.Event.Subscribe(BattenCancleEventArgs.EventId,)
        }

        private void GMEventUnsubscribe()
        {
            //火箭与女孩的绑定或解除
            GameEntry.Event.Unsubscribe(RocketWithGirlBindEventArgs.EventId, OnRocketWithGirlBind);
            GameEntry.Event.Unsubscribe(RocketWithGirlDetachEventArgs.EventId, OnRocketWithGirlDetach);
            //女孩触发相机移动
            GameEntry.Event.Unsubscribe(GirlTriggerCameraMoveEventArgs.EventId, OnGirlTriggerCameraMove);
            //顶部豆腐Id更新
            GameEntry.Event.Unsubscribe(TopTofuIdUpdateEventArgs.EventId, OnTopTofuIdUpdate);
            //当前豆腐Id更新
            GameEntry.Event.Unsubscribe(NowTofuIdUpdateEventArgs.EventId, OnNowTofuIdUpdate);
            //开启台阶构建
            GameEntry.Event.Unsubscribe(OpenStairGenerateEventArgs.EventId, OnOpenStairGenerate);
            //关闭台阶构建
            GameEntry.Event.Unsubscribe(CloseStairGenerateEventArgs.EventId, OnCloseStairGenerate);
            //台阶构建成功
            GameEntry.Event.Unsubscribe(StairGenerateSuccessEventArgs.EventId, OnStairGenerateSuccess);
        }
        #region 监听木条撤销事件
        private void OnBattenCancle(object sender,GameEventArgs gEArgs)
        {
            BattenCancleEventArgs args = gEArgs as BattenCancleEventArgs;
            if(args==null)
            {
                return;
            }
            Log.Info("木条撤销");
            m_BattenCancle = true;
        }

        #endregion

        #region 监听台阶构建成功事件
        private void OnStairGenerateSuccess(object sender,GameEventArgs gEArgs)
        {
            StairGenerateSuccessEventArgs args = gEArgs as StairGenerateSuccessEventArgs;
            if (args == null)
            {
                return;
            }
            m_StairGenerateSuccess = true;
            m_ElapseSeconds = 0;
            Log.Info("台阶构建完成");
        }
        #endregion

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
                        if(cameraAimPosition.y<= GameEntry.Entity.GetEntity(CameraSerialId).transform.position.y)
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

        #region 监听盾牌与女孩的绑定或解除事件
        /// <summary>
        /// 监听护盾与女孩绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gEArgs"></param>
        private void OnShieldWithGirlBind(object sender, GameEventArgs gEArgs)
        {
            ShieldWithGirlBindEventArgs args = gEArgs as ShieldWithGirlBindEventArgs;
            if(args==null)
            {
                return;
            }
            m_ShieldBindGirl = true;
            Log.Info("护盾与女孩绑定");
            //更新护盾桥接数据
            ShieldBridgeDataUpdate();
            //创建护盾
            BuilderShieldEntity();
        }
        /// <summary>
        /// 监听护盾与女孩解除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gEArgs"></param>
        private void OnShieldWithGirlDetach(object sender, GameEventArgs gEArgs)
        {
            ShieldWithGirlDetachEventArgs args = gEArgs as ShieldWithGirlDetachEventArgs;
            if (args == null)
            {
                return;
            }
            m_ShieldBindGirl = false;
            Log.Info("护盾与女孩解除");
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
            m_StairGenerate = false;
            m_RocketBindGirl = true;
            m_RocketTriggerTofuNum = 0;//重置
            Log.Info("火箭与女孩的绑定");
            //更新台阶创建时间
            StairGenerateTimeUpdate();
            //更新火箭桥接数据
            RocketBridgeDataUpdate();
            //创建火箭
            BuilderRocketEntity();
            

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
