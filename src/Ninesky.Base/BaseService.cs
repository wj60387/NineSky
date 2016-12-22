/*======================================
 作者：洞庭夕照
 创建：2016.12.19
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
using Microsoft.EntityFrameworkCore;
using Ninesky.Models;

namespace Ninesky.Base
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseService<T>:InterfaceBaseService<T> where T:class
    {
        protected DbContext _dbContext;
        public BaseService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual int Add(T entity, bool isSave = true)
        {
            _dbContext.Set<T>().Add(entity);
            if (isSave) return _dbContext.SaveChanges();
            else return 0;
        }

        public virtual int AddRange(T[] entities, bool isSave = true)
        {
            _dbContext.Set<T>().AddRange(entities);
            if (isSave) return _dbContext.SaveChanges();
            else return 0;
        }

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>记录数</returns>
        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Count(predicate);
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>是否存在</returns>
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Count(predicate) > 0;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public virtual T Find(int Id)
        {
            return _dbContext.Set<T>().Find(Id);
        }

        public virtual T Find(object[] keyValues)
        {
            return _dbContext.Set<T>().Find(keyValues);
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().SingleOrDefault(predicate);
        }

        public virtual IQueryable<T> FindList<TKey>(int number, Expression<Func<T, bool>> predicate)
        {
            var entityList = _dbContext.Set<T>().Where(predicate);
            if (number > 0) return entityList.Take(number);
            else return entityList;
        }

        public virtual IQueryable<T> FindList<TKey>(int number, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool isAsc)
        {
            var entityList = _dbContext.Set<T>().Where(predicate);
            if (isAsc) entityList = entityList.OrderBy(keySelector);
            else entityList.OrderByDescending(keySelector);
            if (number > 0) return entityList.Take(number);
            else return entityList;
        }


        public virtual Paging<T> FindList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool isAsc, Paging<T> paging)
        {
            var entityList = _dbContext.Set<T>().Where(predicate);
            paging.Total = entityList.Count();
            if (isAsc) entityList = entityList.OrderBy(keySelector);
            else entityList.OrderByDescending(keySelector);
            paging.Entities = entityList.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            return paging;
        }

        public virtual Paging<T> FindList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool isAsc, int pageIndex, int pageSize)
        {
            Paging<T> paging = new Paging<T> { PageIndex = pageIndex, PageSize = pageSize };
            return FindList(predicate, keySelector, isAsc, paging);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>是否删除成功</returns>
        public virtual bool Remove(T entity, bool isSave = true)
        {
            _dbContext.Set<T>().Remove(entity);
            if (isSave) return _dbContext.SaveChanges() > 0;
            else return false;
        }

        /// <summary>
        /// 删除[批量]
        /// </summary>
        /// <param name="entities">实体数组</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>成功删除的记录数</returns>
        public virtual int RemoveRange(T[] entities, bool isSave = true)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            if (isSave) return _dbContext.SaveChanges();
            else return 0;
        }


        public virtual int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>是否保存成功</returns>
        public virtual bool Update(T entity, bool isSave = true)
        {
            _dbContext.Set<T>().Update(entity);
            if (isSave) return _dbContext.SaveChanges() > 0;
            else return false;
        }

        /// <summary>
        /// 更新[批量]
        /// </summary>
        /// <param name="entities">实体数组</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>更新成功的记录数</returns>
        public virtual int UpdateRange(T[] entities, bool isSave = true)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            if (isSave) return _dbContext.SaveChanges();
            else return 0;
        }
    }
}
