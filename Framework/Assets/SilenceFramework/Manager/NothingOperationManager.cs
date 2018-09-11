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
//  * 文件名：NothingOperationManager.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */

using UnityEngine;

namespace BlankFramework
{
    /// <summary>
    /// 无操作管理器
    /// </summary>
    public class NothingOperationManager : MonoBehaviour
    {

        private int m_nothingOperationTimeInterval;
        private BlankAction m_callback;
        /// <summary>
        /// 设置无操作的时间间隔
        /// </summary>
        /// <param name="nothingOperationTimeInterval">时间长度 单位秒</param>
        /// <param name="callback">达到设置的时间长度触发的回调</param>
        public void SetNothingOperationTimeInterval(int nothingOperationTimeInterval, BlankAction callback)
        {
            m_nothingOperationTimeInterval = nothingOperationTimeInterval;
            m_callback = callback;
        }

        private int m_timer;
        void Start()
        {
            m_nothingOperationTimeInterval = -1;
            InvokeRepeating("UpdateTimer", 0, 1);
        }

        private void Update()
        {
            //检测是否有交互操作
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                m_timer = 0;
            }
        }

        private void UpdateTimer()
        {
            if (m_nothingOperationTimeInterval > 0)
            {
                m_timer++;
                if (m_timer >= m_nothingOperationTimeInterval)
                {
                    m_timer = 0;
                    CancelInvoke("UpdateTimer");
                    if (m_callback != null)
                    {
                        try
                        {
                            m_callback();
                        }
                        catch (System.Exception exception)
                        {
                            // ignored
                            Debug.LogError(exception.Message + exception.StackTrace + exception.TargetSite);
                        }
                    }
                }
            }
            else
            {
                Debug.LogError(" 调用 SetNothingOperationTimeInterval 来设置 无操作时间 和 时间达标回调 ");
            }
        }
    }
}