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
//  * 文件名：NetWorkManager.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */


using UnityEngine;

#pragma warning disable 0219



namespace BlankFramework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using BestHTTP;
    /// <summary>
    /// 请求失败回调代理协议
    /// </summary>
    public delegate void OnErrorCallBack(MessageModel errorModel);

    /// <summary>
    /// 请求成功代理协议
    /// </summary>
    public delegate void OnSuccessCallBack(MessageModel successModel);


    /// <summary>
    /// 网络管理器
    /// </summary>
    public partial class NetWorkManager : BaseManager
    {
        private static NetWorkManager _instance;
        public static NetWorkManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AppBridgeLink.Instance.GetManager<NetWorkManager>(ManagerName.NET_WORK);
                }
                return _instance;
            }
        }

        private void Awake()
        {
            HTTPManager.MaxConnectionPerServer = 20;
            HTTPManager.ConnectTimeout = TimeSpan.FromSeconds(5);
            HTTPManager.RequestTimeout = TimeSpan.FromSeconds(5);
        }

        #region GET





        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="successCallBack">请求成功回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetType(string urlString, BlankActionForString successCallBack)
        {
            return SendGetType(urlString, new Dictionary<string, string>(), s =>
            {
                if (successCallBack != null)
                {
                    HTTPResponse httpResponse = (HTTPResponse)s.Body;
                    if (httpResponse != null)
                    {
                        successCallBack(httpResponse.DataAsText);
                    }
                }
            }, e =>
            {

            });
        }
        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// /// <param name="paramsDict">请求参数</param>
        /// <param name="successCallBack">请求成功回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetType(string urlString, Dictionary<string, string> paramsDict, BlankActionForString successCallBack)
        {
            return SendGetType(urlString, paramsDict, s =>
            {
                if (successCallBack != null)
                {
                    HTTPResponse httpResponse = (HTTPResponse)s.Body;
                    if (httpResponse != null)
                    {
                        successCallBack(httpResponse.DataAsText);
                    }
                }
            }, e =>
            {

            });
        }
        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="successCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetType(string urlString, OnSuccessCallBack successCallBack, OnErrorCallBack errorCallBack)
        {
            return SendGetType(urlString, new Dictionary<string, string>(), successCallBack, errorCallBack);
        }


        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="successCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetType(string urlString, BlankAction<byte[], string> successCallBack, OnErrorCallBack errorCallBack)
        {
            return SendGetType(urlString, new Dictionary<string, string>(), (s) =>
            {
                HTTPResponse response = (HTTPResponse)s.Body;
                if (response != null)
                {
                    if (successCallBack != null)
                    {
                        successCallBack(response.Data, urlString);
                    }
                }
            }, errorCallBack);
        }

        /// <summary>
        /// 发送GET 方式请求
        /// Tip : 
        ///     [基础函数]
        ///     1. 其他Get请求方式函数都是从该函数扩展而成
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetType(string urlString, Dictionary<string, string> paramsDict, OnSuccessCallBack sucessCallBack, OnErrorCallBack errorCallBack)
        {
            return SendGetType(urlString, paramsDict, (request, response) =>
                {
                    if (request != null)
                    {
                        switch (request.State)
                        {
                            case HTTPRequestStates.Error:
                            case HTTPRequestStates.ConnectionTimedOut:
                            case HTTPRequestStates.TimedOut:
                                //   返回错误消息
                                errorCallBack(new MessageModel("NetWork Error!", "网络错误", "network"));
                                break;
                            case HTTPRequestStates.Finished:
                                //   返回请求成功的数据
                                sucessCallBack(new MessageModel("success", response, "network"));
                                break;
                        }
                    }
                });
        }


        /// <summary>
        /// 发送GET  请求成功函数
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调包含返回码和返回的JsonNode数据</param>
        /// <returns></returns>
        public HTTPRequest SendGetType(string urlString, Dictionary<string, string> paramsDict, BlankAction<string, TinyJson.Node> jsonNodeCallBack)
        {
            return SendGetType(urlString, paramsDict, jsonNodeCallBack, null);
        }

        /// <summary>
        /// 发送GET  请求成功函数.
        /// Tip ：[内部调用] 
        ///     1 .此函数处理服务器错误问题 
        ///     2 .其他函数不要处理服务器的问题
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调包含返回码和返回的JsonNode数据</param>
        /// <param name="errorCallBack"></param>
        /// <returns></returns>
        public HTTPRequest SendGetType(string urlString, Dictionary<string, string> paramsDict, BlankAction<string, TinyJson.Node> jsonNodeCallBack, BlankAction errorCallBack)
        {
            return SendGetType(urlString, paramsDict, s =>
            {
                HTTPResponse response = s.Body as HTTPResponse;
                //Debug.LogError(response.DataAsText);
                if (response != null)
                {
                    TinyJson.Node jsonNode = m_parse.Load(response.DataAsText);
                    if (jsonNode == null)
                    {
                        if (errorCallBack != null)
                        {
                            errorCallBack();
                        }
                    }
                    else
                    {
                        string resultCode = jsonNode[NetWorkReslutKeyConst.CODE_GET].ToString();
                        if (jsonNodeCallBack != null)
                        {
                            jsonNodeCallBack(resultCode, jsonNode);
                        }
                    }
                }
            }, e =>
            {
                if (errorCallBack != null)
                {
                    errorCallBack();
                }
                Log.E("网络错误 ==>" + urlString);
            });
        }
        /// <summary>
        /// 发送GET 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <param name="errorCallBack">错误回调</param>
        /// <returns></returns>
        public HTTPRequest SendGetType(string urlString, Dictionary<string, string> paramsDict, BlankAction<TinyJson.Node> jsonNodeCallBack, BlankAction errorCallBack)
        {

            return SendGetType(urlString, paramsDict, (resultCode, jsonNode) =>
               {
                   if (jsonNodeCallBack != null)
                   {
                       jsonNodeCallBack(jsonNode);
                   }
               }, errorCallBack);
        }

        /// <summary>
        /// 发送GET 方式的请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">发送的参数</param>
        /// <param name="onRequestFinishedDelegate">回调函数</param>
        /// <returns>请求对象</returns>
        // [Obsolete("该函数不建议使用,建议使用 SendGetType(string, Dictionary<string, string>, OnSuccessCallBack, OnErrorCallBack) ")]
        private HTTPRequest SendGetType(string urlString, Dictionary<string, string> paramsDict, OnRequestFinishedDelegate onRequestFinishedDelegate)
        {
            if (IsURL(urlString))
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(urlString);
                string url = urlString;
                if (paramsDict != null && paramsDict.Count > 0)
                {
                    stringBuilder.Append("?");
                    List<string> list = new List<string>();
                    foreach (var keyValuePair in paramsDict)
                    {
                        list.Add(string.Format("{0}={1}", keyValuePair.Key, keyValuePair.Value));
                    }
                    stringBuilder.Append(string.Join("&", list.ToArray()));
                    url = stringBuilder.ToString();
                }
                HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Get, onRequestFinishedDelegate);

                return request.Send();
            }
            if (onRequestFinishedDelegate != null)
            {
                onRequestFinishedDelegate(null, null);
            }
            return null;
        }

        private bool IsURL(string urlstring)
        {
            bool ismatch = Regex.IsMatch(urlstring, "^(http|https|ftp|rtsp|mms)://");
            return ismatch;
        }

        #endregion

        #region POST

        /// <summary>
        /// 发送POST  方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendPostType(string urlString, OnSuccessCallBack sucessCallBack, OnErrorCallBack errorCallBack)
        {
            return SendPostType(urlString, null, sucessCallBack, errorCallBack);
        }

        /// <summary>
        /// 发送POST 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <param name="paramsDict">参数列表</param>
        /// <returns>返回请求对象</returns>
        public HTTPRequest SendPostType(string urlString, Dictionary<string, string> paramsDict, OnSuccessCallBack sucessCallBack, OnErrorCallBack errorCallBack)
        {
            return SendPostType(urlString, sucessCallBack, errorCallBack, paramsDict, null);
        }



        /// <summary>
        /// 发送POST 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <param name="paramsDict">参数列表</param>
        /// <param name="parameterModel">文件参数列表</param>
        /// <returns>返回请求对象</returns>
        public HTTPRequest SendPostType(string urlString, OnSuccessCallBack sucessCallBack, OnErrorCallBack errorCallBack, Dictionary<string, string> paramsDict, params ParameterModel[] parameterModel)
        {
            return SendPostType(sucessCallBack, errorCallBack, urlString, paramsDict, parameterModel);
        }
        /// <summary>
        /// 发送POST 方式请求
        /// </summary>
        /// <param name="successCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">参数列表</param>
        /// <param name="parameterModel">文件参数列表</param>
        /// <returns>返回请求对象</returns>
        private HTTPRequest SendPostType(OnSuccessCallBack successCallBack, OnErrorCallBack errorCallBack,
            string urlString, Dictionary<string, string> paramsDict, params ParameterModel[] parameterModel)
        {
            return SendPostType(urlString, (request, response) =>
            {
                if (request != null)
                {
                    switch (request.State)
                    {
                        case HTTPRequestStates.Error:
                        case HTTPRequestStates.ConnectionTimedOut:
                        case HTTPRequestStates.TimedOut:
                            //   返回错误消息
                            errorCallBack(new MessageModel("NetWork Error!", "网络错误", "network"));
                            break;
                        case HTTPRequestStates.Finished:
                            //   返回请求成功的数据
                            successCallBack(new MessageModel("success", response, "network"));
                            break;
                    }
                }
            }, paramsDict, parameterModel);
        }

        /// <summary>
        /// 发送POST 方式的请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求错误回调</param>
        /// <param name="parameterModel">请求需要发送的文件数据</param>
        /// <returns></returns>
        public HTTPRequest SendPostType(string urlString, OnSuccessCallBack sucessCallBack, OnErrorCallBack errorCallBack,
           params ParameterModel[] parameterModel)
        {
            return SendPostType(urlString, sucessCallBack, errorCallBack, null, parameterModel);
        }

        /// <summary>
        /// 发送POST 方式的请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="onRequestFinishedDelegate">回调函数</param>
        /// <param name="paramsDict">请求的普通参数</param>
        /// <param name="parameterModel">请求需要发送的文件数据</param>
        /// <returns>请求对象</returns>
        //  [Obsolete("该函数不建议使用,建议使用 SendPostType(string, OnSuccessCallBack, OnErrorCallBack) ")]
        private HTTPRequest SendPostType(string urlString, OnRequestFinishedDelegate onRequestFinishedDelegate, Dictionary<string, string> paramsDict, params ParameterModel[] parameterModel)
        {
            return SendPostType(onRequestFinishedDelegate, urlString, paramsDict, parameterModel);
        }
        /// <summary>
        /// 发送POST 方式的请求
        /// </summary>
        /// <param name="onRequestFinishedDelegate">回调函数</param>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求的普通参数</param>
        /// <param name="parameterModel">请求需要发送的文件数据</param>
        /// <returns>请求对象</returns>
        private HTTPRequest SendPostType(OnRequestFinishedDelegate onRequestFinishedDelegate, string urlString,
            Dictionary<string, string> paramsDict, params ParameterModel[] parameterModel)
        {
            HTTPRequest request = new HTTPRequest(new Uri(urlString), HTTPMethods.Post, onRequestFinishedDelegate);



            //普通Post参数
            if (paramsDict != null && paramsDict.Count > 0)
            {
                request.SetHeader("Content-Type", "application/x-www-form-urlencoded");
                foreach (KeyValuePair<string, string> keyValuePair in paramsDict)
                {
                    request.AddField(keyValuePair.Key, keyValuePair.Value);
                }
            }
            //文件参数
            if (parameterModel != null)
            {
                foreach (ParameterModel model in parameterModel)
                {
                    request.AddBinaryData(model.FieldName, model.Content, model.FileName, model.MimeType);
                }
            }
            //发送
            return request.Send();
        }

        #endregion
    }

    /// <summary>
    /// 网络传输中的Post文件类型模型
    /// </summary>
    public class ParameterModel
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 内容字节数组
        /// </summary>
        public byte[] Content { get; set; }
        /// <summary>
        /// 文件名,必须带上扩展名 [可选]
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// MimeType 类型 [可选]  例： image/jpeg , image/png 
        /// </summary>
        public string MimeType { get; set; }
    }

}
