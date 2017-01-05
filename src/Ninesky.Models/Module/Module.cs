/*======================================
 作者：洞庭夕照
 创建：2016.12.27
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ninesky.Models
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
        [Required(ErrorMessage = "必填")]
        [StringLength(50)]
        [Display(Name = "模块名称")]
        public string Name { get; set; }

        /// <summary>
        /// 模块控制器
        /// </summary>
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

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "启用")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual List<ModuleOrder> ModuleOrders { get; set; }
    }
}
