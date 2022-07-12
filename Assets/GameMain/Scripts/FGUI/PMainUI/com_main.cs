/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class com_main : GComponent
    {
        public GGraph spin_pic;
        public GButton btn_setting;
        public GButton btn_play;
        public GGraph spine_bingo;
        public GTextField text_card;
        public com_played btn_checkpoint;
        public btn_vip btn_vip;
        public btn_youToBe btn_youtobe;
        public btn_cashUpgrade btn_cashUpgrade;
        public btn_luckyWheel btn_luckyWheel;
        public GButton btn_luckySpin;
        public btn_task btn_task;
        public GButton btn_sign;
        public GButton btn_activity;
        public GButton btn_puzzle;
        public com_cardNumber com_card;
        public GTextField lb_txt;
        public GGraph mask;
        public Transition tran_flyText;
        public const string URL = "ui://olhhy8tqhbsv6b";

        public static com_main CreateInstance()
        {
            return (com_main)UIPackage.CreateObject("PMainUI", "com_main");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            spin_pic = (GGraph)GetChildAt(0);
            btn_setting = (GButton)GetChildAt(2);
            btn_play = (GButton)GetChildAt(3);
            spine_bingo = (GGraph)GetChildAt(4);
            text_card = (GTextField)GetChildAt(5);
            btn_checkpoint = (com_played)GetChildAt(6);
            btn_vip = (btn_vip)GetChildAt(9);
            btn_youtobe = (btn_youToBe)GetChildAt(10);
            btn_cashUpgrade = (btn_cashUpgrade)GetChildAt(11);
            btn_luckyWheel = (btn_luckyWheel)GetChildAt(12);
            btn_luckySpin = (GButton)GetChildAt(15);
            btn_task = (btn_task)GetChildAt(16);
            btn_sign = (GButton)GetChildAt(17);
            btn_activity = (GButton)GetChildAt(18);
            btn_puzzle = (GButton)GetChildAt(19);
            com_card = (com_cardNumber)GetChildAt(22);
            lb_txt = (GTextField)GetChildAt(23);
            mask = (GGraph)GetChildAt(24);
            tran_flyText = GetTransitionAt(0);
        }
    }
}