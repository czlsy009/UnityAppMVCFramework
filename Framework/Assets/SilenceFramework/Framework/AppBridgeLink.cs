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
//  * 文件名：AppBridgeLink.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */

namespace BlankFramework
{
    /// <summary>
    /// 核心框架桥梁
    /// </summary>
    public class AppBridgeLink : BridgeLink
    {
        private static AppBridgeLink _instance;
        /// <summary>
        /// 单例对象
        /// </summary>
        public static AppBridgeLink Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppBridgeLink();
                }
                return _instance;
            }
        }

        public AppBridgeLink():base()
        {
            
        }

        /// <summary>
        /// 重写初始化框架函数
        /// </summary>
        protected override void InitFramework()
        {
            base.InitFramework();
            RegisterCommand(NotificationConst.START_UP,typeof(StartUpCommand));
        }

        /// <summary>
        /// 启动框架
        /// </summary>
        public void StartUp()
        {
            SendMessageCommand(NotificationConst.START_UP);
            RemoveMultiCommand(NotificationConst.START_UP);
        }
    }
}