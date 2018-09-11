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
//  * 文件名：View.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */

using System.Collections.Generic;
using BlankUIFrameWork;
using FairyGUI;

namespace BlankFramework
{

    /// <summary>
    /// UI  界面 控制器
    /// </summary>
    public class UIViewController : GComponent, IView
    {
        /// <summary>
        /// 界面下的所有界面控制器
        /// </summary>
        protected readonly List<UIViewController> ViewControllers = new List<UIViewController>();
        /// <summary>
        /// Present Modal ViewController
        /// </summary>
        public UIViewController PresentedViewController { protected set; get; }
        /// <summary>
        /// Presenting Modal ViewController
        /// </summary>
        public UIViewController PresentingViewController { protected set; get; }

        public int LayerOrder { private set; get; }

        /// <summary>
        /// 根VIEW 
        /// </summary>
        private GComponent m_view;
        public GComponent View
        {
            get { return m_view; }
        }

        public UIViewController()
        {
            // 监听 加载到屏幕上的事件
            m_view.onAddedToStage.Add(ViewDidAppear);
        }

        /// <summary>
        /// 创建 物体的时候执行
        /// </summary>
        protected override void CreateDisplayObject()
        {
            // 页面即将开始加载
            ViewWillLoad();
            // 设置 主UI
            m_view = this;
            LayerOrder = 0;
            base.CreateDisplayObject();
            ViewWillAppear();
            AddObserver();
        }
        /// <summary>
        /// UI 对象销毁的时候调用
        /// </summary>
        public override void Dispose()
        {
            RemoveObserver();
            // 页面被销毁的时候执行
            Dealloc();
            base.Dispose();
        }
        #region Show

        /// <summary>
        /// 页面开始创建。页面开始加载
        /// </summary>
        protected virtual void ViewWillLoad()
        {
            Log.W(GetType() + " 页面 开始加载 ");
        }
        /// <summary>
        /// 页面即将显示到屏幕上
        /// </summary>
        protected virtual void ViewWillAppear()
        {
            Log.W(GetType() + " 页面即将 显示到屏幕 上 ");
        }
        /// <summary>
        /// 页面已经在屏幕上渲染完成
        /// </summary>
        /// <param name="eventContext"></param>
        protected virtual void ViewDidAppear(EventContext eventContext)
        {
            Log.W(GetType() + " 页面已经在界面上渲染完成 ");
        }


        #endregion

        #region Hide
        /// <summary>
        /// 页面即将从屏幕上移除
        /// </summary>
        protected virtual void ViewWillDisappear(GObject child)
        {
            Log.W(GetType() + " 页面即将移除 ");
        }
        /// <summary>
        /// 页面已经从屏幕上移除。用户已经看不见页面了
        /// </summary>
        protected virtual void ViewDidDisappear()
        {
            Log.W(GetType() + " 页面已经从屏幕上移除。用户已经看不见页面了 ");
        }
        /// <summary>
        /// 视图界面被销毁之前的时候执行
        /// </summary>
        protected virtual void Dealloc()
        {
            Log.W(GetType() + " 页面销毁 ");
        }


        #endregion


        public void AddChildViewController(UIViewController viewController)
        {
            ViewControllers.Add(viewController);
        }

        public List<UIViewController> GetChildViewControllers()
        {
            return ViewControllers;
        }
        /// <summary>
        /// 适用于 UIViewController
        /// 使用 DismissViewController 销毁
        /// </summary>
        /// <param name="viewController"></param>
        /// <param name="animated"></param>
        public virtual void PresentViewController(UIViewController viewController, bool animated = false)
        {
            if (PresentingViewController != null)
            {
                UIRoot.Instance.PopupRoot.RemoveChild(PresentingViewController);
            }
            viewController.sortingOrder = LayerOrder + 1;
            PresentedViewController = this;
            PresentingViewController = viewController;

            UIRoot.Instance.PopupRoot.AddChild(viewController);
        }

        /// <summary>
        ///  销毁 
        ///  PresentViewController 中的View Controller 
        /// </summary>
        /// <param name="animated"></param>
        public virtual void DismissViewController(bool animated = false)
        {
            try
            {
                ViewWillDisappear(this);
                RemoveChildren();
                RemoveFromParent();
                ViewDidDisappear();
                PresentedViewController = null;
                PresentingViewController = null;
                Dispose();
            }
            catch
            {
            }
        }
        /// <summary>
        /// 执行发送消息
        /// </summary>
        /// <param name="message"></param>
        public virtual void OnMessage(IMessageModel message)
        {
        }

        /// <summary>
        /// 注册通知专用函数
        /// </summary>
        protected virtual void AddObserver()
        {
            Log.W(GetType() + " 注册监听通知 ");
        }

        /// <summary>
        /// 取消注册通知专用函数
        /// </summary>
        protected virtual void RemoveObserver()
        {
            Log.W(GetType() + " 移除监听通知 ");
        }
    }
}