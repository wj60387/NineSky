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

namespace Ninesky.Web.Areas.System.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// </summary>
    [Area("System")]
    public class CategoryController : Controller
    {
        private NineskyDbContext _dbContext;
        private InterfaceCategoryService _categoryService;
        public CategoryController(InterfaceCategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View("22");
        }
    }
}