/*======================================
 作者：洞庭夕照
 创建：2016.12.27
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 ======================================
 说明:
 通用:<=0-默认排序
        1-ID升序
        2-ID降序
        3-发布时间升序
        4-发布时间降序
        5-阅读次数升序
        6-阅读次数降序
 =====================================*/
using System.ComponentModel.DataAnnotations;


namespace Ninesky.Models
{
    /// <summary>
    /// 模块排序类型
    /// </summary>
    public class ModuleOrder
    {
        [Key]
        public int ModuleOrderId { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        [Required]
        [Display(Name = "模块ID")]
        public int ModuleId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required]
        [Display(Name = "值")]
        public int Order { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public virtual Module Module { get; set; }
    }
}
