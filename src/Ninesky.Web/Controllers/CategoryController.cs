/*======================================
 作者：洞庭夕照
 创建：2016.12.03
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


namespace Ninesky.Web.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// </summary>
    public class CategoryController : Controller
    {

        /// <summary>
        /// 栏目服务
        /// </summary>
        private InterfaceCategoryService _categoryService;

        public CategoryController(InterfaceCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// 查看栏目
        /// </summary>
        /// <param name="id">栏目Id</param>
        /// <returns></returns>
        [Route("/Category/{id:int}")]
        public IActionResult Index(int id)
        {
            var category = _categoryService.Find(id);
            if (category == null) return View("Error", new Models.Error { Title = "错误消息", Name="栏目不存在", Description="访问ID为【"+id+"】的栏目时发生错误，该栏目不存在。" });
            switch (category.Type)
            {
                case CategoryType.General:
                    
                    return View(category.View, category);
                case CategoryType.Page:
                    
                    return View(category.View, category);
                case CategoryType.Link:

                    return Redirect(category.LinkUrl);
                default:
                    return View("Error", new Models.Error { Title = "错误消息", Name = "栏目数据错误", Description = "栏目【" + category.Name + "】的类型错误。" });

            }
        }
    }
}
