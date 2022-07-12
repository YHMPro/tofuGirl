using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.TofuGirl.Data;
using UnityGameFramework.Runtime;
namespace Project.TofuGirl
{
    /// <summary>
    /// 游戏管理器(数据)
    /// </summary>
    public partial class GameManager
    {
        /// <summary>
        /// 女孩死亡
        /// </summary>
        public bool GirlDied { get; private set; }

        
        #region 实体Id       
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
        public int TopTofuSerialId { get;private set; }
        #endregion

        #region 桥接数据
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

        #region 关卡数据
        /// <summary>
        /// 关卡数据
        /// </summary>
        private LevelData m_LData;
        #endregion

        #region 数据初始化
        private void DataInit()
        {
            GameStart = false;
            GameOver = false;
            m_StairGenerate = true;

            CameraSerialId = 0;
            GirlSerialId = 0;
            NowTofuSerialId = 0;
            StageSerialId = 0;
            TopTofuSerialId = 0;

            m_ElapseSeconds = 0;

            m_DaoJuTofuTotal = 0;
            m_JinSeTeShuTofuTotal = 0;
            m_PuTongTofuTotal = 0;
            m_TeShuTofuTotal = 0;
            m_TeShuTofu = 0;
            m_JinSeTeShuTofu = 0;


            BridgeDataInit();
        }

        private void BridgeDataInit()
        {
            #region 舞台桥接数据
            m_SBData = StageBirdgeData.Create();
            m_SBData.InitPosition = Vector3.zero;
            m_SBData.InitRotation = Vector3.zero;
            #endregion

            #region 豆腐桥接数据
            m_TBData = TofuBridgeData.Create();
            m_TBData.InitPosition = new Vector3(0, -3f, 0);
            m_TBData.InitRotation = Vector3.zero;
            m_TBData.FirstTofu = true;
            m_TBData.TofuType = EnumTofu.PuTong;
            m_TBData.PrevId = 0;
            #endregion

            #region 女孩桥接数据
            m_GBData = GirlBridgeData.Create();
            m_GBData.Gravity = 1f;
            m_GBData.InitPosition = new Vector3(0, -2.5f, 0);
            m_GBData.Speed = m_LData.GData.BaseSpeed;
            #endregion

            #region 木条桥接数据
            m_BBData = BattenBridgeData.Create();
            m_SBData.InitPosition = Vector3.zero;
            m_SBData.InitPosition = Vector3.zero;
            #endregion

            #region 相机桥接数据
            m_CBData = CameraBridgeData.Create();
            m_CBData.InitPosition = new Vector3(0, 0, -10);
            m_CBData.InitRotation = Vector3.zero;
            #endregion

            #region 火箭桥接数据
            m_RBData = RocketBridgeData.Create();
            m_RBData.Speed = m_LData.RData.Speed;
            m_RBData.InitRotation = Vector3.zero;
            m_RBData.InitPosition = Vector3.zero;
            #endregion
        }
        #endregion

        #region 数据更新
        #region 台阶创建时间更新
        private void StairGenerateTimeUpdate()
        {
            //基础
            m_StairGenerateTime = m_LData.BData.CreateTimeBase;
            //其余逻辑影响 m_StairGenerateTime+=?

            //火箭因素
            if(m_RocketBindGirl)
            {
                m_StairGenerateTime = m_LData.BData.RocketCreateTime;
            }
        }
        #endregion
        #region 木条桥接数据更新
        private void BattenBridgeDataUpdate()
        {
            //速度
            #region 速度判断逻辑
            #endregion
            m_BBData.Speed = m_LData.BData.SpeedBase;//基础 + 火箭 + ?
            m_BBData.Speed += m_RocketBindGirl ? m_LData.BData.RocketAppendSpeed :0;//火箭附加的速度
            //其余影响待......


            //左右方向
            m_BBData.MoveType = RandRateTool.BattenMoveRandRate(m_LData.BData.LeftDir, m_LData.BData.RightDir);
            //位置
            m_BBData.InitPosition.y = GameEntry.Entity.GetEntity(NowTofuSerialId).transform.position.y + m_LData.BData.Interval;
            m_BBData.InitPosition.x = 5f * ((EnumBattenMove.Left == m_BBData.MoveType) ? 1 : -1);
            //目标位置
            m_BBData.AimPosition.y = m_BBData.InitPosition.y;
            m_BBData.AimPosition.x = 1.415f * ((EnumBattenMove.Left == m_BBData.MoveType) ? 1 : -1);
            Log.Info("木条信息=>木条速度:{0},木条方向:{1}", m_BBData.Speed, m_BBData.MoveType);
        }
        #endregion

