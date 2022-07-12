
using GameFramework.Fsm;
using GameFramework.Procedure;
using Project.TofuGirl.Entity;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Event;
using GameFramework.Event;
using Project.TofuGirl.Data;
using FairyGUI;
using UnityEngine;
using UI.PMainUI;
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

            GRoot.inst.SetContentScaleFactor(720, 1280);//设置尺寸大小
            UIPackage.AddPackage("UI");//加载对应的包
            FontManager.RegisterFont(new DynamicFont("Mogra-Regular", Resources.Load<Font>("Mogra-Regular")));//注册字体
            PMainUIBinder.BindAll();//建立绑定关系
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

        #region 测试代码
        private void InitUI()
        {

        }

        #endregion

    }
}
