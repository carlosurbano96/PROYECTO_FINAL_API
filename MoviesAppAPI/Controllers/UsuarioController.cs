using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAppAPI.Models;
using MoviesAppAPI.Models.ModelViews;
using MoviesAppAPI.Negocio;

namespace MoviesAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }        

        [HttpGet]
        [Route("lista")]
        
        public List<Usuario> GetUsuarios()
        {
            //competario
            Login neg = new Login(_configuration);
            return neg.GetUsers();
        }

        [HttpPost]
        [Route("Registrar")]
        public bool Registrar([FromBody] Registrar Form)
        {
            Login neg = new Login(_configuration);
            return neg.Registra(Form);
        }

        [HttpGet]
        [Route("IniciarSesion/{User}/{Pass}")]
        public bool IniciarSesion(string User, string Pass)
        {
            Login neg = new Login(_configuration);
            return neg.IniciarSesion(User, Pass);
        }

        [HttpPost]
        [Route("AddMovie")]
        public IActionResult AddMovie([FromForm] AddMovie objMovie)
        {
            Login neg = new Login(_configuration);
            bool result = false; 
            try
            {
                result = neg.AddMovie(objMovie);
                if (result)
                {
                    return Ok(new { status = "ok", mensaje = "se inserto correctamente" });
                }
                else
                {
                    return BadRequest(new { status = "error", mensaje = "se inserto correctamente" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { status = "error", mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetListMovies")]
        public IActionResult GetListMovies()
        {
            Login neg = new Login(_configuration);
            try
            {
                List<Movie> result = neg.GetListMovies();
                return StatusCode(StatusCodes.Status200OK, new { status = "ok", data = result, count=result.Count() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { status = "error", mensaje = ex.Message });
            }
            
        }

        [HttpGet]
        [Route("GetMovie/{idMovie}")]
        public IActionResult GetMovie(int idMovie)
        {
            Login neg = new Login(_configuration);
            try
            {
                Movie movie = neg.GetMovie(idMovie);
                return Ok(new { status = "ok", data = movie });
            }
            catch (Exception ex)
            {

                return BadRequest(new { status = "error", mensaje = ex.Message });
            }
        }
    }
}
