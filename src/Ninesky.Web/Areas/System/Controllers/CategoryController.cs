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

        public IActionResult Add()
        {
            return View(new Category() { Type = CategoryType.General, ParentId=0, Order=0, Target= LinkTarget._self, General= new CategoryGeneral() { View="Index"} });
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
            return Json(_categoryService.FindTree(CategoryType.General));
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
                    nodes.Add(node);
                }
            }
            else nodes = new List<zTreeNode>();
            return Json(nodes);
        }
    }
}