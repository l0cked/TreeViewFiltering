using System.Collections.Generic;

namespace TreeViewFiltering.Model
{
    internal class Node
    {
        public string Text { get; set; }
        public List<Node> Children { get; set; }

        public Node(string text)
        {
            Text = text;
        }

        public void AddChild(Node node)
        {
            if (Children == null) Children = new List<Node>();

            Children.Add(node);
        }

        public static bool ShowNode(string filterText, Node node)
        {
            if (node.Text.Contains(filterText)) return true;

            if (node.Children?.Count > 0)
                foreach (var child in node.Children)
                    if (ShowNode(filterText, child))
                        return true;

            return false;
        }
    }
}
