/*======================================
 ���ߣ���ͥϦ��
 ������2016.12.28
 ��վ��www.ninesky.cn
       mzwhj.cnblogs.com
 ���룺git.oschina.net/ninesky/Ninesky
 �汾��v1.0.0.0
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
        /// ��ϸ
        /// </summary>
        /// <param name="id">ģ��ID</param>
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
                jsonResponse.message = "ģ�鲻����";
            }
            else
            {
                module.Enabled = enabled;
                var oResult = await _moduleService.UpdateAsync(module);
                if(oResult.Succeed)
                {
                    jsonResponse.succeed = true;
                    jsonResponse.message = "ģ����" + (enabled ? "����" : "����");
                }
                else
                {
                    jsonResponse.succeed = false;
                    jsonResponse.message = "��������ʧ��";
                }
            }
            return Json(jsonResponse);
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// ģ���б�
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> List()
        {
            return Json((await _moduleService.FindListAsync()).ToList());
        }
    }
}