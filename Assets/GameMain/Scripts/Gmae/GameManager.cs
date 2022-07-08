
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
            gm.m_GBData.Gravity = 1.2f;
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

        }
    }
    /// <summary>
    /// 数据修改逻辑
    /// </summary>
    public sealed partial class GameManager
    {
        private const int m_TotalRate = 100;

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
        private void BattenBridgeDataUpdate()
        {
            //速度
            m_BBData.Speed = 2;
            //左右方向
            int leftDir = 50;
            m_BBData.MoveType = RandRateTool.BattenMoveRandRate(leftDir, m_TotalRate - leftDir);
            //位置
            m_BBData.InitPosition.y = GameEntry.Entity.GetEntity(NowTofuSerialId).transform.position.y+0.7f;
            m_BBData.InitPosition.x = 5.5f * ((EnumBattenMove.Left == m_BBData.MoveType) ? 1 : -1);
            //目标位置
            m_BBData.AimPosition.y = m_BBData.InitPosition.y;

            Log.Info("木条信息=>木条速度:{0},木条方向:{1}", m_BBData.Speed, m_BBData.MoveType);
        }
        #endregion

        #region 豆腐桥接数据更新
        private void TofuBridgeDataUpdate()
        {
            //位置
            m_TBData.InitPosition = Vector3.zero;//也可不赋值

            //豆腐类型
            ++m_TeShuTofu;
            ++m_JinSeTeShuTofu;

            #region 金色特殊台阶概率计算
            if (m_JinSeTeShuTofu>=4)
            {
                //有百分之50%的概率出现  或 达到阈值:5
                if((RandRateTool.RandRate(new int[] { 50, 50 }, 100)==1)|| (m_JinSeTeShuTofu>=6))
                {
                    //生成金色特殊豆腐
                    m_JinSeTeShuTofu = 0;
                    m_JinSeTeShuTofuTotal++;
                    //Log.Info("生成金色特殊豆腐");
                    m_TBData.TofuType = EnumTofu.JinSeTeShu;
                }
            }
            #endregion

            #region 道具台阶概率计算
            if((m_JinSeTeShuTofu!=0)&& m_TeShuTofu>=3)
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

            #region 道具台阶与普通台阶概率计算
            if ((m_JinSeTeShuTofu!=0)&& m_TeShuTofu!=0)
            {
                //80%普通豆腐 20%道具豆腐
                if (RandRateTool.RandRate(new int[] { 80, 20 }, 100) == 0)
                {
                    //生成普通豆腐                   
                    //Log.Info("生成普通豆腐");
                    m_TBData.TofuType = EnumTofu.PuTong;
                    m_PuTongTofuTotal++;
                }
                else
                {
                    //生成道具豆腐
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
            GameEntry.Coroutine.Delay(2, () =>
            {
                GameEntry.Event.Fire(this, CameraFollowEventArgs.Create((GameEntry.Entity.GetEntity(StageSerialId).Logic as StageEntityLogic).DownTran.position));
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
    }

    /// <summary>
    /// 游戏逻辑调控
    /// </summary>
    public sealed partial class GameManager 
    {
        private float m_ElapseSeconds = 0;
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
            //台阶创建
            StairGenerate(elapseSeconds, realElapseSeconds);
        }


        public void StairGenerate(float elapseSeconds, float realElapseSeconds)
        {
            m_ElapseSeconds += elapseSeconds;
            if(m_ElapseSeconds<=StairGenerateTime())//满足创建时间即可创建,受女孩死亡事件影响
            {
                return;
            }
            m_ElapseSeconds = 0;       
            #region 台阶桥接数据修改
            TofuBridgeDataUpdate();
            BattenBridgeDataUpdate();
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
            m_ELoader.Clear();
            m_ELoader = null;
        }
    }


}
