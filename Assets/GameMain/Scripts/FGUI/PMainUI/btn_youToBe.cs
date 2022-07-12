/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class btn_youToBe : GButton
    {
        public GTextField text_time;
        public const string URL = "ui://olhhy8tqdati5h";

        public static btn_youToBe CreateInstance()
        {
            return (btn_youToBe)UIPackage.CreateObject("PMainUI", "btn_youToBe");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_time = (GTextField)GetChildAt(1);
        }
    }
}