/**
 *　　　　　　　　┏┓　　　┏┓+ +
 *　　　　　　　┏┛┻━━━┛┻┓ + +
 *　　　　　　　┃　　　　　　　┃ 　
 *　　　　　　　┃　　　━　　　┃ ++ + + +
 *　　　　　　 ████━████ ┃+
 *　　　　　　　┃　　　　　　　┃ +
 *　　　　　　　┃　　　┻　　　┃
 *　　　　　　　┃　　　　　　　┃ + +
 *　　　　　　　┗━┓　　　┏━┛
 *　　　　　　　　　┃　　　┃　　　　　　　　　　　
 *　　　　　　　　　┃　　　┃ + + + +
 *　　　　　　　　　┃　　　┃　　　　Code is far away from bug with the animal protecting　　　　　　　
 *　　　　　　　　　┃　　　┃ + 　　　　神兽保佑,代码无bug　　
 *　　　　　　　　　┃　　　┃
 *　　　　　　　　　┃　　　┃　　+　　　　　　　　　
 *　　　　　　　　　┃　 　　┗━━━┓ + +
 *　　　　　　　　　┃ 　　　　　　　┣┓
 *　　　　　　　　　┃ 　　　　　　　┏┛
 *　　　　　　　　　┗┓┓┏━┳┓┏┛ + + + +
 *　　　　　　　　　　┃┫┫　┃┫┫
 *　　　　　　　　　　┗┻┛　┗┻┛+ + + +
 *
 *
 * ━━━━━━感觉萌萌哒━━━━━━
 * 
 * 说明：
 * 
 * 文件名：NotificationManager.cs
 * 创建时间：2016年08月03日 
 * 创建人：Blank Alian
 */


using System;

namespace BlankFramework
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 通知中心管理器
    /// </summary>
    public class NotificationManager : MonoBehaviour
    {

        protected class NotificationModel
        {
            public string notifyName;
            public List<BlankActionForObject> observerList;
            public string filterName;
        }

        private static NotificationManager _instance;
        private readonly static object Obj = new object();
        private readonly Dictionary<string, NotificationModel> m_dictionary = new Dictionary<string, NotificationModel>();
        #region PublicAPI
        private NotificationManager()
        {
        }
        public static NotificationManager Instance
        {
            get
            {
                lock (Obj)
                {
                    if (_instance == null)
                    {
                        _instance = AppBridgeLink.Instance.GetManager<NotificationManager>(ManagerName.NOTIFICATION);
                    }
                    return _instance;
                }
            }
        }


        /// <summary>
        /// 添加单个通知监听
        /// </summary>
        /// <param name="notifyName">通知名称</param>
        /// <param name="sender"></param>
        /// <param name="action">执行函数回调</param>
        public void AddObserver(string notifyName, string sender, BlankActionForObject action)
        {

            // Log.W(sender, "SENDER");
            if (m_dictionary.ContainsKey(notifyName))
            {
                NotificationModel notificationModel = m_dictionary[notifyName];
                if (notificationModel != null)
                {
                    notificationModel.filterName = sender;
                    notificationModel.notifyName = notifyName;
                    List<BlankActionForObject> list = notificationModel.observerList;
                    if (list != null && list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Equals(action))
                            {
                                list[i] = action;
                                return;
                            }
                        }
                        list.Add(action);
                    }
                    else
                    {
                        m_dictionary[notifyName] = notificationModel;
                    }
                }
            }
            else
            {
                m_dictionary.Add(notifyName, new NotificationModel { notifyName = notifyName, observerList = new List<BlankActionForObject> { action }, filterName = sender });
            }
        }
        /// <summary>
        /// 移除通知名称 下面的所有通知
        /// </summary>
        /// <param name="notifyName">通知名称</param>
        /// <param name="action">动作</param>
        public void RemoveObserver(string notifyName, BlankActionForObject action)
        {
            if (m_dictionary.ContainsKey(notifyName))
            {
                // 移除values 中的数据
                NotificationModel notificationModel = m_dictionary[notifyName];
                if (notificationModel != null)
                {
                    if (notificationModel.observerList != null && notificationModel.observerList.Remove(action))
                    {
                        if (notificationModel.observerList.Count <= 0)
                        {
                            // 当values 的长度为0 时 移除整个通知
                            m_dictionary.Remove(notifyName);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notifyName">通知名称</param>
        /// <param name="data">数据</param>
        /// <param name="senderObject"></param>
        public void PostNotification(string notifyName, object data, string senderObject)
        {
            NotificationModel valueActions;
            if (m_dictionary.TryGetValue(notifyName, out valueActions))
            {
                if (valueActions != null)
                {
                    if (valueActions.filterName != null)
                    {
                        if (valueActions.filterName.Equals(senderObject))
                        {
                            if (valueActions.observerList != null && valueActions.observerList.Count > 0)
                            {
                                for (int i = 0; i < valueActions.observerList.Count; i++)
                                {
                                    try
                                    {
                                        valueActions.observerList[i](data);
                                    }
                                    catch
                                    {
                                        // ignored
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (valueActions.observerList != null && valueActions.observerList.Count > 0)
                        {
                            for (int i = 0; i < valueActions.observerList.Count; i++)
                            {
                                try
                                {
                                    valueActions.observerList[i](data);
                                }
                                catch
                                {
                                    // ignored
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
