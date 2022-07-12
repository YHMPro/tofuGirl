/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class com_resurrection : GComponent
    {
        public GTextField title;
        public const string URL = "ui://olhhy8tqwilx93";

        public static com_resurrection CreateInstance()
        {
            return (com_resurrection)UIPackage.CreateObject("PMainUI", "com_resurrection");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            title = (GTextField)GetChildAt(4);
        }
    }
}