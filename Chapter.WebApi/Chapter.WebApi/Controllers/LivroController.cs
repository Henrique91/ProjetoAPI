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
    }
}
