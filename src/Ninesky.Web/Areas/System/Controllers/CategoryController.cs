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
        /// 添加
        /// </summary>
        /// <param name="moduleService"></param>
        /// <param name="categoryType">栏目类型</param>
        /// <returns></returns>
        public async Task<IActionResult> Add([FromServices]InterfaceModuleService moduleService, CategoryType? categoryType)
        {

            var modules = await moduleService.FindListAsync(true);
            var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modeleArry.Insert(0, new SelectListItem() { Text = "无", Value = "0", Selected = true });
            ViewData["Modules"] = modeleArry;
            return View(new Category() { Type = CategoryType.General, ParentId = 0, View = "Index", Order = 0, Target = LinkTarget._self, General = new CategoryGeneral() { ContentView = "Index" } });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromServices]InterfaceModuleService moduleService, Category category)
        {
            if (ModelState.IsValid)
            {
                //检查父栏目
                if (category.ParentId > 0)
                {
                    var parentCategory = await _categoryService.FindAsync(category.ParentId);
                    if (parentCategory == null) ModelState.AddModelError("ParentId", "父栏目不存在");
                    else if (parentCategory.Type != CategoryType.General) ModelState.AddModelError("ParentId", "父栏目不能添加子栏目");
                    else category.ParentPath = parentCategory.ParentPath + "," + parentCategory.CategoryId;
                }
                else category.ParentPath = "0";
                //检查栏目类型
                switch (category.Type)
                {
                    case CategoryType.General:
                        if (category.General == null) ModelState.AddModelError("General.Type", "请填写常规栏目内容");
                        else
                        {
                            if (category.General.ModuleId > 0)
                            {
                                if (string.IsNullOrEmpty(category.General.ContentView)) ModelState.AddModelError("General.ContentView", "请填写栏目视图");
                                if (category.General.ContentOrder == null) ModelState.AddModelError("General.ContentOrder", "请选择内容排序方式");
                            }
                            else
                            {
                                if (category.Page != null) category.Page = null;
                                if (category.Link != null) category.Link = null;
                            }
                        }
                        break;
                    case CategoryType.Page:
                        //检查
                        if (category.Page == null) ModelState.AddModelError("General.Type", "请填写单页栏目内容");
                        else
                        {
                            if (string.IsNullOrEmpty(category.Page.Content)) ModelState.AddModelError("Page.Content", "请输入单页栏目内容");
                            else
                            {
                                if (category.General != null) category.General = null;
                                if (category.Link != null) category.Link = null;
                            }
                        }
                        break;
                    case CategoryType.Link:
                        //检查
                        if (category.Link == null) ModelState.AddModelError("General.Type", "请填写连接栏目内容");
                        else
                        {
                            if (string.IsNullOrEmpty(category.Link.Url)) ModelState.AddModelError("Link.Url", "请选择输入链接地址");
                            else
                            {
                                if (category.General != null) category.General = null;
                                if (category.General != null) category.General = null;
                            }
                        }
                        break;
                }

                //保存到数据库
                if (ModelState.IsValid)
                {
                    if (await _categoryService.AddAsync(category) > 0) return View("AddSucceed", category);
                    else ModelState.AddModelError("", "保存数据失败");
                }
            }
            var modules = await moduleService.FindListAsync(true);
            var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modeleArry.Insert(0, new SelectListItem() { Text = "无", Value = "0", Selected = true });
            ViewData["Modules"] = modeleArry;
            return View(category);
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
        public async Task<IActionResult> Details([FromServices]InterfaceModuleService moduleService, int id)
        {
            var modules = await moduleService.FindListAsync(true);
            var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modeleArry.Insert(0, new SelectListItem() { Text = "无", Value = "0", Selected = true });
            ViewData["Modules"] = modeleArry;
            return View(await _categoryService.FindAsync(id));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="moduleService"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Details([FromServices]InterfaceModuleService moduleService, Category category)
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
                    var node = new zTreeNode() { id = category.CategoryId, pId= category.ParentId, name = category.Name, url = Url.Action("Details", "Category", new { id = category.CategoryId }) };
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