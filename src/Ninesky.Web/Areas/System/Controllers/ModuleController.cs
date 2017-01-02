/*======================================
 作者：洞庭夕照
 创建：2016.12.28
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
using Ninesky.Web.Models;
using Ninesky.InterfaceBase;

namespace Ninesky.Web.Areas.System.Controllers
{
    [Area("System")]
    public class ModuleController : Controller
    {
        private InterfaceModuleService _moduleService;

        public ModuleController(InterfaceModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        /// <summary>
        /// 详细
        /// </summary>
        /// <param name="id">模块ID</param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            return View(_moduleService.Find(id));
        }

        [HttpPost]
        public IActionResult Enable (int id, bool enabled)
        {
            JsonResponse jsonResponse = new JsonResponse();
            var module = _moduleService.Find(id);
            if(module == null)
            {
                jsonResponse.succeed = false;
                jsonResponse.message = "模块不存在";
            }
            else
            {
                module.Enabled = enabled;
                if(_moduleService.Update(module))
                {
                    jsonResponse.succeed = true;
                    jsonResponse.message = "模块已" + (enabled ? "启用" : "禁用");
                }
                else
                {
                    jsonResponse.succeed = false;
                    jsonResponse.message = "保存数据失败";
                }
            }
            return Json(jsonResponse);
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 模块列表
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            return Json(_moduleService.FindList().ToList());
        }

        /// <summary>
        /// 排序列表
        /// </summary>
        /// <param name="id">模块Id</param>
        /// <returns></returns>
        public IActionResult OrderList(int id)
        {
            return Json(_moduleService.FindOrderList(id).ToList());
        }

    }
}