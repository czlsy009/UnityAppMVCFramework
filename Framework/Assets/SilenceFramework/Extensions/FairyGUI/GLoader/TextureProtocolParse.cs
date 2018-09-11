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
//  * 文件名：TextureAgreementParse.cs
//  * 创建时间：2016年12月22日 
//  * 创建人：Blank Alian
//  */

using System.Text.RegularExpressions;

namespace BlankFramework
{
    /// <summary>
    /// 程序的图像加载协议
    /// // [imgplaceholder://][http://192.168.1.8:8080/videos/mp4.mp4][http://192.168.1.8:6666/musicimages/40aba8773912b31b2effcffd8718367adbb4e17d.jpg][ui://nwadc9a5g5hq1]
    /// // [协议头][实际数据地址][占位图的地址][备用占位图地址]
    /// // [videoplaceholder://][http://192.168.1.8:8080/videos/mp4.mp4][http://192.168.1.8:6666/musicimages/40aba8773912b31b2effcffd8718367adbb4e17d.jpg][ui://nwadc9a5g5hq1]
    /// // [协议头][实际数据地址][占位图的地址][备用占位图地址]
    /// 
    /// </summary>
    public class TextureProtocolParse
    {

        /// <summary>
        /// 图片占位图的协议头
        /// </summary>
        public const string IMG_PLACE_HOLDER = "imgplaceholder://";

        /// <summary>
        /// 视频占位图的协议头
        /// </summary>
        public const string VIDEO_PLACE_HOLDER = "videoplaceholder://";


        /// <summary>
        /// 协议解析的正则表达式
        /// [videoplaceholder://][http://192.168.1.8:8080/videos/mp4.mp4][http://192.168.1.8:6666/musicimages/40aba8773912b31b2effcffd8718367adbb4e17d.jpg][ui://nwadc9a5g5hq1]
        /// (\[(\S+://)\])(\[(\S+)\])(\[(\S+){0,}\])(\[(\S+)\])
        /// 结果：
        /// 
        /// </summary>
        private const string Pattern = @"(\[(\S+?://\S*?)\])";

        /// <summary>
        /// 解析一个协议字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TextureLoaderProtocolModel Parse(string input)
        {
            MatchCollection matchCollection = Regex.Matches(input, Pattern);
            int count = matchCollection.Count;
            bool isMatch = count > 2;
            TextureLoaderProtocolModel textureLoaderProtocolModel = new TextureLoaderProtocolModel();
            if (isMatch)
            {
                // 找到协议
                textureLoaderProtocolModel.IsProtocol = true;
                // 获取协议头
                string protocolTitle = matchCollection[0].Groups[2].Value;

                // 真实数据
                string realData = matchCollection[1].Groups[2].Value;
                // 占位图地址
                string placeholderData = matchCollection[2].Groups[2].Value;
                // 备用占位图
                string backupData = matchCollection[3].Groups[2].Value;

                textureLoaderProtocolModel.ProtocolTitle = protocolTitle;
                textureLoaderProtocolModel.RealData = realData;
                textureLoaderProtocolModel.PlaceholderData = placeholderData;
                textureLoaderProtocolModel.BackupData = backupData;
            }
            else
            {
                textureLoaderProtocolModel.IsProtocol = false;
                // 未找到协议==》直接加载
            }
            return textureLoaderProtocolModel;
        }

        /// <summary>
        /// 构建一个协议体
        /// </summary>
        /// <param name="protocolTitleName">协议名称 名称不能包含 <strong>://</strong>> </param>
        /// <param name="realData">真实数据地址</param>
        /// <param name="placeholderData">占位数据地址</param>
        /// <param name="backupData">备用数据地址</param>
        /// <returns></returns>
        public static string Structure(string protocolTitleName, string realData, string placeholderData, string backupData)
        {
            if (protocolTitleName.EndsWith("://"))
            {
                protocolTitleName = protocolTitleName.Replace("://", string.Empty);
            }

            return string.Format("[{0}://][{1}][{2}][{3}]", protocolTitleName, realData, placeholderData, backupData);
        }
    }


    /// <summary>
    /// 贴图加载协议模型
    /// </summary>
    public class TextureLoaderProtocolModel
    {
        /// <summary>
        /// 标记是否是协议
        /// </summary>
        public bool IsProtocol;
        /// <summary>
        /// 协议头
        /// </summary>
        public string ProtocolTitle;
        /// <summary>
        /// 真实数据
        /// </summary>
        public string RealData;
        /// <summary>
        /// 占位图的数据
        /// </summary>
        public string PlaceholderData;
        /// <summary>
        /// 备用的数据地址
        /// </summary>
        public string BackupData;

    }
}