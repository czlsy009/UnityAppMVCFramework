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
//  * 文件名：TextureLoader.cs
//  * 创建时间：2016年07月11日 
//  * 创建人：Blank Alian
//  */

using BlankFramework;
using FairyGUI;
using UnityEngine;

/// <summary>
/// // [imgplaceholder://][http://192.168.1.8:8080/videos/mp4.mp4][http://192.168.1.8:6666/musicimages/40aba8773912b31b2effcffd8718367adbb4e17d.jpg][ui://nwadc9a5g5hq1]
/// // [协议头][实际数据地址][占位图的地址][备用占位图地址]
/// // [videoplaceholder://][http://192.168.1.8:8080/videos/mp4.mp4][http://192.168.1.8:6666/musicimages/40aba8773912b31b2effcffd8718367adbb4e17d.jpg][ui://nwadc9a5g5hq1]
/// // [协议头][实际数据地址][占位图的地址][备用占位图地址]
/// </summary>
public class TextureLoader : GLoader
{
    protected override void CreateDisplayObject()
    {
        UIPackage.AddPackage("UI/PlaceHolder/PlaceHolder");
        base.CreateDisplayObject();
    }


    /// <summary>
    ///  加载失败调用的函数
    /// </summary>
    public BlankActionForString LoadErrorCallBack;
    /// <summary>
    ///  加载成功调用的函数
    /// </summary>
    public BlankActionForTexture LoadSuccessCallBack;

    protected override void LoadExternal()
    {
        Log.I("URL 扩展加载。地址：" + url);

        TextureLoaderProtocolModel textureLoaderProtocolModel = TextureProtocolParse.Parse(url);
        if (textureLoaderProtocolModel.IsProtocol)
        {
            url = textureLoaderProtocolModel.PlaceholderData;
            ImageCacheManager.GetTextureWithPlaceholderurl(textureLoaderProtocolModel.RealData, LoadSuccess, LoadError, LoadErrorPlaceholderCallback, true, textureLoaderProtocolModel.PlaceholderData);
        }
        else
        {
            if (url.StartsWith("ui://"))
            {
                UIPackage.AddPackage("UI/PlaceHolder/PlaceHolder");
            }
            else
            {
                ImageCacheManager.GetTexture(url, LoadSuccess, LoadError);
            }
        }
        //        if (url.StartsWith(IMG_PLACE_HOLDER))
        //        {
        //
        //
        //
        //            // 图片占位图
        //            UIPackage.AddPackage("UI/PlaceHolder/PlaceHolder");
        //            string imgplaceholderUrl = url;
        //            this.url = AppPlaceholderConst.IMAGE_PLACEHOLDER;
        //            string targetUrl = imgplaceholderUrl.Replace(IMG_PLACE_HOLDER, string.Empty);
        //            Debug.Log(targetUrl);
        //            ImageCacheManager.GetTexture(targetUrl, LoadSuccess, LoadError);
        //        }
        //        else if (url.StartsWith(VIDEO_PLACE_HOLDER))
        //        {
        //
        //            // 视频占位图
        //            UIPackage.AddPackage("UI/PlaceHolder/PlaceHolder");
        //
        //
        //
        //            string placeholderurl = url.Replace(VIDEO_PLACE_HOLDER, string.Empty);
        //            Log.I(placeholderurl);
        //            int startindex = placeholderurl.IndexOf("[", StringComparison.Ordinal);
        //            int endindex = placeholderurl.IndexOf("]", StringComparison.Ordinal);
        //            if (startindex >= 0 && endindex > 0)
        //            {
        //                int length = endindex - startindex;
        //                Log.I(length);
        //
        //                string resulturl = placeholderurl.Substring(0, length + 1);
        //
        //                string placeholderResultUrl = resulturl.Replace("[", string.Empty).Replace("]", string.Empty);
        //
        //                string targetUrl = placeholderurl.Replace(resulturl, string.Empty);
        //                ImageCacheManager.GetTexture(targetUrl, LoadSuccess, LoadError, LoadErrorPlaceholderCallback, true, placeholderResultUrl);
        //                Log.I(resulturl);
        //            }
        //            //            this.url = AppPlaceholderConst.VIDEO_PLACEHOLDER;
        //        }
        //        else if (url.StartsWith("placeholder://"))
        //        {
        //            string placeholderurl = url.Replace("placeholder://", "");
        //            Log.I(placeholderurl);
        //            int startindex = placeholderurl.IndexOf("[", StringComparison.Ordinal);
        //            int endindex = placeholderurl.IndexOf("]", StringComparison.Ordinal);
        //            if (startindex >= 0 && endindex > 0)
        //            {
        //                int length = endindex - startindex;
        //                Log.I(length);
        //
        //                string resulturl = placeholderurl.Substring(0, length + 1);
        //
        //                string placeholderResultUrl = resulturl.Replace("[", string.Empty).Replace("]", string.Empty);
        //
        //                string targetUrl = placeholderurl.Replace(resulturl, string.Empty);
        //                ImageCacheManager.GetTexture(targetUrl, LoadSuccess, LoadError, LoadErrorPlaceholderCallback, true, placeholderResultUrl);
        //                Log.I(resulturl);
        //            }
        //        }
        //        else
        //        {
        //            ImageCacheManager.GetTexture(url, LoadSuccess, LoadError);
        //        }
    }

    private void LoadErrorPlaceholderCallback(string placeholderResultUrl)
    {
        Log.I(placeholderResultUrl);
        if (!string.IsNullOrEmpty(placeholderResultUrl))
        {
            this.url = placeholderResultUrl;
        }
    }



    private void LoadError(string str)
    {
        if (LoadErrorCallBack != null)
        {
            LoadErrorCallBack(str);
        }

        Debug.LogError("图像下载失败！地址 ： " + url + " 错误信息：" + str);
        this.onExternalLoadFailed();
    }

    private void LoadSuccess(Texture nTexture)
    {
        if (LoadSuccessCallBack != null)
        {
            LoadSuccessCallBack(nTexture);
        }
        Debug.Log(nTexture);

        NTexture m_texture = new NTexture(nTexture);
        
        onExternalLoadSuccess(m_texture);
    }

    protected override void FreeExternal(NTexture ntexture)
    {
        ntexture.Dispose();
        base.FreeExternal(ntexture);
    }

    public override void Dispose()
    {
        Debug.Log("public override void Dispose()");
        base.Dispose();
    }
}
