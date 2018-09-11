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
//  * 文件名：BridgeLink.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */


namespace BlankFramework
{


    using System;
    using System.Collections.Generic;
    using UnityEngine;


    public class BridgeLink
    {
        protected IController m_controller;
        static GameObject _sceneManager;
        static Dictionary<string, object> _managers = new Dictionary<string, object>();

        #region Init

        /// <summary>
        /// 场景管理器对象
        /// </summary>
        GameObject AppSceneManager
        {
            get
            {
                if (_sceneManager == null)
                {
                    string name = "Main";
                    GameObject manager = GameObject.Find(name);
                    if (manager == null)
                    {
                        manager = new GameObject(name);
                        manager.name = name;
                    }
                    _sceneManager = manager;
                }
                return _sceneManager;
            }
        }

        protected BridgeLink()
        {
            InitFramework();
        }

        /// <summary>
        /// 初始化框架
        /// </summary>
        protected virtual void InitFramework()
        {
            if (m_controller == null)
            {
                m_controller = BaseController.Instance;
            }
        }

        #endregion

        #region CRUD Command

        /// <summary>
        /// 注册执行命令
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="commandType"></param>
        public virtual void RegisterCommand(string commandName, Type commandType)
        {
            m_controller.RegisterCommand(commandName, commandType);
        }
        /// <summary>
        /// 删除执行命令
        /// </summary>
        /// <param name="commandName"></param>
        public virtual void RemoveCommand(string commandName)
        {
            m_controller.RemoveCommand(commandName);
        }
        /// <summary>
        /// 检查是否有该条命令
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public virtual bool HasCommand(string commandName)
        {
            return m_controller.HasCommand(commandName);
        }
        /// <summary>
        /// 注册多条执行消息
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandNames"></param>
        public void RegisterMultiCommand(Type commandType, params string[] commandNames)
        {
            int count = commandNames.Length;
            for (int i = 0; i < count; i++)
            {
                RegisterCommand(commandNames[i], commandType);
            }
        }

        /// <summary>
        /// 删除多条执行消息
        /// </summary>
        /// <param name="commandName"></param>
        public void RemoveMultiCommand(params string[] commandName)
        {
            int count = commandName.Length;
            for (int i = 0; i < count; i++)
            {
                RemoveCommand(commandName[i]);
            }
        }

        /// <summary>
        /// 发送执行消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="body"></param>
        public void SendMessageCommand(string message, object body = null)
        {
            m_controller.ExecuteCommand(new MessageModel(message, body));
        }

        #endregion

        #region CRUD Manager

        /// <summary>
        /// 添加管理器
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <param name="typeClass">类</param>
        public void AddManager(string typeName, object typeClass)
        {
            if (!_managers.ContainsKey(typeName))
            {
                _managers.Add(typeName, typeClass);
            }
        }

        /// <summary>
        /// 添加Unity对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName">类型名</param>
        /// <returns></returns>
        public T AddManager<T>(string typeName) where T : Component
        {
            if (_managers.ContainsKey(typeName))
            {
                object result = null;
                _managers.TryGetValue(typeName, out result);
                if (result != null && !result.ToString().Equals("null"))
                {
                    return result as T;
                }
                else
                {
                    _managers[typeName] = AppSceneManager.AddComponent<T>();
                    return default(T);
                }
            }
            else
            {
                Component c = AppSceneManager.AddComponent<T>();
                _managers.Add(typeName, c);
                return default(T);
            }
        }
        /// <summary>
        /// 获取系统管理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName">类型名</param>
        /// <returns></returns>
        public T GetManager<T>(string typeName) where T : class
        {
            if (_managers.ContainsKey(typeName))
            {
                object manager = null;
                _managers.TryGetValue(typeName, out manager);
                return manager as T;
            }
            return null;
        }
        /// <summary>
        /// 删除管理器
        /// </summary>
        /// <param name="typeName">类型名</param>
        public void RemoveManager(string typeName)
        {
            if (_managers.ContainsKey(typeName))
            {
                object manager = null;
                _managers.TryGetValue(typeName, out manager);
                if (manager != null)
                {
                    Type type = manager.GetType();
                    if (type.IsSubclassOf(typeof(MonoBehaviour)))
                    {
                        GameObject.Destroy((Component)manager);
                    }
                    _managers.Remove(typeName);
                }

            }
        }
        #endregion
    }
}