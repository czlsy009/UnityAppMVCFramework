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
//  * 文件名：DownLoadManager.cs
//  * 创建时间：2016年09月23日 
//  * 创建人：Blank Alian
//  */


using System;
using System.Collections.Generic;
using System.IO;
using BestHTTP;
using UnityEngine;

namespace BlankFramework
{
    public delegate void BlankActionForDownloadItemModel(DownloadItemModel downloadItemModel);

    /// <summary>
    /// 下载管理器中的通知常量类
    /// </summary>
    public class DownLoadManagerNotificationConst
    {

        /// <summary>
        /// 下载管理器中的下载任务下载完成的时候触发
        /// </summary>
        public const string DOWN_LOAD_MANAGER_TASK_FINSH = "DOWN_LOAD_MANAGER_TASK_FINSH";

        /// <summary>
        /// 下载管理器中的下载任务进度发生发生改变的时候触发
        /// </summary>
        public const string DOWN_LOAD_MANAGER_TASK_SPEED_CHANGED = "DOWN_LOAD_MANAGER_TASK_SPEED_CHANGED";
        /// <summary>
        /// 下载管理器中的任务状态发生改变 
        /// </summary>
        public const string DOWN_LOAD_MANAGER_TASK_STATE_CHANGED = "DOWN_LOAD_MANAGER_TASK_STATE_CHANGED";

        /// <summary>
        /// 下载管理器中的任务发生错误 
        /// </summary>
        public const string DOWN_LOAD_MANAGER_TASK_ERROR = "DOWN_LOAD_MANAGER_TASK_ERROR";

        /// <summary>
        /// 下载管理器中的任务终止
        /// </summary>
        public const string DOWN_LOAD_MANAGER_TASK_STOP = "DOWN_LOAD_MANAGER_TASK_STOP";

        /// <summary>
        /// 下载管理器的通知过滤器
        /// </summary>
        public const string DOWN_LOAD_MANAGER_FILTER = "DOWN_LOAD_MANAGER_FILTER";
    }

    /// <summary>
    /// 文件下载器
    /// </summary>
    public class DownLoadManager : MonoBehaviour
    {
        #region Const

        /// <summary>
        /// 下载管理器的模块名称
        /// </summary>
        public const string DOWN_LOAD_MANAGER_MODULE_NAME = "download";

        #endregion


        /// <summary>
        /// 下载的对象字典
        /// </summary>
        private readonly Dictionary<string, DownloadItemModel> m_downloadItemModels = new Dictionary<string, DownloadItemModel>();
        /// <summary>
        /// 下载器的对象字典
        /// </summary>
        private readonly Dictionary<string, HTTPRequest> m_downloadHttpRequests = new Dictionary<string, HTTPRequest>();

