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
//  * 说明：保存文件到相册的插件
//  *     Android 平台 
//  *       文件列表： 
//  *                 BlankGalleryScreenshot.jar 文件一个
//  *     IOS 平台 
//  *                 AHGalleryScreenshot.h 文件一个
//  *                 AHGalleryScreenshot.mm 文件一个
//  * 需要在清单文件中 添加一个Activity
//  *       <!-- 保存图片到相册插件 -->
//  *       <activity android:name="com.alianhome.galleryscreenshot.MainActivity" />
//  *
//  * 文件名：BlankGalleryScreenshot.cs
//  * 创建时间：2016年08月03日 
//  * 创建人：Blank Alian
//  * 联系方式：wangfj11@foxmail.com
//  */
#if UNITY_IOS
using System.Runtime.InteropServices;
#endif
using System;
using System.Collections;
using System.IO;
using BlankFramework;
using FairyGUI;
using UnityEngine;


public class BlankGalleryScreenshot : MonoBehaviour
{
    /// <summary>
    /// 截屏后的图像回调
    /// </summary>
    public static event Action<Texture2D> OnCaptureScreenshot;
    /// <summary>
    /// 状态发生改变的时候触发的事件 。目前只有两种情况  Start  OR Finish
    /// </summary>
    public static event Action<string> GalleryScreenshotStateChangeEvent;

#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void addImageToGallery(string path);
#endif

    private static BlankGalleryScreenshot _instance;
    public static BlankGalleryScreenshot Instance
    {
        get
        {
            if (_instance == null)
            {
                string GalleryScreenshotBridgeLink = "GalleryScreenshotBridgeLink";
                GameObject go = GameObject.Find(GalleryScreenshotBridgeLink);
                if (go != null)
                {
                    DestroyImmediate(go);
                }
                go = new GameObject(GalleryScreenshotBridgeLink);
                go.hideFlags = HideFlags.HideAndDontSave;
                string savePath = Application.persistentDataPath + "/screenshots/";
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                _instance = go.AddComponent<BlankGalleryScreenshot>();
            }
            return _instance;
        }
    }

    public void SaveGalleryScreenshotForUI(GComponent ui)
    {
        ui.visible = false;
        SaveGalleryScreenshot(() =>
        {
            ui.visible = true;
        });
    }

    public void SaveGalleryScreenshot()
    {
        StartCoroutine(CaptureScreenshot(null));
    }

    /// <summary>
    /// 截屏 并保存到相册
    /// </summary>
    public void SaveGalleryScreenshot(BlankAction finshCallBack)
    {
        StartCoroutine(CaptureScreenshot(finshCallBack));
        //MusicManager.Instance.PlayOneShotAudio(Resources.Load("Audios/photo_graph_effect") as AudioClip);
    }

    private IEnumerator CaptureScreenshot(BlankAction finshCallBack)
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture2D.Apply();
        byte[] bytes = texture2D.EncodeToJPG();

        if (OnCaptureScreenshot != null)
        {
            OnCaptureScreenshot(texture2D);
        }
        else
        {
            DestroyImmediate(texture2D);
        }

        string filePath = Application.persistentDataPath + "/screenshots/" + DateTime.Now.ToFileTime() + ".jpg";
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("图片保存完成 保存路径 ： " + filePath);
        SaveGalleryScreenshot(filePath);

        if (finshCallBack != null)
        {
            finshCallBack();
        }
    }

    /// <summary>
    /// 把Application.persistentDataPath 目录下的图像文件移动到相册目录 并且注册到相册
    /// </summary>
    /// <param name="filePath"></param>
    public void SaveGalleryScreenshot(string filePath)
    {
#if UNITY_EDITOR
        ScreenCapture.CaptureScreenshot(filePath);
#elif UNITY_ANDROID 
        using (AndroidJavaClass ajc = new AndroidJavaClass("com.alianhome.galleryscreenshot.MainActivity"))
        {
            ajc.CallStatic("addImageToGallery", filePath);
        }
#elif UNITY_IOS
        addImageToGallery(filePath);
#endif
    }


    /// <summary>
    /// 该 函数为原生回调 勿删
    /// </summary>
    /// <param name="state"></param>
    void GalleryScreenshotStateChange(string state)
    {
        if (GalleryScreenshotStateChangeEvent != null)
        {
            GalleryScreenshotStateChangeEvent(state);
        }

        if (state.Equals("Finish"))
        {
            Debug.Log("保存完成");
        }
        else if (state.Equals("Start"))
        {
            Debug.Log("开始保存");
        }
    }
}
