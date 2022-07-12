/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class btn_cashUpgrade : GButton
    {
        public GTextField text_value;
        public const string URL = "ui://olhhy8tqmg4s66";

        public static btn_cashUpgrade CreateInstance()
        {
            return (btn_cashUpgrade)UIPackage.CreateObject("PMainUI", "btn_cashUpgrade");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_value = (GTextField)GetChildAt(1);
        }
    }
}