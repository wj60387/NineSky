/*======================================
 ���ߣ���ͥϦ��
 ������2016.12.28
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
        /// ��ϸ
        /// </summary>
        /// <param name="id">ģ��ID</param>
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
                jsonResponse.message = "ģ�鲻����";
            }
            else
            {
                module.Enabled = enabled;
                if(_moduleService.Update(module))
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
        public IActionResult List()
        {
            return Json(_moduleService.FindList().ToList());
        }

        /// <summary>
        /// �����б�
        /// </summary>
        /// <param name="id">ģ��Id</param>
        /// <returns></returns>
        public IActionResult OrderList(int id)
        {
            return Json(_moduleService.FindOrderList(id).ToList());
        }

    }
}