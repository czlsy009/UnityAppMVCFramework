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
//  * 文件名：NetWorkResultCodeConst.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */
namespace BlankFramework
{

    /// <summary>
    /// 网络请求返回码常量类
    /// </summary>
    public class NetWorkResultCodeConst
    {

        #region      单独接口部分

        /// <summary>
        /// 000000	请求成功 .已激活
        /// </summary>
        public const string CODE_000000 = "000000";
        /// <summary>
        /// 000001 未激活用户
        /// </summary>
        public const string CODE_000001 = "000001";
        /// <summary>
        /// 100401	暂无活动
        /// </summary>
        public const string CODE_100401 = "100401";
        /// <summary>
        /// 100402	暂无排名，但在data中返回排行榜
        /// </summary>
        public const string CODE_100402 = "100402";
        /// <summary>
        /// 100403	排名分数提交失败
        /// </summary>
        public const string CODE_100403 = "100403";
        /// <summary>
        /// 100404	账号被封禁
        /// </summary>
        public const string CODE_100404 = "100404";
        /// <summary>
        /// 100909	代码出现了未知的异常，在data里返回
        /// </summary>
        public const string CODE_100909 = "100909";

        #endregion

        #region 通用接口部分

        /// <summary>
        /// 100300	用户信息校验失败，再次校验
        /// </summary>
        public const string CODE_100300 = "100300";
        /// <summary>
        /// 100301	账号异常（出现多条类似账号）
        /// </summary>
        public const string CODE_100301 = "100301";
        /// <summary>
        /// 100302	注销失败
        /// </summary>
        public const string CODE_100302 = "100302";
        /// <summary>
        /// 100200	当前版本已经是最新
        /// </summary>
        public const string CODE_100200 = "100200";
        /// <summary>
        /// 100203	异常的版本号（版本号格式不对）
        /// </summary>
        public const string CODE_100203 = "100203";
        /// <summary>
        /// 100204	书籍数据异常，数据库内没有这个id的书籍
        /// </summary>
        public const string CODE_100204 = "100204";
        /// <summary>
        /// 100205	请求参数无效，接收到的参数在data里返回
        /// </summary>
        public const string CODE_100205 = "100205";
        /// <summary>
        /// 100206	错误的数据（同一设备同一书籍有多条数据）
        /// </summary>
        public const string CODE_100206 = "100206";
        /// <summary>
        /// 100207	该用户已激活
        /// </summary>
        public const string CODE_100207 = "100207";
        /// <summary>
        /// 100208	数据库更新异常，错误的数据
        /// </summary>
        public const string CODE_100208 = "100208";
        /// <summary>
        /// 100209	激活码激活次数达到上限
        /// </summary>
        public const string CODE_100209 = "100209";
        /// <summary>
        /// 100210	错误的数据（数据库中没有这个用户，先调用login再激活）
        /// </summary>
        public const string CODE_100210 = "100210";
        /// <summary>
        /// 100908	代码出现了未知的异常，异常信息在data里返回
        /// </summary>
        public const string CODE_100908 = "100908";
        /// <summary>
        /// 游戏帐号已存在
        /// </summary>
        public const string CODE_100226 = "100226";
        /// <summary>
        /// 游戏帐号不存在
        /// </summary>
        public const string CODE_100227 = "100227";
        /// <summary>
        /// 修改昵称失败
        /// </summary>
        public const string CODE_100228 = "100228";


        #endregion
        #region 答题游戏

        /// <summary>
        /// 100224	题库为空
        /// </summary>
        public const string CODE_100224 = "100224";
        #endregion
        #region 系统
        /// <summary>
        /// 登录失败
        /// </summary>
        public const string CODE_100223 = "100223";
        /// <summary>
        /// 验证码已过期
        /// </summary>
        public const string CODE_100215 = "100215";
        /// <summary>
        /// 验证码校验失败
        /// </summary>
        public const string CODE_100216 = "100216";
        /// <summary>
        /// 新旧密码相同
        /// </summary>
        public const string CODE_100221 = "100221";
        /// <summary>
        /// 密码更新失败
        /// </summary>
        public const string CODE_100222 = "100222";
        /// <summary>
        /// 该用户不存在
        /// </summary>
        public const string CODE_100220 = "100220";
        /// <summary>
        /// 该用户不存在(登录)
        /// </summary>
        public const string CODE_100213 = "100213";
        /// <summary>
        /// 输入的密码不正确
        /// </summary>
        public const string CODE_100234 = "100234";
        /// <summary>
        /// 该用户已存在
        /// </summary>
        public const string CODE_100218 = "100218";
        /// <summary>
        /// 注册失败，请稍后再试
        /// </summary>
        public const string CODE_100219 = "100219";
        #endregion


        #region 视频
        /// <summary>
        /// 不存在的激活码
        /// </summary>
        public const string CODE_500001 = "500001";

        /// <summary>
        /// 激活码无效，已被使用
        /// </summary>
        public const string CODE_500002 = "500002";

        /// <summary>
        /// 已激活，重复激活
        /// </summary>
        public const string CODE_500003 = "500003";

        /// <summary>
        /// 激活超时，请重试
        /// </summary>
        public const string CODE_500004 = "500004";

        #endregion

        #region 图书激活
       
        /// <summary>
        /// 激活码次数耗尽
        /// </summary>
        public const string CODE_600001 = "600001";

        /// <summary>
        /// 激活失败
        /// </summary>
        public const string CODE_600002 = "600002";

        /// <summary>
        /// 激活失败
        /// </summary>
        public const string CODE_600003 = "600003";

        /// <summary>
        /// 用户已激活
        /// </summary>
        public const string CODE_600004 = "600004";

        /// <summary>
        /// 激活码不存在
        /// </summary>
        public const string CODE_600005 = "600005";

        #endregion


#region 家长锁
        /// <summary>
        /// 密码已经设置过，data里返回
        /// </summary>
        public const string CODE_700002 = "700002";
        /// <summary>
        /// 密码设置失败
        /// </summary>
        public const string CODE_700001 = "700001";
#endregion
    }
}