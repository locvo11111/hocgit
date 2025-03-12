using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class TreeListHelper
    {
        public static List<TreeListNode> GetAllChildNodeAllLevel(TreeListNode node)
        {
            List<TreeListNode> nodes = new List<TreeListNode>();
            ProcessNode(node, nodes);
            return nodes;
        }


        static void ProcessNode(TreeListNode node, List<TreeListNode> nodes)
        {
            if (node == null)
                return;

            foreach (TreeListNode child in node.Nodes)
            {
                nodes.Add(child);
                if (child.HasChildren)
                    ProcessNode(child, nodes);
            }
        }

        public static void SetParentNotTrueFalseStateByCommand(TreeList treeList)
        {
            foreach (TreeListNode node in treeList.GetNodeList().OrderByDescending(x => x.Level))
            {
                if (!node.HasChildren)
                    continue;


                foreach (string cmd in Enum.GetNames(typeof(CommandCode)))
                {
                    int trueNodesCount = node.Nodes.Count(x => bool.TryParse(x.GetValue(cmd).ToString(), out bool parse) && parse);
                    int falseNodesCount = node.Nodes.Count(x => bool.TryParse(x.GetValue(cmd).ToString(), out bool parse) && !parse);

                    if (trueNodesCount == node.Nodes.Count())
                        node.SetValue(cmd, true);
                    else if (falseNodesCount == node.Nodes.Count())
                        node.SetValue(cmd, false);
                    else
                        node.SetValue(cmd, null);
                }
            }
        }
    }
}
