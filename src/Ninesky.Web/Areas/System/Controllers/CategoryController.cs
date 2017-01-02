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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ninesky.InterfaceBase;
using Ninesky.Models;
using Ninesky.Web.Models;

namespace Ninesky.Web.Areas.System.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// </summary>
    [Area("System")]
    public class CategoryController : Controller
    {
        private InterfaceCategoryService _categoryService;
        public CategoryController(InterfaceCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Add([FromServices]InterfaceModuleService moduleService, CategoryType? categoryType)
        {
            switch(categoryType)
            {
                case CategoryType.Page:
                    return View("Page");
                case CategoryType.Link:
                    return View("Link");
                default:
                    var modules = moduleService.FindList(true).Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
                    modules.Insert(0, new SelectListItem() { Text = "无", Value = "", Selected = true });
                    ViewData["Modules"] = modules;
                    return View(new Category() { Type = CategoryType.General, ParentId = 0, Order = 0, Target = LinkTarget._self, General = new CategoryGeneral() { View = "Index", ContentView = "Index" } });
            }
        }

        [HttpPost]
        public IActionResult Add([FromServices]InterfaceModuleService moduleService,Category category)
        {
            if(ModelState.IsValid)
            {
                //检查父栏目
                if (category.ParentId > 0)
                {
                    var parentCategory = _categoryService.Find(category.ParentId);
                    if (parentCategory == null) ModelState.AddModelError("ParentId", "父栏目不存在");
                    else if(parentCategory.Type != CategoryType.General) ModelState.AddModelError("ParentId", "父栏目不能添加子栏目");
                }
                //检查栏目类型
                switch (category.Type)
                {
                    case CategoryType.General:
                        if (category.General == null) ModelState.AddModelError("General.Module", "请选择内容模型");
                        else
                        {
                            if (category.Page != null) category.Page = null;
                            if (category.Link != null) category.Link = null;
                        }
                        break;
                    case CategoryType.Page:
                        //检查
                        if (category.Page == null) ModelState.AddModelError("General.Module", "请选择内容模型");
                        else
                        {
                            if (category.General != null) category.General = null;
                            if (category.Link != null) category.Link = null;
                        }
                        break;
                    case CategoryType.Link:
                        //检查
                        if (category.Link == null) ModelState.AddModelError("General.Module", "请选择内容模型");
                        else
                        {
                            if (category.General != null) category.General = null;
                            if (category.General != null) category.General = null;
                        }
                        break;
                }
                
                //保存到数据库
                if(ModelState.IsValid)
                {
                    if (_categoryService.Add(category) > 0) return View("AddSucceed", category);
                    else ModelState.AddModelError("General.Module", "请选择内容模型");
                }
            }
            var modules = moduleService.FindList(true).Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modules.Insert(0, new SelectListItem() { Text = "无", Value = "", Selected = true });
            ViewData["Modules"] = modules;
            return View(category);
        }

        /// <summary>
        /// 栏目首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// 父栏目树
        /// </summary>
        /// <returns></returns>
        public IActionResult ParentTree()
        {
            return Json(_categoryService.FindTree(CategoryType.General).Select(c => new zTreeNode { id = c.CategoryId, name = c.Name, pId = c.CategoryId, iconOpen="fa-open", iconClose="fa-close",iconSkin="fa fa-folder" }));
        }

        /// <summary>
        /// 栏目树
        /// </summary>
        /// <returns></returns>
        public IActionResult Tree()
        {
            List<zTreeNode> nodes;
            var categories = _categoryService.FindTree(null);
            if (categories != null)
            {
                nodes = new List<zTreeNode>(categories.Count());
                foreach(var category in categories)
                {
                    var node = new zTreeNode() { id = category.CategoryId, pId= category.ParentId, name = category.Name, url = Url.Action("Details", "Category", new { id = category.CategoryId }) };
                    switch(category.Type)
                    {
                        case CategoryType.General:
                            node.iconSkin = "fa fa-folder";
                            break;
                        case CategoryType.Page:
                            node.iconSkin = "fa fa-file";
                            break;
                        case CategoryType.Link:
                            node.iconSkin = "fa fa-link";
                            break;
                    }
                    nodes.Add(node);
                }
            }
            else nodes = new List<zTreeNode>();
            return Json(nodes);
        }
    }
}