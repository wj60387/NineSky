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
using Ninesky.InterfaceBase;
using Ninesky.Models;
using Microsoft.EntityFrameworkCore;

namespace Ninesky.Base
{
    public class ModuleService:BaseService<Module>,InterfaceModuleService
    {
        public ModuleService(DbContext dbContext) : base(dbContext)
        { }
    }
}
