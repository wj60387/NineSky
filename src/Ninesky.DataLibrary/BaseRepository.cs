/*======================================
 作者：洞庭夕照
 创建：2016.12.05
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ninesky.InterfaceDataLibrary;

namespace Ninesky.DataLibrary
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    public class BaseRepository<T> :InterfaceBaseRepository<T> where T : class
    {
        protected DbContext _dbContext;
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 查询[不含导航属性]
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>实体</returns>
        public virtual T Find(params object[] keyValue)
        {
            return _dbContext.Set<T>().Find(keyValue);
        }

        /// <summary>
        /// 查询[不含导航属性]
        /// </summary>
        /// <param name="predicate">查询表达式</param>
        /// <returns>实体</returns>
        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().SingleOrDefault(predicate);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="includeParams">导航属性</param>
        /// <param name="predicate">查询表达式</param>
        /// <returns>实体</returns>
        public virtual T Find(string[] includeParams, Expression<Func<T, bool>> predicate)
        {
            var queryable = _dbContext.Set<T>().AsQueryable();
            if (includeParams != null)
            {
                foreach (string param in includeParams)
                {
                    queryable = queryable.Include(param);
                }
            }
            return queryable.SingleOrDefault(predicate);
        }
    }
}
