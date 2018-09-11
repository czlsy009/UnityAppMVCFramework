

namespace BlankFramework
{
    using System;
    using System.IO;
    using BestHTTP;
    using UnityEngine;

    /// <summary>
    /// 图像文件缓存 管理器
    /// </summary>
    public class ImageCacheManager : MonoBehaviour
    {

        private static readonly string RootPath = Application.persistentDataPath + "/imgscaches/";

        void Awake()
        {
            if (!Directory.Exists(RootPath))
            {
                Directory.CreateDirectory(RootPath);
            }
        }


        #region PublicAPI
        /// <summary>
        /// 加载图像
        /// </summary>
        /// <param name="urlString">网络地址</param>
        /// <param name="loadedCallBack">加载完毕回调</param>
        public static void GetTexture(string urlString, BlankActionForTexture2D loadedCallBack)
        {
            GetTexture(urlString, loadedCallBack, null);
        }




        /// <summary>
        /// 加载图像
        /// </summary>
        /// <param name="urlString">网络地址</param>
        /// <param name="loadedCallBack">加载完毕回调</param>
        /// <param name="loadErrorCallback">加载</param>
        public static void GetTexture(string urlString, BlankActionForTexture2D loadedCallBack, BlankActionForString loadErrorCallback)
        {
            GetTextureWithPlaceholderurl(urlString, loadedCallBack, loadErrorCallback, null, false, null);
            return;
            #region Old


            //            // Debug.Log(path);
            //            urlString = urlString.ToLower();
            //            int isForceIndex = urlString.IndexOf("?", StringComparison.Ordinal);
            //
            //            // 是否强制刷新 网络图像
            //            bool isForceLoadNetWork = isForceIndex != -1;
            //            if (isForceLoadNetWork)
            //            {
            //                // 干掉 问号
            //                urlString = urlString.Substring(0, isForceIndex);
            //            }
            //            // 获取文件名
            //            string fileName = Path.GetFileName(urlString);
            //
            //            if (string.IsNullOrEmpty(fileName))
            //            {
            //                if (loadErrorCallback != null)
            //                {
            //                    loadErrorCallback(" Load path Error " + urlString);
            //                }
            //                return;
            //            }
            //            // Debug.Log("资源路径 " + );
            //
            //            // 获取资源根路径的位置
            //
            //            // 拼接 文件存储根路径 和 文件保存路径  得到保存文件的完整路径
            //            string resourcePath = RootPath + GetResourecePath(urlString);
            //
            //            // 判断 存储路径是否存在
            //            if (!Directory.Exists(resourcePath))
            //            {
            //                Directory.CreateDirectory(resourcePath);
            //            }
            //
            //            string imageSavePath = string.Format("{0}{1}", resourcePath, fileName);
            //
            //            // 判断是否强制加载网络图片 只有在加载网络图像失败的时候才加载本地图像
            //            #region  判断是否强制加载网络图片 只有在加载网络图像失败的时候才加载本地图像
            //
            //            if (isForceLoadNetWork)
            //            {
            //                NetWorkManager.Instance.SendGetType(urlString, success =>
            //                {
            //                    HTTPResponse response = (HTTPResponse)success.Body;
            //                    if (response != null)
            //                    {
            //                        File.WriteAllBytes(imageSavePath, response.Data);
            //                        if (loadedCallBack != null)
            //                        {
            //                            loadedCallBack(response.DataAsTexture2D);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (loadErrorCallback != null)
            //                        {
            //                            loadErrorCallback("未知错误");
            //                        }
            //                    }
            //                }, error =>
            //                {
            //                    // 加载本地图像
            //                    if (File.Exists(imageSavePath))
            //                    {
            //                        // Log.I("Load Local !!!");
            //                        Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            //                        texture.LoadImage(File.ReadAllBytes(imageSavePath));
            //                        if (loadedCallBack != null)
            //                        {
            //                            loadedCallBack(texture);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (loadErrorCallback != null)
            //                        {
            //                            loadErrorCallback("Load Net Work Texture Error! Net Work Not Link!");
            //                        }
            //                        Log.E(" Load Net Work Texture Error! Net Work Not Link!");
            //                    }
            //                });
            //                return;
            //            }
            //            #endregion
            //
            //            if (File.Exists(imageSavePath))
            //            {
            //                // Log.I("Load Local !!!");
            //                Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            //                texture.LoadImage(File.ReadAllBytes(imageSavePath));
            //                if (loadedCallBack != null)
            //                {
            //                    loadedCallBack(texture);
            //                }
            //            }
            //            else
            //            {
            //                //Log.I("Load Net Work !!!");
            //                NetWorkManager.Instance.SendGetType(urlString, success =>
            //                {
            //                    HTTPResponse response = (HTTPResponse)success.Body;
            //                    if (response != null)
            //                    {
            //                        File.WriteAllBytes(imageSavePath, response.Data);
            //                        if (loadedCallBack != null)
            //                        {
            //                            loadedCallBack(response.DataAsTexture2D);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (loadErrorCallback != null)
            //                        {
            //                            loadErrorCallback("未知错误");
            //                        }
            //                    }
            //                }, error =>
            //                {
            //                    if (loadErrorCallback != null)
            //                    {
            //                        loadErrorCallback("Load Net Work Texture Error! Net Work Not Link!");
            //                    }
            //                    Log.E(" Load Net Work Texture Error! Net Work Not Link!");
            //                });
            //            }
            #endregion
        }

