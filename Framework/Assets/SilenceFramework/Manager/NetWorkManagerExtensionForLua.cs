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
//  * 文件名：NetWorkManagerExtensionForLua.cs
//  * 创建时间：2017年01月06日 
//  * 创建人：Blank Alian
//  */


using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP;

namespace BlankFramework
{
    public partial class NetWorkManager
    {
        #region GET
        #region SendGetTypeForByte

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="successCallBack">请求成功回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetTypeForByte(string urlString, BlankActionForByte successCallBack)
        {
            return SendGetTypeForByte(urlString, successCallBack, null);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="successCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetTypeForByte(string urlString, BlankActionForByte successCallBack, BlankAction errorCallBack)
        {
            return SendGetTypeForByte(urlString, string.Empty, successCallBack, errorCallBack);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="jsonText"></param>
        /// <param name="successCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetTypeForByte(string urlString, string jsonText, BlankActionForByte successCallBack, BlankAction errorCallBack)
        {
            return SendGetType(urlString, jsonText, s =>
            {
                if (successCallBack != null)
                {
                    HTTPResponse httpResponse = (HTTPResponse)s.Body;
                    if (httpResponse != null)
                    {
                        successCallBack(httpResponse.Data);
                    }
                }
            }, e =>
            {
                if (errorCallBack != null)
                {
                    errorCallBack();
                }
            });
        }

        #endregion

        #region SendGetTypeForByte

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="successCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetTypeForString(string urlString, BlankActionForString successCallBack, BlankAction errorCallBack = null)
        {
            return SendGetTypeForString(urlString, string.Empty, successCallBack, errorCallBack);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="jsonText"></param>
        /// <param name="successCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetTypeForString(string urlString, string jsonText, BlankActionForString successCallBack, BlankAction errorCallBack)
        {
            return SendGetType(urlString, jsonText, s =>
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
                if (errorCallBack != null)
                {
                    errorCallBack();
                }
            });
        }

        #endregion

        #region Base

        /// <summary>
        /// 发送GET 方式请求
        /// Tip : 
        ///     [基础函数]
        ///     1. 其他Get请求方式函数都是从该函数扩展而成
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="jsonText">发送的参数</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendGetType(string urlString, string jsonText, OnSuccessCallBack sucessCallBack, OnErrorCallBack errorCallBack)
        {
            return SendGetType(urlString, jsonText, (request, response) =>
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
        /// 发送GET 方式的请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="jsonText">发送的参数</param>
        /// <param name="onRequestFinishedDelegate">回调函数</param>
        /// <returns>请求对象</returns>
        // [Obsolete("该函数不建议使用,建议使用 SendGetType(string, Dictionary<string, string>, OnSuccessCallBack, OnErrorCallBack) ")]
        private HTTPRequest SendGetType(string urlString, string jsonText, OnRequestFinishedDelegate onRequestFinishedDelegate)
        {
            Log.I(urlString);

            if (IsURL(urlString))
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(urlString);
                string url = urlString;
                if (!string.IsNullOrEmpty(jsonText))
                {
                    stringBuilder.Append("?");
                    stringBuilder.Append(jsonText);
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


        #endregion
        #endregion

        #region POST
        #region SendPostTypeForByte

        /// <summary>
        /// 发送POST  方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendPostTypeForByte(string urlString, BlankActionForByte sucessCallBack)
        {
            return SendPostType((successModel) =>
            {
                HTTPResponse httpResponse = successModel.Body as HTTPResponse;
                if (httpResponse != null)
                {
                    if (sucessCallBack != null)
                    {
                        sucessCallBack(httpResponse.Data);
                    }
                }
                else
                {
                    Log.I(" 响应失败 " + urlString);
                }
            }, (error) =>
            {
                Log.I(" 请求失败 " + urlString);
            }, urlString, string.Empty);
        }

        /// <summary>
        /// 发送POST  方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="jsonText">参数列表</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendPostTypeForByte(string urlString, BlankActionForByte sucessCallBack, string jsonText)
        {
            return SendPostType((successModel) =>
            {
                HTTPResponse httpResponse = successModel.Body as HTTPResponse;
                if (httpResponse != null)
                {
                    if (sucessCallBack != null)
                    {
                        sucessCallBack(httpResponse.Data);
                    }
                }
                else
                {
                    Log.I(" 响应失败 " + urlString);
                }
            }, (error) =>
            {
                Log.I(" 请求失败 " + urlString);
            }, urlString, jsonText);
        }

        /// <summary>
        /// 发送POST  方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <param name="jsonText">参数列表</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendPostTypeForByte(string urlString, BlankActionForByte sucessCallBack, BlankAction errorCallBack, string jsonText)
        {
            return SendPostType((successModel) =>
            {
                HTTPResponse httpResponse = successModel.Body as HTTPResponse;
                if (httpResponse != null)
                {
                    if (sucessCallBack != null)
                    {
                        sucessCallBack(httpResponse.Data);
                    }
                }
                else
                {
                    if (errorCallBack != null)
                    {
                        errorCallBack();
                    }
                }
            }, (error) =>
            {
                if (errorCallBack != null)
                {
                    errorCallBack();
                }
            }, urlString, jsonText);
        }

        #endregion
        #region SendPostTypeForString

        /// <summary>
        /// 发送POST  方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendPostTypeForString(string urlString, BlankActionForString sucessCallBack)
        {
            return SendPostTypeForString(urlString, sucessCallBack, string.Empty);
        }
        /// <summary>
        /// 发送POST  方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="jsonText">参数列表</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendPostTypeForString(string urlString, BlankActionForString sucessCallBack, string jsonText)
        {
            return SendPostType((successModel) =>
            {
                HTTPResponse httpResponse = successModel.Body as HTTPResponse;
                if (httpResponse != null)
                {
                    if (sucessCallBack != null)
                    {
                        sucessCallBack(httpResponse.DataAsText);
                    }
                }
                else
                {
                    Log.I(" 响应失败 " + urlString);
                }
            }, (error) =>
            {
                Log.I(" 请求失败 " + urlString);
            }, urlString, jsonText);
        }


        /// <summary>
        /// 发送POST  方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <param name="jsonText">参数列表</param>
        /// <returns>请求对象</returns>
        public HTTPRequest SendPostTypeForString(string urlString, BlankActionForString sucessCallBack, BlankAction errorCallBack, string jsonText)
        {
            return SendPostType((successModel) =>
            {
                HTTPResponse httpResponse = successModel.Body as HTTPResponse;
                if (httpResponse != null)
                {
                    if (sucessCallBack != null)
                    {
                        sucessCallBack(httpResponse.DataAsText);
                    }
                }
                else
                {
                    if (errorCallBack != null)
                    {
                        errorCallBack();
                    }
                }
            }, (error) =>
            {
                if (errorCallBack != null)
                {
                    errorCallBack();
                }
            }, urlString, jsonText);
        }

        #endregion

        /// <summary>
        /// 发送POST  方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="sucessCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <param name="jsonText">参数列表</param>
        /// <returns>请求对象</returns>
        private HTTPRequest SendPostType(string urlString, OnSuccessCallBack sucessCallBack, OnErrorCallBack errorCallBack, string jsonText)
        {
            return SendPostType(sucessCallBack, errorCallBack, urlString, jsonText);
        }


        /// <summary>
        /// 发送POST 方式请求
        /// </summary>
        /// <param name="successCallBack">请求成功回调</param>
        /// <param name="errorCallBack">请求失败回调</param>
        /// <param name="urlString">请求地址</param>
        /// <param name="jsonText">参数列表</param>
        /// <returns>返回请求对象</returns>
        private HTTPRequest SendPostType(OnSuccessCallBack successCallBack, OnErrorCallBack errorCallBack,
            string urlString, string jsonText)
        {
            return SendPostTypeLua((request, response) =>
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
            }, urlString, jsonText);
        }

        /// <summary>
        /// 发送POST 方式的请求
        /// </summary>
        /// <param name="onRequestFinishedDelegate">回调函数</param>
        /// <param name="urlString">请求地址</param>
        /// <param name="jsonText">请求的普通参数</param>
        /// <param name="parameterModel">请求需要发送的文件数据</param>
        /// <returns>请求对象</returns>
        private HTTPRequest SendPostTypeLua(OnRequestFinishedDelegate onRequestFinishedDelegate, string urlString,
            string jsonText, params  ParameterModel[] parameterModel)
        {
            HTTPRequest request = new HTTPRequest(new Uri(urlString), HTTPMethods.Post, onRequestFinishedDelegate);

            Dictionary<string, string> paramsDict = JSONToDictionary(jsonText);
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

        /// <summary>
        /// JSON 转 字典 类型
        /// </summary>
        /// <param name="jsonText">JSON 字符串</param>
        /// <returns></returns>
        private Dictionary<string, string> JSONToDictionary(string jsonText)
        {
            TinyJson.Node jsonNode = TinyJson.Parser.Instance.Load(jsonText);

            if (jsonNode != null && jsonNode.IsArray())
            {
                List<TinyJson.Node> list = (List<TinyJson.Node>)jsonNode;
                int jsonCount = list.Count;

                if (jsonCount > 0)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    for (int i = 0; i < jsonCount; i++)
                    {
                        TinyJson.Node node = list[i];
                        dictionary.Add(node[0]["key"].ToString(), node[0]["value"].ToString());
                    }
                    return dictionary;
                }
            }
            return null;
        }

    }
}