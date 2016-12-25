/*======================================
 作者：洞庭夕照
 创建：2016.12.19
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninesky.Models;

namespace Ninesky.InterfaceBase
{
    /// <summary>
    /// 栏目服务接口
    /// </summary>
    public interface InterfaceCategoryService:InterfaceBaseService<Category>
    {
        /// <summary>
        /// 查找树形菜单
        /// </summary>
        /// <param name="categoryType">栏目类型，可以为空</param>
        /// <returns></returns>
        List<Category> FindTree(CategoryType? categoryType);
    }
}
