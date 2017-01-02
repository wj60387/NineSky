/*======================================
 作者：洞庭夕照
 创建：2017.01.01
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/

namespace Ninesky.Web.Models
{
    /// <summary>
    /// 返回Json数据类型
    /// </summary>
    public class JsonResponse
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool succeed { get; set; }

        /// <summary>
        /// 操作结果详细代码【必要时】
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 操作结果消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 操作产生的数据【必要时】
        /// </summary>
        public dynamic Data { get; set; }

        public JsonResponse()
        {
            succeed = false;
            message = "未知错误";
        }
    }
}
