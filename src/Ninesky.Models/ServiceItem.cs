/*======================================
 作者：洞庭夕照
 创建：2016.12.19
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ninesky.Models
{
    /// <summary>
    /// 注入的服务项
    /// </summary>
    public class ServiceItem
    {
        /// <summary>
        /// 服务类型[含命名空间]
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 实现类类型[含命名空间]
        /// </summary>
        public string ImplementationType { get; set; }

        /// <summary>
        /// 生命周期
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ServiceLifetime lifetime { get; set; }
    }
}
