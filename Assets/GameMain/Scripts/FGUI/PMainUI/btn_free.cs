/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class btn_free : GComponent
    {
        public GTextField title;
        public GTextField title_2;
        public const string URL = "ui://olhhy8tqwilx98";

        public static btn_free CreateInstance()
        {
            return (btn_free)UIPackage.CreateObject("PMainUI", "btn_free");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            title = (GTextField)GetChildAt(1);
            title_2 = (GTextField)GetChildAt(2);
        }
    }
}