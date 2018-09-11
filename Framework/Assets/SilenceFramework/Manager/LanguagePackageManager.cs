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
//  * 文件名：LanguagePackageManager.cs
//  * 创建时间：2017年02月23日 
//  * 创建人：Blank Alian
//  */

using System.Collections.Generic;
using BlankFramework.XML;
using UnityEngine;

namespace BlankFramework
{
    /// <summary>
    /// 语言包管理器
    /// </summary>
    public class LanguagePackageManager : BaseManager
    {
        private static LanguagePackageManager _instance;
        public static LanguagePackageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AppBridgeLink.Instance.GetManager<LanguagePackageManager>(ManagerName.LANGUAGE_PACKAGE);
                }
                return _instance;
            }
        }

        private Dictionary<string, string> m_languageDictionary = new Dictionary<string, string>();

        private string m_languagename;
        void Awake()
        {
            m_languageDictionary = new Dictionary<string, string>();
            string localLanguageName = Tools.ReadString("locallanguage");
            if (string.IsNullOrEmpty(localLanguageName))
            {
                // 中文
                this.m_languagename = "zh_cn";
            }
            else
            {
                // 其他语言
                this.m_languagename = localLanguageName;
            }
            Debug.Log(this.m_languagename);
        }

        void Start()
        {
            Init(false);
        }

        /// <summary>
        /// 初始化语言包
        /// </summary>
        /// <param name="isReset">是否是刷新重置语言包数据</param>
        protected virtual void Init(bool isReset)
        {
            TextAsset languageTextAsset = Resources.Load("languages/" + m_languagename) as TextAsset;

            if (languageTextAsset != null)
            {
                string languageText = languageTextAsset.text;
                Resources.UnloadAsset(languageTextAsset);

                XML.XML xml = new XML.XML(languageText);

                XMLList xmlList = xml.Elements();

                int xmlListCount = xmlList.Count;
                for (int i = 0; i < xmlListCount; i++)
                {
                    XML.XML element = xmlList[i];

                    string key = element.GetAttribute("key");
                    string value = element.text;
                    //Debug.Log(key);
                    if (isReset)
                    {
                        m_languageDictionary[key] = value;
                    }
                    else
                    {
                        m_languageDictionary.Add(key, value);
                    }
                }
            }
            else
            {
                Log.E(" Not Found languageName :" + m_languagename);
            }
        }
        /// <summary>
        /// 刷新语言包数据
        /// </summary>
        /// <param name="mlanguagename"></param>
        public void ResetLanguage(string mlanguagename)
        {
            this.m_languagename = mlanguagename;
            Init(true);
            Tools.WriteString("locallanguage", mlanguagename);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            string value = m_languageDictionary[key];

            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Not Found Key ：" + key + "  Check!!! ");
                return "";
            }
            else
            {
                return value.Replace("##", "\r\n");
            }
        }
    }
}