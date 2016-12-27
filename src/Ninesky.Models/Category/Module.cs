/*======================================
 作者：洞庭夕照
 创建：2016.12.27
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models.Category
{
    /// <summary>
    /// 模块模型
    /// </summary>
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "模块名称")]
        public string Name { get; set; }

        /// <summary>
        /// 模块控制器
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "模块控制器")]
        public string Controller { get; set; }

        /// <summary>
        /// 模块说明
        /// </summary>
        [DataType(DataType.MultilineText)]
        [StringLength(1000)]
        [Display(Name = "模块说明")]
        public string Description { get; set; }
    }
}
