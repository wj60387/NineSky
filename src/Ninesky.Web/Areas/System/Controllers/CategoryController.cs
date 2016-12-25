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
using Ninesky.InterfaceBase;
using Ninesky.Models;

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

        public IActionResult Add()
        {
            return View(new Category() { Type = CategoryType.General, ParentId=0, Order=0, Target= LinkTarget._self, General= new CategoryGeneral() { View="Index"} });
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
            return Json(_categoryService.FindTree(CategoryType.General));
        }

        /// <summary>
        /// ��Ŀ��
        /// </summary>
        /// <returns></returns>
        public IActionResult Tree()
        {
            return Json(_categoryService.FindTree(null));
        }
    }
}