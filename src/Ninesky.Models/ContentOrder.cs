/*======================================
 作者：洞庭夕照
 创建：2016.12.27
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 ======================================*/
using System.ComponentModel.DataAnnotations;


namespace Ninesky.Models
{
    /// <summary>
    /// 模块排序类型
    /// </summary>
    public enum ContentOrder
    {
        [Display(Name = "ID降序")]
        IdAsc,
        [Display(Name = "ID升序")]
        IdDesc,
        [Display(Name = "更新时间降序")]
        UpdatedAsc,
        [Display(Name = "更新时间升序")]
        UpdatedDesc,
        [Display(Name = "点击数降序")]
        HitsAsc,
        [Display(Name = "点击数升序")]
        HitsDesc,
        [Display(Name = "评论数降序")]
        CommentsAsc,
        [Display(Name = "评论数升序")]
        CommentsDesc
    }
}
