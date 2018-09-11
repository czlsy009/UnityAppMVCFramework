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
//  * 文件名：NetWorkManagerExtensionNetWorkState.cs
//  * 创建时间：2016年05月19日 
//  * 创建人：Blank Alian
//  */


using System;

namespace BlankFramework
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 增加网络状态的扩展
    /// 增加网络状态改变的通知
    /// </summary>
    public partial class NetWorkManager : ITimerBehaviour
    {

        #region Notifycation Content Keys Const
        /// <summary>
        /// 是否有网络
        /// </summary>
        public const string IS_NET_AVAILABLE_KEY = "isNetAvailable";
        /// <summary>
        /// 是否是WIFI 网络
        /// </summary>
        public const string IS_WIFI_KEY = "isWifi";
        #endregion

        #region Notification Const Name
        /// <summary>
        /// 网络发生改变的时候通知名称
        /// </summary>
        public const string NET_WORK_STATE_CHANGED = "NET_WORK_STATE_CHANGED";
        #endregion
        #region Notification Filter Const Name
        /// <summary>
        /// 网络管理器过滤器名称
        /// </summary>
        public const string NET_WORK_FILTER = "NET_WORK_FILTER";
        #endregion


        /// <summary>
        /// 网络是否可用
        /// </summary>
        public static bool IsNetAvailable
        {
            //get { return Application.internetReachability != NetworkReachability.NotReachable; }
            get;
            set;
        }

        /// <summary>
        /// 是否是无线
        /// </summary>
        public static bool IsWifi
        {
            //get { return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork; }
            get;
            set;
        }

        private bool m_lastNetWorkState = IsNetAvailable;
        private TimerInfo m_timerInfo;
        void Start()
        {
            m_timerInfo = new TimerInfo();
            m_timerInfo.ClassName = typeof(NetWorkManager).ToString();
            m_timerInfo.Target = this;
            TimerMgr.AddTimerEvent(m_timerInfo);
        }

        void Update()
        {
            IsWifi = Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
            IsNetAvailable = Application.internetReachability != NetworkReachability.NotReachable;
        }

        #region ITimerBehaviour 成员

        public void TimerUpdate(TimerInfo timerInfo)
        {
            if (m_lastNetWorkState != IsNetAvailable)
            {
                m_lastNetWorkState = IsNetAvailable;
                //  网络状态发生改变的时候发送通知
                NotificationManager.Instance.PostNotification(NET_WORK_STATE_CHANGED, new Dictionary<string, object> { { "object", this }, { IS_NET_AVAILABLE_KEY, m_lastNetWorkState }, { IS_WIFI_KEY, IsWifi } }, NET_WORK_FILTER);
            }
        }

        #endregion
    }
}