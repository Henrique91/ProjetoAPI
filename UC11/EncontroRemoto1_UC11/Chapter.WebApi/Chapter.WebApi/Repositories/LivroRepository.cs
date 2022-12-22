using Chapter.WebApi.Contexts;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;

namespace Chapter.WebApi.Repositories
{
    public class LivroRepository : ILivroRepository
    {

        private readonly ChapterContext _chapterContext;

        public LivroRepository(ChapterContext context) 
        {
            _chapterContext = context;
        }
        public List<Livro> Ler()
        {
            return _chapterContext.Livros.ToList();
        }
    }
}