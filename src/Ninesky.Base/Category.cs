/*======================================
 作者：洞庭夕照
 创建：2016.12.03
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Base
{
    /// <summary>
    /// 栏目模型
    /// </summary>
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        [Required]
        [StringLength(50)]
        
        public string Name { get; set; }

        /// <summary>
        /// 栏目类型
        /// </summary>
        [Required]
        [Display(Name= "栏目类型")]
        public CategoryType Type { get; set; }

        /// <summary>
        /// 上级栏目ID
        /// </summary>
        /// <remarks>
        /// 0-表示本栏目是根栏目，无上级栏目
        /// </remarks>
        [Required]
        [Display(Name = "上级栏目")]
        public int ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <remarks>
        /// 数字越小越靠前
        /// </remarks>
        [Required]
        [Display(Name = "排序")]
        public int Order { get; set; }

        /// <summary>
        /// 打开目标
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "打开目标")]
        public string Target { get; set; }

        /// <summary>
        /// 栏目说明
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "栏目说明")]
        public string Description { get; set; }

        /// <summary>
        /// 常规栏目
        /// </summary>
        public virtual CategoryGeneral General { get; set; }

        /// <summary>
        /// 单页栏目
        /// </summary>
        public virtual CategoryPage Page { get; set; }

        /// <summary>
        /// 链接栏目
        /// </summary>
        public virtual CategoryLink Link { get; set; }
    }
}