        /// <summary>
        /// 通过图像的URL地址 获取图像的缓存地址
        /// </summary>
        /// <param name="urlPath">URL 地址</param>
        /// <returns></returns>
        public static string GetImageCachePath(string urlPath)
        {
            string imageCachePath = string.Format("{0}{1}.jpg", RootPath, Tools.ConvertToMD5String(urlPath));
            return imageCachePath;
        }


        /// <summary>
        /// 加载图像
        /// </summary>
        /// <param name="urlString">网络地址</param>
        /// <param name="loadedCallBack">加载完毕回调</param>
        /// <param name="loadErrorCallback">加载</param>
        /// <param name="placeholderurl"></param>
        public static void GetTextureWithPlaceholderurl(string urlString, BlankActionForTexture2D loadedCallBack, BlankActionForString loadErrorCallback, string placeholderurl)
        {
            GetTextureWithPlaceholderurl(urlString, loadedCallBack, loadErrorCallback, false, placeholderurl);
        }

        /// <summary>
        /// 加载图像
        /// </summary>
        /// <param name="urlString">网络地址</param>
        /// <param name="loadedCallBack">加载完毕回调</param>
        /// <param name="loadErrorCallback">加载</param>
        /// <param name="isplaceholderurl"></param>
        /// <param name="placeholderurl"></param>
        public static void GetTextureWithPlaceholderurl(string urlString, BlankActionForTexture2D loadedCallBack, BlankActionForString loadErrorCallback, bool isplaceholderurl, string placeholderurl = null)
        {
            GetTextureWithPlaceholderurl(urlString, loadedCallBack, loadErrorCallback, null, isplaceholderurl, placeholderurl);
        }

        /// <summary>
        /// 加载图像
        /// </summary>
        /// <param name="urlString">网络地址</param>
        /// <param name="loadedCallBack">加载完毕回调</param>
        /// <param name="loadErrorCallback">加载</param>
        /// <param name="loadErrorPlaceholderCallback"></param>
        /// <param name="isplaceholderurl">是否有备用占位图</param>
        /// <param name="placeholderurl"></param>
        public static void GetTextureWithPlaceholderurl(string urlString, BlankActionForTexture2D loadedCallBack, BlankActionForString loadErrorCallback, BlankActionForString loadErrorPlaceholderCallback, bool isplaceholderurl, string placeholderurl)
        {

            // TODO  这里 可能会出现 BUG  有些图像就没有文件名。目前还没有遇见到这种情况
            // 获取文件名
            string fileName = Path.GetFileName(urlString);

            if (string.IsNullOrEmpty(fileName))
            {
                // 文件名为空
                if (loadErrorCallback != null)
                {
                    loadErrorCallback(" Load path Error " + urlString);
                }
                if (isplaceholderurl)
                {
                    if (loadErrorPlaceholderCallback != null)
                    {
                        loadErrorPlaceholderCallback(placeholderurl);
                    }
                }
            }
            else
            {
                // 是否强制刷新 网络图像
                if (IsForceLoadNetWork(urlString))
                {
                    urlString = string.Format("{0}?t={1}", urlString, DateTime.Now.ToFileTime());
                }
                // 拼接 文件存储根路径 和 文件保存路径  得到保存文件的完整路径
                string imageSavePath = GetImageCachePath(urlString);

                GetImage(urlString, loadedCallBack, loadErrorCallback, loadErrorPlaceholderCallback, isplaceholderurl, placeholderurl, imageSavePath);
            }
        }


