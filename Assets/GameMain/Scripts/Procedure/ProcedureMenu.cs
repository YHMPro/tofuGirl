
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
using Project.TofuGirl.UI;
namespace Project.TofuGirl
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_UILoadFinish = false;
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);


        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            InitUI();
            
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (m_UILoadFinish)
            {
                ChangeState<ProcedureLevel>(procedureOwner);
            }
        }


        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);


        }

        #region 测试代码
        private void InitUI()
        {
            GRoot.inst.SetContentScaleFactor(720, 1280);//设置尺寸大小
            UIPackage.AddPackage("PMainUI");//加载对应的包
            PMainUIBinder.BindAll();//建立绑定关系
            RegisterFonts(new string []{ "BLUELAGOON", "BalooBhaina", "Peralta", "kabelu", });
            LoadMainWindow();
        }
        private void RegisterFonts(string[] fontNames)
        {
            foreach(string fontName in fontNames)
            {
                FontManager.RegisterFont(new DynamicFont(fontName, Resources.Load<Font>(fontName)));//注册字体
            }
        }

        private void LoadMainWindow()
        {
            //实例ItemWindow
            FWindowManager.AddWindow(com_main.URL, (widnow) =>
            {
                //实例窗口
                widnow.contentPane = com_main.CreateInstance();
                //设置为非模态窗口
                widnow.modal = false;
                //显示这个窗口
                widnow.Show();
                //初始化ItemWindow
                (widnow.contentPane as com_main).Init();
            });




            m_UILoadFinish = true;
        }
        #endregion

    }
}
