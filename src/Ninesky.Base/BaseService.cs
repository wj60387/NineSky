/*======================================
 作者：洞庭夕照
 创建：2016.12.19
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 修订：
 添加异步实现：2017.01.05
 =====================================*/
using Microsoft.EntityFrameworkCore;
using Ninesky.InterfaceBase;
using Ninesky.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>添加的记录数[isSave=true时有效]</returns>
        public virtual int Add(T entity, bool isSave = true)
        {
            _dbContext.Set<T>().Add(entity);
            if (isSave) return _dbContext.SaveChanges();
            else return 0;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>添加的记录数[isSave=true时有效]</returns>
        public virtual async Task<int> AddAsync(T entity, bool isSave = true)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            if (isSave) return await _dbContext.SaveChangesAsync();
            else return 0;
        }

        /// <summary>
        /// 添加[批量]
        /// </summary>
        /// <param name="entities">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>添加的记录数[isSave=true时有效]</returns>
        public virtual int AddRange(T[] entities, bool isSave = true)
        {
            _dbContext.Set<T>().AddRange(entities);
            if (isSave) return _dbContext.SaveChanges();
            else return 0;
        }

        /// <summary>
        /// 添加[批量]
        /// </summary>
        /// <param name="entities">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>添加的记录数[isSave=true时有效]</returns>
        public virtual async Task<int> AddRangeAsync(T[] entities, bool isSave = true)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            if (isSave) return await _dbContext.SaveChangesAsync();
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
        /// 查询记录数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>记录数</returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().CountAsync(predicate);
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>是否存在</returns>
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Any(predicate);
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>是否存在</returns>
        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
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

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(int Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues">主键</param>
        /// <returns></returns>
        public virtual T Find(object[] keyValues)
        {
            return _dbContext.Set<T>().Find(keyValues);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues">主键</param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(object[] keyValues)
        {
            return await _dbContext.Set<T>().FindAsync(keyValues);
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().SingleOrDefault(predicate);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> FindList()
        {
            return _dbContext.Set<T>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns>实体列表</returns>
        public virtual async Task<IQueryable<T>> FindListAsync()
        {
            IQueryable<T> result = _dbContext.Set<T>();
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体列表</returns>
        public virtual IQueryable<T> FindList(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体列表</returns>
        public virtual async Task<IQueryable<T>> FindListAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(FindList(predicate));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="number">返回记录数</param>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体列表</returns>
        public virtual IQueryable<T> FindList(int number, Expression<Func<T, bool>> predicate)
        {
            var entityList = _dbContext.Set<T>().Where(predicate);
            if (number > 0) return entityList.Take(number);
            else return entityList;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="number">返回记录数</param>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体列表</returns>
        public virtual async Task<IQueryable<T>> FindListAsync(int number, Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(FindList(number, predicate));
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="number">显示数量[小于等于0-不启用]</param>
        /// <typeparam name="TKey">排序字段</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序</param>
        /// <param name="isAsc">正序</param>
        /// <returns></returns>
        public virtual IQueryable<T> FindList<TKey>(int number, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool isAsc)
        {
            var entityList = _dbContext.Set<T>().Where(predicate);
            if (isAsc) entityList = entityList.OrderBy(keySelector);
            else entityList.OrderByDescending(keySelector);
            if (number > 0) return entityList.Take(number);
            else return entityList;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="number">显示数量[小于等于0-不启用]</param>
        /// <typeparam name="TKey">排序字段</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序</param>
        /// <param name="isAsc">正序</param>
        /// <returns></returns>
        public virtual async Task<IQueryable<T>> FindListAsync<TKey>(int number, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool isAsc)
        {
            var entityList = _dbContext.Set<T>().Where(predicate);
            if (isAsc) entityList = entityList.OrderBy(keySelector);
            else entityList.OrderByDescending(keySelector);
            if (number > 0) entityList = entityList.Take(number);
            return await Task.FromResult(entityList);
        }

        /// <summary>
        /// 查询[分页]
        /// </summary>
        /// <typeparam name="TKey">排序属性</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="paging">分页数据</param>
        /// <returns></returns>
        public virtual Paging<T> FindList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool isAsc, Paging<T> paging)
        {
            var entityList = _dbContext.Set<T>().Where(predicate);
            paging.Total = entityList.Count();
            if (isAsc) entityList = entityList.OrderBy(keySelector);
            else entityList.OrderByDescending(keySelector);
            paging.Entities = entityList.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            return paging;
        }

        /// <summary>
        /// 查询[分页]
        /// </summary>
        /// <typeparam name="TKey">排序属性</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="paging">分页数据</param>
        /// <returns></returns>
        public virtual async Task<Paging<T>> FindListAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool isAsc, Paging<T> paging)
        {
            var entityList = _dbContext.Set<T>().Where(predicate);
            paging.Total = await entityList.CountAsync();
            if (isAsc) entityList = entityList.OrderBy(keySelector);
            else entityList.OrderByDescending(keySelector);
            paging.Entities = await entityList.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();
            return paging;
        }

        /// <summary>
        /// 查询[分页]
        /// </summary>
        /// <typeparam name="TKey">排序属性</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public virtual Paging<T> FindList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool isAsc, int pageIndex, int pageSize)
        {
            Paging<T> paging = new Paging<T> { PageIndex = pageIndex, PageSize = pageSize };
            return FindList(predicate, keySelector, isAsc, paging);
        }

        /// <summary>
        /// 查询[分页]
        /// </summary>
        /// <typeparam name="TKey">排序属性</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public virtual async Task<Paging<T>> FindListAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool isAsc, int pageIndex, int pageSize)
        {
            Paging<T> paging = new Paging<T> { PageIndex = pageIndex, PageSize = pageSize };
            return await FindListAsync(predicate, keySelector, isAsc, paging);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>是否删除成功[isSave=true时有效]</returns>
        public virtual bool Remove(T entity, bool isSave = true)
        {
            _dbContext.Set<T>().Remove(entity);
            if (isSave) return _dbContext.SaveChanges() > 0;
            else return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>是否删除成功[isSave=true时有效]</returns>
        public virtual async Task<bool> RemoveAsync(T entity, bool isSave = true)
        {
            _dbContext.Set<T>().Remove(entity);
            if (isSave) return await _dbContext.SaveChangesAsync() > 0;
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

        /// <summary>
        /// 删除[批量]
        /// </summary>
        /// <param name="entities">实体数组</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>成功删除的记录数[isSave=true时有效]</returns>
        public virtual async Task<int> RemoveRangeAsync(T[] entities, bool isSave = true)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            if (isSave) return await _dbContext.SaveChangesAsync();
            else return 0;
        }
        
        /// <summary>
        ///  保存数据
        /// </summary>
        /// <returns>更改的记录数</returns>
        public virtual int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        ///  保存数据
        /// </summary>
        /// <returns>更改的记录数</returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
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
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>[isSave=true时有效]\n
        /// OperationResult.Succeed操作是否成功。
        /// </returns>
        public virtual async Task<OperationResult> UpdateAsync(T entity, bool isSave = true)
        {
            var oResult = new OperationResult();
            _dbContext.Set<T>().Update(entity);
            if (isSave) oResult.Succeed = await _dbContext.SaveChangesAsync() > 0;
            return oResult;
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

        /// <summary>
        /// 更新[批量]
        /// </summary>
        /// <param name="entities">实体数组</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>更新成功的记录数[isSave=true时有效]</returns>
        public virtual async Task<int> UpdateRangeAsync(T[] entities, bool isSave = true)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            if (isSave) return await _dbContext.SaveChangesAsync();
            else return 0;
        }
    }
}
