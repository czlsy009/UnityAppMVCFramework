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
//  * 文件名：GeoGraphicalPositionExtension.cs
//  * 创建时间：2016年05月30日 
//  * 创建人：Blank Alian
//  */

using System.Collections;
using BlankFramework;
using UnityEngine;

namespace Assets.BlankFramework.Extensions.SystemComponent
{
    /// <summary>
    /// 地理位置 扩展
    /// </summary>
    public class GeoGraphicalPositionExtension : MonoBehaviour
    {
        /// <summary>
        /// GEO 位置状态
        /// </summary>
        enum GeoState
        {
            /// <summary>
            /// 准备
            /// </summary>
            Ready,
            /// <summary>
            ///初始化
            /// </summary>
            Init,
            /// <summary>
            /// 不可用
            /// </summary>
            Enable,
            /// <summary>
            /// 打开中
            /// </summary>
            Opening,
            /// <summary>
            /// 打开失败
            /// </summary>
            OpenError,
            /// <summary>
            /// 获取成功
            /// </summary>
            Success
        }

        private static readonly object Obj = new object();
        private static GeoGraphicalPositionExtension _instance;
        public static GeoGraphicalPositionExtension Instance
        {
            get
            {
                lock (Obj)
                {
                    if (_instance == null)
                    {
                        GameObject geoGameObject = GameObject.Find("GeoLocation");
                        if (geoGameObject == null)
                        {
                            geoGameObject = new GameObject("GeoLocation");
                            geoGameObject.name = "GeoLocation";
                        }

                        GeoGraphicalPositionExtension geoGraphicalPositionExtension =
                            geoGameObject.GetComponent<GeoGraphicalPositionExtension>();
                        if (geoGraphicalPositionExtension != null)
                        {
                            UnityEngine.Object.Destroy(geoGraphicalPositionExtension);
                        }
                        geoGraphicalPositionExtension = geoGameObject.AddComponent<GeoGraphicalPositionExtension>();
                        _instance = geoGraphicalPositionExtension;
                        UnityEngine.Object.DontDestroyOnLoad(geoGameObject);
                    }
                    return _instance;
                }
            }
        }
        /// <summary>
        /// 地理位置信息数据
        /// </summary>
        private string m_location = 00.0000f + "/" + 00.000001f;
        /// <summary>
        /// Gps 状态
        /// </summary>
        private GeoState m_geoState = GeoState.Ready;
        /// <summary>
        /// 超时
        /// </summary>
        private float m_timeOut = 0;
        /// <summary>
        /// 标记是否获取过Gps 信息 
        /// </summary>
        private bool m_isGetLocationValue;
        /// <summary>
        /// 标记是否获取过Gps 信息 
        /// </summary>
        public bool IsGetLocationValue
        {
            get { return m_isGetLocationValue; }
        }

        /// <summary>
        /// 获取地理位置信息
        /// </summary>
        /// <param name="locationCallBack">信息回调 ,包含GPS  坐标</param>
        public void GetGeoLocation(BlankActionForString locationCallBack)
        {
            StartCoroutine(WaitGeoInfo(locationCallBack));
        }
        public string LocationValue
        {
            get
            {
                if (m_geoState == GeoState.Success)
                {
                    return m_location;
                }
                else
                {
                    return m_location;
                }
            }
        }
        private IEnumerator WaitGeoInfo(BlankActionForString locationCallBack)
        {
            yield return StartCoroutine(GetState());
            m_isGetLocationValue = true;
            if (locationCallBack != null)
            {
                locationCallBack(m_location);
            }
        }

        private IEnumerator GetState()
        {
            while (true)
            {
                if (m_timeOut >= 10)
                {
                    m_timeOut = 0;
                    Log.I("GPS  获取超时 ");
                    yield break;
                } if (m_geoState == GeoState.Enable || m_geoState == GeoState.OpenError)
                {
                    Log.I("GPS  不可用");
                    yield break;
                }
                if (m_geoState == GeoState.Success)
                {
                    Log.I("GPS  获取成功");
                    yield break;
                }

                if (m_geoState != GeoState.Enable || m_geoState != GeoState.OpenError || m_geoState != GeoState.Success)
                {
                    m_timeOut += 0.1f;
                    Log.I(" 状态值：" + m_geoState.ToString());
                    Log.I("m_timeOut : =>" + m_timeOut);
                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    Log.I(" 状态值：" + m_geoState.ToString());
                    yield break;
                }
            }
        }

        private void Awake()
        {
            m_geoState = GeoState.Init;
            m_isGetLocationValue = false;
            StartCoroutine(GetLocation());
        }


        /// <summary>
        /// 获取地理位置
        /// </summary>
        /// <returns></returns>
        private IEnumerator GetLocation()
        {
            m_location = "/";
            // First, check if user has location service enabled
            if (!Input.location.isEnabledByUser)
            {
                m_geoState = GeoState.Enable;
                yield break;
            }

            // Start service before querying location
            Input.location.Start();
            if (Input.location.status == LocationServiceStatus.Initializing)
            {
                m_geoState = GeoState.Opening;
            }
            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                m_geoState = GeoState.OpenError;
                yield break;
            }
            else
            {
                m_location = Input.location.lastData.latitude + "/" + Input.location.lastData.longitude;
                m_geoState = GeoState.Success;
            }

            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
        }
    }
}