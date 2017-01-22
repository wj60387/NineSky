/*======================================
 作者：洞庭夕照
 创建：2016.01.13
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System.Collections.Generic;

namespace Ninesky.Models
{
    /// <summary>
    /// 方法操作结果
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Succeed { get; set; }

        /// <summary>
        /// 操作结果详细代码【必要时】
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 操作结果消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 操作产生的数据【必要时,慎用-会降低性能】
        /// </summary>
        public dynamic Data { get; set; }

        public OperationResult()
        {
            Succeed = false;
            Message = "未知错误";
        }
    }
}
