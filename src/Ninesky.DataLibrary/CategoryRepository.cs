/*======================================
 作者：洞庭夕照
 创建：2016.12.13
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
using Ninesky.Models;
using Microsoft.EntityFrameworkCore;

namespace Ninesky.DataLibrary
{
    /// <summary>
    /// 栏目仓储
    /// </summary>
    public class CategoryRepository:BaseRepository<Category>
    {
        public CategoryRepository(DbContext dbContext) : base(dbContext)
        { }

        /// <summary>
        /// 查找栏目[包含导航属性]
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>栏目</returns>
        public override Category Find(Expression<Func<Category, bool>> predicate)
        {
            return _dbContext.Set<Category>().Include(c => c.General).Include(c => c.Page).Include(c => c.Link).SingleOrDefault(predicate);
        }
    }
}
