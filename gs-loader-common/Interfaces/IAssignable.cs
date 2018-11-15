namespace gs_loader_common.Interfaces
{
    /// <summary>
    /// Classe para objetos que podem incorporar os valores de outro objeto
    /// </summary>
    public interface IAssignable
    {
        bool Assign(object value);
    }
}
