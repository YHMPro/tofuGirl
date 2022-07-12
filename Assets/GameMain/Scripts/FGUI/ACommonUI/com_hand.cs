/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.ACommonUI
{
    public partial class com_hand : GComponent
    {
        public Transition tra_hand;
        public const string URL = "ui://oauc2zgjk5vu38";

        public static com_hand CreateInstance()
        {
            return (com_hand)UIPackage.CreateObject("ACommonUI", "com_hand");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tra_hand = GetTransitionAt(0);
        }
    }
}