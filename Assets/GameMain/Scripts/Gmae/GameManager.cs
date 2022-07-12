
using UnityEngine;
using GameFramework;
using Project.TofuGirl.Data;
using Project.TofuGirl.Entity;
using Project.TofuGirl.Event;
using UnityGameFramework.Runtime;
using UnityEngine.Events;
using GameFramework.Event;
namespace Project.TofuGirl
{
    /// <summary>
    /// 数据
    /// </summary>
    public sealed partial class GameManager
    {        
        /// <summary>
        /// 游戏结束
        /// </summary>
        public bool GameOver { get; private set; }
        /// <summary>
        /// 游戏开始
        /// </summary>
        public bool GameStart { get; private set; }      
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public sealed partial class GameManager : IReference
    {       
        /// <summary>
        /// 桥接数据初始化
        /// </summary>
        /// <param name="gm"></param>
        private static void BridgeDataInit(GameManager gm)
        {
            #region 舞台桥接数据
            gm.m_SBData = StageBirdgeData.Create();
            gm.m_SBData.InitPosition = Vector3.zero;
            gm.m_SBData.InitRotation = Vector3.zero;

            #endregion

            #region 豆腐桥接数据
            gm.m_TBData = TofuBridgeData.Create();
            gm.m_TBData.InitPosition = new Vector3(0, -3.5f, 0);
            gm.m_TBData.FirstTofu = true;
            gm.m_TBData.TofuType = EnumTofu.PuTong;
            gm.m_TBData.PrevId = 0;
            #endregion

            #region 女孩桥接数据
            gm.m_GBData = GirlBridgeData.Create();
            gm.m_GBData.Gravity = 1f;
            gm.m_GBData.InitPosition = new Vector3(0, -2.7f, 0);
            gm.m_GBData.Speed = gm.m_LData.GData.BaseSpeed;
            #endregion

            #region 木条桥接数据
            gm.m_BBData = BattenBridgeData.Create();
            gm.m_SBData.InitPosition = Vector3.zero;
            gm.m_SBData.InitPosition = Vector3.zero;
            #endregion

            #region 相机桥接数据
            gm.m_CBData = CameraBridgeData.Create();
            gm.m_CBData.InitPosition = new Vector3(0, 0, -10);
            gm.m_CBData.InitRotation = Vector3.zero;
            #endregion

            #region 火箭桥接数据
            gm.m_RBData = RocketBridgeData.Create();
            gm.m_RBData.Speed = gm.m_LData.RData.Speed;
            #endregion
        }
        /// <summary>
        /// 实体初始化
        /// </summary>
        private static void EntityInit(GameManager gm)
        {
            gm.m_ELoader.ShowEntity<CameraEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.CameraId, (cameraEntity) =>
            {
                gm.CameraSerialId = cameraEntity.Id;
                gm.m_SBData.CameraOrthographicSize = (cameraEntity.Logic as CameraEntityLogic).SelfCamera.orthographicSize;
                gm.m_ELoader.ShowEntity<StageEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.StageId_1, (stageEntity) =>
                {
                    gm.StageSerialId = stageEntity.Id;
                }, StageEntityData.Create(gm.m_SBData));
            }, CameraEntityData.Create(gm.m_CBData));

            GameEntry.Entity.ShowEntity<TofuEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.TufoId, TofuEntityData.Create(gm.m_TBData));

