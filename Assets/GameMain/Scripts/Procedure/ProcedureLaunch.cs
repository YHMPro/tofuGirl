using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;
using UnityEngine;
using System;
using GameFramework.Event;
namespace Project.TofuGirl
{
    /// <summary>
    /// 负责内容；1.初始化语言相关的操作 
    /// </summary>
    public class ProcedureLaunch : ProcedureBase
    {

        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
           
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            ChangeState<ProcedureSplash>(procedureOwner);//切换到Splash流程
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

        }






       
    }
}
