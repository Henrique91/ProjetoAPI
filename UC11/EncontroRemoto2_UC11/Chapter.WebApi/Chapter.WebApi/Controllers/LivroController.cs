using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chapter.WebApi.Controllers
{
    [Produces("application/json")]//formato de resposta
    [Route("api/[controller]")]//rota para acesso da api -- api/Livro
    [ApiController]// que é um controller
    public class LivroController : ControllerBase
    {
        private readonly LivroRepository _livroRepository; 
        public LivroController(LivroRepository livroRepository) 
        {
            _livroRepository= livroRepository;
        }

        [HttpGet]
        public IActionResult Listar() 
        {
            try
            {
                return Ok(_livroRepository.Ler());
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Livro livro) 
        {
            try
            {
                _livroRepository.Cadastrar(livro);
                return Ok(livro);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(int id, Livro livro)
        {
            try
            {
                _livroRepository.Atualizar(id, livro);
                return StatusCode(204);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            try
            {
                _livroRepository.Deletar(id);
                return StatusCode(204);           
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            try
            {
                Livro livro = _livroRepository.BuscarPorId(id);

                if (livro == null)
                {
                    return NotFound();
                }
                return Ok(livro);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
