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
//  * 文件名：AssetBundleManager.cs
//  * 创建时间：2016年10月21日 
//  * 创建人：Blank Alian
//  */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BlankFramework
{

    public class AssetBundleManager : MonoBehaviour
    {
        private static AssetBundleManager _instance;

        public static AssetBundleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AppBridgeLink.Instance.GetManager<AssetBundleManager>(ManagerName.ASSET_BUNDLE);
                }
                return _instance;
            }
        }
        private Dictionary<string, AssetBundleManifest> m_assetBundleManifests;
        private Dictionary<string, AssetBundle> m_assetBundleManifestAssetBundles;

        private Dictionary<string, AssetBundle> m_assetBundles;
        /// <summary>
        /// Asset Bundle File Postfix
        /// Asset Bundle 文件后缀
        /// </summary>
        private const string ASSET_BUNDLE_POSTFIX = ".assetbundle";
        void Awake()
        {
            m_assetBundles = new Dictionary<string, AssetBundle>();
            m_assetBundleManifests = new Dictionary<string, AssetBundleManifest>();
            m_assetBundleManifestAssetBundles = new Dictionary<string, AssetBundle>();
        }



        public void LoadAssetBundleManifest(string manifestFileName, Action<AssetBundleManifest> callback)
        {
            AssetBundle assetBundleManifestAssetBundle;
            if (m_assetBundleManifestAssetBundles.ContainsKey(manifestFileName))
            {
                assetBundleManifestAssetBundle = m_assetBundleManifestAssetBundles[manifestFileName];
            }
            else
            {
                assetBundleManifestAssetBundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(manifestFileName));
                m_assetBundleManifestAssetBundles.Add(manifestFileName, assetBundleManifestAssetBundle);
            }


            AssetBundleManifest assetBundleManifest;
            if (m_assetBundleManifests.ContainsKey(manifestFileName))
            {
                assetBundleManifest = m_assetBundleManifests[manifestFileName];
            }
            else
            {
                assetBundleManifest =
                assetBundleManifestAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                m_assetBundleManifests.Add(manifestFileName, assetBundleManifest);
            }
            if (callback != null)
            {
                callback(assetBundleManifest);
            }
        }


        public void UnLoadAssetBundleManifest(string manifestFileName)
        {
            if (m_assetBundleManifestAssetBundles.ContainsKey(manifestFileName))
            {
                m_assetBundleManifestAssetBundles[manifestFileName].Unload(true);
                m_assetBundleManifestAssetBundles.Remove(manifestFileName);
            }
            if (m_assetBundleManifests.ContainsKey(manifestFileName))
            {
                m_assetBundleManifests.Remove(manifestFileName);
            }
        }


        /// <summary>
        /// 加载无依赖 的AB 文件
        /// </summary>
        /// <param name="assetBundleName">AB  全路径</param>
        /// <returns></returns>
        public AssetBundle LoadAssetBundle(string assetBundleName)
        {
            AssetBundle assetBundle = null;
            if (m_assetBundles.ContainsKey(assetBundleName))
            {
                // 加载过 ,直接返回
                m_assetBundles.TryGetValue(assetBundleName, out assetBundle);
            }
            if (assetBundle == null)
            {
                string url = assetBundleName;

                byte[] stream = File.ReadAllBytes(url);
                assetBundle = AssetBundle.LoadFromMemory(stream);
                m_assetBundles[assetBundleName] = assetBundle;
            }
            return assetBundle;
        }

        /// <summary>
        /// 加载无依赖 的AB 文件
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public AssetBundle LoadAssetBundle(string assetBundleName, string rootPath)
        {
            AssetBundle assetBundle = null;
            if (m_assetBundles.ContainsKey(assetBundleName))
            {
                // 加载过 ,直接返回
                m_assetBundles.TryGetValue(assetBundleName, out assetBundle);
            }
            if (assetBundle == null)
            {
                string url = rootPath + assetBundleName;

                byte[] stream = File.ReadAllBytes(url);
                assetBundle = AssetBundle.LoadFromMemory(stream);
                m_assetBundles[assetBundleName] = assetBundle;
            }
            return assetBundle;
        }

        public AssetBundle LoadAssetBundleWithAssetBundleManifest(string assetBundleName, string rootPath, AssetBundleManifest assetBundleManifest)
        {
            if (!assetBundleName.EndsWith(ASSET_BUNDLE_POSTFIX))
            {
                assetBundleName = string.Format("{0}{1}", assetBundleName,
                    ASSET_BUNDLE_POSTFIX);
            }
            AssetBundle assetBundle = null;
            if (m_assetBundles.ContainsKey(assetBundleName))
            {
                // 加载过 ,直接返回
                m_assetBundles.TryGetValue(assetBundleName, out assetBundle);

            }

            if (assetBundle == null)
            {
                // 未加载过 ,加载 
                LoadDependencies(assetBundleName, rootPath, assetBundleManifest);

                string url = rootPath + assetBundleName;

                byte[] stream = File.ReadAllBytes(url);
                assetBundle = AssetBundle.LoadFromMemory(stream);
                m_assetBundles[assetBundleName] = assetBundle;
            }
            return assetBundle;
        }
        /// <summary>
        /// 加载依赖资源
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="rootPath"></param>
        /// <param name="assetBundleManifest"></param>
        private void LoadDependencies(string assetBundleName, string rootPath, AssetBundleManifest assetBundleManifest)
        {
            if (assetBundleManifest == null)
            {
                Debug.LogError(" 加载错误 。先调用 Init 加载资源依赖列表 ");
                return;
            }
            string[] dependencies = assetBundleManifest.GetAllDependencies(assetBundleName);
            if (dependencies.Length == 0)
            {
                // 没有依赖 资源
                return;
            }

            for (int i = 0; i < dependencies.Length; i++)
            {
                LoadAssetBundleWithAssetBundleManifest(dependencies[i], rootPath, assetBundleManifest);
            }
        }
        public void UnLoadAssetBundle(string assetBundleName)
        {
            if (m_assetBundles.ContainsKey(assetBundleName))
            {
                AssetBundle assetBundle = m_assetBundles[assetBundleName];
                if (assetBundle != null)
                {
                    assetBundle.Unload(false);
                }
                m_assetBundles.Remove(assetBundleName);
            }
        }
    }
}