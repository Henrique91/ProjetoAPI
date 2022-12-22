using Chapter.WebApi.Models;

namespace Chapter.WebApi.Interfaces
{
    public interface ILivroRepository
    {
        List<Livro> Ler();
    }
}