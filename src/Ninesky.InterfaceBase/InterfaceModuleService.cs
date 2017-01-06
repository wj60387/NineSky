/*======================================
 作者：洞庭夕照
 创建：2016.12.28
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using Ninesky.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Ninesky.InterfaceBase
{
    public interface InterfaceModuleService:InterfaceBaseService<Module>
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="enable">启用</param>
        /// <returns></returns>
        Task<IQueryable<Module>> FindListAsync(bool? enable);
        /// <summary>
        /// 查找排序列表
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns></returns>
        Task<IQueryable<ModuleOrder>> FindOrderListAsync(int moduleId);
    }
}
