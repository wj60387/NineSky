/*======================================
 作者：洞庭夕照
 创建：2016.12.13
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ninesky.InterfaceBase;
using Ninesky.Models;
using Ninesky.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// 添加常规栏目
        /// </summary>
        /// <param name="moduleService"></param>
        /// <param name="categoryType">栏目类型</param>
        /// <returns></returns>
        public async Task<IActionResult> Add([FromServices]InterfaceModuleService moduleService, CategoryType? categoryType)
        {
            switch(categoryType)
            {
                case CategoryType.Page:
                    return View("AddPage", new Category() { Type = CategoryType.Page, ParentId = 0, View = "Index", Target = LinkTarget._self });
                case CategoryType.Link:
                    return View("AddLink", new Category() { Type = CategoryType.Link, ParentId = 0, LinkUrl="http://", Target = LinkTarget._self });
                default:
                    var modules = await moduleService.FindListAsync(true);
                    var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
                    modeleArry.Insert(0, new SelectListItem() { Text = "无", Value = "0", Selected = true });
                    ViewData["Modules"] = modeleArry;
                    return View(new Category() { Type = CategoryType.General, ParentId = 0, View = "Index", PageSize=20, Order = 0, Target = LinkTarget._self});
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromServices]InterfaceModuleService moduleService, Category category)
        {
            if (category == null) return Content("栏目信息能不能为空");
            if (ModelState.IsValid)
            {
                category.Creater = "洞庭夕照";
                var opsResult = await _categoryService.AddAsync(category);
                if (opsResult.Succeed) return View("AddSucceed", category);
                else ModelState.AddModelError("", opsResult.Message);
            }
            switch (category.Type)
            {
                case CategoryType.Page:
                    return View("AddPage", category);
                case CategoryType.Link:
                    return View("AddLink", category);
                default:
                    var modules = await moduleService.FindListAsync(true);
                    var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
                    modeleArry.Insert(0, new SelectListItem() { Text = "无", Value = "0", Selected = true });
                    ViewData["Modules"] = modeleArry;
                    return View(category);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _categoryService.RemoveAsync(id));
        }

        /// <summary>
        /// 详细(修改)
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public async Task<IActionResult> Modify([FromServices]InterfaceModuleService moduleService, int id)
        {

            var category = await _categoryService.FindAsync(id);
            if (category == null) return Content("栏目信息能不能为空");
            switch (category.Type)
            {
                case CategoryType.Page:
                    return View("AddPage", category);
                case CategoryType.Link:
                    return View("AddLink", category);
                default:
                    var modules = await moduleService.FindListAsync(true);
                    var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
                    modeleArry.Insert(0, new SelectListItem() { Text = "无", Value = "0", Selected = true });
                    ViewData["Modules"] = modeleArry;
                    return View(category);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="moduleService"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Modify([FromServices]InterfaceModuleService moduleService, Category category)
        {
            //模型验证是否通过
            if (ModelState.IsValid)
            {
                var oResult = await _categoryService.UpdateAsync(category);
                if (oResult.Succeed) return View("UpdateSucceed", category);
                else ModelState.AddModelError("", oResult.Message);
            }
            var modules = await moduleService.FindListAsync(true);
            var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modeleArry.Insert(0, new SelectListItem() { Text = "无", Value = "0", Selected = true });
            ViewData["Modules"] = modeleArry;
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
        public async Task<IActionResult> ParentTree()
        {
            var categories = await _categoryService.FindTreeAsync(CategoryType.General);
            return Json(categories.Select(c => new zTreeNode { id = c.CategoryId, name = c.Name, pId = c.ParentId, iconSkin="fa fa-folder" }));
        }

        /// <summary>
        /// 栏目树
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Tree()
        {
            List<zTreeNode> nodes;
            var categories = await _categoryService.FindTreeAsync(null);
            if (categories != null)
            {
                nodes = new List<zTreeNode>(categories.Count());
                foreach(var category in categories)
                {
                    var node = new zTreeNode() { id = category.CategoryId, pId= category.ParentId, name = category.Name, url = Url.Action("Modify", "Category", new { id = category.CategoryId }) };
                    switch(category.Type)
                    {
                        case CategoryType.General:
                            node.iconSkin = "fa fa-folder";
                            node.iconOpen = "fa fa-folder-open";
                            node.iconClose = "fa fa-folder";
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