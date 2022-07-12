/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class com_cardNumber : GComponent
    {
        public GTextField text_num;
        public const string URL = "ui://olhhy8tqqr938p";

        public static com_cardNumber CreateInstance()
        {
            return (com_cardNumber)UIPackage.CreateObject("PMainUI", "com_cardNumber");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_num = (GTextField)GetChildAt(1);
        }
    }
}