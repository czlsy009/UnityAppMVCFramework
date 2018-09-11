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
//  * 文件名：MessageModel.cs
//  * 创建时间：2016年05月12日 
//  * 创建人：Blank Alian
//  */

namespace BlankFramework
{
    /// <summary>
    /// 消息模型
    /// </summary>
    public class MessageModel : IMessageModel
    {
        /// <summary>
        /// 消息模型
        /// </summary>
        /// <param name="name">消息的名称</param>
        public MessageModel(object body)
            : this(null, body, null)
        { }

        /// <summary>
        /// 消息模型
        /// </summary>
        /// <param name="name">消息的名称</param>
        public MessageModel(string name)
            : this(name, null, null)
        { }
        /// <summary>
        /// 消息模型
        /// </summary>
        /// <param name="name">消息的名称</param>
        /// <param name="body">消息的构成体</param>
        public MessageModel(string name, object body)
            : this(name, body, null)
        { }
        /// <summary>
        /// 消息模型
        /// </summary>
        /// <param name="name">消息名称</param>
        /// <param name="body">消息构成体</param>
        /// <param name="type">消息的类型</param>
        public MessageModel(string name, object body, string type)
        {
            m_name = name;
            m_body = body;
            m_type = type;
        }

        /// <summary>
        /// Get the string representation of the <c>Notification instance</c>
        /// </summary>
        /// <returns>The string representation of the <c>Notification</c> instance</returns>
        public override string ToString()
        {

            return string.Format("Notification Name: {0} Body: {1} Type： {2}", Name,
                    ((Body == null) ? "null" : Body.ToString()), (Type ?? "null"));

        }
        /// <summary>
        /// 属性：消息的名称
        /// </summary>
        public string Name
        {
            get { return m_name; }
        }
        /// <summary>
        /// 属性：消息构成体
        /// </summary>
        public object Body
        {
            get
            {
                return m_body;
            }
            set
            {
                m_body = value;
            }
        }
        /// <summary>
        /// 属性：消息的类型
        /// </summary>
        public string Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }


        private string m_name;


        private string m_type;


        private object m_body;
    }
}