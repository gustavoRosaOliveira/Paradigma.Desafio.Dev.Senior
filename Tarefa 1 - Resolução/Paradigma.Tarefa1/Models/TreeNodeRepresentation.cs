using Paradigma.Tarefa1.Exceptions;
using Paradigma.Tarefa1.Exceptions.SpecificExceptions;
using System.Text;

namespace Paradigma.Tarefa1.Models
{
    public class TreeNodeRepresentation
    {
        public string Value { get; private set; }
        public List<TreeNodeRepresentation> Children { get; private set; }

        public const byte MAX_CHILDREN = 2;

        private TreeNodeRepresentation(string value)
        {
            Value = value;
            Children = new List<TreeNodeRepresentation>();
        }

        private void AddChild(TreeNodeRepresentation child)
        {
            Children.Add(child);
        }

        public override string ToString()
        {
            const char SEPARATOR_START = '[';
            const char SEPARATOR_END = ']';

            var result = new StringBuilder(Value);

            if (Children.Count > 0)
            {
                result.Append(SEPARATOR_START);

                for (int i = 0; i < Children.Count; i++)
                {
                    result.Append(Children[i].ToString());

                    if (i < Children.Count - 1)
                    {
                        result.Append($"{SEPARATOR_END}{SEPARATOR_START}");
                    }
                }

                result.Append(SEPARATOR_END);
            }

            return result.ToString();
        }

        /// <summary>
        /// Build a TreeNodeRepresentation
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns></returns>
        /// <exception cref="E1TooManyChildrenException"></exception>
        /// <exception cref="E2PresentCycleException"></exception>
        /// <exception cref="E3MultipleRootsException"></exception>
        public static TreeNodeRepresentation Build((string, string)[] pairs)
        {
            try
            {
                var nodes = new Dictionary<string, TreeNodeRepresentation>();
                var childNodes = new HashSet<string>();

                foreach (var (parent, child) in pairs)
                {
                    if (!nodes.ContainsKey(parent))
                    {
                        nodes[parent] = new TreeNodeRepresentation(parent);
                    }

                    if (!nodes.ContainsKey(child))
                    {
                        nodes[child] = new TreeNodeRepresentation(child);
                    }

                    if (childNodes.Contains(child))
                    {
                        throw new E3MultipleRootsException();
                    }

                    bool HasMaxChildren()
                    {
                        return nodes[parent].Children.Count >= MAX_CHILDREN;
                    }

                    if (HasMaxChildren())
                        throw new E1TooManyChildrenException(nodes[parent]);

                    nodes[parent].AddChild(nodes[child]);
                    childNodes.Add(child);
                }

                var rootCandidates = nodes.Keys.Except(childNodes).ToList();

                if (rootCandidates.Count == 0)
                {
                    throw new E2PresentCycleException();
                }

                if (rootCandidates.Count > 1)
                {
                    throw new E3MultipleRootsException();
                }

                return nodes[rootCandidates.First()];
            }
            catch (Exception ex)
            when (ex is not EBaseSpecificException)
            {
                throw new E4GeneralTreeException(ex);
            }
        }
    }
}
