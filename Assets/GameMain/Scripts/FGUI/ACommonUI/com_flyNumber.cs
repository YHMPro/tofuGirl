/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.ACommonUI
{
    public partial class com_flyNumber : GComponent
    {
        public Controller cont_color;
        public GTextField text_yellow;
        public GTextField text_green;
        public GTextField text_white;
        public const string URL = "ui://oauc2zgjucqs4g";

        public static com_flyNumber CreateInstance()
        {
            return (com_flyNumber)UIPackage.CreateObject("ACommonUI", "com_flyNumber");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_color = GetControllerAt(0);
            text_yellow = (GTextField)GetChildAt(0);
            text_green = (GTextField)GetChildAt(1);
            text_white = (GTextField)GetChildAt(2);
        }
    }
}