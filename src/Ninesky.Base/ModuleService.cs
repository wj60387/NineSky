/*======================================
 作者：洞庭夕照
 创建：2016.12.28
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using Microsoft.EntityFrameworkCore;
using Ninesky.InterfaceBase;
using Ninesky.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Ninesky.Base
{
    public class ModuleService:BaseService<Module>,InterfaceModuleService
    {
        public ModuleService(DbContext dbContext) : base(dbContext)
        { }


        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="enable">启用</param>
        /// <returns></returns>
        public async Task<IQueryable<Module>> FindListAsync(bool? enable)
        {
            if (enable == null) return await FindListAsync();
            else return await FindListAsync(m => m.Enabled == enable);
        }
    }
}
