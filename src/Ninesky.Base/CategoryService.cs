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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ninesky.Models;
using Ninesky.InterfaceBase;

namespace Ninesky.Base
{
    /// <summary>
    /// 栏目服务类
    /// </summary>
    public class CategoryService:BaseService<Category>,InterfaceCategoryService
    {
        public CategoryService(DbContext dbContext):base(dbContext)
        {
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="Id">栏目ID</param>
        /// <returns></returns>
        public override Category Find(int Id)
        {
            return _dbContext.Set<Category>().Include("General").Include("Page").Include("Link").SingleOrDefault(c => c.CategoryId == Id);
        }

        /// <summary>
        /// 查找子栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public IQueryable<Category> FindChildren(int id)
        {
            return FindList(0, c => c.ParentId == id, c => c.Order, true);
        }

        /// <summary>
        /// 查找子栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public async Task<IQueryable<Category>> FindChildrenAsync(int id)
        {
            return await FindListAsync(0, c => c.ParentId == id, c => c.Order, true);
        }

        /// <summary>
        /// 查找树形菜单
        /// </summary>
        /// <param name="categoryType">栏目类型，可以为空</param>
        /// <returns></returns>
        public async Task<IQueryable<Category>> FindTreeAsync(CategoryType? categoryType)
        {
            var categories = await FindListAsync();
            //根据栏目类型分类处理
            switch (categoryType)
            {
                case null:
                    break;
                case CategoryType.General:
                    categories = categories.Where(c => c.Type == categoryType);
                    break;
                    //默认-Page或Link类型
                default:
                    //Id数组-含本栏目及父栏目
                    List<int> idArray = new List<int>();
                    //查找栏目id及父栏目路径
                    var categoryArray = categories.Where(c => c.Type == categoryType).Select(c => new { CategoryId = c.CategoryId, ParentPath = c.ParentPath });
                    if(categoryArray != null)
                    {
                        //添加栏目ID到
                        idArray.AddRange(categoryArray.Select(c => c.CategoryId));
                        foreach (var parentPath in categoryArray.Select(c=>c.ParentPath))
                        {
                            var parentIdArray = parentPath.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parentIdArray != null)
                            {
                                int parseId = 0;
                                foreach(var parentId in parentIdArray)
                                {
                                    if (int.TryParse(parentId, out parseId)) idArray.Add(parseId);
                                }
                            }
                        }
                    }
                    categories = categories.Where(c => idArray.Contains(c.CategoryId));
                    break;
            }
            return categories.OrderBy(c => c.ParentPath).ThenBy(C => C.Order);
        }
    }
}
