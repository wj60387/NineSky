/*======================================
 作者：洞庭夕照
 创建：2016.12.05
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ninesky.DataLibrary;
using Ninesky.Models;

namespace Ninesky.Base
{
    /// <summary>
    /// 栏目服务类
    /// </summary>
    public class CategoryService
    {
        private CategoryRepository _categoryRepository;
        public CategoryService(DbContext dbContext)
        {
            _categoryRepository = new CategoryRepository(dbContext);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="Id">栏目Id</param>
        /// <returns></returns>
        public Category Find(int Id)
        {
            return _categoryRepository.Find(c => c.CategoryId == Id);
        }
    }
}
