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
//  * 文件名：Controller.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */


namespace BlankFramework
{
    using System;
    using System.Collections.Generic;

    public class BaseController : IController
    {
        /// <summary>
        /// 控制层 池
        /// </summary>
        protected IDictionary<string, Type> m_commandMap;
        /// <summary>
        /// 视图层 池
        /// </summary>
        protected IDictionary<IView, List<string>> m_viewCommandMap;
        /// <summary>
        /// volatile   标记每次都从内存中获取最新的,不是从缓存中获取,不论线程是否在其他线程更新这个变量的值
        /// </summary>
        protected static volatile IController m_instance;
        protected static readonly object m_syncRoot = new object();
        protected static readonly object m_staticSyncRoot = new object();
        /// <summary>
        /// 单例对象
        /// </summary>
        public static IController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new BaseController();
                        }
                    }
                }
                return m_instance;
            }
        }
        #region Init

        protected BaseController()
        {
            InitializeController();
        }

        static BaseController()
        {

        }
        /// <summary>
        /// 初始化控制器
        /// </summary>
        private void InitializeController()
        {
            m_commandMap = new Dictionary<string, Type>();
            m_viewCommandMap = new Dictionary<IView, List<string>>();
        }

        #endregion
        #region Execute  Command 
        /// <summary>
        /// 执行命令消息
        /// </summary>
        /// <param name="message"></param>
        public void ExecuteCommand(IMessageModel message)
        {
            Type commandType = null;
            List<IView> views = null;
            lock (m_syncRoot)
            {
                #region Sreach Command Line
                if (m_commandMap.ContainsKey(message.Name))
                {
                    commandType = m_commandMap[message.Name];
                }
                else
                {
                    views = new List<IView>();
                    foreach (KeyValuePair<IView, List<string>> keyValuePair in m_viewCommandMap)
                    {
                        if (keyValuePair.Value.Contains(message.Name))
                        {
                            views.Add(keyValuePair.Key);
                        }
                    }
                }

                #endregion
                #region Command Line

                if (commandType != null)
                {
                    object commandInstance = Activator.CreateInstance(commandType);

                    ICommand command = commandInstance as ICommand;
                    if (command != null)
                    {
                        command.Execute(message);
                    }
                }
                #endregion
                #region Send View Message

                if (views != null && views.Count > 0)
                {
                    int viewsCount = views.Count;
                    for (int i = 0; i < viewsCount; i++)
                    {
                        IView view = views[i];
                        view.OnMessage(message);
                    }
                    views = null;
                }
                #endregion
            }
        } 
        #endregion
        #region CRUD Command


        /// <summary>
        /// 删除控制命令
        /// </summary>
        /// <param name="commandName"></param>
        public void RemoveCommand(string commandName)
        {
            lock (m_syncRoot)
            {
                if (m_commandMap.ContainsKey(commandName))
                {
                    m_commandMap.Remove(commandName);
                }
            }
        }
        /// <summary>
        /// 注册控制命令
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="commandType"></param>
        public void RegisterCommand(string commandName, System.Type commandType)
        {
            lock (m_syncRoot)
            {
                if (m_commandMap.ContainsKey(commandName))
                {
                    m_commandMap[commandName] = commandType;
                }
                else
                {
                    m_commandMap.Add(commandName, commandType);
                }
            }
        }
        /// <summary>
        /// 注册视图层的命令
        /// </summary>
        /// <param name="view"></param>
        /// <param name="commandNames"></param>
        public void RegisterViewCommand(IView view, string[] commandNames)
        {
            lock (m_syncRoot)
            {
                if (m_viewCommandMap.ContainsKey(view))
                {
                    List<string> commandNameList = null;
                    if (m_viewCommandMap.TryGetValue(view, out commandNameList))
                    {
                        for (int i = 0; i < commandNames.Length; i++)
                        {
                            if (commandNameList.Contains(commandNames[i]))
                            {
                                continue;
                            }
                            else
                            {
                                commandNameList.Add(commandNames[i]);
                            }
                        }
                    }
                }
                else
                {
                    m_viewCommandMap.Add(view, new List<string>(commandNames));
                }
            }
        }
        /// <summary>
        /// 删除视图层的命令
        /// </summary>
        /// <param name="view"></param>
        /// <param name="commandNames"></param>
        public void RemoveViewCommand(IView view, string[] commandNames)
        {
            lock (m_syncRoot)
            {
                if (m_viewCommandMap.ContainsKey(view))
                {
                    List<string> list = null;
                    if (m_viewCommandMap.TryGetValue(view, out list))
                    {
                        for (int i = 0; i < commandNames.Length; i++)
                        {
                            if (list.Contains(commandNames[i]))
                            {
                                list.Remove(commandNames[i]);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 检查控制层的命令是否存在
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public bool HasCommand(string commandName)
        {
            lock (m_syncRoot)
            {
                return m_commandMap.ContainsKey(commandName);
            }
        } 
        #endregion
    }
}