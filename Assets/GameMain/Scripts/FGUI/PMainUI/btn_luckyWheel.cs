/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class btn_luckyWheel : GButton
    {
        public GTextField text_time;
        public const string URL = "ui://olhhy8tqmg4s65";

        public static btn_luckyWheel CreateInstance()
        {
            return (btn_luckyWheel)UIPackage.CreateObject("PMainUI", "btn_luckyWheel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_time = (GTextField)GetChildAt(1);
        }
    }
}