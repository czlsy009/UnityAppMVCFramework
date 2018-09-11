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
//  * 文件名：Tools.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

#pragma warning disable 0162
/// <summary>
/// 工具类
/// 提供一些常用的工具函数
/// </summary>
public static class Tools
{
    private const string Yes = "true";
    private const string No = "false";

    #region 获取设备是否是平板


#if UNITY_EDITOR
    private static bool m_isFlat=true;
#endif

    /// <summary>
    /// 获取设备是否是平板
    /// </summary>
    /// <returns></returns>
    public static bool IsFalt
    {
#if UNITY_EDITOR
        set { m_isFlat = value; }
#endif
        get
        {
#if UNITY_EDITOR
            return false;
#endif
            const string isFlatKey = "IsFlat";
            //判断是否有存储是否为Pad
            if (HasKey(isFlatKey))
            {
                return ReadBool(isFlatKey);
            }
            else
            {
                //第一次启动
                #region IsFlat

#if UNITY_IOS

                if (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadUnknown
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini3Gen
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini2Gen
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini1Gen
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadAir2
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadAir1
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad4Gen
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad3Gen
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad2Gen
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad1Gen
                    || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini3Gen)
                {
                    //平板
                     Tools.WriteBool(isFlatKey, true);
                     return true;
                }
                else if (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneUnknown
                  || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone4
                  || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone4S
                  || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone5
                  || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone5C
                  || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone5S
                  || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone6
                  || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone6Plus)
                {
                    //手机
                    Tools.WriteBool(isFlatKey, false);
                    return false;
                }else{
                    return false;
                }
#endif

#if UNITY_ANDROID
                float x = Mathf.Pow(Screen.width / Screen.dpi, 2);
                float y = Mathf.Pow(Screen.height / Screen.dpi, 2);
                float screenInches = Mathf.Sqrt(x + y);
                WriteBool(isFlatKey, screenInches > 6.5);
                return screenInches > 6.5;
#endif
                #endregion
            }
        }
    }

    #endregion

    #region 字符串 处理

    /// <summary>
    /// 返回一个object 的字符串
    /// </summary>
    /// <returns></returns>
    public static string GetObjectString()
    {
        return "object";
    }

