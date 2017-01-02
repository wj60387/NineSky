/*======================================
 作者：洞庭夕照
 创建：2016.12.28
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ninesky.InterfaceBase;
using Ninesky.Models;
using Microsoft.EntityFrameworkCore;

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
        public IQueryable<Module> FindList(bool? enable)
        {
            if (enable == null) return FindList();
            else return FindList(0, m => m.Enabled == enable);
        }
        /// <summary>
        /// 查找排序列表
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns></returns>
        public IQueryable<ModuleOrder> FindOrderList(int moduleId)
        {
            return _dbContext.Set<ModuleOrder>().Where(mo => mo.ModuleId == moduleId);
        }
    }
}
