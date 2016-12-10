/*======================================
 作者：洞庭夕照
 创建：2016.12.10
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/

namespace Ninesky.Web.Models
{
    /// <summary>
    /// 错误模型
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 错误
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
