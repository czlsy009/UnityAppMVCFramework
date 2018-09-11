/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;
using BlankUIFrameWork;

namespace StartPage
{
	public partial class UILoginButton : GButton
	{
		public Controller mbutton;
		public GGraph mn0;
		public GGraph mn1;
		public GGraph mn2;
		public GTextField mtitle;
		public GLoader micon;

		protected static string PackageName
       		{
            		get { return "StartPage"; }
        	}

		protected static string ResName
        	{
            		get { return UIPathTools.Combine("UILoginButton").Substring(2); }
        	}
		public static string URL
        	{
            		get { return UIPackage.GetItemURL(PackageName, ResName); }
        	}

		public static void BindPackageItemExtension()
        	{
            		UIObjectFactory.SetPackageItemExtension(URL, typeof(UILoginButton));
            		
        	}
		public static UILoginButton CreateInstance()
		{
			UIPackage.AddPackage(UIPathTools.CombineMainPackagePath(PackageName));
            BindPackageItemExtension();
            object gObject = UIPackage.CreateObjectFromURL(URL);
            if (gObject is UILoginButton)
            {
                return gObject as UILoginButton;
            }
            else
            {
                UnityEngine.Debug.LogError(" Init Faild !!!PackageName:" + PackageName + " ResName : " + ResName);
                return null;
            }
		}

		public UILoginButton()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			mbutton = this.GetController("button");
			mn0 = (GGraph)this.GetChild("n0");
			mn1 = (GGraph)this.GetChild("n1");
			mn2 = (GGraph)this.GetChild("n2");
			mtitle = (GTextField)this.GetChild("title");
			micon = (GLoader)this.GetChild("icon");
		}
		
		internal static void DisposeInstance()
        	{
            UIPackage.RemovePackage(UIPathTools.CombineMainPackagePath(PackageName));
        	}
	}}