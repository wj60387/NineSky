/*======================================
 ���ߣ���ͥϦ��
 ������2016.12.13
 ��վ��www.ninesky.cn
       mzwhj.cnblogs.com
 ���룺git.oschina.net/ninesky/Ninesky
 �汾��v1.0.0.0
 =====================================*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ninesky.InterfaceBase;
using Ninesky.Models;
using Ninesky.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="moduleService"></param>
        /// <param name="categoryType">��Ŀ����</param>
        /// <returns></returns>
        public async Task<IActionResult> Add([FromServices]InterfaceModuleService moduleService, CategoryType? categoryType)
        {

            var modules = await moduleService.FindListAsync(true);
            var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modeleArry.Insert(0, new SelectListItem() { Text = "��", Value = "0", Selected = true });
            ViewData["Modules"] = modeleArry;
            return View("View1", new Category() { Type = CategoryType.General, ParentId = 0, View = "Index", Order = 0, Target = LinkTarget._self});
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromServices]InterfaceModuleService moduleService, Category category)
        {
            if (ModelState.IsValid)
            {
                var opsResult = await _categoryService.AddAsync(category);
                if (opsResult.Succeed) return View("AddSucceed", category);
                else ModelState.AddModelError("", opsResult.Message);
            }
            var modules = await moduleService.FindListAsync(true);
            var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modeleArry.Insert(0, new SelectListItem() { Text = "��", Value = "0", Selected = true });
            ViewData["Modules"] = modeleArry;
            return View("View1",category);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="id">��ĿID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _categoryService.RemoveAsync(id));
        }

        /// <summary>
        /// ��ϸ(�޸�)
        /// </summary>
        /// <param name="id">��ĿID</param>
        /// <returns></returns>
        public async Task<IActionResult> Details([FromServices]InterfaceModuleService moduleService, int id)
        {
            var modules = await moduleService.FindListAsync(true);
            var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modeleArry.Insert(0, new SelectListItem() { Text = "��", Value = "0", Selected = true });
            ViewData["Modules"] = modeleArry;
            return View(await _categoryService.FindAsync(id));
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="moduleService"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Details([FromServices]InterfaceModuleService moduleService, Category category)
        {
            //ģ����֤�Ƿ�ͨ��
            if (ModelState.IsValid)
            {
                var oResult = await _categoryService.UpdateAsync(category);
                if (oResult.Succeed) return View("UpdateSucceed", category);
                else ModelState.AddModelError("", oResult.Message);
            }
            var modules = await moduleService.FindListAsync(true);
            var modeleArry = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modeleArry.Insert(0, new SelectListItem() { Text = "��", Value = "0", Selected = true });
            ViewData["Modules"] = modeleArry;
            return View(category);
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
        public async Task<IActionResult> ParentTree()
        {
            var categories = await _categoryService.FindTreeAsync(CategoryType.General);
            return Json(categories.Select(c => new zTreeNode { id = c.CategoryId, name = c.Name, pId = c.ParentId, iconSkin="fa fa-folder" }));
        }

        /// <summary>
        /// ��Ŀ��
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Tree()
        {
            List<zTreeNode> nodes;
            var categories = await _categoryService.FindTreeAsync(null);
            if (categories != null)
            {
                nodes = new List<zTreeNode>(categories.Count());
                foreach(var category in categories)
                {
                    var node = new zTreeNode() { id = category.CategoryId, pId= category.ParentId, name = category.Name, url = Url.Action("Details", "Category", new { id = category.CategoryId }) };
                    switch(category.Type)
                    {
                        case CategoryType.General:
                            node.iconSkin = "fa fa-folder";
                            node.iconOpen = "fa fa-folder-open";
                            node.iconClose = "fa fa-folder";
                            break;
                        case CategoryType.Page:
                            node.iconSkin = "fa fa-file";
                            break;
                        case CategoryType.Link:
                            node.iconSkin = "fa fa-link";
                            break;
                    }
                    nodes.Add(node);
                }
            }
            else nodes = new List<zTreeNode>();
            return Json(nodes);
        }
    }
}