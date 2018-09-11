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
//  * 文件名：UILog.cs
//  * 创建时间：2016年04月21日 
//  * 创建人：Blank Alian
//  */

using System;
using UnityEngine;

namespace Assets.BlankUIFrameWork.Utility
{
    public class UILog
    {
        /// <summary>
        /// true 为启用Debug 模式
        /// </summary>
        public static bool IsDebug = false;

        /// <summary>
        /// 打印普通消息
        /// </summary>
        /// <param name="message">消息</param>
        public static void I(object message)
        {
            if (IsDebug)
            {
                Debug.Log(message);
            }
        }
        /// <summary>
        /// 打印带标签标记的消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="tag">标签标记</param>
        public static void I(object message, string tag)
        {
            if (IsDebug)
            {
                Debug.Log(tag + "=====>" + message);

            }
        }
        /// <summary>
        /// 打印错误消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public static void E(object message)
        {
            if (IsDebug)
            {
                Debug.LogError(message);
            }
        }

        /// <summary>
        /// 打印带标签标记的错误消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="tag">标签标记</param>
        public static void E(object message, string tag)
        {
            if (IsDebug)
            {
                Debug.LogError(tag + "=====>" + message);
            }
        }
        public static void LogException(Exception message)
        {
            if (IsDebug)
            {
                Debug.LogException(message);
            }
        }
        /// <summary>
        /// 打印警告消息
        /// </summary>
        /// <param name="message">消息</param>
        public static void W(object message)
        {
            if (IsDebug)
            {
                Debug.LogWarning(message);
            }
        }

        /// <summary>
        /// 打印带标签标记的警告消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="tag">标签标记</param>
        public static void W(object message, string tag)
        {
            if (IsDebug)
            {
                Debug.LogWarning(tag + "=====>" + message);
            }
        }
        /// <summary>
        /// UI Slider  查找专用函数
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="path"></param>
        public static void UIE_Slider(string componentName, string path)
        {
            E(string.Format("{0} == null ==> [{1}] is uiSlider ?", componentName, path));
        }

        /// <summary>
        /// UI Button  查找专用函数
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="path"></param>
        public static void UIE_Button(string componentName, string path)
        {
            E(string.Format("{0} == null ==> [{1}] is uiButton ?", componentName, path));
        }

        /// <summary>
        /// UI Button  查找专用函数
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="path"></param>
        public static void UIE_Text(string componentName, string path)
        {
            E(string.Format("{0} == null ==> [{1}] is Text ?", componentName, path));
        }
        /// <summary>
        /// UI Button  查找专用函数
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="path"></param>
        public static void UIE_Image(string componentName, string path)
        {
            E(string.Format("{0} == null ==> [{1}] is Image ?", componentName, path));
        }
    }
}
