/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class btn_task : GButton
    {
        public Controller cont_red;
        public const string URL = "ui://olhhy8tqhbsv79";

        public static btn_task CreateInstance()
        {
            return (btn_task)UIPackage.CreateObject("PMainUI", "btn_task");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_red = GetControllerAt(0);
        }
    }
}