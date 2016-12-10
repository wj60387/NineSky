﻿/*======================================
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

namespace Ninesky.Base
{
    /// <summary>
    /// 栏目服务类
    /// </summary>
    public class CategoryService
    {
        private BaseRepository<Category> _baseRepository;
        public CategoryService(DbContext dbContext)
        {
            _baseRepository = new BaseRepository<Category>(dbContext);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="Id">栏目Id</param>
        /// <returns></returns>
        public Category Find(int Id)
        {
            return _baseRepository.Find(new string[] { "General", "Page", "Link" }, c => c.CategoryId == Id);
        }
    }
}