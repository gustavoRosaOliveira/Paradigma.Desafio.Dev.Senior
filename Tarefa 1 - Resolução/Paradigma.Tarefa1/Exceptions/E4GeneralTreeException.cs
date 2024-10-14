namespace Paradigma.Tarefa1.Exceptions
{
    internal sealed class E4GeneralTreeException : Exception
    {
        internal E4GeneralTreeException(Exception innerException) : base("Qualquer outro erro", innerException)
        {

        }
    }
}
