
using GameFramework.Fsm;
using GameFramework.Procedure;
using Project.TofuGirl.Entity;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Event;
using GameFramework.Event;
using Project.TofuGirl.Data;

namespace Project.TofuGirl
{
    public class ProcedureMenu : ProcedureBase
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

            ChangeState<ProcedureLevel>(procedureOwner);
        }


        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);


        }
    }
}
