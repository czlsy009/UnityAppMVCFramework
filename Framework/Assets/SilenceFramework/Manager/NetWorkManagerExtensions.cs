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
//  * 文件名：NetWorkManagerExtensions.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */


using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP;
using UnityEngine;

namespace BlankFramework
{
    /// <summary>
    /// 网络管理器 扩展文件
    /// </summary>
    public partial class NetWorkManager
    {
        private TinyJson.Parser m_parse = new TinyJson.Parser();

        #region Get Success


        /// <summary>
        /// 发送Get 方式请求 默认使用请求成功返回码/和无参数的请求方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="jsonNodeCallBack">请求成功回调</param>
        /// <returns></returns>
        public HTTPRequest SendGetSuccess(string urlString, BlankActionForTinyJsonNode jsonNodeCallBack)
        {
            return SendGetSuccess(urlString, NetWorkResultCodeConst.CODE_000000, jsonNodeCallBack);
        }
        /// <summary>
        /// 发送Get 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConst">需要判断的响应码</param>
        /// <param name="jsonNodeCallBack">请求成功回调</param>
        /// <returns></returns>
        public HTTPRequest SendGetSuccess(string urlString, string netWorkResultCodeConst, BlankActionForTinyJsonNode jsonNodeCallBack)
        {
            return SendGetSuccess(urlString, netWorkResultCodeConst, null, jsonNodeCallBack);
        }

        /// <summary>
        /// 发送Get 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数字典</param>
        /// <param name="jsonNodeCallBack">请求成功回调</param>
        /// <returns></returns>
        public HTTPRequest SendGetSuccess(string urlString, Dictionary<string, string> paramsDict, BlankActionForTinyJsonNode jsonNodeCallBack = null)
        {
            return SendGetSuccess(urlString, NetWorkResultCodeConst.CODE_000000, paramsDict, jsonNodeCallBack);
        }
        /// <summary>
        /// 发送GET  请求成功函数
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConst">需要的响应参数KEy。如果需要多个参数。就换个函数</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <returns></returns>
        public HTTPRequest SendGetSuccess(string urlString, string netWorkResultCodeConst, Dictionary<string, string> paramsDict, BlankActionForTinyJsonNode jsonNodeCallBack)
        {

            return SendGetType(urlString, paramsDict, (resultCode, jsonNode) =>
                {
                    if (resultCode.Equals(netWorkResultCodeConst))
                    {
                        if (jsonNodeCallBack != null)
                        {
                            jsonNodeCallBack(jsonNode);
                        }
                    }
                    else
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (KeyValuePair<string, string> keyValuePair in paramsDict)
                        {
                            stringBuilder.Append(keyValuePair.Key + ":" + keyValuePair.Value);
                        }

                        Debug.LogError("没有找到匹配的返回码 ==> 服务器返回码：" + resultCode + " 想要匹配的返回码：" + netWorkResultCodeConst + "请求地址： " + urlString + " 参数列表： " + stringBuilder.ToString());
                    }
                });

            //return SendGetType(urlString, paramsDict, s =>
            //{
            //    HTTPResponse response = s.Body as HTTPResponse;
            //    if (response != null)
            //    {
            //        Log.I(response.DataAsText);
            //        TinyJson.Node jsonNode = m_parse.Load(response.DataAsText);
            //        if (jsonNode != null)
            //        {
            //            string resultCode = jsonNode[NetWorkReslutKeyConst.CODE_GET].ToString();
            //            if (resultCode.Equals(netWorkResultCodeConst))
            //            {
            //                if (jsonNodeCallBack != null)
            //                {
            //                    jsonNodeCallBack(jsonNode);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            Log.E("服务器出现错误！！！", "服务器出现错误");
            //        }
            //    }
            //}, e =>
            //{
            //    Log.E("网络错误" + urlString);
            //});
        }

        public HTTPRequest SendGetSuccess(string urlString, BlankActionForTinyJsonNode jsonNodeCallBack,
            BlankAction errorCallBack)
        {
            return SendGetSuccess(urlString, null, jsonNodeCallBack, errorCallBack);
        }

