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

        public void Atualizar(int id, Livro livro)
        {
            Livro livroBuscado = _chapterContext.Livros.Find(id);

            if (livroBuscado != null)
            {
                livroBuscado.Titulo = livro.Titulo;
                livroBuscado.QuantidadePaginas = livro.QuantidadePaginas;
                livroBuscado.Disponivel = livro.Disponivel;
            }

            _chapterContext.Livros.Update(livroBuscado);
            _chapterContext.SaveChanges();
        }

        public Livro BuscarPorId(int id)
        {
            return _chapterContext.Livros.Find(id);
        }

        public void Cadastrar(Livro livro)
        {
            _chapterContext.Livros.Add(livro);
            _chapterContext.SaveChanges();
        }

        public void Deletar(int id)
        {
            Livro livro = _chapterContext.Livros.Find(id);
            _chapterContext.Livros.Remove(livro);
            _chapterContext.SaveChanges();

        }

        public List<Livro> Ler()
        {
            return _chapterContext.Livros.ToList();
        }
    }
}