        #endregion

        /// <summary>
        /// 清理图像缓存目录下的所有数据
        /// </summary>
        public static void ClearImageCache()
        {
            if (Directory.Exists(RootPath))
            {
                Directory.Delete(RootPath, true);
            }
        }


        #region Private

        /// <summary>
        /// 获取是否强制请求网络数据
        /// </summary>
        /// <param name="urlString"></param>
        /// <returns></returns>
        private static bool IsForceLoadNetWork(string urlString)
        {
            int isForceIndex = urlString.IndexOf("?", StringComparison.Ordinal);
            // 是否强制刷新 网络图像
            return isForceIndex >= 0;
        }


        /// <summary>
        /// 获取图像
        /// </summary>
        /// <param name="urlString"></param>
        /// <param name="loadedCallBack"></param>
        /// <param name="loadErrorCallback"></param>
        /// <param name="loadErrorPlaceholderCallback"></param>
        /// <param name="isplaceholderurl"></param>
        /// <param name="placeholderurl"></param>
        /// <param name="imageSavePath"></param>
        private static void GetImage(string urlString, BlankActionForTexture2D loadedCallBack, BlankActionForString loadErrorCallback,
            BlankActionForString loadErrorPlaceholderCallback, bool isplaceholderurl, string placeholderurl, string imageSavePath)
        {
            if (File.Exists(imageSavePath))
            {
                Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
                if (texture.LoadImage(File.ReadAllBytes(imageSavePath)))
                {
                    if (loadedCallBack != null)
                    {
                        loadedCallBack(texture);
                    }
                }
                else
                {
                    if (loadErrorCallback != null)
                    {
                        loadErrorCallback("Load Net Work Texture Error! Net Work Not Link! URL:" + urlString);
                    }
                    if (isplaceholderurl)
                    {
                        if (loadErrorPlaceholderCallback != null)
                        {
                            loadErrorPlaceholderCallback(placeholderurl);
                        }
                    }
                }
            }
            else
            {
                NetWorkManager.Instance.SendGetType(urlString, success =>
                {
                    HTTPResponse response = (HTTPResponse)success.Body;
                    if (response != null && response.IsSuccess)
                    {
                        // Success

                        File.WriteAllBytes(imageSavePath, response.Data);
                        if (loadedCallBack != null)
                        {
                            loadedCallBack(response.DataAsTexture2D);
                        }
                    }
                    else
                    {
                        // Error 
                        if (loadErrorCallback != null)
                        {
                            loadErrorCallback(response.StatusCode + "   " + response.Message);
                        }
                        if (isplaceholderurl)
                        {
                            if (loadErrorPlaceholderCallback != null)
                            {
                                loadErrorPlaceholderCallback(placeholderurl);
                            }
                        }
                    }
                }, error =>
                {
                    if (loadErrorCallback != null)
                    {
                        loadErrorCallback("Load Net Work Texture Error! Net Work Not Link! URL:" + urlString);
                    }
                    if (isplaceholderurl)
                    {
                        if (loadErrorPlaceholderCallback != null)
                        {
                            loadErrorPlaceholderCallback(placeholderurl);
                        }
                    }
                    Debug.LogError("Load Net Work Texture Error! Net Work Not Link! URL:" + urlString);
                });
            }
        }

        #endregion

    }
}
