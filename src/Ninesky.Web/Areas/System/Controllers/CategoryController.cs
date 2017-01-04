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
using Microsoft.AspNetCore.Mvc.Rendering;
using Ninesky.InterfaceBase;
using Ninesky.Models;
using Ninesky.Web.Models;

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

        public IActionResult Add([FromServices]InterfaceModuleService moduleService, CategoryType? categoryType)
        {

            var modules = moduleService.FindList(true).Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modules.Insert(0, new SelectListItem() { Text = "��", Value = "0", Selected = true });
            ViewData["Modules"] = modules;
            return View(new Category() { Type = CategoryType.General, ParentId = 0, View="Index", Order = 0, Target = LinkTarget._self, General = new CategoryGeneral() { ContentView = "Index" } });
        }

        [HttpPost]
        public IActionResult Add([FromServices]InterfaceModuleService moduleService,Category category)
        {
            if(ModelState.IsValid)
            {
                //��鸸��Ŀ
                if (category.ParentId > 0)
                {
                    var parentCategory = _categoryService.Find(category.ParentId);
                    if (parentCategory == null) ModelState.AddModelError("ParentId", "����Ŀ������");
                    else if (parentCategory.Type != CategoryType.General) ModelState.AddModelError("ParentId", "����Ŀ�����������Ŀ");
                    else category.ParentPath = parentCategory.ParentPath + "," + parentCategory.CategoryId;
                }
                else category.ParentPath = "0";
                //�����Ŀ����
                switch (category.Type)
                {
                    case CategoryType.General:
                        if (category.General == null) ModelState.AddModelError("General.Type", "����д������Ŀ����");
                        else
                        {
                            if (category.General.ModuleId > 0)
                            {
                                if (string.IsNullOrEmpty(category.General.ContentView)) ModelState.AddModelError("General.ContentView", "����д��Ŀ��ͼ");
                                if (category.General.ContentOrder == null) ModelState.AddModelError("General.ContentOrder", "��ѡ����������ʽ");
                            }
                            else
                            {
                                if (category.Page != null) category.Page = null;
                                if (category.Link != null) category.Link = null;
                            }
                        }
                        break;
                    case CategoryType.Page:
                        //���
                        if (category.Page == null) ModelState.AddModelError("General.Type", "����д��ҳ��Ŀ����");
                        else
                        {
                            if (string.IsNullOrEmpty(category.Page.Content)) ModelState.AddModelError("Page.Content", "�����뵥ҳ��Ŀ����");
                            else
                            {
                                if (category.General != null) category.General = null;
                                if (category.Link != null) category.Link = null;
                            }
                        }
                        break;
                    case CategoryType.Link:
                        //���
                        if (category.Link == null) ModelState.AddModelError("General.Type", "����д������Ŀ����");
                        else
                        {
                            if (string.IsNullOrEmpty(category.Link.Url)) ModelState.AddModelError("Link.Url", "��ѡ���������ӵ�ַ");
                            else
                            {
                                if (category.General != null) category.General = null;
                                if (category.General != null) category.General = null;
                            }
                        }
                        break;
                }
                
                //���浽���ݿ�
                if(ModelState.IsValid)
                {
                    if (_categoryService.Add(category) > 0) return View("AddSucceed", category);
                    else ModelState.AddModelError("", "��������ʧ��");
                }
            }
            var modules = moduleService.FindList(true).Select(m => new SelectListItem { Text = m.Name, Value = m.ModuleId.ToString() }).ToList();
            modules.Insert(0, new SelectListItem() { Text = "��", Value = "0"});
            ViewData["Modules"] = modules;
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
        public IActionResult ParentTree()
        {
            return Json(_categoryService.FindTree(CategoryType.General).Select(c => new zTreeNode { id = c.CategoryId, name = c.Name, pId = c.ParentId, iconOpen="fa-open", iconClose="fa-close",iconSkin="fa fa-folder" }));
        }

        /// <summary>
        /// ��Ŀ��
        /// </summary>
        /// <returns></returns>
        public IActionResult Tree()
        {
            List<zTreeNode> nodes;
            var categories = _categoryService.FindTree(null);
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