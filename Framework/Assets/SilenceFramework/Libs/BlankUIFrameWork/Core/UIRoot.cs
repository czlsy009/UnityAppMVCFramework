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
//  * 文件名：UIRoot.cs
//  * 创建时间：2016年08月16日 
//  * 创建人：Blank Alian
//  */

using FairyGUI;
using UnityEngine;

namespace BlankUIFrameWork
{
    public class UIRoot : MonoBehaviour
    {
        private static UIRoot _instance;

        public static UIRoot Instance
        {
            get
            {
                if (_instance == null)
                {
                    const string uiRootName = "UIRoot";
                    GameObject go = GameObject.Find(uiRootName);
                    if (go != null)
                    {
                        DestroyObject(go);
                    }
                    go = new GameObject() { name = uiRootName };

                    _instance = go.AddComponent<UIRoot>();

                    if (Tools.IsFalt)
                    {
                        GRoot.inst.SetContentScaleFactor(2024, 1518);
                    }
                    else
                    {
                        GRoot.inst.SetContentScaleFactor(1920, 1080);
                    }

                }
                return _instance;
            }
        }

        //void Awake()
        //{
        //    #region WindowRoot

        //    mWindowRoot = new GComponent();
        //    mWindowRoot.displayObject.gameObject.name = "WindowRoot";
        //    mWindowRoot.displayObject.gameObject.transform.position = new Vector3(0, 0, 1500);
        //    mWindowRoot.sortingOrder = -50;
        //    GRoot.inst.AddChild(mWindowRoot);

        //    #endregion

        //    #region NormalRoot

        //    mNormalRoot = new GComponent();
        //    mNormalRoot.displayObject.gameObject.name = "NormalRoot";
        //    mNormalRoot.displayObject.gameObject.transform.position = new Vector3(0, 0, 0);
        //    mNormalRoot.sortingOrder = 0;
        //    GRoot.inst.AddChild(mNormalRoot);

        //    #endregion


        //    #region FixedRoot
        //    mFixedRoot = new GComponent();
        //    mFixedRoot.displayObject.gameObject.name = "FixedRoot";
        //    mFixedRoot.displayObject.gameObject.transform.position = new Vector3(0, 0, -1500);
        //    mFixedRoot.sortingOrder = 1500;
        //    GRoot.inst.AddChild(mFixedRoot);
        //    #endregion


        //    #region PopupRoot

        //    mPopupRoot = new GComponent();
        //    mPopupRoot.displayObject.gameObject.name = "PopupRoot";
        //    mPopupRoot.displayObject.gameObject.transform.position = new Vector3(0, 0, -3000);
        //    mPopupRoot.sortingOrder = 3000;
        //    GRoot.inst.AddChild(mPopupRoot);

        //    #endregion

        //}
        /// <summary>
        /// 根Window 层
        /// </summary>
        private GComponent mWindowRoot;
        public GComponent WindowRoot
        {
            get
            {
                if (mWindowRoot == null)
                {
                    mWindowRoot = new GComponent();
                    mWindowRoot.displayObject.gameObject.name = "WindowRoot";
                    mWindowRoot.displayObject.gameObject.transform.position = new Vector3(0, 0, 1500);
                    mWindowRoot.sortingOrder = -50;
                    GRoot.inst.AddChild(mWindowRoot);
                }
                return mWindowRoot;
            }
        }

        /// <summary>
        /// 正常UI 层
        /// </summary>
        private GComponent mNormalRoot;

        public GComponent NormalRoot
        {
            get
            {
                if (mNormalRoot == null)
                {
                    mNormalRoot = new GComponent();
                    mNormalRoot.displayObject.gameObject.name = "NormalRoot";
                    mNormalRoot.displayObject.gameObject.transform.position = new Vector3(0, 0, 0);
                    mNormalRoot.sortingOrder = 0;
                    GRoot.inst.AddChild(mNormalRoot);
                }
                return mNormalRoot;
            }
        }

        /// <summary>
        /// 工具条层  NavigationBar  ToolBar 
        /// </summary>
        private GComponent mBarRoot;
        public GComponent BarRoot
        {
            get
            {
                if (mBarRoot == null)
                {
                    mFixedRoot = new GComponent();
                    mFixedRoot.displayObject.gameObject.name = "FixedRoot";
                    mFixedRoot.displayObject.gameObject.transform.position = new Vector3(0, 0, -1500);
                    mFixedRoot.sortingOrder = 1500;
                    GRoot.inst.AddChild(mFixedRoot);
                }
                return mNormalRoot;
            }
        }


        /// <summary>
        /// 固定层 
        /// </summary>
        private GComponent mFixedRoot;
        public GComponent FixedRoot
        {
            get
            {
                if (mFixedRoot == null)
                {
                    mFixedRoot = new GComponent();
                    mFixedRoot.displayObject.gameObject.name = "FixedRoot";
                    mFixedRoot.displayObject.gameObject.transform.position = new Vector3(0, 0, -1500);
                    mFixedRoot.sortingOrder = 1500;
                    GRoot.inst.AddChild(mFixedRoot);
                }
                return mFixedRoot;
            }
        }


        /// <summary>
        /// 全屏弹窗层
        /// </summary>
        private GComponent mPopupRoot;
        public GComponent PopupRoot
        {
            get
            {
                if (mPopupRoot == null)
                {
                    mPopupRoot = new GComponent();
                    mPopupRoot.displayObject.gameObject.name = "PopupRoot";
                    mPopupRoot.displayObject.gameObject.transform.position = new Vector3(0, 0, -3000);
                    mPopupRoot.sortingOrder = 3000;
                    GRoot.inst.AddChild(mPopupRoot);
                }
                return mPopupRoot;
            }
        }

        public void Dispose()
        {
            DestroyImmediate(_instance, true);
            _instance = null;
        }

        void OnDestroy()
        {
            GRoot.inst.RemoveChildren();
        }
    }
}