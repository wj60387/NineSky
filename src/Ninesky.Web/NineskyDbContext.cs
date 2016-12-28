/*======================================
 作者：洞庭夕照
 创建：2016.12.08
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using Microsoft.EntityFrameworkCore;
using Ninesky.Models;

namespace Ninesky.Web
{
    public class NineskyDbContext:DbContext
    {
        public DbSet<Module> Modules { get; set; }
        public DbSet<Category> Categories { get; set; }

        public NineskyDbContext(DbContextOptions options):base(options)
        {

        }
    }
}
