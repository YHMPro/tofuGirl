/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.PMainUI
{
    public partial class com_giftLight : GComponent
    {
        public Transition tra_giftLight;
        public const string URL = "ui://olhhy8tqkvi25y";

        public static com_giftLight CreateInstance()
        {
            return (com_giftLight)UIPackage.CreateObject("PMainUI", "com_giftLight");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tra_giftLight = GetTransitionAt(0);
        }
    }
}