/*======================================
 作者：洞庭夕照
 创建：2016.12.21
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System.Collections.Generic;

namespace Ninesky.Models
{
    /// <summary>
    /// 程序集注入项目
    /// </summary>
    public class AssemblyItem
    {
        /// <summary>
        ///  服务的程序集名称[不含后缀]
        /// </summary>
        public string ServiceAssembly { get; set; }
        /// <summary>
        /// 实现程序集名称[含后缀.dll]
        /// </summary>
        public string ImplementationAssembly { get; set; }

        /// <summary>
        /// 注入服务集合
        /// </summary>
        public List<ServiceItem> DICollections { get; set; }
    }
}
