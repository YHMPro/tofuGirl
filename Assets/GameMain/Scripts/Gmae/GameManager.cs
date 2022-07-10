
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
        /// 实体加载器
        /// </summary>
        private EntityLoader m_ELoader;
        #region  桥接数据
        /// <summary>
        /// 火箭桥接数据(豆腐道具)
        /// </summary>
        private RocketBridgeData m_RBData;
        /// <summary>
        /// 相机桥接数据
        /// </summary>
        private CameraBridgeData m_CBData;
        /// <summary>
        /// 舞台桥接数据
        /// </summary>
        private StageBirdgeData m_SBData;
        /// <summary>
        /// 女孩桥接数据
        /// </summary>
        private GirlBridgeData m_GBData;
        /// <summary>
        /// 木条桥接数据
        /// </summary>
        private BattenBridgeData m_BBData;
        /// <summary>
        /// 豆腐桥接数据
        /// </summary>
        private TofuBridgeData m_TBData;
        #endregion
        /// <summary>
        /// 关卡数据
        /// </summary>
        private LevelData m_LData;
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
        public static GameManager Create(LevelData levelData)
        {
            return Init(ReferencePool.Acquire<GameManager>(),levelData);
        }

        private static GameManager Init(GameManager gm, LevelData levelData)
        {
            gm.m_LData = levelData;
            gm.GameStart = false;
            gm.GameOver = false;
            gm.GirlDied = false;
            gm.m_StairGenerate = true;

            gm.CameraSerialId = 0;
            gm.GirlSerialId = 0;
            gm.NowTofuSerialId = 0;
            gm.StageSerialId = 0;
            gm.TopTofuSerialId = 0;

            gm.m_ElapseSeconds = 0;

            gm.m_DaoJuTofuTotal = 0;
            gm.m_JinSeTeShuTofuTotal = 0;
            gm.m_PuTongTofuTotal = 0;
            gm.m_TeShuTofuTotal = 0;
            gm.m_TeShuTofu = 0;
            gm.m_JinSeTeShuTofu = 0;

            gm.m_ELoader = EntityLoader.Create();
            gm.EventSubscribe();
            BridgeDataInit(gm);
            EntityInit(gm);
            return gm;
        }
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
            gm.m_RBData.Speed = 15f;
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

            gm.GameStart = true;
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
            GameEntry.Event.Subscribe(UpdateStairGenerateEventArgs.EventId, OnUpdateStairGenerateInfo);
            GameEntry.Event.Subscribe(TofuWithGirlCollisionEventArgs.EventId, OnTofuWithGirlCollision);
            GameEntry.Event.Subscribe(UpdateCameraFollowEventArgs.EventId, OnUpdateCameraFollow);


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
            GameEntry.Event.Unsubscribe(UpdateStairGenerateEventArgs.EventId, OnUpdateStairGenerateInfo);
            GameEntry.Event.Unsubscribe(TofuWithGirlCollisionEventArgs.EventId, OnTofuWithGirlCollision);
            GameEntry.Event.Unsubscribe(UpdateCameraFollowEventArgs.EventId, OnUpdateCameraFollow);


            GameEntry.Event.Subscribe(UpdateCameraFollowInfoEventArgs.EventId, OnUpdateCameraFollowInfo);
        }
    }
    /// <summary>
    /// 数据修改逻辑
    /// </summary>
    public sealed partial class GameManager
    {
        private const int m_TotalRate = 100;
        private const float m_LeftPointToBattenCenterDis = -1.85f;
        private const float m_RightPointToBattenCenterDis = 1.85f;

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
        #region 台阶
        /// <summary>
        /// 台阶创建时间
        /// </summary>
        /// <returns></returns>
        private float StairGenerateTime()
        {
            float time = 0;

            return m_LData.BData.CreateTimeBase+time;
        }
        #region 木条桥接数据更新
        private void BattenBridgeDataUpdate(bool daoju=false)
        {
            //速度
            m_BBData.Speed = daoju?25:2;
            //左右方向
            int leftDir = 50;
            m_BBData.MoveType = RandRateTool.BattenMoveRandRate(leftDir, m_TotalRate - leftDir);
            //位置
            m_BBData.InitPosition.y = GameEntry.Entity.GetEntity(NowTofuSerialId).transform.position.y+0.7f;
            m_BBData.InitPosition.x = 7f * ((EnumBattenMove.Left == m_BBData.MoveType) ? 1 : -1);
            //目标位置
            m_BBData.AimPosition.y = m_BBData.InitPosition.y;
            m_BBData.AimPosition.x=1.85f* ((EnumBattenMove.Left == m_BBData.MoveType) ? 1 : -1);
            Log.Info("木条信息=>木条速度:{0},木条方向:{1}", m_BBData.Speed, m_BBData.MoveType);
        }
        #endregion

        #region 豆腐桥接数据更新
        /// <summary>
        /// 豆腐桥接数据更新
        /// </summary>
        private void TofuBridgeDataUpdate(bool daoju=false)
        {
            m_TBData.FirstTofu = false;
            #region 豆腐位置计算
            //得到木条左右点相对于木条中心的相对位置
            m_TBData.InitPosition = m_BBData.InitPosition + new Vector3((EnumBattenMove.Left== m_BBData.MoveType)?m_LeftPointToBattenCenterDis:m_RightPointToBattenCenterDis, 0, 0);       
            m_TBData.InitRotation = Vector3.zero;
            #endregion
            //豆腐类型
            ++m_TeShuTofu;
            ++m_JinSeTeShuTofu;
            if (daoju)
            {              
                #region 金色特殊豆腐概率计算
                if (m_JinSeTeShuTofu >= 4)
                {
                    //有百分之50%的概率出现  或 达到阈值:5
                    if ((RandRateTool.RandRate(new int[] { 50, 50 }, 100) == 1) || (m_JinSeTeShuTofu >= 6))
                    {
                        //生成金色特殊豆腐
                        m_JinSeTeShuTofu = 0;
                        m_JinSeTeShuTofuTotal++;
                        //Log.Info("生成金色特殊豆腐");
                        m_TBData.TofuType = EnumTofu.JinSeTeShu;
                    }
                }
                #endregion

                #region 特殊豆腐概率计算
                if ((m_JinSeTeShuTofu != 0) && (m_TeShuTofu >= 3))
                {
                    //有百分之50%的概率出现  或 达到阈值:5
                    if ((RandRateTool.RandRate(new int[] { 50, 50 }, 100) == 1) || (m_TeShuTofu >= 5))
                    {
                        //生成特殊豆腐
                        m_TeShuTofu = 0;
                        //Log.Info("生成特殊豆腐");
                        m_TBData.TofuType = EnumTofu.TeShu;
                        m_TeShuTofuTotal++;
                    }
                }
                #endregion
            }
            #region 道具豆腐与普通豆腐概率计算
            if ((m_JinSeTeShuTofu!=0)&& (m_TeShuTofu!=0))
            {
                //80%普通豆腐 20%道具豆腐   当触发道具时 100%普通豆腐  
                if ((RandRateTool.RandRate(new int[] { 0, 100 }, 100) == 0)|| daoju)
                {
                    //生成普通豆腐                   
                    //Log.Info("生成普通豆腐");
                    m_TBData.TofuType = EnumTofu.PuTong;
                    m_PuTongTofuTotal++;
                }
                else
                {
                    //生成道具豆腐
                    m_StairGenerate = false;//停止台阶的生成 但本台阶还是会生成
                    //Log.Info("生成道具豆腐");
                    m_TBData.TofuType = EnumTofu.DaoJu;
                    m_DaoJuTofuTotal++;
                }
            }
            #endregion               
            
            //对接的上一个豆腐Id
            m_TBData.PrevId = NowTofuSerialId;
            Log.Info("豆腐信息=> 豆腐类型:{0},累计的特殊豆腐条件值:{1},累计的金色特殊豆腐条件值:{2},已生成的普通豆腐总和:{3},已生成的道具豆腐总和:{4},已生成的特殊豆腐总和:{5},已生成的金色特殊豆腐总和:{6}",
                m_TBData.TofuType, m_TeShuTofu, m_JinSeTeShuTofu, m_PuTongTofuTotal, m_DaoJuTofuTotal, m_TeShuTofuTotal, m_JinSeTeShuTofuTotal
                );
        }
        #endregion
        #endregion

        #region 道具

        #region 火箭桥接数据更新
        private void RocketBridgeDataUpdate()
        {
            //位置:当前顶部豆腐的位置 + 0.7f
            m_RBData.InitPosition.x = 0;
            m_RBData.InitPosition.y = GameEntry.Entity.GetEntity(TopTofuSerialId).transform.position.y + 0.7f;
            m_RBData.InitRotation = Vector3.zero;
            //目标位置: 越过的数量*0.7
            m_RBData.AimPosition.x = 0;
            m_RBData.AimPosition.y = m_LData.RData.TofuNum * 0.7f;
            //速度
            m_RBData.Speed = m_LData.RData.Speed;
        }
        #endregion

        #endregion
    }
    /// <summary>
    /// 游戏事件监听
    /// </summary>
    public sealed partial class GameManager
    {
        /// <summary>
        /// 女孩死亡
        /// </summary>
        public bool GirlDied { get; private set; }
        /// <summary>
        /// 舞台Id
        /// </summary>
        public int StageSerialId { get; private set; }
        /// <summary>
        /// 相机Id
        /// </summary>
        public int CameraSerialId { get; private set; }
        /// <summary>
        /// 女孩Id
        /// </summary>
        public int GirlSerialId { get; private set; }

        /// <summary>
        /// 当前豆腐Id
        /// </summary>
        public int NowTofuSerialId { get; private set; }
        /// <summary>
        /// 顶部豆腐Id
        /// </summary>
        public int TopTofuSerialId { get; set; }      
        private void OnGirlDied(object sender,GameEventArgs gEArgs)
        {
            GirlDiedEventArgs args = gEArgs as GirlDiedEventArgs;
            if (args == null)
            {
                return;
            }
            Log.Info("角色死亡");
            GirlDied = true;
            //派发木条移动事件
            GameEntry.Event.Fire(this, BattenMoveEventArgs.Create(false));
            //派发相机跟随事件
            GameEntry.Coroutine.Delay(2, () =>//暂时2秒后
            {
            Vector3 cameraAimPosition = (GameEntry.Entity.GetEntity(StageSerialId).Logic as StageEntityLogic).DownTran.position;
                cameraAimPosition.z = -10;
                GameEntry.Event.Fire(this, CameraFollowEventArgs.Create(cameraAimPosition,EnumEntity.Stage,4));
            });  
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
            UpdateStairGenerateEventArgs args = gEArgs as UpdateStairGenerateEventArgs;
            if(args==null)
            {
                return;
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
            switch((sender as TofuEntityLogic).TofuType)
            {
                case EnumTofu.DaoJu:
                    {
                        RocketBridgeDataUpdate();
                        //生成一个火箭实体
                        m_ELoader.ShowEntity<RocketEntityLogic>(GameEntry.Entity.GenerateSerialId(),Constant.EntityId.RocketId, (rocketEntity) => 
                        {
                           
                        }, RocketEntityData.Create(m_RBData));
                        break;
                    }
            }
        }

        private void OnUpdateCameraFollow(object sender,GameEventArgs gEArgs)
        {
            UpdateCameraFollowEventArgs args = gEArgs as UpdateCameraFollowEventArgs;
            if(args==null)
            {
                return;
            }
            

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
            switch(args.FollowType)
            {
                case EnumCameraFollow.Rocket:
                    {
                        Debug.Log(m_RBData.Speed);
                        //速度
                        speed = m_RBData.Speed;
                        //位置
                        cameraAimPosition = m_RBData.AimPosition;
                        cameraAimPosition.z = -10;
                        //延迟
                        delayTime = 0;
                        break;
                    }
                case EnumCameraFollow.Stage:
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
                case EnumCameraFollow.Girl:
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
            Log.Info("相机跟随信息更新 目标位置:{0},跟随样式:{1},跟随速度:{2}", cameraAimPosition, args.FollowType, speed);
            GameEntry.Event.Fire(this, SetCameraFollowInfoEventArgs.Create(cameraAimPosition, args.FollowType, speed, delayTime));
        }
    }

    /// <summary>
    /// 游戏逻辑调控
    /// </summary>
    public sealed partial class GameManager 
    {
        private float m_ElapseSeconds = 0;
        /// <summary>
        /// 台阶构建
        /// </summary>
        private bool m_StairGenerate = false;
        /// <summary>
        /// 状态轮询时调用(游戏更新)
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public void LogicUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (GirlDied)
            {
                return;
            }
            if(!m_StairGenerate)
            {
                return;
            }
            //台阶创建
            StairGenerate(elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 台阶构建
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        private void StairGenerate(float elapseSeconds, float realElapseSeconds)
        {
            m_ElapseSeconds += elapseSeconds;
            if(m_ElapseSeconds<=StairGenerateTime())//满足创建时间即可创建,受女孩死亡事件影响
            {
                return;
            }
            m_ElapseSeconds = 0;       
            #region 台阶桥接数据修改      
            BattenBridgeDataUpdate();
            TofuBridgeDataUpdate();
            #endregion

            #region 台阶生成
            m_ELoader.ShowEntity<BattenEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.BattemId, (battenEntity) =>
            {
                m_ELoader.ShowEntity<TofuEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.TufoId, (tofuEntity) =>
                {
                    GameEntry.Entity.AttachEntity(tofuEntity, battenEntity);
                }, TofuEntityData.Create(m_TBData));
            }, BattenEntityData.Create(m_BBData));
            #endregion
        }


        public void Clear()
        {
            EventUnsubscribe();
            m_BBData = null;
            m_CBData = null;
            m_GBData = null;
            m_LData = null;
            m_SBData = null;
            m_TBData = null;
            m_RBData = null;
            m_ELoader.Clear();
            m_ELoader = null;
        }
    }


}
