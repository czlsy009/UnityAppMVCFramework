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
//  * 文件名：SceneManager.cs
//  * 创建时间：2016年07月28日 
//  * 创建人：Blank Alian
//  */

using System.Collections;
using UnityEngine;

namespace BlankFramework
{
    /// <summary>
    /// 场景加载中的委托
    /// </summary>
    public delegate void LoadingSceneCallback(float progressValue);

    /// <summary>
    /// 场景加载完成的委托
    /// </summary>
    /// <param name="targetLevelName"></param>
    /// <param name="srcLevelName"></param>
    public delegate void LoadedSceneCallback(string targetLevelName, string srcLevelName);

    public class SceneManager : MonoBehaviour
    {
        /// <summary>
        /// 加载的目标场景名
        /// </summary>
        public string LoadLevelName { get; set; }
        /// <summary>
        /// 上一个场景名
        /// </summary>
        public string LoadedLevelName { get; set; }


        private static SceneManager _instance;
        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AppBridgeLink.Instance.GetManager<SceneManager>(ManagerName.SCENE);
                }
                return _instance;
            }
        }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        private bool m_isDone;
        private AsyncOperation m_asyncOperation;
        private SceneManager() { }

        public event LoadingSceneCallback LoadingSceneEvent;

        public event LoadedSceneCallback LoadedSceneEvent;
        public void LoadLevel(string sceneName)
        {
            Application.LoadLevel(sceneName);
        }
        public void LoadLevel(int index)
        {
            Application.LoadLevel(index);
        }

        /// <summary>
        /// 追加的方式 打开场景
        /// </summary>
        public void LoadLevelAdditiveAsync(string targetLevelName)
        {
            m_isDone = false;
            LoadedLevelName = Application.loadedLevelName;
            LoadLevelName = targetLevelName;
            Debug.Log(targetLevelName);
            StartCoroutine(LoadScene(targetLevelName));
        }

        public void LoadLevelLoadingAdditiveAsync(string targetLevelName)
        {
            m_isDone = false;
            LoadLevelName = targetLevelName;
            //Application.LoadLevel(AppScenesNameConst.SCENE_LOADING);
            // LoadLevelAdditiveAsync(targetLevelName);
        }


        IEnumerator LoadScene(string targetLevelName)
        {
            m_asyncOperation = Application.LoadLevelAdditiveAsync(targetLevelName);
            yield return m_asyncOperation;
        }


        void Update()
        {
            if (m_asyncOperation != null)
            {
                if (m_asyncOperation.isDone && m_isDone == false)
                {
                    m_isDone = true;
                    OnLoadedSceneEvent(LoadLevelName, LoadedLevelName);
                    m_asyncOperation = null;
                }
                else
                {
                    float progess = (m_asyncOperation.progress / 0.90f) * 100;
                    if (progess > 100)
                    {
                        progess = 100;
                    }
                    OnLoadingSceneEvent(progess);
                }
            }
        }
        protected virtual void OnLoadingSceneEvent(float progressValue)
        {
            var handler = LoadingSceneEvent;
            if (handler != null)
            {
                handler(progressValue);
            }
        }

        protected virtual void OnLoadedSceneEvent(string targetlevelname, string srclevelname)
        {
            var handler = LoadedSceneEvent;
            if (handler != null)
            {
                handler(targetlevelname, srclevelname);
            }
        }
    }
}