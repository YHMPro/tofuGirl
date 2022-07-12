/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.ACommonUI
{
    public partial class com_light : GComponent
    {
        public Transition tra_light;
        public const string URL = "ui://oauc2zgjk5vu39";

        public static com_light CreateInstance()
        {
            return (com_light)UIPackage.CreateObject("ACommonUI", "com_light");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tra_light = GetTransitionAt(0);
        }
    }
}