/*======================================
 作者：洞庭夕照
 创建：2016.12.28
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using Microsoft.AspNetCore.Mvc;
using Ninesky.InterfaceBase;
using Ninesky.Web.Models;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Details(int id)
        {
            return View(await _moduleService.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Enable (int id, bool enabled)
        {
            JsonResponse jsonResponse = new JsonResponse();
            var module = await _moduleService.FindAsync(id);
            if(module == null)
            {
                jsonResponse.succeed = false;
                jsonResponse.message = "模块不存在";
            }
            else
            {
                module.Enabled = enabled;
                var oResult = await _moduleService.UpdateAsync(module);
                if(oResult.Succeed)
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
        public async Task<IActionResult> List()
        {
            return Json((await _moduleService.FindListAsync()).ToList());
        }
    }
}