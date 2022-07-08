


using UnityEngine;
using GameFramework;
using Project.TofuGirl.Data;
using Project.TofuGirl.Entity;
using Project.TofuGirl.Event;
using UnityGameFramework.Runtime;
using UnityEngine.Events;
namespace Project.TofuGirl
{
    public sealed class GameController : IReference
    {
        public bool GameStart { get; private set; }
        public bool GameOver { get; private set; }

        public UnityAction<float, float> LogicUpdateAction { get; private set;}
        private EntityLoader m_ELoader;
        private LevelData m_LevelData;
        private float m_ElapseSeconds = 0;
        private float m_StairGenerateTime = 0;

        #region 桥接数据实例
        private GirlBridgeData m_GirlBridgeData;
        private TofuBridgeData m_TofuBridgeData;
        private BattenBridgeData m_BattenBridgeData;
        #endregion
        /// <summary>
        /// 舞台Id
        /// </summary>
        public int StageId { get; private set; }
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
        public int NowTufoSerialId { get; private set; }
        /// <summary>
        /// 顶部豆腐Id
        /// </summary>
        public int TopTufoSerialId { get; set; }
        /// <summary>
        /// 当前木条Id
        /// </summary>
        public int NowBattenSerialId { get; private set; }
        public GameController()
        {
            
        }

        public static GameController Create(LevelData levelData)
        {
            GameController controller = ReferencePool.Acquire<GameController>();
            controller.m_LevelData = levelData;

            controller.GameOver = false;
            controller.LogicUpdateAction = null;
            controller.m_ELoader = EntityLoader.Create();
            #region 桥接数据
            controller.m_BattenBridgeData = BattenBridgeData.Create();
            controller.m_BattenBridgeData.InitPosition = new Vector3(-5.5f, -3.55f, 0);
            controller.m_BattenBridgeData.InitRotation = Vector3.zero;
            controller.m_BattenBridgeData.AimPosition = Vector3.zero;

            controller.m_GirlBridgeData = GirlBridgeData.Create();
            controller.m_GirlBridgeData.InitPosition = new Vector3(0, -2.7f, 0);
            controller.m_GirlBridgeData.Speed = controller.m_LevelData.GData.BaseSpeed;

            controller.m_TofuBridgeData = TofuBridgeData.Create();
            controller.m_TofuBridgeData.InitPosition = new Vector3(-999, -999, -999);//暂时这样写
            controller.m_TofuBridgeData.InitRotation = Vector3.zero;
            #endregion

            controller.CameraSerialId = controller.m_ELoader.ShowEntity<CameraEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.CameraId, (entity) =>
            {
                controller.StageId = GameEntry.Entity.GenerateSerialId();
                GameEntry.Entity.ShowEntity<StageEntityLogic>(controller.StageId, Constant.EntityId.StageId_1, StageEntityData.Create(Vector3.zero, Vector3.zero));
            }, CameraEntityData.Create(new Vector3(0, 0, -10), Vector3.zero));

            controller.GirlSerialId = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowEntity<GirlEntityLogic>(controller.GirlSerialId, Constant.EntityId.GirlId, GirlEntityData.Create(controller.m_GirlBridgeData));

            controller.NowTufoSerialId = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowEntity<TofuEntityLogic>(controller.NowTufoSerialId, Constant.EntityId.TufoId, TofuEntityData.Create(new Vector3(0, -3.55f, 0), Vector3.zero, controller));

            controller.LogicUpdateAction += controller.Update;
            return controller;
        }
        public void Clear()
        {

        }

        public void Start()
        {
            GameStart = true;
            GameEntry.Event.Fire(this, LevelStartEventArgs.Create());
            Log.Info("游戏开始");
        }

        public void End()
        {
            LogicUpdateAction = null;
            GameEntry.Coroutine.Delay(2f, () =>
             {
                 Vector3 pos = (GameEntry.Entity.GetEntity(StageId).Logic as StageEntityLogic).DownTran.position;
                 pos.z = -10;
                 GameEntry.Event.Fire(this, UpdateCameraStateEventArgs.Create(pos,true));
             });          
        }

        #region 逻辑层
        /// <summary>
        /// 状态轮询时调用(游戏更新)
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        private void Update(float elapseSeconds, float realElapseSeconds)
        {
            StairGenerate(elapseSeconds, realElapseSeconds);
        }