        private static DownLoadManager _instance;
        /// <summary>
        /// 文件下载的单例对象
        /// </summary>
        public static DownLoadManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AppBridgeLink.Instance.GetManager<DownLoadManager>(ManagerName.DOWNLOAD);
                }
                return _instance;
            }
        }
        /// <summary>
        /// 停止所有的下载任务
        /// </summary>
        public void StopAllDownLoad()
        {
            foreach (KeyValuePair<string, HTTPRequest> downloadHttpRequest in m_downloadHttpRequests)
            {
                if (downloadHttpRequest.Value != null)
                {
                    downloadHttpRequest.Value.Abort();
                }
            }
            m_downloadHttpRequests.Clear();
            m_downloadItemModels.Clear();
        }
        /// <summary>
        /// 停止下载任务
        /// </summary>
        /// <param name="downloadurl">下载地址</param>
        /// <returns></returns>
        public bool StopDownLoad(string downloadurl)
        {
            if (m_downloadHttpRequests.ContainsKey(downloadurl))
            {
                if (m_downloadItemModels.ContainsKey(downloadurl))
                {
                    m_downloadItemModels.Remove(downloadurl);
                }

                HTTPRequest httpRequest;
                m_downloadHttpRequests.TryGetValue(downloadurl, out httpRequest);
                if (httpRequest != null)
                {
                    httpRequest.Abort();
                }
                m_downloadHttpRequests.Remove(downloadurl);
                return true;
            }
            return false;
        }



        /// <summary>
        /// Thorough  彻底删除 下载任务
        /// </summary>
        /// <param name="downloadurl">下载地址</param>
        public DownloadItemModel Remove(string downloadurl)
        {
            // 停止下载任务
            StopDownLoad(downloadurl);

            List<DownloadItemModel> allDownloadTaskList = GetAllDownloadTaskList();
            DownloadItemModel resultDownloadItemModel = null;

            for (int index = 0; index < allDownloadTaskList.Count; index++)
            {
                DownloadItemModel downloadItemModel = allDownloadTaskList[index];

                if (downloadItemModel.DownloadUrl.Equals(downloadurl))
                {
                    resultDownloadItemModel = downloadItemModel;
                    break;
                }
            }



            // 删除数据文件
            if (resultDownloadItemModel != null && File.Exists(resultDownloadItemModel.FileSaveName))
            {
                if (string.IsNullOrEmpty(resultDownloadItemModel.FragmentsFileSaveName))
                {
                    resultDownloadItemModel.FileSaveName = resultDownloadItemModel.FileSaveName;
                }
                File.Delete(resultDownloadItemModel.FileSaveName);
                File.Delete(resultDownloadItemModel.FragmentsFileSaveName);
            }
            DataCacheManager.Instance.RemoveItemList(resultDownloadItemModel, DOWN_LOAD_MANAGER_MODULE_NAME);
            return resultDownloadItemModel;
        }

        /// <summary>
        /// 获取当前的下载中的任务列表
        /// </summary>
        /// <returns></returns>
        public List<DownloadItemModel> GetDownloadTaskList()
        {

            List<DownloadItemModel> downloadItemModels = new List<DownloadItemModel>();
            foreach (KeyValuePair<string, DownloadItemModel> downloadItemModel in m_downloadItemModels)
            {
                downloadItemModels.Add(downloadItemModel.Value);
            }
            return downloadItemModels;
        }

        /// <summary>
        /// 获取全部的下载任务列表  包含之前所有 的任务
        /// </summary>
        /// <returns></returns>
        public List<DownloadItemModel> GetAllDownloadTaskList()
        {
            return DataCacheManager.Instance.ReadList<DownloadItemModel>(DOWN_LOAD_MANAGER_MODULE_NAME);
        }

        /// <summary>
        /// 获取全部下载中的任务列表
        /// </summary>
        /// <returns></returns>
        public List<DownloadItemModel> GetAllDownloadingTaskList()
        {
            List<DownloadItemModel> allDownloadTaskList = GetAllDownloadTaskList();
            List<DownloadItemModel> allDownloadingTaskList = new List<DownloadItemModel>();
            int allDownloadTaskListCount = allDownloadTaskList.Count;

            for (int i = 0; i < allDownloadTaskListCount; i++)
            {
                DownloadItemModel downloadItemModel = allDownloadTaskList[i];
                if (downloadItemModel.IsDownloadFinsh == false)
                {
                    allDownloadingTaskList.Add(downloadItemModel);
                }
            }
            return allDownloadingTaskList;
        }

        /// <summary>
        /// 获取全部下载完成的任务列表
        /// </summary>
        /// <returns></returns>
        public List<DownloadItemModel> GetAllDownloadedTaskList()
        {
            List<DownloadItemModel> allDownloadTaskList = GetAllDownloadTaskList();
            List<DownloadItemModel> allDownloadedTaskList = new List<DownloadItemModel>();
            int allDownloadTaskListCount = allDownloadTaskList.Count;

            for (int i = 0; i < allDownloadTaskListCount; i++)
            {
                DownloadItemModel downloadItemModel = allDownloadTaskList[i];
                if (downloadItemModel.IsDownloadFinsh)
                {
                    allDownloadedTaskList.Add(downloadItemModel);
                }
            }
            return allDownloadedTaskList;
        }

        /// <summary>
        /// 获取下载任务的列表长度
        /// </summary>
        /// <returns></returns>
        public int GetDownloadingTaskCount()
        {
            return m_downloadHttpRequests.Count;
        }
        /// <summary>
        /// 查找全部的下载任务中是否包含该下载地址=》是否执行过下载
        /// </summary>
        /// <param name="downloadurl">下载地址</param>
        /// <returns>返回 是否执行过下载 操作</returns>
        public bool ContainsDownloadUrl(string downloadurl)
        {
            List<DownloadItemModel> downloadItemModels = GetAllDownloadTaskList();
            for (int index = 0; index < downloadItemModels.Count; index++)
            {
                DownloadItemModel downloadItemModel = downloadItemModels[index];
                if (downloadItemModel.DownloadUrl.Equals(downloadurl))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取当前的下载地址是否处于下载暂停状态
        /// </summary>
        /// <param name="downloadurl">下载地址</param>
        /// <returns>如果在全部的下载列表中没有找到该下载地址则返回空</returns>
        public bool? IsDownloadPause(string downloadurl)
        {
            List<DownloadItemModel> downloadItemModels = GetAllDownloadTaskList();
            for (int index = 0; index < downloadItemModels.Count; index++)
            {
                DownloadItemModel downloadItemModel = downloadItemModels[index];
                if (downloadItemModel.DownloadUrl.Equals(downloadurl))
                {
                    if (IsDownloading(downloadItemModel.DownloadUrl))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取当前的下载地址是否正在下载中
        /// </summary>
        /// <param name="downloadurl"></param>
        /// <returns></returns>
        public bool IsDownloading(string downloadurl)
        {
            if (m_downloadHttpRequests.ContainsKey(downloadurl))
            {
                HTTPRequest httpRequest;
                // 下载中
                if (m_downloadHttpRequests.TryGetValue(downloadurl, out httpRequest))
                {
                    if (httpRequest != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取当前的下载地址是否下载完成过
        /// </summary>
        /// <param name="downloadurl">下载地址</param>
        /// <returns></returns>
        public bool IsDownloaded(string downloadurl)
        {
            List<DownloadItemModel> allDownloadedTaskList = GetAllDownloadedTaskList();

            int allDownloadedTaskListCount = allDownloadedTaskList.Count;

            for (int i = 0; i < allDownloadedTaskListCount; i++)
            {
                DownloadItemModel itemModel = allDownloadedTaskList[i];
                if (itemModel.DownloadUrl.Equals(downloadurl))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取当前的下载地址是否下载完成过
        /// </summary>
        /// <param name="downloadurl">下载地址</param>
        /// <param name="fileSavePath">返回文件的存储位置</param>
        /// <returns></returns>
        public bool IsDownloaded(string downloadurl, out string fileSavePath)
        {
            List<DownloadItemModel> allDownloadedTaskList = GetAllDownloadedTaskList();

            int allDownloadedTaskListCount = allDownloadedTaskList.Count;

            for (int i = 0; i < allDownloadedTaskListCount; i++)
            {
                DownloadItemModel itemModel = allDownloadedTaskList[i];
                if (itemModel.DownloadUrl.Equals(downloadurl))
                {
                    fileSavePath = itemModel.FileSaveName;
                    return true;
                }
            }
            fileSavePath = null;
            return false;
        }

        /// <summary>
        /// 获取文件的大小 单位为 GB/MB/KB/B 中的一种
        /// </summary>
        /// <param name="contentLength"></param>
        /// <returns></returns>
        public string GetContentSize(long contentLength)
        {
            float gb = (contentLength * 1.0f / (1024 * 1024 * 1024));
            if (gb > 1)
            {
                // GB
                return string.Format("{0:F}GB", gb);
            }
            float mb = (contentLength * 1.0f / (1024 * 1024));
            if (mb > 1)
            {
                // MB
                return string.Format("{0:F}MB", mb);
            }
            float kb = (contentLength * 1.0f / 1024);
            if (kb > 1)
            {
                // KB
                return string.Format("{0:F}KB", kb);
            }
            else
            {
                // B 
                return string.Format("{0}B", contentLength);
            }
        }


        /// <summary>
        /// 获取下载文件的内容长度
        /// </summary>
        /// <param name="downloadurl">下载地址</param>
        /// <param name="contentLengthSize">请求成功回调 ,包含 内容长度</param>
        /// <param name="errorCallBack">请求错误</param>
        /// <returns></returns>
        public HTTPRequest GetContentLength(string downloadurl, BlankActionForLong contentLengthSize, BlankAction errorCallBack)
        {
            HTTPRequest httpRequest = new HTTPRequest(new Uri(downloadurl), (req, rep) =>
            {
                if (rep != null)
                {
                    string contentLength = rep.GetFirstHeaderValue("content-length");
                    if (contentLengthSize != null)
                    {
                        StopDownLoad(downloadurl);
                        contentLengthSize(long.Parse(contentLength));
                    }
                }
                switch (req.State)
                {
                    case HTTPRequestStates.Initial:
                        break;
                    case HTTPRequestStates.Queued:
                        break;
                    case HTTPRequestStates.Processing:
                        break;
                    case HTTPRequestStates.Finished:
                        break;
                    case HTTPRequestStates.Error:
                    case HTTPRequestStates.Aborted:
                    case HTTPRequestStates.ConnectionTimedOut:
                    case HTTPRequestStates.TimedOut:
                        if (errorCallBack != null)
                        {
                            errorCallBack();
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
            httpRequest.Send();
            m_downloadHttpRequests.Add(downloadurl, httpRequest);
            return httpRequest;
        }

        /// <summary>
        /// 恢复下载
        /// </summary>
        /// <param name="downloadurl"></param>
        /// <returns></returns>
        public HTTPRequest ResumeDownLoad(string downloadurl)
        {
            List<DownloadItemModel> downloadItemModels = GetAllDownloadTaskList();

            for (int i = 0; i < downloadItemModels.Count; i++)
            {
                DownloadItemModel downloadItemModel = downloadItemModels[i];
                if (downloadurl.Equals(downloadItemModel.DownloadUrl))
                {
                    // 有相同的下载任务
                    return DownLoad(downloadItemModel.DownloadUrl, downloadItemModel.FileSaveName, null, null, null, false, downloadItemModel.Data, downloadItemModel.Tag);
                }
            }
            return null;
        }


        /// <summary>
        /// 开始下载文件
        /// </summary>
        /// <param name="downloadurl">下载地址</param>
        /// <param name="saveFileName">文件保存位置。包含文件的保存路径和完整的文件名</param>
        /// <param name="downloadingCallback">下载中的回调</param>
        /// <param name="downloadedFinshCallback">下载完成后的回调</param>
        /// <param name="isResetDownload">标记是否强制重新下载</param>
        /// <returns></returns>
        public HTTPRequest DownLoad(string downloadurl, string saveFileName, BlankActionForDownloadItemModel downloadingCallback, BlankActionForDownloadItemModel downloadedFinshCallback, bool isResetDownload = false)
        {
            return DownLoad(downloadurl, saveFileName, downloadingCallback, downloadedFinshCallback, null,
                isResetDownload);
        }


        /// <summary>
        /// 开始下载文件
        /// </summary>
        /// <param name="downloadurl">下载地址</param>
        /// <param name="saveFileName">文件保存位置。包含文件的保存路径和完整的文件名</param>
        /// <param name="downloadingCallback">下载中的回调</param>
        /// <param name="downloadedFinshCallback">下载完成后的回调</param>
        /// <param name="downloadErrorCallBack">下载错误回调</param>
        /// <param name="isResetDownload">标记是否强制重新下载</param>
        /// <param name="data">下载任务的附加数据</param>
        /// <param name="mtag">下载任务的附加数据</param>
        /// <returns></returns>
        public HTTPRequest DownLoad(string downloadurl, string saveFileName, BlankActionForDownloadItemModel downloadingCallback, BlankActionForDownloadItemModel downloadedFinshCallback, BlankAction downloadErrorCallBack, bool isResetDownload = false, string data = "", string mtag = "")
        {
            if (!Directory.Exists(saveFileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(saveFileName));
            }

            if (m_downloadHttpRequests.ContainsKey(downloadurl))
            {
                if (m_downloadItemModels.ContainsKey(downloadurl))
                {
                    DownloadItemModel downloadItemModel = m_downloadItemModels[downloadurl];
                    downloadItemModel.DownloadingCallback = downloadingCallback;
                    downloadItemModel.DownloadedFinshCallback = downloadedFinshCallback;
                }
                // 下载中
                return m_downloadHttpRequests[downloadurl];
            }
            else
            {
                // 未下载中
                // 读取 断点数据
                DownloadItemModel downloadItemModel = ReadProcessFragmentsData(saveFileName, isResetDownload);
                if (string.IsNullOrEmpty(downloadItemModel.ID))
                {
                    downloadItemModel.ID = DateTime.Now.ToFileTime().ToString();
                }
                downloadItemModel.Data = data;
                downloadItemModel.Tag = mtag;
                downloadItemModel.LastDownloadTimeSpan = Time.realtimeSinceStartup;
                downloadItemModel.DownloadUrl = downloadurl;
                downloadItemModel.FileSaveName = saveFileName;
                downloadItemModel.DownloadingCallback = downloadingCallback;
                downloadItemModel.DownloadedFinshCallback = downloadedFinshCallback;

                HTTPRequest httpRequest = new HTTPRequest(new Uri(downloadurl), (req, rep) =>
                {
                    if (rep != null)
                    {
                        if (downloadItemModel.DownloadSize <= 0)
                        {
                            string contentLength = rep.GetFirstHeaderValue("content-length");
                            downloadItemModel.DownloadSize = long.Parse(contentLength);
                        }
                    }
                    switch (req.State)
                    {
                        case HTTPRequestStates.Initial:
                            break;
                        case HTTPRequestStates.Queued:
                            break;
                        case HTTPRequestStates.Processing:
                            {
                                downloadItemModel.IsDownloadFinsh = false;
                                ProgressDownloadData(saveFileName, rep, downloadItemModel);
                                // 下载中的回调
                                if (downloadItemModel.DownloadingCallback != null)
                                {
                                    try
                                    {
                                        downloadingCallback(downloadItemModel);
                                    }
                                    catch (Exception exception)
                                    {
                                        Debug.LogError("DownloadURL:" + downloadurl + exception.Message + exception.Source + exception.TargetSite + downloadItemModel.ToString());
                                        throw;
                                    }
                                }
                            }
                            break;
                        case HTTPRequestStates.Finished:

                            if (rep != null && rep.IsSuccess)
                            {
                                ProgressDownloadData(saveFileName, rep, downloadItemModel);
                                if (rep.IsStreamingFinished)
                                {

                                    UpdateDownloadFinshState(downloadItemModel);

                                    // 下载完成后回调
                                    if (downloadedFinshCallback != null)
                                    {
                                        try
                                        {
                                            downloadedFinshCallback(downloadItemModel);
                                        }
                                        catch (Exception exception)
                                        {
                                            Debug.LogError("DownloadURL:" + downloadurl + exception.Message + exception.Source + exception.TargetSite + downloadItemModel.ToString());
                                            throw;
                                        }
                                    }
                                    Log.I("下载完成");
                                }
                                StopDownLoad(downloadItemModel.DownloadUrl);
                            }
                            break;
                        case HTTPRequestStates.Aborted:
                            {
                                // 下载 终止
                                Log.I(string.Format(" 下载任务  {0} 终止 ", downloadurl));
                                NotificationManager.Instance.PostNotification(DownLoadManagerNotificationConst.DOWN_LOAD_MANAGER_TASK_STOP, downloadurl, DownLoadManagerNotificationConst.DOWN_LOAD_MANAGER_FILTER);
                            }
                            break;
                        case HTTPRequestStates.Error:
                        case HTTPRequestStates.ConnectionTimedOut:
                        case HTTPRequestStates.TimedOut:
                            {
                                if (downloadErrorCallBack != null)
                                {
                                    downloadErrorCallBack();
                                }
                                // 下载错误
                                Log.I(string.Format(" 下载任务  {0} 错误 ", downloadurl));
                                NotificationManager.Instance.PostNotification(DownLoadManagerNotificationConst.DOWN_LOAD_MANAGER_TASK_ERROR, downloadurl, DownLoadManagerNotificationConst.DOWN_LOAD_MANAGER_FILTER);
                            }
                            break;
                    }
                })
                {
                    IsKeepAlive = true,
                    DisableCache = true,
                    UseStreaming = true,
                    StreamFragmentSize = HTTPResponse.MinBufferSize * 100,
                    ConnectTimeout = TimeSpan.FromSeconds(5),
                    Timeout = TimeSpan.FromSeconds(5)
                };
                if (downloadItemModel.IsDownloadFinsh)
                {
                    Log.I("已经下载过该文件" + downloadItemModel.ModelToJsonString());

                    UpdateDownloadFinshState(downloadItemModel);

                    // 下载完成后回调
                    if (downloadedFinshCallback != null)
                    {
                        try
                        {
                            downloadedFinshCallback(downloadItemModel);
                        }
                        catch (Exception exception)
                        {
                            Debug.LogError("DownloadURL:" + downloadurl + exception.Message + exception.Source + exception.TargetSite + downloadItemModel);
                            throw;
                        }
                    }
                    return httpRequest;
                }
                if (downloadItemModel.DownloadedSize > 0)
                {

                    httpRequest.SetRangeHeader((int)downloadItemModel.DownloadedSize);
                    httpRequest.Tag = downloadItemModel.DownloadedSize;
                    Log.I(" 开始断点下载 " + downloadItemModel.ModelToJsonString());
                }
                else
                {
                    DataCacheManager.Instance.AppendList(downloadItemModel, DOWN_LOAD_MANAGER_MODULE_NAME);
                }
                httpRequest.Send();
                m_downloadItemModels.Add(downloadurl, downloadItemModel);
                m_downloadHttpRequests.Add(downloadurl, httpRequest);
                return httpRequest;
            }
        }



        #region private

        /// <summary>
        /// 处理下载的数据
        /// </summary>
        /// <param name="saveFileName">完整的保存路径.包含文件名</param>
        /// <param name="response"></param>
        /// <param name="downloadItemModel">下载数据模型</param>
        private void ProgressDownloadData(string saveFileName, HTTPResponse response, DownloadItemModel downloadItemModel)
        {
            // 记录 上一次的下载量
            long lastDownloadedSize = downloadItemModel.DownloadedSize;
            int downloadedSize = 0;
            List<byte[]> byteses = response.GetStreamedFragments();
            if (byteses != null)
            {
                int byteCount = byteses.Count;
                for (int i = 0; i < byteCount; i++)
                {
                    downloadedSize += byteses[i].Length;
                }
            }
            // 记录已经下载的长度
            downloadItemModel.DownloadedSize = downloadItemModel.DownloadedSize + downloadedSize;

            ProcessFragments(byteses, saveFileName);


            if (downloadItemModel.DownloadedSize >= downloadItemModel.DownloadSize)
            {
                downloadItemModel.IsDownloadFinsh = true;
            }
            else
            {
                downloadItemModel.IsDownloadFinsh = false;
            }

            // 记录当前的时间
            downloadItemModel.ThisDownloadTimeSpan = Time.realtimeSinceStartup;
            // 计算等待时间的长度
            float waitingTimeLength = downloadItemModel.ThisDownloadTimeSpan - downloadItemModel.LastDownloadTimeSpan;
            long downloadSizeDifference = downloadItemModel.DownloadedSize - lastDownloadedSize;
            downloadItemModel.DownloadSpeedValue = SpeedCalculator(downloadSizeDifference, waitingTimeLength);


            downloadItemModel.LastDownloadTimeSpan = downloadItemModel.ThisDownloadTimeSpan;

            // 设置下载进度 百分比
            downloadItemModel.DownloadProgressValue =
                (downloadItemModel.DownloadedSize * 1.0f / downloadItemModel.DownloadSize) * 100f;
            // 更新下载进度
            UpdateSpeedValue(downloadItemModel);
            // 保存下载数据进度
            SaveProcessFragmentsData(downloadItemModel);

        }
        /// <summary>
        /// 计算下载速度
        /// </summary>
        /// <param name="downloadSizeDifference"></param>
        /// <param name="waitingTimeLength"></param>
        /// <returns></returns>
        private string SpeedCalculator(long downloadSizeDifference, float waitingTimeLength)
        {
            float downloadSpeedValue = downloadSizeDifference * 1.0f / waitingTimeLength * 1.0f;

            float gb = (downloadSpeedValue / (1024 * 1024 * 1024));
            if (gb > 1)
            {
                // GB
                return string.Format("{0:F}GB/S", gb);
            }
            float mb = (downloadSpeedValue / (1024 * 1024));
            if (mb > 1)
            {
                // MB
                return string.Format("{0:F}MB/S", mb);
            }
            float kb = (downloadSpeedValue / 1024);
            if (kb > 1)
            {
                // KB
                return string.Format("{0:F}KB/S", kb);
            }
            else
            {
                // B 
                return string.Format("{0}B/S", downloadSpeedValue);
            }
        }


        /// <summary>
        /// 读取断点下载的数据碎片 
        /// </summary>
        /// <param name="saveFileName"></param>
        /// <param name="isResetDownload"></param>
        private DownloadItemModel ReadProcessFragmentsData(string saveFileName, bool isResetDownload)
        {
            DownloadItemModel downloadItemModel = new DownloadItemModel();
            downloadItemModel.FileSaveName = saveFileName;
            if (isResetDownload)
            {
                // 删除断点下载记录文件
                if (File.Exists(downloadItemModel.FragmentsFileSaveName))
                {
                    File.Delete(downloadItemModel.FragmentsFileSaveName);
                }
                // 删除文件
                if (File.Exists(downloadItemModel.FileSaveName))
                {
                    File.Delete(downloadItemModel.FileSaveName);
                }
            }
            else
            {
                // 读取断点数据
                if (File.Exists(downloadItemModel.FragmentsFileSaveName))
                {
                    // 读取断点数据
                    downloadItemModel = new DownloadItemModel().JsonToModelString(TinyJson.Parser.Instance.Load(File.ReadAllText(downloadItemModel.FragmentsFileSaveName)));

                    return downloadItemModel;
                }
            }
            return downloadItemModel;
        }

        /// <summary>
        /// 保存断点下载的数据碎片 
        /// </summary>
        /// <param name="downloadItemModel"></param>
        private void SaveProcessFragmentsData(DownloadItemModel downloadItemModel)
        {
            if (!File.Exists(downloadItemModel.FragmentsFileSaveName))
            {
                File.Create(downloadItemModel.FragmentsFileSaveName).Dispose();
            }
            File.WriteAllText(downloadItemModel.FragmentsFileSaveName, downloadItemModel.ModelToJsonString());
        }

        /// <summary>
        /// 处理 数据碎片 
        /// </summary>
        /// <param name="fragments"></param>
        /// <param name="saveFileName"></param>
        private void ProcessFragments(List<byte[]> fragments, string saveFileName)
        {
            if (fragments != null && fragments.Count > 0)
            {
                using (FileStream fs = new FileStream(saveFileName, FileMode.Append))
                {
                    for (int i = 0; i < fragments.Count; ++i)
                    {
                        fs.Write(fragments[i], 0, fragments[i].Length);
                    }
                }
            }
        }


        /// <summary>
        /// 更新下载速度的存储
        /// </summary>
        /// <param name="model"></param>
        private void UpdateSpeedValue(DownloadItemModel model)
        {
            // 更新下载任务的缓存数据
            DataCacheManager.Instance.UpdateList(model, DOWN_LOAD_MANAGER_MODULE_NAME);

            // 发出 下载进度改变 的通知
            NotificationManager.Instance.PostNotification(DownLoadManagerNotificationConst.DOWN_LOAD_MANAGER_TASK_SPEED_CHANGED, model, DownLoadManagerNotificationConst.DOWN_LOAD_MANAGER_FILTER);
        }

        /// <summary>
        /// 更新下载完成的状态
        /// </summary>
        /// <param name="model"></param>
        private void UpdateDownloadFinshState(DownloadItemModel model)
        {
            // 更新下载任务的缓存数据
            DataCacheManager.Instance.UpdateList(model, DOWN_LOAD_MANAGER_MODULE_NAME);

            // 发出 下载任务完成 的通知
            NotificationManager.Instance.PostNotification(DownLoadManagerNotificationConst.DOWN_LOAD_MANAGER_TASK_FINSH, model, DownLoadManagerNotificationConst.DOWN_LOAD_MANAGER_FILTER);
        }

        #endregion
    }

    /// <summary>
    /// 下载文件的对象模型
    /// </summary>
    public class DownloadItemModel : IBaseItemModel<DownloadItemModel>, IBaseItemsModel<DownloadItemModel>
    {
        /// <summary>
        /// 下载任务的ID 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 附加标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }
        /// <summary>
        /// 保存的文件名 .包含保存路径
        /// </summary>
        private string m_fileSaveName;
        /// <summary>
        /// 保存的文件名 .包含保存路径
        /// </summary>
        public string FileSaveName
        {
            get { return m_fileSaveName; }
            set
            {
                m_fileSaveName = value;
                FragmentsFileSaveName = string.Format("{0}.db", m_fileSaveName);
            }
        }

        /// <summary>
        /// 断点下载保存的文件名 .包含保存路径
        /// </summary>
        public string FragmentsFileSaveName { private set; get; }
        /// <summary>
        /// 下载的文件总大小
        /// </summary>
        public long DownloadSize { get; set; }


        /// <summary>
        /// 下载进度值
        /// </summary>
        public float DownloadProgressValue { get; set; }

        /// <summary>
        /// 已经下载的文件大小
        /// </summary>
        public long DownloadedSize { get; set; }

        /// <summary>
        /// 上一次下载的时间
        /// </summary>
        public float LastDownloadTimeSpan { get; set; }
        /// <summary>
        /// 当前下载的时间
        /// </summary>
        public float ThisDownloadTimeSpan { get; set; }

        /// <summary>
        /// 当前的下载速度 字节每秒
        /// </summary>
        public string DownloadSpeedValue { get; set; }

        /// <summary>
        /// 标记是否下载完成
        /// </summary>
        public bool IsDownloadFinsh { get; set; }


        /// <summary>
        /// 下载中的回调
        /// </summary>
        public BlankActionForDownloadItemModel DownloadingCallback { get; set; }
        /// <summary>
        /// 下载完成回调
        /// </summary>
        public BlankActionForDownloadItemModel DownloadedFinshCallback { get; set; }



        public override string ToString()
        {
            return ModelToJsonString();
        }




        public string ModelToJsonString()
        {
            return "{" + string.Format("\"DownloadUrl\":\"{0}\",\"FileSaveName\":\"{1}\",\"DownloadSize\":\"{2}\",\"DownloadProgressValue\":\"{3}\",\"DownloadedSize\":\"{4}\",\"FragmentsFileSaveName:\":\"{5}\",\"IsDownloadFinsh\":\"{6}\",\"DownloadSpeedValue\":\"{7}\",\"Tag\":\"{8}\",\"Data\":\"{9}\",\"ID\":\"{10}\"", DownloadUrl, FileSaveName, DownloadSize, DownloadProgressValue, DownloadedSize, FragmentsFileSaveName, IsDownloadFinsh, DownloadSpeedValue, Tag, Data, ID) + "}";
        }

        public DownloadItemModel JsonToModelString(TinyJson.Node node)
        {
            DownloadUrl = node["DownloadUrl"].ToString();
            FileSaveName = node["FileSaveName"].ToString();
            FragmentsFileSaveName = node["FragmentsFileSaveName"].ToString();
            DownloadSize = long.Parse(node["DownloadSize"].ToString());
            DownloadProgressValue = float.Parse(node["DownloadProgressValue"].ToString());
            DownloadedSize = long.Parse(node["DownloadedSize"].ToString());
            IsDownloadFinsh = bool.Parse(node["IsDownloadFinsh"].ToString());
            DownloadSpeedValue = node["DownloadSpeedValue"].ToString();
            Tag = node["Tag"].ToString();
            Data = node["Data"].ToString();
            ID = node["ID"].ToString();
            return this;
        }
    }
}