            gm.m_ELoader.ShowEntity<GirlEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.GirlId, (girlEntity) =>
            {
                gm.GirlSerialId = girlEntity.Id;
            }, GirlEntityData.Create(gm.m_GBData));
            //gm.GameStart = true;
            Log.Info("暂时写这里，后续通过事件调控游戏开始:{0}", gm.GameStart);
        }
        /// <summary>
        /// 事件注册
        /// </summary>
        /// <param name="gm"></param>
        private void EventSubscribe()
        {
            GameEntry.Event.Subscribe(GirlDiedEventArgs.EventId,OnGirlDied);
            GameEntry.Event.Subscribe(UpdateNowTofuSerialldEventArgs.EventId, OnUpdateNowTofuSerialId);
            GameEntry.Event.Subscribe(UpdateTopTofuSerialldEventArgs.EventId, OnUpdateTopTofuSerialId);
            GameEntry.Event.Subscribe(GameOverEventArgs.EventId, OnGameOver);
            GameEntry.Event.Subscribe(TofuWithGirlCollisionEventArgs.EventId, OnTofuWithGirlCollision);

            GameEntry.Event.Subscribe(UpdateStairGenerateInfoEventArgs.EventId, OnUpdateStairGenerateInfo);
            GameEntry.Event.Subscribe(UpdateBattenMoveInfoEventArgs.EventId, OnUpdateBattenMoveInfo);
            GameEntry.Event.Subscribe(UpdateCameraFollowInfoEventArgs.EventId, OnUpdateCameraFollowInfo);
        }
        /// <summary>
        /// 事件注销
        /// </summary>
        /// <param name="gm"></param>
        private void EventUnsubscribe()
        {
            GameEntry.Event.Unsubscribe(GirlDiedEventArgs.EventId,OnGirlDied);
            GameEntry.Event.Unsubscribe(UpdateNowTofuSerialldEventArgs.EventId, OnUpdateNowTofuSerialId);
            GameEntry.Event.Unsubscribe(UpdateTopTofuSerialldEventArgs.EventId, OnUpdateTopTofuSerialId);
            GameEntry.Event.Unsubscribe(GameOverEventArgs.EventId, OnGameOver);
            GameEntry.Event.Unsubscribe(TofuWithGirlCollisionEventArgs.EventId, OnTofuWithGirlCollision);



            GameEntry.Event.Unsubscribe(UpdateStairGenerateInfoEventArgs.EventId, OnUpdateStairGenerateInfo);
            GameEntry.Event.Unsubscribe(UpdateBattenMoveInfoEventArgs.EventId, OnUpdateBattenMoveInfo);
            GameEntry.Event.Unsubscribe(UpdateCameraFollowInfoEventArgs.EventId, OnUpdateCameraFollowInfo);
        }
    }
    /// <summary>
    /// 数据修改逻辑
    /// </summary>
    public sealed partial class GameManager
    {
        


        private const int m_TotalRate = 100;
        private const float m_LeftPointToBattenCenterDis = -1.415f;
        private const float m_RightPointToBattenCenterDis = 1.415f;

        private int m_PuTongTofuTotal = 0;
        private int m_TeShuTofuTotal = 0;
        private int m_JinSeTeShuTofuTotal = 0;
        private int m_DaoJuTofuTotal = 0;

        /// <summary>
        /// 特殊豆腐
        /// </summary>
        private int m_TeShuTofu = 0;//记录没有出现这种豆腐的的次数  每次出现都会重置
        /// <summary>
        /// 金色特殊豆腐
        /// </summary>
        private int m_JinSeTeShuTofu = 0;//记录没有出现这种豆腐的的次数  每次出现都会重置       
        /// <summary>
        /// 木条移动速度
        /// </summary>
        private float m_BattenSpeed = 0;    
    }
    /// <summary>
    /// 游戏事件监听
    /// </summary>
    public sealed partial class GameManager
    {
        

        private void OnGirlDied(object sender,GameEventArgs gEArgs)
        {
            GirlDiedEventArgs args = gEArgs as GirlDiedEventArgs;
            if (args == null)
            {
                return;
            }
            Log.Info("角色死亡");
        }


        private void OnUpdateNowTofuSerialId(object sender, GameEventArgs gEArgs)
        {
            UpdateNowTofuSerialldEventArgs args = gEArgs as UpdateNowTofuSerialldEventArgs;
            if (args == null)
            {
                return;
            }
            if(NowTofuSerialId== args.EntityId)
            {
                return;
            }
            Log.Info("更新当前豆腐Id:{0}===>{1}", NowTofuSerialId, args.EntityId);
            NowTofuSerialId = args.EntityId;        
        }

        private void OnUpdateTopTofuSerialId(object sender, GameEventArgs gEArgs)
        {
            UpdateTopTofuSerialldEventArgs args = gEArgs as UpdateTopTofuSerialldEventArgs;
            if (args == null)
            {
                return;
            }
            if(TopTofuSerialId== args.EntityId)
            {
                return;
            }
            Log.Info("更新顶部豆腐Id:{0}===>{1}", TopTofuSerialId, args.EntityId);
            TopTofuSerialId = args.EntityId;
        }       

        private void OnGameOver(object sender, GameEventArgs gEArgs)
        {
            GameOverEventArgs args = gEArgs as GameOverEventArgs;
            if(args==null)
            {
                return;
            }
            Log.Info("游戏结束");
            GameOver = true;
        }

        private void OnUpdateStairGenerateInfo(object sender,GameEventArgs gEArgs)
        {
            UpdateStairGenerateInfoEventArgs args = gEArgs as UpdateStairGenerateInfoEventArgs;
            if(args==null)
            {
                return;
            }

            switch(args.SenderType)
            {
                case EnumSender.Rocket:
                    {
                        m_RocketBindGirl = true;
                        //设置创建台阶的时间间隔 并重置当前累计的时间
                        m_ElapseSeconds = 0;
                        m_StairGenerateTime = -1.6f;
                        //木条附加速度
                        m_BattenSpeed = 10;
                        break;
                    }
                default:break;
            }
            m_StairGenerate=args.StairGenerate;
        }

        private void OnTofuWithGirlCollision(object sender,GameEventArgs gEArgs)
        {
            TofuWithGirlCollisionEventArgs args = gEArgs as TofuWithGirlCollisionEventArgs;
            if(args==null)
            {
                return;
            }
            if(!(sender is TofuEntityLogic))
            {
                return;
            }
            //switch((sender as TofuEntityLogic).TofuType)
            //{
            //    case EnumTofu.DaoJu:
            //        {
            //            RocketBridgeDataUpdate();
            //            //生成一个火箭实体
            //            m_ELoader.ShowEntity<RocketEntityLogic>(GameEntry.Entity.GenerateSerialId(),Constant.EntityId.RocketId, (rocketEntity) => 
            //            {
                           
            //            }, RocketEntityData.Create(m_RBData));
            //            break;
            //        }
            //}
        }

        
        private void OnGirlReliefRocket(object sender,GameEventArgs gEArgs)
        {
            GirlReliefRocketEventArgs args = gEArgs as GirlReliefRocketEventArgs;
            if(args==null)
            {
                return;
            }
        }
        /// <summary>
        /// 监听木条移动信息更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gEArgs"></param>
        private void OnUpdateBattenMoveInfo(object sender, GameEventArgs gEArgs)
        {
            UpdateBattenMoveInfoEventArgs args = gEArgs as UpdateBattenMoveInfoEventArgs;
            if(args==null)
            {
                return;
            }
            GameEntry.Event.Fire(this, SetBattenMoveInfoEventArgs.Create(args.Move));
        }
        /// <summary>
        /// 监听相机跟随信息更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gEArgs"></param>
        private void OnUpdateCameraFollowInfo(object sender, GameEventArgs gEArgs)
        {
            UpdateCameraFollowInfoEventArgs args = gEArgs as UpdateCameraFollowInfoEventArgs;
            if(args==null)
            {
                return;
            }
            Vector3 cameraAimPosition = Vector3.zero;
            float speed = 0;
            float delayTime = 0;
            switch(args.SenderType)
            {
                case EnumSender.Rocket:
                    {
                        //速度
                        speed = m_RBData.Speed;
                        //位置
                        cameraAimPosition = m_RBData.AimPosition;
                        cameraAimPosition.z = -10;
                        //延迟
                        delayTime = 0;
                        break;
                    }
                case EnumSender.Stage:
                    {
                        //速度
                        speed = 6;
                        //位置
                        cameraAimPosition.y = (GameEntry.Entity.GetEntity(StageSerialId).Logic as StageEntityLogic).DownTran.position.y;
                        cameraAimPosition.z = -10;
                        //延迟
                        delayTime = 2;
                        break;
                    }
                case EnumSender.Girl:
                    {
                        //速度
                        speed = 5;
                        //位置
                        cameraAimPosition.y = GameEntry.Entity.GetEntity(TopTofuSerialId).transform.position.y;
                        cameraAimPosition.z = -10;
                        //延迟

                        if (cameraAimPosition.y<=0)
                        {
                            return;
                        }
                        break;
                    }
            }
            Log.Info("相机跟随信息更新 目标位置:{0},跟随样式:{1},跟随速度:{2}", cameraAimPosition, args.SenderType, speed);
            GameEntry.Event.Fire(this, SetCameraFollowInfoEventArgs.Create(cameraAimPosition, args.SenderType, speed, delayTime));
        }
    }

    /// <summary>
    /// 游戏逻辑调控
    /// </summary>
    public sealed partial class GameManager 
    {
        


        
    }


}
