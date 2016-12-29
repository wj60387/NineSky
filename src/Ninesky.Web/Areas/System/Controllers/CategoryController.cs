/*======================================
 ���ߣ���ͥϦ��
 ������2016.12.13
 ��վ��www.ninesky.cn
       mzwhj.cnblogs.com
 ���룺git.oschina.net/ninesky/Ninesky
 �汾��v1.0.0.0
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
    /// ��Ŀ������
    /// </summary>
    [Area("System")]
    public class CategoryController : Controller
    {
        private InterfaceCategoryService _categoryService;
        public CategoryController(InterfaceCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Add([FromServices]InterfaceModuleService moduleService)
        {
            var modules = moduleService.FindList().Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modules.Insert(0, new SelectListItem() { Text = "��", Value = "", Selected = true });
            ViewData["Modules"] = modules;
            return View(new Category() { Type = CategoryType.General, ParentId=0, Order=0, Target= LinkTarget._self, General= new CategoryGeneral() { View="Index"} });
        }

        [HttpPost]
        public IActionResult Add([FromServices]InterfaceModuleService moduleService,Category category)
        {
            var modules = moduleService.FindList().Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modules.Insert(0, new SelectListItem() { Text = "��", Value = "", Selected = true });
            ViewData["Modules"] = modules;
            return View(category);
        }

        /// <summary>
        /// ��Ŀ��ҳ
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// ����Ŀ��
        /// </summary>
        /// <returns></returns>
        public IActionResult ParentTree()
        {
            return Json(_categoryService.FindTree(CategoryType.General).Select(c => new zTreeNode { id = c.CategoryId, name = c.Name, pId = c.CategoryId }));
        }

        /// <summary>
        /// ��Ŀ��
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
                    nodes.Add(node);
                }
            }
            else nodes = new List<zTreeNode>();
            return Json(nodes);
        }
    }
}