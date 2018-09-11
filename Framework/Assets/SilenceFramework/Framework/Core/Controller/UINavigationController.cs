// /**
//  *　　　　　　　　┏┓　　　┏┓+ +
//  *　　　　　　　┏┛┻━━━┛┻┓ + +
//  *　　　　　　　┃　　　　　　　┃ 　
//  *　　　　　　　┃　　　━　　　┃ ++ + + +
//  *　　　　　　 ████━████ ┃+
//  *　　　　　　　┃　　　　　　　┃ +
//  *　　　　　　　┃　　　┻　　　┃
//  *　　　　　　　┃　　　　　　　┃ + +
//  *　　　　　　　┗━┓　　　┏━┛
//  *　　　　　　　　　┃　　　┃　　　　　　　　　　　
//  *　　　　　　　　　┃　　　┃ + + + +
//  *　　　　　　　　　┃　　　┃　　　　Code is far away from bug with the animal protecting　　　　　　　
//  *　　　　　　　　　┃　　　┃ + 　　　　神兽保佑,代码无bug　　
//  *　　　　　　　　　┃　　　┃
//  *　　　　　　　　　┃　　　┃　　+　　　　　　　　　
//  *　　　　　　　　　┃　 　　┗━━━┓ + +
//  *　　　　　　　　　┃ 　　　　　　　┣┓
//  *　　　　　　　　　┃ 　　　　　　　┏┛
//  *　　　　　　　　　┗┓┓┏━┳┓┏┛ + + + +
//  *　　　　　　　　　　┃┫┫　┃┫┫
//  *　　　　　　　　　　┗┻┛　┗┻┛+ + + +
//  *
//  *
//  * ━━━━━━感觉萌萌哒━━━━━━
//  * 
//  * 说明：
//  * 
//  * 文件名：UINavigationController.cs
//  * 创建时间：2016年08月19日 
//  * 创建人：Blank Alian
//  */

using BlankUIFrameWork;
using FairyGUI;

namespace BlankFramework
{
    public class UINavigationController : UIViewController
    {
        public static UINavigationController Instance;

        /// <summary>
        /// 退回到某一个控制器
        /// </summary>
        /// <param name="viewController"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public UIViewController PopToViewController(UIViewController viewController, bool animated)
        {
            for (int i = ViewControllers.Count - 1; i > 0; i--)
            {
                if (ViewControllers[i].Equals(viewController))
                {
                    return ViewControllers[i];
                }
                else
                {
                    ViewControllers[i].DismissViewController();
                    ViewControllers.RemoveAt(i);
                }
            }
            return null;
        }


        /// <summary>
        /// 返回到最初的控制器
        /// </summary>
        public void PopToRootViewController()
        {
            for (int i = ViewControllers.Count - 1; i > 0; i--)
            {
                ViewControllers[i].DismissViewController();
                ViewControllers.RemoveAt(i);
            }
        }


        /// <summary>
        /// 跳转到新的控制器
        /// </summary>
        /// <param name="viewController"></param>
        /// <param name="animated"></param>
        public virtual void PushViewController(UIViewController viewController, bool animated = false)
        {

            ViewControllers.Add(viewController);
            viewController.sortingOrder = LayerOrder + 1;
            
          
            UIRoot.Instance.NormalRoot.AddChild(viewController);
        }

        /// <summary>
        /// 销毁 Push 出来的控制器
        /// </summary>
        /// <param name="animated"></param>
        public virtual UIViewController PopViewController(bool animated = false)
        {
            GObject[] gObjects = UIRoot.Instance.NormalRoot.GetChildren();
            int index = gObjects.Length - 1;
            UIViewController pushViewController = (UIViewController)UIRoot.Instance.NormalRoot.GetChildAt(index);
            pushViewController.DismissViewController(animated);
            ViewControllers.Remove(pushViewController);
            return pushViewController;
        }
    }
}