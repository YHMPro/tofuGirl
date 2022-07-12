/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class com_vip : GComponent
    {
        public Transition tra_vip;
        public const string URL = "ui://olhhy8tqkvi25v";

        public static com_vip CreateInstance()
        {
            return (com_vip)UIPackage.CreateObject("PMainUI", "com_vip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tra_vip = GetTransitionAt(0);
        }
    }
}