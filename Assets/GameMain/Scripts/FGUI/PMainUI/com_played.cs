/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class com_played : GComponent
    {
        public GButton btn_add;
        public GButton btn_del;
        public GTextField text_checkPoint;
        public const string URL = "ui://olhhy8tqessm89";

        public static com_played CreateInstance()
        {
            return (com_played)UIPackage.CreateObject("PMainUI", "com_played");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            btn_add = (GButton)GetChildAt(1);
            btn_del = (GButton)GetChildAt(2);
            text_checkPoint = (GTextField)GetChildAt(3);
        }
    }
}