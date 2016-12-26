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
        /// <summary>
        /// 节点ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public int pId { get; set; }

        /// <summary>
        /// 结点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 图标
        /// </summary>

        public string icon { get; set; }
        /// <summary>
        /// 打开的图片
        /// </summary>
        public string iconClose { get; set; }
        /// <summary>
        /// 关闭的图标
        /// </summary>
        public string iconOpen { get; set; }
        /// <summary>
        /// 图标css
        /// </summary>

        public string iconSkin { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 打开方式
        /// </summary>
        public string target { get; set; }

        public zTreeNode()
        {
            target = "_self";
        }
    }
}
