/*======================================
 作者：洞庭夕照
 创建：2017.01.01
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using Ninesky.Models;
using System.Collections.Generic;

namespace Ninesky.Web.Models
{
    /// <summary>
    /// 数据库初始化
    /// </summary>
    public class DbInitializer
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="dbContext"></param>
        public static void Initialize(NineskyDbContext dbContext)
        {
            InitializeModule(dbContext);

        }

        /// <summary>
        /// 初始化模块数据
        /// </summary>
        /// <param name="dbContext"></param>
        public static void InitializeModule(NineskyDbContext dbContext)
        {
            var modules = new List<Module>();
            var module = new Module()
            {
                Controller = "Article",
                Description = "实现文章功能",
                Enabled = true,
                Name = "文章模块",
                ModuleOrders = new List<ModuleOrder> {
                new ModuleOrder {   Name="ID升序", Order=0},
                new ModuleOrder { Name="ID降序",Order=1 },
                new ModuleOrder {Name="发布时间升序", Order=2 },
                new ModuleOrder { Name="发布时间降序",Order=3},
                new ModuleOrder { Name="点击升序",Order=4},
                new ModuleOrder { Name="点击降序",Order=5}
            }
            };
            modules.Add(module);
            dbContext.Modules.AddRange(modules);
            dbContext.SaveChanges();
        }
    }
}
