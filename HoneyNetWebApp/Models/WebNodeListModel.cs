using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneyNetWebApp.Models
{
    public class WebNodeListModel : IEnumerable<WebNodeModel>
    {
        public List<WebNodeModel> _nodes { get; set; }

        public WebNodeListModel()
        {
          //  _node = node;
        }

        public void AddNode(WebNodeModel node)
        {
            _nodes.Append(node);
        }

        public IEnumerator<WebNodeModel> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
