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
//  * 文件名：Main.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */

using System.Collections.Generic;
using BestHTTP;
using LitJson;
using UnityEngine;

namespace BlankFramework
{
    /// <summary>
    /// 框架入口,框架启动器
    /// </summary>
    public class Main : MonoBehaviour
    {

        public bool IsDebugMode;
#if UNITY_EDITOR
        public bool IsRunFlatUI;
#endif
        private static bool _isInit = false;

        /// <summary>
        /// 启动框架
        /// </summary>
        private void Awake()
        {
            Application.targetFrameRate = 45;
            if (_isInit)
            {
                GameObject main = GameObject.Find("Main");
                if (main != null)
                {
                    Destroy(main);
                }
                return;
            }
#if UNITY_EDITOR
            Tools.IsFalt = IsRunFlatUI;
#endif
            Log.IsDebug = true;
            //启动
            AppBridgeLink.Instance.SendMessageCommand(NotificationConst.START_UP);
            this.name += "Framework";
            DontDestroyOnLoad(this);
            _isInit = true;




        }

        void OnGUI()
        {
            //if (GUI.Button(new Rect(0, 200, 150, 150), "TestNetwork"))
            //{
            //    Dictionary<string, string> pas = new Dictionary<string, string>();
            //    pas.Add("templateId", "1038343388014780418");
            //    NetWorkManager.Instance.SendPostType("http://java.3plus.ltd:8087/" + "api/template/get",
            //        (successMessage =>
            //        {
            //            HTTPResponse res = (HTTPResponse)successMessage.Body;
            //            Debug.Log(res.DataAsText);
            //        }), (errorMessage =>
            //         {

            //         }), pas);
            //}
            //if (GUI.Button(new Rect(0, 500, 150, 150), "TestNetwork"))
            //{
            //    Dictionary<string, string> pas = new Dictionary<string, string>();
            //    pas.Add("name", "name");
            //    pas.Add("description","des");
            //    NetWorkManager.Instance.SendPostType("http://java.3plus.ltd:8087/" + "api/template/upload",
            //        (successMessage =>
            //        {
            //            HTTPResponse res = (HTTPResponse)successMessage.Body;
            //            Debug.Log(JsonMapper.ToObject(res.DataAsText).ToJson());
            //        }), (errorMessage =>
            //        {

            //        }), pas,new FileParamModel("json", "C:\\Users\\3plus-9\\AppData\\LocalLow\\ThreePlus\\TemplateTool\\1038343388014780418\\template.json"));
            //}

            if (GUI.Button(new Rect(0, 500, 150, 150), "TestNetwork"))
            {
                startPageUiViewController = new StartPageUIViewController();
                UIWindow.InitWithRootViewController(startPageUiViewController);
            }
        }

        private StartPageUIViewController startPageUiViewController;
        void Start()
        {

        }
    }
}