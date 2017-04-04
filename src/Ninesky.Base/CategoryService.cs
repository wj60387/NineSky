/*======================================
 作者：洞庭夕照
 创建：2016.12.05
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 ======================================
 2017.3.13：解决删除栏目会同时删除模块问题
 =====================================*/
using Microsoft.EntityFrameworkCore;
using Ninesky.InterfaceBase;
using Ninesky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ninesky.Base
{
    /// <summary>
    /// 栏目服务类
    /// </summary>
    public class CategoryService : BaseService<Category>, InterfaceCategoryService
    {
        public CategoryService(DbContext dbContext) : base(dbContext)
        {
        }

        public OperationResult CanParent(Category parent, Category category)
        {
            OperationResult oResult = new OperationResult() { Succeed = true };
            if (parent.ParentId == 0) return oResult;
            else
            {
                if (parent.Type != CategoryType.General)
                {
                    oResult.Succeed = false;
                    oResult.Message = "父栏目不是常规栏目。";
                }
                else if (parent.ParentPath.IndexOf(category.ParentPath, 0) == 0)
                {
                    oResult.Succeed = false;
                    oResult.Message = "父栏目不能是其本身或子栏目。";
                }
            }
            return oResult;
        }


        public override async Task<OperationResult> AddAsync(Category entity, bool isSave = true)
        {
            //设置默认值
            entity.ChildNum = 0;
            OperationResult opsResult = new OperationResult() { Succeed = true };
            //检查父栏目
            if (entity.ParentId > 0)
            {
                var parentCategory = await FindAsync(entity.ParentId);
                if (parentCategory == null)
                {
                    opsResult.Succeed = false;
                    opsResult.Message = "父栏目不存在";
                }
                else if (parentCategory.Type != CategoryType.General)
                {
                    opsResult.Succeed = false;
                    opsResult.Message = "父栏目不是常规栏目";
                }
                else
                {
                    entity.ParentPath = parentCategory.ParentPath + "," + parentCategory.CategoryId;
                    entity.Depth = parentCategory.Depth + 1;
                    parentCategory.ChildNum++;
                    await UpdateAsync(parentCategory, false);
                }
            }
            else
            {
                entity.ParentPath = "0";
                entity.Depth = 0;
            }

            if (opsResult.Succeed) opsResult = await base.AddAsync(entity);

            return opsResult;
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
                    if (categoryArray != null)
                    {
                        //添加栏目ID到
                        idArray.AddRange(categoryArray.Select(c => c.CategoryId));
                        foreach (var parentPath in categoryArray.Select(c => c.ParentPath))
                        {
                            var parentIdArray = parentPath.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parentIdArray != null)
                            {
                                int parseId = 0;
                                foreach (var parentId in parentIdArray)
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

        /// <summary>
        /// 删除子栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public async Task<OperationResult> RemoveAsync(int id)
        {
            OperationResult opsResult = new OperationResult();
            Category category = await FindAsync(id);
            if (category == null) return new OperationResult() { Succeed = false, Message = "栏目不存在" };
            if (category.Type == CategoryType.General)
            {
                if (category.ChildNum > 0) return new OperationResult() { Succeed = false, Message = "请先删除子栏目" };
                //此处应检查是否有内容
            }
            int parentId = category.ParentId;
            opsResult.Succeed = await RemoveAsync(category);
            if (opsResult.Succeed)
            {
                //父栏目中的子栏目数目减1
                if(parentId >0)
                {
                    var parent = await FindAsync(parentId);
                    if(parent != null)
                    {
                        parent.ChildNum--;
                        await base.UpdateAsync(parent);
                    }
                }
                opsResult.Message = "删除栏目成功";
            }
            return opsResult;
        }

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="entity">栏目实体</param>
        /// <param name="isSave">立即保存</param>
        /// <returns></returns>
        public override async Task<OperationResult> UpdateAsync(Category category, bool isSave = true)
        {
            var oResult = new OperationResult() { Succeed = true, Message = "更新成功栏目" };
            var originalCategory = await FindAsync(category.CategoryId);
            //栏目是否存在
            if (originalCategory == null) return new OperationResult() { Succeed = false, Message = "栏目不存在，请确认栏目【" + category.Name + "】是否已被删除？" };
            //栏目类型是否更改
            else if (category.Type != originalCategory.Type) return new OperationResult() { Succeed = false, Message = "禁止修改栏目类型" };
            //父栏目是否更改
            else if (category.ParentId != originalCategory.ParentId)
            {
                //查找新父栏目
                var parentCategory = await FindAsync(category.ParentId);
                //新父栏目不存在
                if (parentCategory == null) return new OperationResult() { Succeed = false, Message = "父栏目不存在" };
                else
                {
                    //是否可以作为当前栏目的父栏目
                    var canParentResult = CanParent(parentCategory, originalCategory);
                    if (canParentResult.Succeed)
                    {
                        if (category.ParentId == 0)
                        {
                            originalCategory.ParentId = category.ParentId;
                            originalCategory.ParentPath = "0";
                            originalCategory.Depth = 0;
                        }
                        else
                        {
                            //更改原来父栏目的子栏目数
                            var originalParent = await FindAsync(originalCategory.ParentId);
                            if(originalParent !=null)
                            {
                                originalParent.ChildNum--;
                                await base.UpdateAsync(originalParent, false);
                            }
                            //更改栏目的父栏目ID，路径及深度
                            originalCategory.ParentId = category.ParentId;
                            originalCategory.ParentPath = parentCategory.ParentPath + "," + parentCategory.CategoryId;
                            originalCategory.Depth = parentCategory.Depth + 1;
                            

                        }
                        //此处应考虑此栏目如果有子栏目，则需要更改Depth。
                    }
                    else return canParentResult;
                }

                switch (category.Type)
                {
                    //常规栏目
                    case CategoryType.General:
                        break;
                    case CategoryType.Page:
                        break;
                    case CategoryType.Link:
                        break;
                }
            }
            originalCategory.Name = category.Name;
            originalCategory.Order = category.Order;
            originalCategory.Target = category.Target;
            originalCategory.View = category.View;
            originalCategory.Description = category.Description;
            oResult = await base.UpdateAsync(originalCategory);
            return oResult;
        }
    }
}