        #region 台阶生成逻辑
        /// <summary>
        /// 台阶生成更新
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        private void StairGenerate(float elapseSeconds, float realElapseSeconds)
        {
            m_ElapseSeconds += elapseSeconds;
            if(m_ElapseSeconds> StairGenerateTime())
            {
                m_ElapseSeconds = 0;
                BattenBridgeDataChange();//木条桥接数据改变
                TofuBridgeDataChange();//豆腐桥接数据改变
                m_ELoader.ShowEntity<BattenEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.BattemId, (battenEntity) =>
                {
                    NowBattenSerialId = battenEntity.Id;
                    m_ELoader.ShowEntity<TofuEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.TufoId, (tofuEntity) =>
                    {
                        NowTufoSerialId = tofuEntity.Id;
                        GameEntry.Entity.AttachEntity(tofuEntity, battenEntity);
                    }, TofuEntityData.Create(m_TofuBridgeData));
                }, BattenEntityData.Create(m_BattenBridgeData));
                #region 备份
                //NowBattenSerialId = m_ELoader.ShowEntity<BattenEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.BattemId, (battenEntity) =>
                //{
                //    Vector3 tufoPosition = Vector3.zero;
                //    switch ((battenEntity.Logic as BattenEntityLogic).MoveType)
                //    {
                //        case EnumBattenMove.Left:
                //            {
                //                tufoPosition = (battenEntity.Logic as BattenEntityLogic).LeftPoint.position;
                //                break;
                //            }
                //        case EnumBattenMove.Right:
                //            {
                //                tufoPosition = (battenEntity.Logic as BattenEntityLogic).RightPoint.position;
                //                break;
                //            }
                //    }
                //    m_ELoader.ShowEntity<TofuEntityLogic>(GameEntry.Entity.GenerateSerialId(), Constant.EntityId.TufoId, (tofuEntity) =>
                //    {
                //        NowTufoSerialId = tofuEntity.Id;
                //        GameEntry.Entity.AttachEntity(tofuEntity, battenEntity);
                //    }, TofuEntityData.Create(tufoPosition, Vector3.zero));
                //}, BattenEntityData.Create(m_InitPosition, Vector3.zero, new Vector3(0, m_InitPosition.y, 0)));
                #endregion
            }
        }



        private float StairGenerateTime()
        {
            float time = 0;//浮动值

            #region 逻辑影响

            #endregion

            m_StairGenerateTime = m_LevelData.BData.CreateTimeBase + time;
            return m_StairGenerateTime;
        }
        #endregion

        #region 桥接数据修改  依据关卡逻辑来进行数值修改
    


        
        //木条桥接数据修改
        private void BattenBridgeDataChange()
        {
            #region 木条上升幅度计算
            m_BattenBridgeData.InitPosition.y += 0.7f;//木条每次上升的高度为0.7
            #endregion

            #region 木条目标位置计算
            m_BattenBridgeData.AimPosition.y = m_BattenBridgeData.InitPosition.y;
            #endregion
            #region 木条左右移动概率计算   
            bool result;
            #region 逻辑影响  0 为左  1 为右
            int leftFloat = 0;
            int total = 100;//总概率
            int left = m_LevelData.BData.DirBase+ leftFloat;//左概率
            #region leftFloat 调配  待定





            #endregion
            int right = total - left;//右概率
            result = RandRate(new int[] { left, right }, total) == 0;
            #endregion
            m_BattenBridgeData.MoveType = result ? EnumBattenMove.Left : EnumBattenMove.Right;
            switch (m_BattenBridgeData.MoveType)
            {
                case EnumBattenMove.Left:
                    {
                        m_BattenBridgeData.InitPosition.x = 5.5f;
                        break;
                    }
                case EnumBattenMove.Right:
                    {
                        m_BattenBridgeData.InitPosition.x = -5.5f;
                        break;
                    }
            }
            #endregion

            #region 木条移动速度   
            float speed = 0;//浮动值  
            #region 逻辑影响   浮动值计算:  记录连续完美豆腐的数量(每个0.06加成)  记录豆腐的累计数量(每个0.01加成)  
            int toduTotal = 0;
            int seriesPrefectTotal = 0;         
            bool tufoPrefect = true;
            int tufoSerialId = TopTufoSerialId;
            while (GameEntry.Entity.HasEntity(tufoSerialId))//计算连续完美豆腐数量
            {
                TofuEntityLogic logic = (GameEntry.Entity.GetEntity(tufoSerialId).Logic as TofuEntityLogic);
                if (!logic.Prefect)
                {
                    tufoPrefect = false;
                }
                if (tufoPrefect)
                {
                    seriesPrefectTotal++;
                }
                toduTotal++;
                tufoSerialId = (GameEntry.Entity.GetEntity(tufoSerialId).Logic as TofuEntityLogic).PrevId;
            }
            Log.Info("完美累计数量:" + seriesPrefectTotal);
            speed = seriesPrefectTotal * 0.2f+ toduTotal*0.02f;
            speed = Mathf.Clamp(speed, m_LevelData.BData.SpeedMin, m_LevelData.BData.SpeedMax);
            
            int add = 60;
            int notAdd = total - add;
            result = RandRate(new int[] { add, notAdd }, total) == 0;

            Log.Info("豆腐积累数量:{0} 木条加成速度【完美豆腐加成:{1}+豆腐累计加成:{2}】,附加木条加成加速:{3},木条方向{4}", toduTotal, seriesPrefectTotal * 0.06f, toduTotal * 0.01f, result, m_BattenBridgeData.MoveType.ToString());
            #endregion
            m_BattenBridgeData.Speed = m_LevelData.BData.DirBase + (result ? Random.Range(0,speed) : 0);
            #endregion

            
        }
        //豆腐桥接数据修改
        private void TofuBridgeDataChange()
        {
            #region 豆腐所受重力大小

            #endregion

            #region 豆腐类型
            //int toduTotal = 0;
            //int tufoSerialId = NowTufoSerialId;
            //while (GameEntry.Entity.HasEntity(tufoSerialId))//计算豆腐总和
            //{
            //    toduTotal++;
            //    tufoSerialId = (GameEntry.Entity.GetEntity(tufoSerialId).Logic as TofuEntityLogic).PrevId;
            //}



            #endregion

        }
        //女孩桥接数据修改   修改时派发桥接数据修改事件通知女孩逻辑类进行数据修正
        private void GirlBridgeDataChange()
        {
            #region 女孩跳跃速度修改(标量)
            float speed = 0;//浮动值
            m_GirlBridgeData.Speed = m_LevelData.GData.BaseSpeed+ speed;
            #endregion

            #region 女孩所受重力大小

            #endregion
        }
        #endregion

        #endregion

        /// <summary>
        /// 概率分配计算
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public int RandRate(int[] rate, int total)
        {
            int rand = Random.Range(0, total + 1);
            for (int i = 0; i < rate.Length; i++)
            {
                rand -= rate[i];
                if (rand <= 0)
                {
                    return i;
                }
            }
            return 0;
        }
    }

    
}
