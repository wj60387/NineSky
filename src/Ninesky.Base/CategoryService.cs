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
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="Id">栏目ID</param>
        /// <returns></returns>
        public override Category Find(int Id)
        {
            return _dbContext.Set<Category>().Include("General").Include("Page").Include("Link").SingleOrDefault(c => c.CategoryId == Id);
        }

        public override async Task<Category> FindAsync(int Id)
        {
            return await _dbContext.Set<Category>().Include("General").Include("Page").Include("Link").FirstOrDefaultAsync(c => c.CategoryId == Id);
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
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public async Task<OperationResult> UpdateAsync(InterfaceModuleService moduleService, Category category, bool isSave = true)
        {
            var oResult = new OperationResult() { Succeed = true, Message = "更新成功栏目" };
            var originalCategory = await FindAsync(category.CategoryId);
            //修改的栏目是否存在
            if (originalCategory == null)
            {
                oResult.Succeed = false;
                oResult.Message = "栏目不存在，请确认栏目【" + category.Name + "】是否已被删除？";
            }
            else
            {
                //父栏目是否更改
                if (category.ParentId != originalCategory.ParentId)
                {
                    var parentCategory = await FindAsync(category.ParentId);
                    if(parentCategory ==null)
                    {
                        oResult.Succeed = false;
                        oResult.Message = "父栏目不存在";
                    }
                    var canParentResult = CanParent(parentCategory, originalCategory);
                    if (canParentResult.Succeed)
                    {
                        if (category.ParentId == 0)
                        {
                            originalCategory.ParentId = category.ParentId;
                            originalCategory.ParentPath = "0";
                        }
                        else
                        {
                            originalCategory.ParentId = category.ParentId;
                            originalCategory.ParentPath = parentCategory.ParentPath + "," + parentCategory.CategoryId;
                        }
                    }
                    else
                    {
                        oResult.Succeed = false;
                        oResult.Message = canParentResult.Message;
                    }
                }
                //栏目类型是否更改
                if (oResult.Succeed && category.Type != originalCategory.Type)
                {
                    originalCategory.Type = category.Type;
                    //原栏目类型是否常规栏目
                    if (originalCategory.Type == CategoryType.General)
                    {
                        //栏目是否设置了内容
                        if (originalCategory.General.ModuleId > 0)
                        {
                            var moduleId = (await FindAsync((int)originalCategory.General.ModuleId)).General.ModuleId;
                            var controller = (await moduleService.FindAsync((int)moduleId)).Controller;
                            switch (controller)
                            {
                                case "Article":
                                    //此栏目是否有内容
                                    break;
                            }
                            //***此处位置是否恰当
                            await moduleService.RemoveAsync(new Module { ModuleId = (int)originalCategory.General.ModuleId }, false);
                        }
                    }
                }
                switch (category.Type)
                {
                    case CategoryType.General:
                        if (category.General == null)
                        {
                            oResult.Succeed = false;
                            oResult.Message = "请填写常规栏目内容。";
                        }
                        else
                        {
                            if (category.General.ModuleId > 0)
                            {
                                if (string.IsNullOrEmpty(category.General.ContentView))
                                {
                                    oResult.Succeed = false;
                                    oResult.Message = "请填写栏目视图。";
                                }
                                else if (category.General.ContentOrder == null)
                                {
                                    oResult.Succeed = false;
                                    oResult.Message = "请选择内容排序方式";
                                }
                            }
                            else
                            {
                                if (originalCategory.General == null) originalCategory.General = category.General;
                                else
                                {
                                    originalCategory.General.ContentOrder = category.General.ContentOrder;
                                    originalCategory.General.ContentView = category.General.ContentView;
                                    originalCategory.General.CategoryId = originalCategory.CategoryId;
                                    originalCategory.General.ModuleId = category.General.ModuleId;
                                }
                                if (originalCategory.Page != null) originalCategory.Page = null;
                                if (originalCategory.Link != null) originalCategory.Link = null;
                            }
                        }
                        break;
                    case CategoryType.Page:
                        //检查
                        if (category.Page == null)
                        {
                            oResult.Succeed = false;
                            oResult.Message = "请填写单页栏目内容";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(category.Page.Content))
                            {
                                oResult.Succeed = false;
                                oResult.Message = "请输入单页栏目内容";
                            }
                            else
                            {
                                if (originalCategory.Page == null) originalCategory.Page = category.Page;
                                else
                                {
                                    originalCategory.Page.Content = category.Page.Content;
                                    
                                }
                                if (originalCategory.General != null) originalCategory.General = null;
                                if (originalCategory.Link != null) originalCategory.Link = null;
                            }
                        }
                        break;
                    case CategoryType.Link:
                        //检查
                        if (category.Link == null)
                        {
                            oResult.Succeed = false;
                            oResult.Message = "请填写连接栏目内容";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(category.Link.Url))
                            {
                                oResult.Succeed = false;
                                oResult.Message = "请选择输入链接地址";
                            }
                            else
                            {
                                if (originalCategory.Link == null) originalCategory.Link = category.Link;
                                else
                                {
                                    originalCategory.Link.Url = category.Link.Url;
                                }
                                if (category.General != null) category.General = null;
                                if (category.General != null) category.General = null;
                            }
                        }
                        break;
                }
            }
            if (oResult.Succeed)
            {
                originalCategory.Name = category.Name;
                originalCategory.Order = category.Order;
                originalCategory.Target = category.Target;
                originalCategory.View = category.View;
                originalCategory.Description = category.Description;
                oResult = await UpdateAsync(originalCategory);
            }
            return oResult;
        }
    }
}
