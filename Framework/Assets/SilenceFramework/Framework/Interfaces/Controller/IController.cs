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
//  * 文件名：IController.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */
using System;

namespace BlankFramework
{
    /// <summary>
    /// 控制器 基接口
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// 注册控制器执行
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="commandType"></param>
        void RegisterCommand(string commandName, Type commandType);
        /// <summary>
        /// 注册视图执行器
        /// </summary>
        /// <param name="view"></param>
        /// <param name="commandNames"></param>
        void RegisterViewCommand(IView view, string[] commandNames);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="message"></param>
        void ExecuteCommand(IMessageModel message);
        /// <summary>
        /// 删除控制器执行
        /// </summary>
        /// <param name="commandName"></param>
        void RemoveCommand(string commandName);
        /// <summary>
        /// 删除视图执行
        /// </summary>
        /// <param name="view"></param>
        /// <param name="commandNames"></param>
        void RemoveViewCommand(IView view, string[] commandNames);
        /// <summary>
        /// 检查是否有该执行
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        bool HasCommand(string commandName);
    }
}