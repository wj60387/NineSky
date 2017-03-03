/*======================================
 作者：洞庭夕照
 创建：2016.12.19
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using Ninesky.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Ninesky.InterfaceBase
{
    /// <summary>
    /// 栏目服务接口
    /// </summary>
    public interface InterfaceCategoryService:InterfaceBaseService<Category>
    {
        /// <summary>
        /// 能否做父栏目
        /// </summary>
        /// <param name="paretn">父栏</param>
        /// <param name="category">栏目</param>
        /// <returns></returns>
        OperationResult CanParent(Category parent, Category category);

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="id">栏目Id</param>
        /// <returns></returns>
        Task<OperationResult> RemoveAsync(int id);

        /// <summary>
        /// 查找树形菜单
        /// </summary>
        /// <param name="categoryType">栏目类型，可以为空</param>
        /// <returns></returns>
        Task<IQueryable<Category>> FindTreeAsync(CategoryType? categoryType);

        /// <summary>
        /// 查找子栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        IQueryable<Category> FindChildren(int id);

        /// <summary>
        /// 查找子栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        Task<IQueryable<Category>> FindChildrenAsync(int id);
    }
}
