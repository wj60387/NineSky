/*======================================
 作者：洞庭夕照
 创建：2017.01.05
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

namespace Ninesky.Web.Components
{
    public class CategoryNavViewComponent:ViewComponent
    {
        private InterfaceCategoryService _categoryService;

        public CategoryNavViewComponent(InterfaceCategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           var categories = _categoryService.FindChildren(0);
           return View(categories);
        }
    }
}
