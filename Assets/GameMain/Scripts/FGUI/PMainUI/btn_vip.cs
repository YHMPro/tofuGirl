/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class btn_vip : GButton
    {
        public Controller cont_spin;
        public const string URL = "ui://olhhy8tqkvi25s";

        public static btn_vip CreateInstance()
        {
            return (btn_vip)UIPackage.CreateObject("PMainUI", "btn_vip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_spin = GetControllerAt(0);
        }
    }
}