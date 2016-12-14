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
using Ninesky.Base;

namespace Ninesky.Web.Areas.System.Controllers
{
    /// <summary>
    /// ��Ŀ������
    /// </summary>
    [Area("System")]
    public class CategoryController : Controller
    {
        private NineskyDbContext _dbContext;
        private CategoryService _categoryService;
        public CategoryController(NineskyDbContext dbcontext)
        {
            _dbContext = dbcontext;
            _categoryService = new CategoryService(dbcontext);
        }
        public IActionResult Index()
        {
            return View(_categoryService.Find(2));
        }
    }
}