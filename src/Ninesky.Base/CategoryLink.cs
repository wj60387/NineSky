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
    /// 链接栏目模型
    /// </summary>
    public class CategoryLink
    {
        [Key]
        public int LinkId { get; set; }

        /// <summary>
        /// 栏目ID
        /// </summary>
        [Required]
        [Display(Name = "栏目ID")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 栏目地址
        /// </summary>
        [Required]
        [DataType(DataType.Url)]
        [StringLength(500)]
        [Display(Name = "栏目地址")]
        public string Url { get; set; }
    }
}
