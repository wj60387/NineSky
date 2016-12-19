/*======================================
 作者：洞庭夕照
 创建：2016.12.19
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System.Collections.Generic;

namespace Ninesky.Models
{
    /// <summary>
    /// 分页类型
    /// </summary>
    public class Paging<T> where T:class
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 页码[序号从1开始]
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 记录
        /// </summary>
        public List<T> Entities { get; set; }
        public Paging()
        {
            PageIndex = 1;
            PageSize = 20;
        }
    }
}
