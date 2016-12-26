/*======================================
 作者：洞庭夕照
 创建：2016.12.26
 网站：www.ninesky.cn
       mzwhj.cnblogs.com
 代码：git.oschina.net/ninesky/Ninesky
 版本：v1.0.0.0
 =====================================*/using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ninesky.Web.Models
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class zTreeNode
    {
        public int id { get; set; }

        public string name { get; set; }

        public string icon { get; set; }
        public string iconClose { get; set; }
        public string iconOpen { get; set; }

        public string iconSkin { get; set; }
        public string url { get; set; }
    }
}
