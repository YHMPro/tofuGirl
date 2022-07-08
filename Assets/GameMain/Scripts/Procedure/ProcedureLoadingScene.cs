

using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Entity;
using UnityEngine;
using GameFramework.Event;
using Project.TofuGirl.Data;
namespace Project.TofuGirl
{

    public class ProcedureLoadingScene : ProcedureBase
    {
        private bool m_LoadSceneFinish;
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner); 
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            m_LoadSceneFinish = false;
            GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            base.OnEnter(procedureOwner);        
            LoadScene();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            //if(!m_LoadSceneFinish)
            //{
            //    return;
            //}
            ChangeState<ProcedureLevel>(procedureOwner);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            base.OnLeave(procedureOwner, isShutdown);   
        }

        protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
        private void LoadScene()
        {
            
            string assetsName = GameEntry.Data.GetData<DataAssetsPath>().GetAssetsPathData(2002).Name;
            GameEntry.Scene.LoadScene(assetsName,0,this);
        }

        private void OnLoadSceneSuccess(object sender,GameEventArgs gEArgs)
        {
            LoadSceneSuccessEventArgs args = gEArgs as LoadSceneSuccessEventArgs;
            if(args.UserData!=this)
            {
                return;
            }
            Log.Info("加载场景成功");
            m_LoadSceneFinish = true;
        }

        private void OnLoadSceneFailure(object sender, GameEventArgs gEArgs)
        {
            LoadSceneFailureEventArgs args = gEArgs as LoadSceneFailureEventArgs;
            if (args.UserData != this)
            {
                return;
            }
            Log.Info("加载场景失败");

        }
    }
}