    /// <summary>
    /// 判断是否是邮箱地址
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsEmail(string str)
    {
        return Regex.IsMatch(str, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
    }

    /// <summary>
    /// 判断是否是手机号
    /// </summary> 
    /// <returns></returns>
    public static bool IsPhoneNumber(string str)
    {
        return Regex.IsMatch(str,
            @"^[1]([3][0-9]{1}|47|45|50|51|52|53|55|56|57|58|59|70|77|78|80|81|82|85|86|87|88|89)[0-9]{8}$");
    }

    /// <summary>
    /// 判断是否是QQ号码
    /// </summary>
    /// <returns></returns>
    public static bool IsQQNumber(string str)
    {
        return Regex.IsMatch(str, @"^[1-9]\d{4,11}$");
    }

    #endregion

    /// <summary>
    /// 获取图像的地址.默认使用缓存方式加载
    /// </summary>
    /// <param name="imgurl"></param>
    /// <param name="rooturl">图像根地址</param>
    /// <param name="isForceLoadNetWork">是否强制加载网络图像</param>
    /// <returns></returns>
    public static string ConvertToImageString(string imgurl, string rooturl, bool isForceLoadNetWork = false)
    {
        if (!imgurl.StartsWith("/"))
        {
            imgurl = "/" + imgurl;
        }
        if (imgurl.IndexOf(@"\") != -1)
        {
            imgurl = imgurl.Replace(@"\", @"/").Replace(@"//", @"/");
        }
        imgurl = imgurl.Replace(@"//", @"/");


        if (isForceLoadNetWork)
        {
            return string.Format("{0}{1}?", rooturl, imgurl);
        }
        else
        {
            return string.Format("{0}{1}", rooturl, imgurl);
        }
    }

    /// <summary>
    /// 获取图像的地址.默认使用缓存方式加载
    /// </summary>
    /// <param name="imgurl"></param>
    /// <param name="rooturl">图像根地址</param>
    /// <param name="placeHolderImageUrl">加载失败显示的图片</param>
    /// <param name="isForceLoadNetWork">是否强制加载网络图像</param>
    /// <returns></returns>
    public static string ConvertPlaceholderToImageString(string imgurl, string rooturl, string placeHolderImageUrl = null, bool isForceLoadNetWork = false)
    {
        if (!imgurl.StartsWith("/"))
        {
            imgurl = "/" + imgurl;
        }
        if (imgurl.IndexOf(@"\", StringComparison.Ordinal) != -1)
        {
            imgurl = imgurl.Replace(@"\", @"/");
        }
        string returnurl;
        if (isForceLoadNetWork)
        {
            returnurl = string.Format("{0}{1}?", rooturl, imgurl);
        }
        else
        {
            returnurl = string.Format("{0}{1}", rooturl, imgurl);
        }

        if (!string.IsNullOrEmpty(placeHolderImageUrl))
        {
            returnurl = string.Format("placeholder://[{0}]{1}", placeHolderImageUrl, returnurl);
        }
        return returnurl;
    }


    /// <summary>
    /// 获取图像的地址.默认使用缓存方式加载
    /// </summary>
    /// <param name="imgurl"></param>
    /// <param name="rooturl">图像根地址</param>
    /// <param name="placeHolderVideoImageUrl">加载失败显示的图片</param>
    /// <param name="isForceLoadNetWork">是否强制加载网络图像</param>
    /// <returns></returns>
    public static string ConvertPlaceholderToVideoString(string imgurl, string rooturl, string placeHolderVideoImageUrl = null, bool isForceLoadNetWork = false)
    {
        if (!imgurl.StartsWith("/"))
        {
            imgurl = "/" + imgurl;
        }
        if (imgurl.IndexOf(@"\", StringComparison.Ordinal) != -1)
        {
            imgurl = imgurl.Replace(@"\", @"/");
        }
        string returnurl;
        if (isForceLoadNetWork)
        {
            returnurl = string.Format("{0}{1}?", rooturl, imgurl);
        }
        else
        {
            returnurl = string.Format("{0}{1}", rooturl, imgurl);
        }

        if (!string.IsNullOrEmpty(placeHolderVideoImageUrl))
        {
            returnurl = string.Format("placeholder://[{0}]{1}", placeHolderVideoImageUrl, returnurl);
        }
        return returnurl;
    }

    #region PlayerPrefs  CRUD

    /// <summary>
    /// 判断本地存储数据中是否有这个值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    /// <summary>
    /// 设置本地存储int类型
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void WriteInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 设置本地存储string类型
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void WriteString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 设置本地存储float类型
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void WriteFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 设置本地存储float类型
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void WriteBool(string key, bool value)
    {

        PlayerPrefs.SetString(key, value == true ? Yes : No);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 删除指定存储数据
    /// </summary>
    /// <param name="key"></param>
    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    /// <summary>
    /// 删除本地存储所有数据
    /// </summary>
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// 读取本地存储int类型
    /// </summary>
    /// <param name="key"></param>
    /// <returns>返回读取到的数据,没有则返回 -1 </returns>
    public static int ReadInt(string key)
    {
        return PlayerPrefs.GetInt(key, -1);
    }

    /// <summary>
    /// 读取本地存储string类型
    /// </summary>
    /// <param name="key"></param>
    /// <returns>返回读取到的数据,没有则返回 Null </returns>
    public static string ReadString(string key)
    {
        return PlayerPrefs.GetString(key, null);
    }

    /// <summary>
    /// 读取本地存储float类型
    /// </summary>
    /// <param name="key"></param>
    /// <returns>返回读取到的数据,没有读取到则返回 -1.0 </returns>>
    public static float ReadFloat(string key)
    {
        return PlayerPrefs.GetFloat(key, -1.0f);
    }

    /// <summary>
    /// 读取本地存储bool类型
    /// </summary>
    /// <param name="key"></param>
    /// <returns>返回读取到的数据,没有读取到则返回 false </returns>
    public static bool ReadBool(string key)
    {
        return bool.Parse(PlayerPrefs.GetString(key, No));
    }
    #endregion



    /// <summary>
    /// 清理内存,卸载资源
    /// </summary>
    public static void ClearMemory()
    {
        GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    #region 加密解密

    #region MD5

    /// <summary>
    /// 计算文件的MD5值，注意MD5值是小写的
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    public static string GetMd5FileValue(string filepath)
    {
        try
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("md5file() fail, error:" + ex.Message);
        }
    }


    /// <summary>
    /// 获取MD5 加密后的字符串。加密后的字符串是小写的
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <returns></returns>
    public static string ConvertToMD5String(string input)
    {
        return ConvertToMD5String(input, Encoding.UTF8);
    }

    /// <summary>
    /// 获取MD5 加密后的字符串。加密后的字符串是小写的
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <param name="encode">字符的编码</param>
    /// <returns></returns>
    public static string ConvertToMD5String(string input, Encoding encode)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(encode.GetBytes(input));
        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        }
        return sb.ToString().ToLower();
    }
    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <returns></returns>
    public static string MD5Encrypt(string input)
    {
        return MD5Encrypt(input, new UTF8Encoding());
    }

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <param name="encode">字符的编码</param>
    /// <returns></returns>
    public static string MD5Encrypt(string input, Encoding encode)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(encode.GetBytes(input));
        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        return sb.ToString();
    }

    /// <summary>
    /// 获取MD5 加密后的字符串。加密后的字符串是小写的
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <param name="encode">字符的编码</param>
    /// <returns></returns>
    public static string ToMD5For16String(string input)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sb = new StringBuilder(16);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x"));
        }
        return sb.ToString().ToLower().Substring(8, 16);
    }

    /// <summary>
    /// 获取MD5 加密后的字符串。加密后的字符串是小写的
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <param name="encode">字符的编码</param>
    /// <returns></returns>
    public static string ToMD5For16String(string input, Encoding encode)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(encode.GetBytes(input));
        StringBuilder sb = new StringBuilder(16);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x"));
        }
        return sb.ToString().ToLower().Substring(8, 16);
    }
    /// <summary>
    /// MD5对文件流加密
    /// </summary>
    /// <param name="sr"></param>
    /// <returns></returns>
    public static string MD5Encrypt(Stream stream)
    {
        MD5 md5serv = MD5CryptoServiceProvider.Create();
        byte[] buffer = md5serv.ComputeHash(stream);
        StringBuilder sb = new StringBuilder();
        foreach (byte var in buffer)
            sb.Append(var.ToString("x2"));
        return sb.ToString();
    }



    #endregion

    #region AES 加密解密

    /// <summary>
    ///  AES 加密
    /// </summary>
    /// <param name="text"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string AESEncrypt(string text, string password)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }
        byte[] toEncryptArray = Encoding.UTF8.GetBytes(text);


        RijndaelManaged rm = new RijndaelManaged
        {
            Key = Encoding.UTF8.GetBytes(password),
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };


        ICryptoTransform cTransform = rm.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }


    /// <summary>
    ///  AES 解密
    /// </summary>
    /// <param name="text"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string AESDecrypt(string text, string password)
    {
        if (string.IsNullOrEmpty(text)) return null;
        byte[] toEncryptArray = Convert.FromBase64String(text);


        RijndaelManaged rm = new RijndaelManaged
        {
            Key = Encoding.UTF8.GetBytes(password),
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };


        ICryptoTransform cTransform = rm.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);


        return Encoding.UTF8.GetString(resultArray);
    }

    #endregion


    #region Base64加密解密
    /// <summary>
    /// Base64加密
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <returns></returns>
    public static string Base64Encrypt(string input)
    {
        return Base64Encrypt(input, new UTF8Encoding());
    }

    /// <summary>
    /// Base64加密
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <param name="encode">字符编码</param>
    /// <returns></returns>
    public static string Base64Encrypt(string input, Encoding encode)
    {
        return Convert.ToBase64String(encode.GetBytes(input));
    }

    /// <summary>
    /// Base64解密
    /// </summary>
    /// <param name="input">需要解密的字符串</param>
    /// <returns></returns>
    public static string Base64Decrypt(string input)
    {
        return Base64Decrypt(input, new UTF8Encoding());
    }

    /// <summary>
    /// Base64解密
    /// </summary>
    /// <param name="input">需要解密的字符串</param>
    /// <param name="encode">字符的编码</param>
    /// <returns></returns>
    public static string Base64Decrypt(string input, Encoding encode)
    {
        return encode.GetString(Convert.FromBase64String(input));
    }
    #endregion

    #region DES加密解密



    /// <summary>
    /// DES 加密过程
    /// </summary>
    /// <param name="dataToEncrypt">待加密数据</param>
    /// <param name="desKey"></param>
    /// <returns></returns>
    public static string EncryptFile(byte[] dataToEncrypt, string desKey)
    {
        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            des.Key = ASCIIEncoding.ASCII.GetBytes(desKey); //建立加密对象的密钥和偏移量
            des.IV = ASCIIEncoding.ASCII.GetBytes(desKey);
            byte[] inputByteArray = dataToEncrypt; //把字符串放到byte数组中

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    StringBuilder ret = new StringBuilder();
                    foreach (byte b in ms.ToArray())
                    {
                        ret.AppendFormat("{0:x2}", b);
                    }
                    return ret.ToString();
                }
            }
        }
    }

    /// <summary>
    /// DES 加密过程
    /// </summary>
    /// <param name="dataToEncrypt">待加密数据</param>
    /// <param name="desKey"></param>
    /// <returns></returns>
    public static string EncryptString(string dataToEncrypt, string desKey)
    {
        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            des.Key = ASCIIEncoding.ASCII.GetBytes(desKey); //建立加密对象的密钥和偏移量
            des.IV = ASCIIEncoding.ASCII.GetBytes(desKey);
            byte[] inputByteArray = Encoding.Default.GetBytes(dataToEncrypt); //把字符串放到byte数组中

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    StringBuilder ret = new StringBuilder();
                    foreach (byte b in ms.ToArray())
                    {
                        ret.AppendFormat("{0:x2}", b);
                    }
                    return ret.ToString();
                }
            }
        }
    }






    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="data">加密数据</param>
    /// <param name="key">8位字符的密钥字符串</param>
    /// <param name="iv">8位字符的初始化向量字符串</param>
    /// <returns></returns>
    public static string DESEncrypt(string data, string key, string iv)
    {
        byte[] byKey = ASCIIEncoding.ASCII.GetBytes(key);
        byte[] byIV = ASCIIEncoding.ASCII.GetBytes(iv);

        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        int i = cryptoProvider.KeySize;
        MemoryStream ms = new MemoryStream();
        CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

        StreamWriter sw = new StreamWriter(cst);
        sw.Write(data);
        sw.Flush();
        cst.FlushFinalBlock();
        sw.Flush();
        return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
    }

    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="data">解密数据</param>
    /// <param name="key">8位字符的密钥字符串(需要和加密时相同)</param>
    /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
    /// <returns></returns>
    public static string DESDecrypt(string data, string key, string iv)
    {
        byte[] byKey = ASCIIEncoding.ASCII.GetBytes(key);
        byte[] byIV = ASCIIEncoding.ASCII.GetBytes(iv);

        byte[] byEnc;
        try
        {
            byEnc = Convert.FromBase64String(data);
        }
        catch
        {
            return null;
        }

        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        MemoryStream ms = new MemoryStream(byEnc);
        CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(cst);
        return sr.ReadToEnd();
    }
    #endregion


    #endregion

    #region Read And Write File

    public static string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }
    public static string[] ReadAllLines(string path)
    {
        return File.ReadAllLines(path);
    }
    public static byte[] ReadAllBytes(string path)
    {
        return File.ReadAllBytes(path);
    }
    public static void WriteAllText(string path, string content)
    {
        File.WriteAllText(path, content);
    }
    public static void WriteAllText(string path, byte[] bytes)
    {
        File.WriteAllBytes(path, bytes);
    }
    public static void WriteAllText(string path, string[] contents)
    {
        File.WriteAllLines(path, contents);
    }
    #endregion



    /// <summary>
    /// 重新设置Shader 
    /// </summary>
    /// <param name="obj"></param>
    public static void ResetShader(UnityEngine.Object obj)
    {
        List<Material> materialsmap = new List<Material>();
        GameObject go = obj as GameObject;
        if (go != null)
        {
            Renderer[] renderers = go.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer renderer1 in renderers)
            {
                Material[] materials = renderer1.materials;
                foreach (Material material in materials)
                {
                    materialsmap.Add(material);
                }
            }
        }
        for (int i = 0; i < materialsmap.Count; i++)
        {
            Material material = materialsmap[i];

            if (material == null)
            {
                continue;
            }
            string shadername = material.shader.name;

            Shader newShader = Shader.Find(shadername);

            if (newShader != null)
            {
                material.shader = newShader;
            }
        }

    }




    /// <summary>
    /// 汉字转换为Unicode编码
    /// </summary>
    /// <param name="str">要编码的汉字字符串</param>
    /// <returns>Unicode编码的的字符串</returns>
    public static string ToUnicode(string str)
    {

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            bool isMatch = Regex.IsMatch(str[i].ToString(), "^[\\u4E00-\u9FFF]+$");

            if (isMatch)
            {
                stringBuilder.Append(ToSingleUnicode(str[i].ToString()));
            }
            else
            {
                stringBuilder.Append(str[i].ToString());
            }
        }
        return stringBuilder.ToString();
    }

    public static string ToSingleUnicode(string str)
    {
        byte[] bts = Encoding.Unicode.GetBytes(str);
        string r = "";
        for (int i = 0; i < bts.Length; i += 2)
        {


            r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
        }
        return r;
    }

    /// <summary>
    /// 将Unicode编码转换为汉字字符串
    /// </summary>
    /// <param name="str">Unicode编码字符串</param>
    /// <returns>汉字字符串</returns>
    public static string ToGB2312(string str)
    {
        string r = "";
        MatchCollection matchCollection = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        byte[] bts = new byte[2];
        foreach (Match match in matchCollection)
        {
            bts[0] = (byte)int.Parse(match.Groups[2].Value, NumberStyles.HexNumber);
            bts[1] = (byte)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber);
            r += Encoding.Unicode.GetString(bts);
        }
        return r;
    }


    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(GameObject go, string subnode)
    {
        return Child(go.transform, subnode);
    }

    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(Transform go, string subnode)
    {
        GameObject result = null;
        int childCount = go.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform tm = go.GetChild(i);

            Log.I(tm.name, subnode);
            if (tm.name.Equals(subnode))
            {
                result = tm.gameObject;
                break;
            }
            else
            {
                result = Child(go.GetChild(i), subnode);
            }
        }
        return result;
    }

    /// <summary>
    /// 16进制的颜色值 转换为rbg 颜色值 带#号
    /// 注意  A 为 255 FF 1
    /// </summary>
    /// <param name="rgb"></param>
    /// <returns></returns>
    public static UnityEngine.Color Convert16BitToRGBColor(string rgb)
    {
        int r = Convert.ToInt32("0x" + rgb.Substring(1, 2), 16);
        int g = Convert.ToInt32("0x" + rgb.Substring(3, 2), 16);
        int b = Convert.ToInt32("0x" + rgb.Substring(5, 2), 16);
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f, 1);
    }

    /// <summary>
    /// 16进制的颜色值 转换为rbg 颜色值 带#号
    /// </summary>
    /// <param name="rgba"></param>
    /// <returns></returns>
    public static UnityEngine.Color Convert16BitToRGBAColor(string rgba)
    {
        int r = Convert.ToInt32("0x" + rgba.Substring(1, 2), 16);
        int g = Convert.ToInt32("0x" + rgba.Substring(3, 2), 16);
        int b = Convert.ToInt32("0x" + rgba.Substring(5, 2), 16);
        int a = Convert.ToInt32("0x" + rgba.Substring(7, 2), 16);
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
    }


    /// <summary>
    /// 获取是否请求成功过数据
    /// </summary>
    /// <param name="moduleName">模块名称</param>
    /// <param name="childModuleId">子模块名称标记</param>
    /// <returns>返回True=>请求成功过</returns>
    public static bool GetIsRequestedSuccess(string moduleName, string childModuleId = null)
    {

        string v = ReadString(moduleName + (string.IsNullOrEmpty(childModuleId) ? string.Empty : childModuleId));
        Log.I(v);
        DateTime dateTime;
        string dataRefreshTime = ReadString("dataRefreshTime");
        Debug.Log(" dataRefreshTime :" + dataRefreshTime);
        DateTime refreshNowDateTime;
        if (DateTime.TryParse(dataRefreshTime, out refreshNowDateTime))
        {

        }
        else
        {
            refreshNowDateTime = DateTime.Now;
        }
        //DateTime refreshNowDateTime = string.IsNullOrEmpty(dataRefreshTime)? Convert.ToDateTime(dataRefreshTime) : DateTime.Now;
        if (DateTime.TryParse(v, out dateTime))
        {
            Debug.Log(" dateTime :" + dateTime);
            if (dateTime.DayOfYear == refreshNowDateTime.DayOfYear)
            {
                if ((dateTime.Hour < refreshNowDateTime.Hour))
                {
                    // 之前获取数据时间 小时 小于  服务器要求时间 小时  ==> 执行刷新数据
                    return false;
                }
                else if (((dateTime.Hour == refreshNowDateTime.Hour)))
                {
                    // 之前获取数据时间 小时 等于  服务器要求时间 小时
                    // 判断分钟

                    if (dateTime.Minute <= refreshNowDateTime.Minute)
                    {
                        // 小于  刷新数据
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    // 之前获取数据时间 小时 大于  服务器要求时间 小时
                }
                // 当日
                return true;

            }
            else
            {
                // 非当日
                return false;
            }
        }
        return false;
    }

    /// <summary>
    /// 设置是否请求成功过的数据
    /// </summary>
    /// <param name="moduleName">模块ID</param>
    /// <param name="childModuleId">子模块ID</param>
    public static void SetIsRequestedSuccess(string moduleName, string childModuleId = null)
    {
        DateTime nowDateTime = DateTime.Now;
        WriteString(moduleName + (string.IsNullOrEmpty(childModuleId) ? string.Empty : childModuleId), nowDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
    }

}


