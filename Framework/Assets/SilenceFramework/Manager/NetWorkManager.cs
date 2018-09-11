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


using System.IO;
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

        #region UploadPost



        #endregion
        /// <summary>
        /// 发送POST 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <param name="paramsDict">参数列表</param>
        /// <param name="parameterModel">文件参数列表</param>
        /// <returns>返回请求对象</returns>
        public HTTPRequest SendPostType(string urlString, OnSuccessCallBack sucessCallBack, OnErrorCallBack errorCallBack, Dictionary<string, string> paramsDict, params FileParamModel[] parameterModel)
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
            string urlString, Dictionary<string, string> paramsDict, params FileParamModel[] parameterModel)
        {
            return SendPostType(urlString, (request, response) =>
            {
                if (request != null)
                {
                    switch (request.State)
                    {
                        case HTTPRequestStates.Error:
                            //   返回错误消息
                            errorCallBack(new MessageModel("NetWork Error!", "网络错误", "network"));
                            break;
                        case HTTPRequestStates.ConnectionTimedOut:
                            //   返回错误消息
                            errorCallBack(new MessageModel("NetWork Error!", "连接超时", "network"));
                            break;
                        case HTTPRequestStates.TimedOut:
                            //   返回错误消息
                            errorCallBack(new MessageModel("NetWork Error!", "请求超时", "network"));
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
           params FileParamModel[] parameterModel)
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
        private HTTPRequest SendPostType(string urlString, OnRequestFinishedDelegate onRequestFinishedDelegate, Dictionary<string, string> paramsDict, params FileParamModel[] parameterModel)
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
            Dictionary<string, string> paramsDict, params FileParamModel[] parameterModel)
        {
            HTTPRequest request = new HTTPRequest(new Uri(urlString), HTTPMethods.Post, onRequestFinishedDelegate);



            //普通Post参数

            //文件参数
            if (parameterModel != null&&parameterModel.Length>0)
            {
                foreach (FileParamModel model in parameterModel)
                {
                    request.AddBinaryData(model.FieldName, model.Content, model.FileName, model.MimeType);
                }
                if (paramsDict != null && paramsDict.Count > 0)
                {
                    request.SetHeader("Content-Type", "application/json");
                    foreach (KeyValuePair<string, string> keyValuePair in paramsDict)
                    {
                        request.AddField(keyValuePair.Key, keyValuePair.Value);
                    }
                }
                Debug.Log("有文件参数"+ parameterModel.Length);
            }
            else
            {
                request.SetHeader("Content-Type", "application/json");
                RequestModel requestModel = new RequestModel();
                if (paramsDict != null && paramsDict.Count > 0)
                {
                    //foreach (KeyValuePair<string, string> keyValuePair in paramsDict)
                    //{
                    //    requestModel.AddData(keyValuePair.Key, keyValuePair.Value);
                    //}

                    requestModel.data = paramsDict;
                }
                Debug.Log(requestModel.ObjectToJson());
                request.RawData = requestModel.GetBytes();
            }
            //发送
            return request.Send();
        }


    }




}
