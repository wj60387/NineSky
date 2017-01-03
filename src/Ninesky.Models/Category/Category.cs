/*======================================
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
        [Display(Name = "栏目名称")]
        public string Name { get; set; }

        /// <summary>
        /// 栏目视图
        /// </summary>
        [Required]
        [StringLength(200)]
        [Display(Name = "栏目视图")]
        public string View { get; set; }

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
        /// 父栏目栏目路径[从根栏目到父栏目—0,1]
        /// </summary>
        public string ParentPath { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <remarks>
        /// 数字越小越靠前
        /// </remarks>
        [Required]
        [Display(Name = "栏目排序")]
        public int Order { get; set; }

        /// <summary>
        /// 打开目标
        /// </summary>
        [Required]
        [Display(Name = "打开目标")]
        public LinkTarget Target { get; set; }

        /// <summary>
        /// 栏目说明
        /// </summary>
        [DataType(DataType.MultilineText)]
        [StringLength(1000)]
        [Display(Name = "栏目说明")]
        public string Description { get; set; }

        /// <summary>
        /// 常规栏目
        /// </summary>
        public CategoryGeneral General { get; set; }

        /// <summary>
        /// 单页栏目
        /// </summary>
        public CategoryPage Page { get; set; }

        /// <summary>
        /// 链接栏目
        /// </summary>
        public CategoryLink Link { get; set; }
    }
}
