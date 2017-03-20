/*======================================
 作者：洞庭夕照
 创建：2016.12.03
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 ======================================
 2017.3.20修订，常规、单页、链接栏目合并到一个表
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
        /// 栏目类型
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "栏目类型")]
        public CategoryType Type { get; set; }

        /// <summary>
        /// 上级栏目ID
        /// </summary>
        /// <remarks>
        /// 0-表示本栏目是根栏目，无上级栏目
        /// </remarks>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "上级栏目")]
        public int ParentId { get; set; }

        /// <summary>
        /// 父栏目栏目路径[从根栏目到父栏目—0,1]
        /// </summary>
        public string ParentPath { get; set; }

        /// <summary>
        /// 深度[节点树的深度，根节点的值为0，子节点的值为该节点所在的层数]
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 子节点数
        /// </summary>
        public int ChildNum { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <remarks>
        /// 数字越小越靠前
        /// </remarks>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "栏目排序")]
        public int Order { get; set; }
        
        /// <summary>
        /// 栏目名称
        /// </summary>
        [Required(ErrorMessage ="{0}必填")]
        [StringLength(50, ErrorMessage = "{0}长度{1}个字符。")]
        [Display(Name = "栏目名称")]
        public string Name { get; set; }

        /// <summary>
        /// 栏目说明
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "栏目说明")]
        public string Description { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [StringLength(255, ErrorMessage = "{0}长度最多{1}个字符。")]
        [Display(Name = "图片地址")]
        public string PicUrl { get; set; }

        /// <summary>
        /// 搜索引擎关键字
        /// </summary>
        [StringLength(255, ErrorMessage = "{0}长度最多{1}个字符。")]
        [Display(Name = "Meta_Keywords")]
        public string Meta_Keywords { get; set; }

        /// <summary>
        /// 搜索引擎描述
        /// </summary>
        [StringLength(255, ErrorMessage = "{0}长度最多{1}个字符。")]
        [Display(Name = "Meta_Description")]
        public string Meta_Description { get; set; }

        /// <summary>
        /// 顶部菜单处显示
        /// </summary>
        [StringLength(255, ErrorMessage = "{0}长度最多{1}个字符。")]
        [Display(Name = "顶部菜单处显示")]
        public bool ShowOnMenu { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [StringLength(255, ErrorMessage = "{0}长度最多{1}个字符。")]
        [Display(Name = "创建者")]
        public string Creater { get; set; }

        /// <summary>
        /// 栏目视图
        /// </summary>
        [StringLength(255,ErrorMessage ="{0}长度{1}个字符。")]
        [Display(Name = "栏目视图")]
        public string View { get; set; }


        /// <summary>
        /// 打开方式
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "打开方式")]
        public LinkTarget Target { get; set; }


        /// <summary>
        /// 内容模型[大于0时有效]
        /// </summary>
        [Display(Name = "内容模型")]
        public int ContentModelId { get; set; }

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
        public int ContentOrder { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        [Display(Name = "每页记录数")]
        public int PageSize { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [DataType(DataType.Url)]
        [StringLength(500)]
        [Display(Name = "链接地址")]
        public string LinkUrl { get; set; }
    }


    /// <summary>
    /// 栏目类型
    /// </summary>
    public enum CategoryType
    {
        [Display(Name = "常规栏目")]
        General,
        [Display(Name = "单页栏目")]
        Page,
        [Display(Name = "链接栏目")]
        Link
    }
}
