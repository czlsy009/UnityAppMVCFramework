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
//  * 文件名：NotificationConst.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */
namespace BlankFramework
{
    /// <summary>
    /// 通知消息常量
    /// </summary>
    public class NotificationConst
    {
        #region Controller 层的消息通知

        /// <summary>
        /// 启动框架 Start_Up
        /// </summary>
        public const string START_UP = "StartUp";
        /// <summary>
        /// 派发信息 DispatchMessage
        /// </summary>
        public const string DISPATCH_MESSAGE = "DispatchMessage";


        #endregion

        #region View 层的消息通知
        /// <summary>
        /// 更新消息,Update_Message
        /// </summary>
        public const string UPDATE_MESSAGE = "UpdateMessage";
        /// <summary>
        /// 更新解包
        /// </summary>
        //public const string UPDATE_EXTRACT = "UpdateExtract";
        /// <summary>
        /// 更新下载,Update_DownLoad
        /// </summary>
        public const string UPDATE_DOWNLOAD = "UpdateDownload";
        /// <summary>
        /// 更新进度,Update_Progress
        /// </summary>
        public const string UPDATE_PROGRESS = "UpdateProgress";

        #endregion

    }
}