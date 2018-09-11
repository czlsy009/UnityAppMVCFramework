
using System;
using UnityEngine;

namespace BlankFramework
{
    /// <summary>
    /// 休息面板管理器
    /// </summary>
    public class RestPanelManager : MonoBehaviour
    {
        private int m_restTimeInterval;
        private BlankAction m_callback;
        public void SetRestTimeInterval(int restTimeInterval, BlankAction callback)
        {
            m_restTimeInterval = restTimeInterval;
        }


        private int timer;

        void Awake()
        {
            m_restTimeInterval = -1;
        }

        void Start()
        {
            InvokeRepeating("UpdateTimer", 0, 1);
        }

        private void UpdateTimer()
        {
            if (m_restTimeInterval > 0)
            {
                timer++;
                if (timer >= m_restTimeInterval)
                {
                    timer = 0;
                    CancelInvoke("UpdateTimer");
                    if (m_callback != null)
                    {
                        try
                        {
                            m_callback();
                        }
                        catch (Exception exception)
                        {
                            // ignored
                            Debug.LogError(exception.Message + exception.StackTrace +exception.TargetSite);
                        }
                    }
                }
            }
            else
            {
                Debug.LogError(" 调用 SetRestTimeInterval 来设置 休息时间 和 时间达标回调 ");
            }
        }

        private void Destroy()
        {
            CancelInvoke("UpdateTimer");
        }
    }
}