        /// <summary>
        /// 发送GET  请求成功函数
        /// 注意 ：如果传入多个返回码 如果有任何一个 函数即终止 查找
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <param name="netWorkResultCodeConsts">需要的响应参数KEy  可多个 必须最少传一个</param>
        /// <returns></returns>
        public HTTPRequest SendGetSuccess(string urlString, Dictionary<string, string> paramsDict, BlankActionForTinyJsonNode jsonNodeCallBack,
            params string[] netWorkResultCodeConsts)
        {

            return SendGetType(urlString, paramsDict, (resultCode, jsonNode) =>
            {
                if (netWorkResultCodeConsts != null && netWorkResultCodeConsts.Length > 0)
                {
                    for (int i = 0; i < netWorkResultCodeConsts.Length; i++)
                    {
                        if (resultCode.Equals(netWorkResultCodeConsts[i]))
                        {
                            if (jsonNodeCallBack != null)
                            {
                                jsonNodeCallBack(jsonNode);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("netWorkResultCodeConsts 错误 必须最少传一个");
                }
            });
        }

        /// <summary>
        /// 发送GET  请求成功函数
        /// 注意 ：如果传入多个返回码 如果有任何一个 函数即终止 查找
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <param name="errorCallBack">错误回调</param>
        /// <param name="netWorkResultCodeConsts">需要的响应参数KEy  可多个 必须最少传一个</param>
        /// <returns></returns>
        public HTTPRequest SendGetSuccess(string urlString, Dictionary<string, string> paramsDict, BlankActionForTinyJsonNode jsonNodeCallBack, BlankAction errorCallBack,
            params string[] netWorkResultCodeConsts)
        {
            return SendGetType(urlString, paramsDict, (resultCode, jsonNode) =>
                {
                    if (netWorkResultCodeConsts != null && netWorkResultCodeConsts.Length > 0)
                    {
                        for (int i = 0; i < netWorkResultCodeConsts.Length; i++)
                        {
                            if (resultCode.Equals(netWorkResultCodeConsts[i]))
                            {
                                if (jsonNodeCallBack != null)
                                {
                                    jsonNodeCallBack(jsonNode);
                                }
                                return;
                            }
                        }

                        if (errorCallBack != null)
                        {
                            errorCallBack();
                        }
                    }
                    else
                    {
                        throw new ArgumentException("netWorkResultCodeConsts 错误 必须最少传一个");
                    }
                }, errorCallBack);
        }



        /// <summary>
        /// 发送GET  请求成功函数
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <param name="errorCallBack">网络错误回调</param>
        /// <returns></returns>
        public HTTPRequest SendGetSuccess(string urlString, Dictionary<string, string> paramsDict,
            BlankActionForTinyJsonNode jsonNodeCallBack, BlankAction errorCallBack)
        {
            return SendGetSuccess(urlString, NetWorkResultCodeConst.CODE_000000, paramsDict, jsonNodeCallBack,
                errorCallBack);
        }

        /// <summary>
        /// 发送GET  请求成功函数
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConst">需要的响应参数KEy。如果需要多个参数。就换个函数</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <param name="errorCallBack">网络错误回调</param>
        /// <returns></returns>
        public HTTPRequest SendGetSuccess(string urlString, string netWorkResultCodeConst, Dictionary<string, string> paramsDict, BlankActionForTinyJsonNode jsonNodeCallBack, BlankAction errorCallBack)
        {
            return SendGetType(urlString, paramsDict, (resultCode, jsonNode) =>
            {
                if (resultCode.Equals(netWorkResultCodeConst))
                {
                    if (jsonNodeCallBack != null)
                    {
                        jsonNodeCallBack(jsonNode);
                    }
                }
                else
                {Debug.LogError(resultCode);
                    if (errorCallBack != null)
                    {
                        errorCallBack();
                    }
                }
            }, errorCallBack);
        }


        /// <summary>
        /// 发送GET  请求成功函数 注意：一旦匹配到任何一个返回码就完成返回码解析
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConsts">需要的响应参数KEY</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <returns></returns>
        public HTTPRequest SendGetSuccess(string urlString, string[] netWorkResultCodeConsts, Dictionary<string, string> paramsDict, BlankAction<TinyJson.Node> jsonNodeCallBack)
        {

            return SendGetType(urlString, paramsDict, (resultCode, jsonNode) =>
            {
                for (int i = 0; i < netWorkResultCodeConsts.Length; i++)
                {
                    if (resultCode.Equals(netWorkResultCodeConsts[i]))
                    {
                        if (jsonNodeCallBack != null)
                        {
                            jsonNodeCallBack(jsonNode);
                        }
                        break;
                    }
                }
            });
        }


        #endregion

        #region Post Success



        /// <summary>
        /// 发送POST  请求成功函数 
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">请求成功回调。第一个参数是JsonNode 对象。第二个为返回码</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, Dictionary<string, string> paramsDict,
            BlankAction<TinyJson.Node, string> jsonNodeCallBack)
        {
            return SendPostSuccess(urlString, paramsDict, jsonNodeCallBack, null);
        }

        /// <summary>
        /// 发送POST  请求成功函数 
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">请求成功回调。第一个参数是JsonNode 对象。第二个为返回码</param>
        /// <param name="errorCallBack">错误回调</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, Dictionary<string, string> paramsDict, BlankAction<TinyJson.Node, string> jsonNodeCallBack, BlankAction errorCallBack)
        {
            return SendPostType(urlString, paramsDict, delegate(MessageModel s)
            {
                HTTPResponse response = s.Body as HTTPResponse;
                if (response != null)
                {
                    Log.I(response.DataAsText);
                    TinyJson.Node jsonNode = m_parse.Load(response.DataAsText);
                    if (jsonNode != null)
                    {
                        string resultCode = jsonNode[NetWorkReslutKeyConst.CODE_GET].ToString();
                        if (jsonNodeCallBack != null)
                        {
                            jsonNodeCallBack(jsonNode, resultCode);
                        }
                    }
                    else
                    {
                        if (errorCallBack != null)
                        {
                            errorCallBack();
                        }
                        Debug.LogError("服务器出现错误数据：URL:" + urlString);
                    }
                }
            }, delegate
            {
                if (errorCallBack != null)
                {
                    errorCallBack();
                }
                Log.E("网络错误" + urlString);
            });
        }
        /// <summary>
        /// 发送Post 方式请求 默认使用请求成功返回码/和无参数的请求方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="jsonNodeCallBack">请求成功回调</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, BlankAction<TinyJson.Node> jsonNodeCallBack)
        {
            return SendPostSuccess(urlString, NetWorkResultCodeConst.CODE_000000, jsonNodeCallBack);
        }

        /// <summary>
        /// 发送Post 方式请求 默认使用请求成功返回码/和无参数的请求方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="jsonNodeCallBack">请求成功回调</param>
        /// <param name="errorCallBack">错误回调</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, BlankAction<TinyJson.Node> jsonNodeCallBack, BlankAction errorCallBack)
        {
            return SendPostSuccess(urlString, NetWorkResultCodeConst.CODE_000000, jsonNodeCallBack, errorCallBack);
        }
        /// <summary>
        /// 发送Post 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConst">需要判断的响应码</param>
        /// <param name="jsonNodeCallBack">请求成功回调</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, string netWorkResultCodeConst, BlankAction<TinyJson.Node> jsonNodeCallBack)
        {
            return SendPostSuccess(urlString, netWorkResultCodeConst, null, jsonNodeCallBack);
        }

        /// <summary>
        /// 发送Post 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConst">需要判断的响应码</param>
        /// <param name="jsonNodeCallBack">请求成功回调</param>
        /// <param name="errorCallBack"></param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, string netWorkResultCodeConst, BlankAction<TinyJson.Node> jsonNodeCallBack, BlankAction errorCallBack)
        {
            return SendPostSuccess(urlString, netWorkResultCodeConst, null, jsonNodeCallBack, errorCallBack);
        }
        /// <summary>
        /// 发送Post 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数字典</param>
        /// <param name="jsonNodeCallBack">请求成功回调</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, Dictionary<string, string> paramsDict, BlankAction<TinyJson.Node> jsonNodeCallBack)
        {
            return SendPostSuccess(urlString, NetWorkResultCodeConst.CODE_000000, paramsDict, jsonNodeCallBack);
        }

        /// <summary>
        /// 发送Post 方式请求
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="paramsDict">请求参数字典</param>
        /// <param name="jsonNodeCallBack">请求成功回调</param>
        /// <param name="errorCallBack">错误回调</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, Dictionary<string, string> paramsDict, BlankAction<TinyJson.Node> jsonNodeCallBack, BlankAction errorCallBack)
        {
            return SendPostSuccess(urlString, NetWorkResultCodeConst.CODE_000000, paramsDict, jsonNodeCallBack, errorCallBack);
        }
        /// <summary>
        /// 发送POST  请求成功函数
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConst">需要的响应参数KEy。如果需要多个参数。就换个函数</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, string netWorkResultCodeConst, Dictionary<string, string> paramsDict, BlankAction<TinyJson.Node> jsonNodeCallBack)
        {
            return SendPostSuccess(urlString, netWorkResultCodeConst, paramsDict, jsonNodeCallBack, null);
        }

        /// <summary>
        /// 发送POST  请求成功函数
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConst">需要的响应参数KEy。如果需要多个参数。就换个函数</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <param name="errorCallBack"></param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, string netWorkResultCodeConst, Dictionary<string, string> paramsDict, BlankAction<TinyJson.Node> jsonNodeCallBack, BlankAction errorCallBack)
        {
            return SendPostSuccess(urlString, new[] { netWorkResultCodeConst }, paramsDict, jsonNodeCallBack, errorCallBack);
        }
        /// <summary>
        /// 发送POST  请求成功函数 注意：一旦匹配到任何一个返回码就完成返回码解析
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConsts">需要的响应参数KEY</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, string[] netWorkResultCodeConsts, Dictionary<string, string> paramsDict, BlankAction<TinyJson.Node> jsonNodeCallBack)
        {
            return SendPostSuccess(urlString, netWorkResultCodeConsts, paramsDict, jsonNodeCallBack, null);
        }

        /// <summary>
        /// 发送POST  请求成功函数 注意：一旦匹配到任何一个返回码就完成返回码解析
        /// </summary>
        /// <param name="urlString">请求地址</param>
        /// <param name="netWorkResultCodeConsts">需要的响应参数KEY</param>
        /// <param name="paramsDict">请求参数</param>
        /// <param name="jsonNodeCallBack">回调</param>
        /// <param name="errorCallBack">错误回调</param>
        /// <returns></returns>
        public HTTPRequest SendPostSuccess(string urlString, string[] netWorkResultCodeConsts, Dictionary<string, string> paramsDict, BlankAction<TinyJson.Node> jsonNodeCallBack, BlankAction errorCallBack)
        {
            return SendPostType(urlString, paramsDict, s =>
            {
                HTTPResponse response = s.Body as HTTPResponse;
                if (response != null)
                {
                    Log.I(response.DataAsText);
                    TinyJson.Node jsonNode = m_parse.Load(response.DataAsText);
                    if (jsonNode != null)
                    {
                        string resultCode = jsonNode[NetWorkReslutKeyConst.CODE_GET].ToString();

                        for (int i = 0; i < netWorkResultCodeConsts.Length; i++)
                        {
                            if (resultCode.Equals(netWorkResultCodeConsts[i]))
                            {
                                if (jsonNodeCallBack != null)
                                {
                                    jsonNodeCallBack(jsonNode);
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (errorCallBack != null)
                        {
                            errorCallBack();
                        }
                        Debug.LogError("服务器出现错误数据：URL:" + urlString);
                    }
                }
            }, e =>
            {
                if (errorCallBack != null)
                {
                    errorCallBack();
                }
                Debug.LogError("网络错误" + urlString);
            });
        }
        #endregion


    }
}
