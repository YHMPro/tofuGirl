


using GameFramework.Fsm;
using GameFramework.Procedure;
using Project.TofuGirl.Entity;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Event;
using GameFramework.Event;
using Project.TofuGirl.Data;
using UnityEngine;
namespace Project.TofuGirl
{
    public class ProcedureLevel : ProcedureBase
    {       
        /// <summary>
        /// 关卡是否开始
        /// </summary>
        public bool LevelStart { get; private set; }
        /// <summary>
        /// 游戏控制器
        /// </summary>
        public GameController GController { get; private set; }


        public GameManager GM { get; private set; }

        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);           
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            GameEntry.Event.Subscribe(LevelStartEventArgs.EventId, OnLevelStart);
            GameEntry.Event.Subscribe(LevelEndEventArgs.EventId, OnLevelStart);

            GM = GameManager.Create(GameEntry.Data.GetData<DataLevel>().GetLevelData(1001));
            //LevelStart = false;
            //GController = GameController.Create(GameEntry.Data.GetData<DataLevel>().GetLevelData(1001));
            base.OnEnter(procedureOwner);
            //GController.Start();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if((!GM.GameOver)&& GM.GameStart)
            {
                GM.LogicUpdate(elapseSeconds, realElapseSeconds);
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.R))
                {
                    GM.Clear();
                    GM = null;
                    GameEntry.Entity.HideAllLoadedEntities();
                    //切换到Menu流程
                    ChangeState<ProcedureMenu>(procedureOwner);
                }
            }
            //if(LevelStart)
            //{
            //    if((!GController.GameOver)&& GController.GameStart)
            //    {
            //        GController.LogicUpdateAction?.Invoke(elapseSeconds, realElapseSeconds);
            //    }
            //}
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LevelStartEventArgs.EventId, OnLevelStart);
            GameEntry.Event.Unsubscribe(LevelEndEventArgs.EventId, OnLevelStart);
            LevelStart = false;
            base.OnLeave(procedureOwner, isShutdown);

        }

        protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);


        }     
        private void OnLevelStart(object sender,GameEventArgs gEArgs)
        {
            LevelStartEventArgs args = gEArgs as LevelStartEventArgs;
            if(args==null)
            {
                return;
            }
            LevelStart = true;
            Log.Info("关卡开始");
        }
        private void OnLevelEnd(object sender, GameEventArgs gEArgs)
        {
            LevelEndEventArgs args= gEArgs as LevelEndEventArgs;
            if (args == null)
            {
                return;
            }
            LevelStart = false;

        }
    }
}
