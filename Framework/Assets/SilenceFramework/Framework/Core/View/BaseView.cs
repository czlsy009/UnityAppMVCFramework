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
//  * 文件名：BaseView.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */


namespace BlankFramework
{
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// 视图界面的基类管理器
    /// </summary>
    public class BaseView : MonoBehaviour
    {
        #region Managers
        /// <summary>
        /// 网络管理器
        /// </summary>
        private NetWorkManager m_netWorkManager;
        /// <summary>
        /// 声音管理器
        /// </summary>
        private MusicManager m_musicManager;
        /// <summary>
        /// 时间管理器
        /// </summary>
        private TimerManager m_timerManager;

        private CoroutineManager m_coroutineManager;
        /// <summary>
        /// 时间管理器
        /// </summary>
        protected TimerManager TimerMgr
        {
            get
            {
                if (m_timerManager == null)
                {
                    m_timerManager = AppBridgeLink.Instance.GetManager<TimerManager>(ManagerName.TIMER);
                }
                return m_timerManager;
            }
        }
        /// <summary>
        /// 网络管理器
        /// </summary>
        protected NetWorkManager NetWorkMgr
        {
            get
            {
                if (m_netWorkManager == null)
                {
                    m_netWorkManager = AppBridgeLink.Instance.GetManager<NetWorkManager>(ManagerName.NET_WORK);
                }
                return m_netWorkManager;
            }
        }
        /// <summary>
        /// 声音管理器
        /// </summary>
        protected MusicManager MusicMgr
        {
            get
            {
                if (m_musicManager == null)
                {
                    m_musicManager = AppBridgeLink.Instance.GetManager<MusicManager>(ManagerName.MUSIC);
                }
                return m_musicManager;
            }
        }
        private AppBridgeLink m_appBridgeLink;
        /// <summary>
        /// 核心框架桥梁
        /// </summary>
        protected AppBridgeLink AppBridgeLink
        {
            get
            {
                if (m_appBridgeLink == null)
                {
                    m_appBridgeLink = AppBridgeLink.Instance;
                }
                return m_appBridgeLink;
            }
        }

        /// <summary>
        /// 协程管理器
        /// </summary>
        protected CoroutineManager CoroutineMgr
        {
            get
            {
                if (m_coroutineManager == null)
                {
                    m_coroutineManager = AppBridgeLink.Instance.GetManager<CoroutineManager>(ManagerName.COROUTINE);
                }
                return m_coroutineManager;
            }
        }

        #endregion


        #region Messages

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="view"></param>
        /// <param name="messages"></param>
        protected void RegisterMessage(IView view, List<string> messages)
        {
            if (view != null && messages != null && messages.Count > 0)
            {
                //注册视图中的消息
                BaseController.Instance.RegisterViewCommand(view, messages.ToArray());
            }
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="view"></param>
        /// <param name="messages"></param>
        protected void RemoveMessage(IView view, List<string> messages)
        {
            if (view != null && messages != null && messages.Count > 0)
            {
                //删除视图中的消息
                BaseController.Instance.RemoveViewCommand(view, messages.ToArray());
            }
        }

        #endregion
    }
}