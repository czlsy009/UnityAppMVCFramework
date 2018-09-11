/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;
using BlankUIFrameWork;

namespace StartPage
{
    public partial class UIStartPage : GComponent
    {
        public UILoginButton mLoginButton;

        protected static string PackageName
        {
            get { return "StartPage"; }
        }

        protected static string ResName
        {
            get { return UIPathTools.Combine("UIStartPage").Substring(2); }
        }
        public static string URL
        {
            get { return UIPackage.GetItemURL(PackageName, ResName); }
        }

        public static void BindPackageItemExtension()
        {
            UIObjectFactory.SetPackageItemExtension(URL, typeof(UIStartPage));
            UILoginButton.BindPackageItemExtension();

        }
        public static UIStartPage CreateInstance()
        {
            UIPackage.AddPackage(UIPathTools.CombineMainPackagePath(PackageName));
            BindPackageItemExtension();
            object gObject = UIPackage.CreateObjectFromURL(URL);
            if (gObject is UIStartPage)
            {
                return gObject as UIStartPage;
            }
            else
            {
                UnityEngine.Debug.LogError(" Init Faild !!!PackageName:" + PackageName + " ResName : " + ResName);
                return null;
            }
        }

        public UIStartPage()
        {
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            mLoginButton = (UILoginButton)this.GetChild("LoginButton");
        }

        internal static void DisposeInstance()
        {
            UIPackage.RemovePackage(UIPathTools.CombineMainPackagePath(PackageName));
        }
    }
}