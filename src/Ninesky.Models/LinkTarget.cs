/*======================================
 作者：洞庭夕照
 创建：2016.12.24
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/

using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    /// <summary>
    /// 链接打开方式
    /// </summary>
    public enum LinkTarget
    {
        [Display(Name = "在新窗口中打开")]
        _blank,
        [Display(Name = "在同一框架中打开[默认]")]
        _self,
        [Display(Name = "在父框架中打开")]
        _parent,
        [Display(Name = "在窗口主体中打开")]
        _top
    }
}
