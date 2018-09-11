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
//  * 文件名：AudioSourceExtension.cs
//  * 创建时间：2016年05月31日 
//  * 创建人：Blank Alian
//  */

using UnityEngine;

/// <summary>
/// 声音音源组件扩展
/// </summary>
public static class AudioSourceExtension
{
    /// <summary>
    /// 扩展函数, 播放声音,只播放一次
    /// </summary>
    /// <param name="audioSource">对象本身</param>
    /// <param name="clip">播放的声音片段</param>
    public static void PlayOneShotAudio(this AudioSource audioSource, AudioClip clip)
    {
        if (clip != null)
        {
            string gameobjectName = "Oneshotaudio" + clip.name;
            GameObject oneshotaudio = GameObject.Find(gameobjectName);
            if (oneshotaudio != null)
            {
                Object.Destroy(oneshotaudio);
            }
            oneshotaudio = new GameObject(gameobjectName) { name = gameobjectName };
            AudioSource source = oneshotaudio.GetComponent<AudioSource>();
            if (source != null)
            {
                Object.Destroy(source);
            }
            source = oneshotaudio.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            source.clip = clip;
            source.volume = 1;
            source.Play();
            Object.Destroy(oneshotaudio, clip.length);
        }
    }
}
