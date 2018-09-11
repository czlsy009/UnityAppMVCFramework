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
//  * 文件名：UIPathTools.cs
//  * 创建时间：2016年06月04日 
//  * 创建人：Blank Alian
//  */


namespace BlankUIFrameWork
{
    /// <summary>
    /// UI路径工具类
    /// </summary>
    public class UIPathTools
    {
        #region PublicAPI

        /// <summary>
        /// 根据是否是平板  拼接 加载的UI路径
        /// </summary>
        /// <param name="uipath"></param>
        /// <returns></returns>
        public static string Combine(string uipath)
        {
            if (Tools.IsFalt)
            {
                uipath = string.Format("{0}{1}", uipath, UIFrameworkConstConfig.FLAT_SUFFIX_NAME);
            }
            return uipath;
        }

        /// <summary>
        /// 根据是否是平板  拼接 加载的UI路径
        /// </summary>
        /// <returns></returns>
        public static string CombineMainPackagePath(string packageName)
        {
            return CombineMainPackagePath(packageName, packageName);
            //        packageName = string.Format("{0}{1}", UiFrameworkConstConfig.UI_FILE_DIRECTORY, packageName);
            //        return packageName;
        }
        /// <summary>
        /// 根据是否是平板  拼接 加载的UI路径
        /// </summary>
        /// <returns></returns>
        public static string CombineMainPackagePath(string pathName, string packageName)
        {

            packageName = string.Format("{0}{1}/{2}", UIFrameworkConstConfig.UI_FILE_DIRECTORY, pathName, packageName);
            return packageName;
        }
        #endregion
    }
}
