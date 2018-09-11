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
//  * 文件名：MusicManager.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */


using System.Collections;
using System.IO;
using BestHTTP;

namespace BlankFramework
{

    using System;
    using System.Collections.Generic;
    using UnityEngine;


    /// <summary>
    /// 声音管理器
    /// </summary>
    public class MusicManager : BaseManager
    {
        private static MusicManager _instance;
        public static MusicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AppBridgeLink.Instance.GetManager<MusicManager>(ManagerName.MUSIC);
                }
                return _instance;
            }
        }


        /// <summary>
        /// 音源
        /// </summary>
        private AudioSource m_audioSource = null;


        private AudioListener m_audioListener;

        ///// <summary>
        ///// 标记介绍音乐是否为暂停
        ///// </summary>
        //private bool m_audioSourceIntroduceIsPause = false;
        ///// <summary>
        ///// 标记背景音乐是否为暂停
        ///// </summary>
        //private bool m_audioSourceBackGroundIsPause = false;
        ///// <summary>
        ///// 标记进入或退出音乐是否为暂停
        ///// </summary>
        //private bool m_audioSourceCrossFadeIsPause = false;

        /// <summary>
        /// 按钮音效
        /// </summary>
        //private AudioClip m_buttonAudioClip;
        /// <summary>
        /// 声音 片段池
        /// </summary>
        private Dictionary<string, AudioClip> m_soundAudioClips;
        /// <summary>
        /// 音源组件池
        /// </summary>
        private Dictionary<string, AudioSource> m_audioSourcess;
        /// <summary>
        /// 音源组件池
        /// </summary>
        private Dictionary<string, BlankAction> m_audioSourcesCallBackUnityActions;


        /// <summary>
        /// 移除 音源组件池
        /// </summary>
        private List<string> m_removeAudioSources;

        /// <summary>
        /// 移除 音源组件池
        /// </summary>
        private List<string> m_removeAudioSourcesCallBackUnityActions;


        /// <summary>
        /// 初始化声音管理器
        /// </summary>
        private void Init()
        {
            m_soundAudioClips = new Dictionary<string, AudioClip>();
            m_audioSourcess = new Dictionary<string, AudioSource>();
            m_audioSourcesCallBackUnityActions = new Dictionary<string, BlankAction>();
            m_removeAudioSources = new List<string>();
            m_removeAudioSourcesCallBackUnityActions = new List<string>();
            string name = ManagerName.MUSIC;
            //查找声音管理器
            GameObject musicManager = GameObject.Find(name);
            if (musicManager != null)
            {
                DestroyImmediate(musicManager);
            }

            //没有找到则创建时间管理器对象
            musicManager = new GameObject(name) { name = name };
            musicManager.transform.SetParent(this.transform);
            //获取音源组件
            m_audioSource = musicManager.GetComponent<AudioSource>();
            if (m_audioSource == null)
            {
                //没有音源组件则添加音源组件
                m_audioSource = musicManager.AddComponent<AudioSource>();
            }
            m_audioSource.playOnAwake = false;

            m_audioListener = musicManager.AddComponent<AudioListener>();

            DontDestroyOnLoad(this);
        }

        void Awake()
        {
            Init();
        }


        /// <summary>
        /// 按钮音效组件
        /// </summary>
        /// <returns></returns>
        public AudioSource ButtonAudioSourceEffect
        {
            get
            {
                //按钮音效组件常量名称
                const string musicButtonEffectName = "musicButtonEffect";

                GameObject musicEffect = GameObject.Find(musicButtonEffectName);
                if (musicEffect == null)
                {
                    musicEffect = new GameObject(musicButtonEffectName);
                    musicEffect.name = musicButtonEffectName;
                }

                musicEffect.transform.SetParent(this.transform);

                //按钮声音音源组件

                AudioSource buttonAudioSourceEffect = musicEffect.GetComponent<AudioSource>();
                if (buttonAudioSourceEffect == null)
                {
                    buttonAudioSourceEffect = musicEffect.AddComponent<AudioSource>();
                    buttonAudioSourceEffect.loop = false;
                    buttonAudioSourceEffect.playOnAwake = false;
                }
                AudioClip buttonAudioClip = (AudioClip)Resources.Load("Audios/BtnClick", typeof(AudioClip));
                buttonAudioSourceEffect.clip = buttonAudioClip;
                return buttonAudioSourceEffect;
            }
        }

        /// <summary>
        /// 背景音乐音源组件
        /// </summary>
        public AudioSource BackGroundAudioSource
        {
            get
            {
                //背景音乐组件常量名称
                const string musicBackGroundName = "musicBackGround";

                GameObject musicBackGround = GameObject.Find(musicBackGroundName);
                if (musicBackGround == null)
                {
                    musicBackGround = new GameObject(musicBackGroundName) { name = musicBackGroundName };
                    musicBackGround.transform.SetParent(this.transform);
                }

                //音源组件

                AudioSource audioSourceBackGround = musicBackGround.GetComponent<AudioSource>();
                if (audioSourceBackGround == null)
                {
                    audioSourceBackGround = musicBackGround.AddComponent<AudioSource>();
                    audioSourceBackGround.playOnAwake = false;
                    audioSourceBackGround.loop = false;
                }

                return audioSourceBackGround;
            }
        }

        private AudioSource m_introduceAudioSource;
        /// <summary>
        /// 介绍音乐音源组件
        /// </summary>
        public AudioSource IntroduceAudioSource
        {
            get
            {
                if (m_introduceAudioSource == null)
                {
                    //介绍音乐组件常量名称
                    const string musicIntroduceName = "musicIntroduce";

                    GameObject musicIntroduce = GameObject.Find(musicIntroduceName);
                    if (musicIntroduce == null)
                    {
                        musicIntroduce = new GameObject(musicIntroduceName) { name = musicIntroduceName };
                        musicIntroduce.transform.SetParent(transform);
                    }


                    //音源组件
                    AudioSource audioSourceIntroduce = musicIntroduce.GetComponent<AudioSource>();
                    if (audioSourceIntroduce == null)
                    {
                        audioSourceIntroduce = musicIntroduce.AddComponent<AudioSource>();
                        audioSourceIntroduce.playOnAwake = false;
                        audioSourceIntroduce.loop = false;
                    }
                    m_introduceAudioSource = audioSourceIntroduce;
                    return audioSourceIntroduce;
                }
                return m_introduceAudioSource;
            }
        }

        /// <summary>
        /// 进入和退出音乐音源组件
        /// </summary>
        public AudioSource CrossFadeAudioSource
        {
            get
            {
                //进入和退出音乐组件常量名称
                const string musicCrossFadeName = "musicCrossFade";

                GameObject musicCrossFade = GameObject.Find(musicCrossFadeName);
                if (musicCrossFade == null)
                {
                    musicCrossFade = new GameObject(musicCrossFadeName) { name = musicCrossFadeName };
                    musicCrossFade.transform.SetParent(this.transform);
                }

                //音源组件

                AudioSource audioSourceCrossFade = musicCrossFade.GetComponent<AudioSource>();
                if (audioSourceCrossFade == null)
                {
                    audioSourceCrossFade = musicCrossFade.AddComponent<AudioSource>();
                    audioSourceCrossFade.playOnAwake = false;
                    audioSourceCrossFade.loop = false;
                }
                return audioSourceCrossFade;
            }
        }


        #region m_audioSourcesCallBackUnityActions CRUD

        /// <summary>
        /// 增加回调
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="callBack"></param>
        private void AddCallBack(string clipName, BlankAction callBack)
        {
            if (m_audioSourcesCallBackUnityActions.ContainsKey(clipName))
            {
                m_audioSourcesCallBackUnityActions[clipName] = callBack;
            }
            else
            {
                m_audioSourcesCallBackUnityActions.Add(clipName, callBack);
            }
        }
        /// <summary>
        /// 移除回调
        /// </summary>
        /// <param name="clipName"></param>
        private void RemoveCallBack(string clipName)
        {
            if (m_audioSourcesCallBackUnityActions.ContainsKey(clipName))
            {
                //UnityAction unityAction = m_audioSourcesCallBackUnityActions[clipName];
                m_audioSourcesCallBackUnityActions.Remove(clipName);
            }
        }

        /// <summary>
        /// 获取回调
        /// </summary>
        /// <param name="clipName">音频名称</param>
        /// <returns></returns>
        private BlankAction GetCallBack(string clipName)
        {
            if (m_audioSourcesCallBackUnityActions.ContainsKey(clipName))
            {
                return m_audioSourcesCallBackUnityActions[clipName];
            }
            return null;
        }

        #endregion



        /// <summary>
        /// 暂停声音播放
        /// </summary>
        //public void Pause()
        //{
        //    m_audioSource.Pause();
        //}
        #region m_soundAudioClips CRUD

        /// <summary>
        /// 增加一个声音片段
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="clip">声音片段</param>
        private AudioClip AddClip(string key, AudioClip clip)
        {
            if (clip != null)
            {
                if (m_soundAudioClips.ContainsKey(key))
                {
                    m_soundAudioClips[key] = clip;
                }
                else
                {
                    m_soundAudioClips.Add(key, clip);
                }
                return m_soundAudioClips[key];
            }
            else
            {
                Log.E(" 声音片段为空！！！ ");
                return null;
            }

        }

        /// <summary>
        /// 增加一个声音片段
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="clip">声音片段</param>
        private AudioClip AddClip(AudioClip clip)
        {
            if (clip != null)
            {
                return AddClip(clip.name, clip);
            }
            return null;
        }

        /// <summary>
        /// 获取一个声音片段
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>声音片段,没有找到则返回Null </returns>
        private AudioClip GetAudioClip(string key)
        {
            if (m_soundAudioClips.ContainsKey(key))
            {
                return m_soundAudioClips[key];
            }
            return null;
        }

        private void RemoveAudioClip(string key)
        {
            if (m_soundAudioClips.Count > 0 && m_soundAudioClips.ContainsKey(key))
            {
                m_soundAudioClips.Remove(key);
            }
        }

        #endregion

        #region m_audioSourcess CRUD

        ///// <summary>
        ///// 播放一个声音片段
        ///// </summary>
        ///// <param name="clip">声音片段</param>
        //public void Play(AudioClip clip)
        //{
        //    if (IsPlaySoundEffect && clip != null)
        //    {
        //        m_audioSource.PlayOneShot(clip);
        //    }
        //    //m_audioSource.PlayOneShot(clip);
        //}

        public AudioSource GetAudioSource(string clipName)
        {
            if (m_audioSourcess != null && m_audioSourcess.Count > 0 && m_audioSourcess.ContainsKey(clipName))
            {
                return m_audioSourcess[clipName];
            }
            return null;
        }
        public void RemoveAudioSource(string clipName)
        {
            if (m_audioSourcess != null && m_audioSourcess.Count > 0 && m_audioSourcess.ContainsKey(clipName))
            {
                AudioSource audioSource = m_audioSourcess[clipName];
                if (audioSource != null)
                {
                    Destroy(audioSource.gameObject);
                }
                m_audioSourcess.Remove(clipName);
            }
        }
        #endregion


        /// <summary>
        /// 扩展函数, 播放声音,只播放一次
        /// </summary>
        /// <param name="clip">播放的声音片段</param>
        public void PlayOneShotAudio(AudioClip clip)
        {
            if (clip != null)
            {
                string gameobjectName = "Oneshotaudio" + clip.name;
                GameObject oneshotaudio = GameObject.Find(gameobjectName);
                if (oneshotaudio != null)
                {
                    DestroyImmediate(oneshotaudio);
                }
                oneshotaudio = new GameObject(gameobjectName) { name = gameobjectName };
                AudioSource source = oneshotaudio.GetComponent<AudioSource>();
                if (source != null)
                {
                    DestroyImmediate(source);
                }
                source = oneshotaudio.AddComponent<AudioSource>();
                source.playOnAwake = false;
                source.loop = false;
                source.clip = clip;
                source.volume = 1;
                source.Play();
                Destroy(oneshotaudio, clip.length);
            }
        }

        /// <summary>
        /// 播放声音片段
        /// </summary>
        /// <param name="clip"></param>
        public void PlayAudioClip(AudioClip clip)
        {
            PlayAudioClip(clip, null);
        }





        /// <summary>
        /// 播放声音片段
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="callBack"></param>
        public void PlayAudioClip(AudioClip clip, BlankAction callBack)
        {
            if (clip != null)
            {
                string clipName = clip.name;
                AudioClip tempAudioClip = null;
                AudioSource audioSource;

                tempAudioClip = AddClip(clipName, clip);
                //   m_soundAudioClips[clip.name]
                if (new Func<bool>(() =>
                {
                    if (m_audioSourcess.TryGetValue(clipName, out audioSource))
                    {
                        if (audioSource == null)
                        {
                            m_audioSourcess.Remove(clipName);
                            return false;
                        }
                        return true;
                    }
                    return false;
                }).Invoke())
                {
                    m_audioSourcess[clipName].Play();
                }
                else
                {
                    GameObject go = GameObject.Find(clipName);
                    if (go == null)
                    {
                        go = new GameObject(clipName);
                        go.name = clipName;
                    }
                    audioSource = go.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        Destroy(audioSource);
                    }
                    audioSource = go.AddComponent<AudioSource>();
                    audioSource.loop = false;
                    audioSource.playOnAwake = false;
                    audioSource.pitch = 1;
                    audioSource.clip = tempAudioClip;
                    m_audioSourcess.Add(clipName, audioSource);
                    audioSource.Play();
                }

                if (callBack != null)
                {
                    AddCallBack(clipName, callBack);
                }
            }
            else
            {
                Log.E(" 声音片段为空！！！ ");
            }
        }

        public void StopAudioClip(AudioClip clip)
        {
            string clipName = clip.name;
            if (m_soundAudioClips.ContainsKey(clipName))
            {
                m_soundAudioClips.Remove(clipName);
            }

            if (m_audioSourcess.ContainsKey(clipName))
            {
                DestroyImmediate(m_audioSourcess[clipName]);
                m_audioSourcess.Remove(clipName);
            }
            RemoveCallBack(clipName);
        }

        public void StopAllAudioClip()
        {
            foreach (KeyValuePair<string, AudioSource> keyValuePair in m_audioSourcess)
            {
                DestroyImmediate(keyValuePair.Value);
            }

            m_soundAudioClips.Clear();
            m_audioSourcess.Clear();
            m_audioSourcesCallBackUnityActions.Clear();
        }


        ///// <summary>
        ///// 播放一个声音片段
        ///// </summary>
        ///// <param name="clip">声音片段</param>
        ///// <param name="position">位置</param>
        //public void Play(AudioClip clip, Vector3 position)
        //{
        //    if (IsPlaySoundEffect && clip != null)
        //    {
        //        AudioSource.PlayClipAtPoint(clip, position, GetVolumeValue());
        //    }
        //    else
        //    {
        //        Log.E(" IsPlayBackGroundSound  == false ??  Clip == null !!!!!!!");
        //    }
        //}

        /// <summary>
        /// 设置或获取 是否 播放背景音乐
        /// </summary>
        public bool IsPlayBackGroundSound
        {
            get
            {
                return true;
                //Tools.ReadBool("IsPlayBackGroundSound");
            }
            private set { Tools.WriteBool("IsPlayBackGroundSound", value); }
        }

        /// <summary>
        /// 设置或获取 是否 播放音效
        /// </summary>
        public bool IsPlaySoundEffect
        {
            get { return Tools.ReadBool("isPlaySoundEffect"); }
            set { Tools.WriteBool("isPlaySoundEffect", value); }
        }

        ///// <summary>
        ///// 设置音量大小,持久保存
        ///// </summary>
        ///// <param name="value"></param>
        //public void SetVolumeValue(float value)
        //{
        //    BlankVolumeChange.SetVolumeValue((int)value);
        //}

        ///// <summary>
        ///// 获取音量大小
        ///// </summary>
        ///// <returns></returns>
        //public float GetVolumeValue()
        //{
        //    return BlankVolumeChange.GetVolumeValue();
        //}
        #region 背景音乐
        /// <summary>
        /// 继续播放背景音乐,
        /// 只有在clip 有值的时候才会播放
        /// </summary>
        public void PlayBackGround()
        {
            if (IsPlayBackGroundSound)
            {
                BackGroundAudioSource.pitch = 1;
                //Log.I(BackGroundAudioSource.clip != null);
                //if (BackGroundAudioSource.clip != null)
                //{
                //    //取消暂停
                //    BackGroundAudioSource.pitch = 1;
                //}
                //else
                //{
                //    Log.E(" 背景音乐音频不存在！！！ 请检查 ");
                //}
            }
        }

        /// <summary>
        /// 播放背景音乐,默认使用循环
        /// </summary>
        /// <param name="clip">声音片段</param>
        public void PlayBackGround(AudioClip clip)
        {
            PlayBackGround(clip, true);
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="clip">声音片段</param>
        /// <param name="isLoop">是否循环播放</param>
        public void PlayBackGround(AudioClip clip, bool isLoop)
        {
            if (IsPlayBackGroundSound && clip != null)
            {

                BackGroundAudioSource.clip = AddClip(clip);
                BackGroundAudioSource.loop = isLoop;
                BackGroundAudioSource.Play();
            }
            else
            {
                Log.E(" IsPlayBackGroundSound  == false ??  Clip == null !!!!!!!");
            }
        }
        /// <summary>
        /// 暂停背景音乐
        /// </summary>
        public void PauseBackGround()
        {
            Log.I("暂停背景音乐");
            BackGroundAudioSource.pitch = 0;
        }

        /// <summary>
        /// 停止播放背景音乐
        /// </summary>
        public void StopBackGround()
        {
            Log.I("停止播放背景音乐");
            if (BackGroundAudioSource != null)
            {
                BackGroundAudioSource.Stop();
            }
        }
        /// <summary>
        /// 背景音乐是否正在播放
        /// </summary>
        public bool IsPlayingBackGround
        {
            get { return BackGroundAudioSource.isPlaying; }
        }

        #endregion


        #region 介绍音乐

        /// <summary>
        /// 设置介绍音
        /// </summary>
        /// <param name="clip"></param>
        public void SetIntroduceAudioClip(AudioClip clip)
        {
            if (clip != null)
            {
                IntroduceAudioSource.clip = AddClip(clip);
                IntroduceAudioSource.pitch = 0;
                IntroduceAudioSource.Play();
            }
            else
            {
                Log.E("介绍音乐 为空 ！！！！ ");
            }
        }


        /// <summary>
        /// 设置介绍音
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="isLoop"></param>
        public void SetIntroduceAudioClip(AudioClip clip, bool isLoop)
        {
            if (clip != null)
            {
                IntroduceAudioSource.clip = AddClip(clip);
                IntroduceAudioSource.pitch = 0;
                IntroduceAudioSource.loop = isLoop;
                IntroduceAudioSource.Play();
            }
            else
            {
                Log.E("介绍音乐 为空 ！！！！ ");
            }
        }

        /// <summary>
        /// 继续播放介绍音乐,
        /// 只有在clip 有值的时候才会播放
        /// </summary>
        public void PlayIntroduce()
        {
            IntroduceAudioSource.pitch = 1;
            //if (IntroduceAudioSource.clip != null)
            //{
            //    IntroduceAudioSource.pitch = 1;
            //}
            //else
            //{
            //    Log.E(" 介绍音乐音频不存在！！！ 请检查 ");
            //}
        }

        /// <summary>
        /// 播放介绍音
        /// </summary>
        /// <param name="clip"></param>
        public void PlayIntroduce(AudioClip clip)
        {
            PlayIntroduce(clip, false);
        }

        /// <summary>
        /// 播放介绍音
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="isLoop"></param>
        public void PlayIntroduce(AudioClip clip, bool isLoop)
        {

            if (clip != null)
            {
                IntroduceAudioSource.clip = AddClip(clip);
                IntroduceAudioSource.loop = isLoop;
                IntroduceAudioSource.Play();
            }
            else
            {
                Log.E("介绍音乐 为空 ！！！！ ");
            }
        }
        /// <summary>
        /// 暂停介绍音
        /// </summary>
        public void PauseIntroduce()
        {
            if (IntroduceAudioSource != null && IntroduceAudioSource.clip != null)
            {
                IntroduceAudioSource.pitch = 0;
            }
            else
            {
                Log.E("介绍音乐 为空 ！！！！ ");
            }
        }
        /// <summary>
        /// 停止介绍音
        /// </summary>
        public void StopIntroduce()
        {
            if (IntroduceAudioSource != null && IntroduceAudioSource.clip != null)
            {
                IntroduceAudioSource.Stop();
            }
            else
            {
                Log.E("介绍音乐 为空 ！！！！ ");
            }
        }

        /// <summary>
        /// 介绍音是否是停止状态
        /// </summary>
        public bool IsPauseIntroduce
        {
            get { return Mathf.Abs(IntroduceAudioSource.pitch) < 0.1f || IntroduceAudioSource.isPlaying; }
        }

        #endregion


        #region 出现和消失音乐


        /// <summary>
        /// 继续播放进入或退出音乐,
        /// 只有在clip 有值的时候才会播放
        /// </summary>
        public void PlayCrossFade()
        {
            CrossFadeAudioSource.pitch = 1;
            //if (CrossFadeAudioSource.clip != null)
            //{
            //    CrossFadeAudioSource.pitch = 1;
            //}
            //else
            //{
            //    Log.E(" 出现和消失音音频不存在！！！ 请检查 ");
            //}
        }

        /// <summary>
        /// 播放出现或消失音
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="isLoop"></param>
        public void PlayCrossFade(AudioClip clip, bool isLoop = false)
        {
            if (clip != null)
            {
                //Log.I(clip.length, " 总时间长度");
                CrossFadeAudioSource.clip = AddClip(clip);
                CrossFadeAudioSource.loop = isLoop;
                CrossFadeAudioSource.Play();
            }
            else
            {
                Log.E("出现和消失音为空 为空 ！！！！ ");
            }
        }
        /// <summary>
        /// 暂停出现或消失音
        /// </summary>
        public void PauseCrossFade()
        {
            if (CrossFadeAudioSource != null && CrossFadeAudioSource.clip != null)
            {
                CrossFadeAudioSource.pitch = 0;
            }
            else
            {
                Log.E("出现和消失音为空 为空 ！！！！ ");
            }
        }

        /// <summary>
        /// 停止出现和消失音
        /// </summary>
        public void StopCrossFade()
        {

            if (CrossFadeAudioSource != null && CrossFadeAudioSource.clip != null)
            {
                CrossFadeAudioSource.Stop();
            }
            else
            {
                Log.E("出现和消失音为空 为空 ！！！！ ");
            }
        }

        /// <summary>
        /// 出现和消失音是否是暂停状态
        /// </summary>
        public bool IsPauseCrossFade
        {
            get { return Math.Abs(CrossFadeAudioSource.pitch) < 0.1 || CrossFadeAudioSource.isPlaying; }
        }

        #endregion

        /// <summary>
        /// 暂停播放 除音效之外的所有音乐
        /// </summary>
        public void PauseAll()
        {
            PauseBackGround();
            PauseCrossFade();
            PauseIntroduce();
        }
        /// <summary>
        /// 停止播放 除音效之外的所有音乐
        /// </summary>
        public void StopAll()
        {
            StopBackGround();
            StopCrossFade();
            StopIntroduce();
        }
        /// <summary>
        /// 继续播放 除音效之外的所有音乐
        /// </summary>
        public void ContinuePlay()
        {
            Log.I("继续播放 除音效之外的所有音乐");
            PlayBackGround();
            PlayCrossFade();
            PlayIntroduce();
        }

        private void Update()
        {
            if (m_soundAudioClips != null && m_soundAudioClips.Count > 0)
            {
                foreach (KeyValuePair<string, AudioClip> soundAudioClip in m_soundAudioClips)
                {
                    AudioSource audioSource = GetAudioSource(soundAudioClip.Key);
                    if (audioSource != null)
                    {
                        if ((soundAudioClip.Value.length - audioSource.time) < 0.1f)
                        {
                            string clipName = audioSource.clip.name;
                            // 播放完了
                            BlankAction unityAction = GetCallBack(clipName);
                            if (unityAction != null)
                            {
                                try
                                {
                                    unityAction();
                                }
                                catch (Exception)
                                {
                                    throw;
                                }

                                m_removeAudioSourcesCallBackUnityActions.Add(clipName);
                                // RemoveCallBack(clipName);
                            }
                            m_removeAudioSources.Add(clipName);
                            // RemoveAudioSource(clipName);
                        }
                    }
                    else
                    {
                        m_removeAudioSources.Add(soundAudioClip.Key);
                        // RemoveAudioSource(soundAudioClip.Key);
                    }
                }
            }

            for (int i = 0; i < m_removeAudioSourcesCallBackUnityActions.Count; i++)
            {
                RemoveCallBack(m_removeAudioSourcesCallBackUnityActions[i]);
            }

            for (int i = 0; i < m_removeAudioSources.Count; i++)
            {
                RemoveAudioSource(m_removeAudioSources[i]);
            }
        }

        /// <summary>
        /// 从网络地址中去加载音频文件。必须为mp3格式
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mcallbackAction"></param>
        public void LoadAudioClip(string path, BlankActionForAudioClip mcallbackAction)
        {

            string fileName = Path.GetFileName(path);
            string extenName = Path.GetExtension(fileName);
            string typeName = extenName.Substring(1);
            if (path.Contains("http://"))
            {
                bool isExist = File.Exists(GetCachePath(fileName, typeName));
                if (isExist)
                {
                    // 加载本地
                    StartCoroutine(
                        LoadLocalAudioClip(new AudioClipModel()
                        {
                            fileName = fileName,
                            typeName = typeName,
                            callbackAction = mcallbackAction
                        }));
                }
                else
                {
                    // 加载网络
                    NetWorkManager.Instance.SendGetType(path, successModel =>
                    {
                        HTTPResponse httpResponse = successModel.Body as HTTPResponse;
                        if (httpResponse != null)
                        {
                            File.WriteAllBytes(GetCachePath(fileName, typeName), httpResponse.Data);
                        }
                        StartCoroutine(
                       LoadLocalAudioClip(new AudioClipModel()
                       {
                           fileName = fileName,
                           typeName = typeName,
                           callbackAction = mcallbackAction
                       }));
                    }, e =>
                    {
                        Debug.Log(" 音频下载失败 ");
                    });
                }
            }
        }
        private class AudioClipModel
        {
            public string fileName;
            public string typeName;
            public BlankActionForAudioClip callbackAction;
        }

        private IEnumerator LoadLocalAudioClip(AudioClipModel model)
        {
            WWW www = new WWW("file:///" + GetCachePath(model.fileName, model.typeName));
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                if (model.callbackAction != null)
                {
                    try
                    {
                        model.callbackAction(www.GetAudioClip());
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError(exception.StackTrace);
                        throw;
                    }

                }
            }
        }

        /// <summary>
        /// 获取文件缓存的路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetCachePath(string fileName, string type)
        {
            string rootPath = string.Format("{0}/caches/{1}/", Application.persistentDataPath, type);
            //  {2}.{3}
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            return string.Format("{0}{1}", rootPath, fileName);
        }

        //        void OnDestroy()
        //        {
        //            Destroy(ButtonAudioSourceEffect.gameObject);
        //            Destroy(BackGroundAudioSource.gameObject);
        //            Destroy(IntroduceAudioSource.gameObject);
        //            Destroy(CrossFadeAudioSource.gameObject);
        //        }
        //#if UNITY_EDITOR
        //        void OnApplicationQuit()
        //        {
        //            Destroy(ButtonAudioSourceEffect.gameObject);
        //            Destroy(BackGroundAudioSource.gameObject);
        //            Destroy(IntroduceAudioSource.gameObject);
        //            Destroy(CrossFadeAudioSource.gameObject);
        //        }
        //#endif
    }
}
