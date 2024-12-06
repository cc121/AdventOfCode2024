namespace AdventOfCode2024.Days.Day5.DataClasses
{
    internal class RuleGraph
    {
        private readonly Dictionary<Node, List<Node>> _nodes = [];
        public bool Empty => _nodes.Keys.All(x => x.Removed == true);

        public RuleGraph(List<string> nodes, List<string> expectedNodes)
        {
            foreach (var node in nodes)
            {
                if (!expectedNodes.Contains(node))
                    continue;

                var children = new List<Node>();
                var newNode = new Node(node, children);

                _nodes.Add(newNode, children);
            }
        }

        public void AddRule(Rule rule)
        {
            foreach (var node in _nodes)
            {
                if (node.Key.Number == rule.FirstPageNumber)
                {
                    foreach (var child in _nodes)
                    {
                        if (child.Key.Number == rule.SecondPageNumber)
                        {
                            _nodes[node.Key].Add(child.Key);
                        }
                    }
                }
            }
        }

        public List<string> RemoveLeaves()
        {
            List<string> leaves = [];
            List<Node> removedNodes = [];
            foreach (var node in _nodes)
            {
                if (node.Key.IsLeaf && !node.Key.Removed)
                {
                    leaves.Add(node.Key.Number);
                    removedNodes.Add(node.Key);
                }
            }
            foreach (var node in removedNodes)
                node.Removed = true;

            return leaves;
        }
    }

    internal class Node(string number, List<Node> childrenReference)
    {
        private readonly List<Node> _children = childrenReference;

        private readonly string _number = number;

        public string Number => _number;

        private bool _removed = false;

        public bool Removed
        {
            get => _removed;
            set => _removed = value;
        }

        public bool IsLeaf => _children.Count == 0 || _children.All(x => x.Removed == true);
    }
}
