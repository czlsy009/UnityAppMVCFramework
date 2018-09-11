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
//  * 文件名：Delegates.cs
//  * 创建时间：2016年11月22日 
//  * 创建人：Blank Alian
//  */

using UnityEngine;

namespace BlankFramework
{
    public delegate void BlankAction();
    public delegate void BlankActionForObject(object obj);
    public delegate void BlankActionForLong(long result);
    public delegate void BlankActionForByte(byte[] result);
    public delegate void BlankActionForString(string result);
    public delegate void BlankActionForTexture(Texture texture);
    public delegate void BlankActionForTexture2D(Texture2D texture2D);
    public delegate void BlankActionForTinyJsonNode(TinyJson.Node jsonNode);
    public delegate void BlankActionForTransform(Transform transform);
    public delegate void BlankActionForStringTinyJsonNode(string result, TinyJson.Node jsonNode);
    public delegate void BlankActionForAudioClip(AudioClip audioClip);

    public delegate void BlankAction<in T>(T t);
    public delegate void BlankAction<in T1, in T2>(T1 t1, T2 t2);
    public delegate void BlankAction<in T1, in T2, in T3>(T1 t1, T2 t2, T3 t3);


    public delegate TResult BlankFunc<in T, TResult>(T t1, TResult tResult);
    public delegate TResult BlankFunc<in T1, in T2, TResult>(T1 t1, T2 t2, TResult tResult);
    public delegate TResult BlankFunc<in T1, in T2, in T3, TResult>(T1 t1, T2 t2, T3 t3, TResult tResult);
}