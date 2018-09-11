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
//  * 文件名：NetWorkReslutKeyConst.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */

using UnityEngine;

namespace BlankFramework
{
    /// <summary>
    /// 网络请求通用返回 KEY 常量
    /// </summary>
    public class NetWorkReslutKeyConst
    {
        #region GET  获取数据


        #region 程序激活界面

        /// <summary>
        /// 激活码
        /// </summary>
        public const string KEYID_SET = "keyid";

        #endregion
        /// <summary>
        /// 用户ID
        /// </summary>
        public const string USER_ID_GET = "userid";

        /// <summary>
        /// 书籍ID
        /// </summary>
        public const string BOOK_ID_GET = "bookid";
        /// <summary>
        /// 返回码
        /// </summary>
        public const string CODE_GET = "code";
        /// <summary>
        /// 消息
        /// </summary>
        public const string MESSAGE_GET = "msg";
        /// <summary>
        /// 附加数据
        /// </summary>
        public const string DATA_GET = "data";

        ///// <summary>
        ///// 游戏昵称
        ///// </summary>
        //public const string GAME_NICK_GET = "gamenick";
        ///// <summary>
        ///// 游戏帐号
        ///// </summary>
        //public const string GAME_ACCOUNT_GET = "gameaccount";
        ///// <summary>
        ///// 上期右侧奖品图片
        ///// </summary>
        //public const string LAST_AWARFS_IMAGE_GET = "lastawarduri";
        ///// <summary>
        ///// 上期右侧奖品介绍图片
        ///// </summary>
        //public const string LAST_AWARFS_INTRODUCE_IMG_GET = "lastawardintroduce";


        //#region Follow
        ///// <summary>
        ///// 二维码图片地址
        ///// </summary>
        //public const string QR_CODE_IMAGE_GET = "officialaccountsqrcode";
        ///// <summary>
        ///// 公众号
        ///// </summary>
        //public const string OFFICI_ALACCOUNTS_GET = "officialaccounts";
        ///// <summary>
        ///// APP介绍
        ///// </summary>
        //public const string APP_INTRODUCE_GET = "appintroduce";
        //#endregion

        #endregion


        #region SET 发送数据

        /// <summary>
        /// 书籍ID
        /// </summary>
        public const string BOOK_ID_SET = "bookid";
        /// <summary>
        /// 用户ID
        /// </summary>
        public static string USER_ID_SET = SystemInfo.deviceUniqueIdentifier;





        ///// <summary>
        ///// 题库版本号
        ///// </summary>
        //public const string QUESTION_VERSION_SET = "questionversion";
        ///// <summary>
        ///// 游戏类型当有多个游戏时区分
        ///// </summary>
        //public const string GAME_TYPE_SET = "gametype";
        ///// <summary>
        ///// 游戏规则本地版本号
        ///// </summary>
        //public const string GAME_RULE_VERSION_SET = "gamerulesversion";
        ///// <summary>
        ///// 游戏昵称
        ///// </summary>
        //public const string NICK_NAME_SET = "gamenickname";
        ///// <summary>
        ///// 游戏昵称
        ///// </summary>
        //public const string GAME_NICK_SET = "gamenick";
        ///// <summary>
        ///// 游戏帐号
        ///// </summary>
        //public const string GAME_ACCOUNT_SET = "gameaccount";

        #endregion



    }
}