        #region 豆腐桥接数据更新 
        /// <summary>
        /// 豆腐桥接数据更新
        /// </summary>
        private void TofuBridgeDataUpdate()
        {
            m_TBData.FirstTofu = false;
            #region 豆腐位置计算
            //得到木条左右点相对于木条中心的相对位置
            m_TBData.InitPosition = m_BBData.InitPosition + new Vector3((EnumBattenMove.Left == m_BBData.MoveType) ? m_LeftPointToBattenCenterDis : m_RightPointToBattenCenterDis, 0, 0);
            m_TBData.InitRotation = Vector3.zero;
            #endregion
            //豆腐类型       
            if (!m_RocketBindGirl)
            {
                ++m_TeShuTofu;
                ++m_JinSeTeShuTofu;
                #region 金色特殊豆腐概率计算
                if (m_JinSeTeShuTofu >= m_LData.TDData.JinSheTeShuMin)
                {
                    //有百分之50%的概率出现  或 达到阈值:
                    if ((RandRateTool.RandRate(new int[] { 50, 50 }, 100) == 1) || (m_JinSeTeShuTofu >= m_LData.TDData.JinSheTeShuMax))
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
                if ((m_JinSeTeShuTofu != 0) && (m_TeShuTofu >= m_LData.TDData.TeShuMin))
                {
                    //有百分之50%的概率出现  或 达到阈值:5
                    if ((RandRateTool.RandRate(new int[] { 50, 50 }, 100) == 1) || (m_TeShuTofu >= m_LData.TDData.TeShuMax))
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
            if ((m_JinSeTeShuTofu != 0) && (m_TeShuTofu != 0))
            {
                //80%普通豆腐 20%道具豆腐   当触发道具时 100%普通豆腐  
                if ((RandRateTool.RandRate(new int[] { m_LData.TDData.PuTong, m_LData.TDData.DaoJu }, 100) == 0) || m_RocketBindGirl)
                {
                    //生成普通豆腐                   
                    //Log.Info("生成普通豆腐");
                    m_TBData.TofuType = EnumTofu.PuTong;
                    m_PuTongTofuTotal++;
                }
                else
                {
                    m_StairGenerate = false;//停止台阶的生成 但当前台阶还是会生成
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

        #region 道具

        #region 火箭桥接数据更新
        private void RocketBridgeDataUpdate()
        {
            //位置:当前顶部豆腐的位置
            m_RBData.InitPosition.x = 0;
            m_RBData.InitPosition.y = GameEntry.Entity.GetEntity(TopTofuSerialId).transform.position.y + m_LData.BData.Interval;
            m_RBData.InitRotation = Vector3.zero;
            //目标位置: 
            m_RBData.AimPosition.x = 0;
            m_RBData.AimPosition.y =  m_LData.RData.TofuNum * m_LData.BData.Interval + m_RBData.InitPosition.y;
            m_RBData.AimPosition.z = 0;
            Log.Info("火箭跨越数量:{0},目标点:{1}", m_LData.RData.TofuNum, m_RBData.AimPosition);
            //速度
        }
        #endregion

        #endregion

        #endregion


    }
}
