using Paradigma.Tarefa1.Models;

namespace Paradigma.Tarefa1.Exceptions.SpecificExceptions
{
    internal sealed class E1TooManyChildrenException : EBaseSpecificException
    {
        internal E1TooManyChildrenException(TreeNodeRepresentation treeNode)
            : base($"Nó '{treeNode.Value}': não é possível inserir mais de {TreeNodeRepresentation.MAX_CHILDREN} filhos em um nó!")
        {
        }
    }
}