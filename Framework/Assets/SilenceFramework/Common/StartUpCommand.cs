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
//  * 文件名：StartUpCommand.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */


namespace BlankFramework
{
    /// <summary>
    /// 启动注册类
    /// </summary>
    public class StartUpCommand : ControllerCommand
    {
        public override void Execute(IMessageModel message)
        {
            //------------------------添加管理器对象-----------------------------
            AppBridgeLink.Instance.AddManager<NetWorkManager>(ManagerName.NET_WORK);
            AppBridgeLink.Instance.AddManager<MusicManager>(ManagerName.MUSIC);
            AppBridgeLink.Instance.AddManager<TimerManager>(ManagerName.TIMER);
            AppBridgeLink.Instance.AddManager<ImageCacheManager>(ManagerName.IMAGE_CACHE);
            AppBridgeLink.Instance.AddManager<NotificationManager>(ManagerName.NOTIFICATION);
            AppBridgeLink.Instance.AddManager<CoroutineManager>(ManagerName.COROUTINE);
            AppBridgeLink.Instance.AddManager<SceneManager>(ManagerName.SCENE);
            AppBridgeLink.Instance.AddManager<DataCacheManager>(ManagerName.DATACACHE);
            AppBridgeLink.Instance.AddManager<DownLoadManager>(ManagerName.DOWNLOAD);
//            AppBridgeLink.Instance.AddManager<DataStatisticsManager>(ManagerName.DATA_STATISTICS);
            AppBridgeLink.Instance.AddManager<AssetBundleManager>(ManagerName.ASSET_BUNDLE);
//            AppBridgeLink.Instance.AddManager<ImageTargetManager>(ManagerName.IMAGE_TARGET);
            AppBridgeLink.Instance.AddManager<LanguagePackageManager>(ManagerName.LANGUAGE_PACKAGE);
        }
    }
}