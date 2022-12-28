using Chapter.WebApi.Interfaces;
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
        private readonly ILivroRepository _ilivroRepository; 
        public LivroController(ILivroRepository ilivroRepository) 
        {
            _ilivroRepository= ilivroRepository;
        }

        [HttpGet]
        public IActionResult Listar() 
        {
            try
            {
                return Ok(_ilivroRepository.Ler());
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
                _ilivroRepository.Cadastrar(livro);
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
                _ilivroRepository.Atualizar(id, livro);
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
                _ilivroRepository.Deletar(id);
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
                Livro livro = _ilivroRepository.BuscarPorId(id);

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
