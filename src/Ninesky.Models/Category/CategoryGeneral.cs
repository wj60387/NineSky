﻿/*======================================
 作者：洞庭夕照
 创建：2016.12.03
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/

using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    /// <summary>
    /// 常规栏目模型
    /// </summary>
    public class CategoryGeneral
    {
        [Key]
        public int GeneralId { get; set; }

        /// <summary>
        /// 栏目ID
        /// </summary>
        [Required]
        [Display(Name = "栏目ID")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 模块id[大于0时有效]
        /// </summary>
        [Display(Name = "内容模块")]
        public int? ModuleId { get; set; }

        /// <summary>
        /// 内容视图
        /// </summary>
        [StringLength(200)]
        [Display(Name = "内容视图")]
        public string ContentView { get; set; }

        /// <summary>
        /// 内容排序
        /// </summary>
        [Display(Name = "内容排序")]
        public int? ContentOrder { get; set; }

        /// <summary>
        /// 栏目
        /// </summary>
        public virtual Category Category { get; set; }
    }
}
