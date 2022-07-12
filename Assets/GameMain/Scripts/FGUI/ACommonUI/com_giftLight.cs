/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.ACommonUI
{
    public partial class com_giftLight : GComponent
    {
        public Transition tra_giftLight;
        public const string URL = "ui://oauc2zgjhi9y56";

        public static com_giftLight CreateInstance()
        {
            return (com_giftLight)UIPackage.CreateObject("ACommonUI", "com_giftLight");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tra_giftLight = GetTransitionAt(0);
        }
    }
}