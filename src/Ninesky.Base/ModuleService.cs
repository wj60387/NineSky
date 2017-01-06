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

        public override Module Find(int Id)
        {
            return _dbContext.Set<Module>().Include(m => m.ModuleOrders).SingleOrDefault(m => m.ModuleId == Id);
        }

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
        /// <summary>
        /// 查找排序列表
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns></returns>
        public async Task<IQueryable<ModuleOrder>> FindOrderListAsync(int moduleId)
        {
            return await Task.FromResult(_dbContext.Set<ModuleOrder>().Where(mo => mo.ModuleId == moduleId));
        }
    }
}
