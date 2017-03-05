/*======================================
 作者：洞庭夕照
 创建：2017.3.5
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using Microsoft.EntityFrameworkCore;
using Ninesky.InterfaceBase;
using Ninesky.Models;

namespace Ninesky.Base
{
    /// <summary>
    /// 单页栏目
    /// </summary>
    public class CategoryLinkService : BaseService<CategoryLink>, InterfaceCategoryLinkService
    {
        public CategoryLinkService(DbContext dbContext) : base(dbContext)
        { }
    }
}
