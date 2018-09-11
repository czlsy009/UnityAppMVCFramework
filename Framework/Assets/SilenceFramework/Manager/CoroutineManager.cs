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
//  * 文件名：CoroutineManager.cs
//  * 创建时间：2016年08月03日 
//  * 创建人：Blank Alian
//  */




namespace BlankFramework
{
    using System.Collections;
    /// <summary>
    /// 协程管理器
    /// </summary>
    public class CoroutineManager : UnityEngine.MonoBehaviour
    {
        private static CoroutineManager _instance;
        public static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AppBridgeLink.Instance.GetManager<CoroutineManager>(ManagerName.COROUTINE);
                }
                return _instance;
            }
        }

        public UnityEngine.Coroutine StartToCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        public UnityEngine.Coroutine StartToCoroutine(string coroutineName)
        {
            return StartCoroutine(coroutineName);
        }

        public void StopToCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }
        public void StopToCoroutine(string coroutineName)
        {
            StopCoroutine(coroutineName);
        }

        public void StopAllToCoroutines()
        {
            StopAllCoroutines();
        }

    }
}