/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.ACommonUI
{
    public partial class com_lightW : GComponent
    {
        public Transition t0;
        public const string URL = "ui://oauc2zgjimds9";

        public static com_lightW CreateInstance()
        {
            return (com_lightW)UIPackage.CreateObject("ACommonUI", "com_lightW");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            t0 = GetTransitionAt(0);
        }
    }
}