/*======================================
 作者：洞庭夕照
 创建：2017.3.3
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
    /// 常规栏目
    /// </summary>
    public class CategoryGeneralService : BaseService<CategoryGeneral>,InterfaceCategoryGeneralService
    {
        public CategoryGeneralService(DbContext dbContext) : base(dbContext)
        { }
    }
}
