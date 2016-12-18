/*======================================
 作者：洞庭夕照
 创建：2016.12.18
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ninesky.InterfaceDataLibrary
{
    /// <summary>
    /// 仓储基类接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface InterfaceBaseRepository<T> where T : class
    {
        /// <summary>
        /// 查询[不含导航属性]
        /// </summary>
        /// <param name="predicate">查询表达式</param>
        /// <returns>实体</returns>
        T Find(Expression<Func<T, bool>> predicate);
    }
}
