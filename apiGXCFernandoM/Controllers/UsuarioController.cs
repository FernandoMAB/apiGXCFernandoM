using apiGXCFernandoM.Exeptions;
using apiGXCFernandoM.Models;
using apiGXCFernandoM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiGXCFernandoM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        IUsuarioService _usuarioService;

        public UsuarioController (ILogger<UsuarioController> logger, IUsuarioService usuarioService)
        {
            this._logger = logger;
            this._usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var usuarios = _usuarioService.GetAll();
                return Ok(usuarios);
            }
            catch (BusinessException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var usuario = _usuarioService.GetById(id);
                return Ok(usuario);
            }
            catch (BusinessException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post ([FromBody] Usuario usuario)
        {
            try
            {
                var resultado = _usuarioService.Save(usuario);
                return Created("Creado Exitosamente!",resultado.Result);
            }
            catch (AggregateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Patch(int id, [FromBody] Usuario usuario)
        {
            try
            {
                var resultado = _usuarioService.Update(id, usuario);
                return Ok(resultado.Result);
            }
            catch (AggregateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var resultado = _usuarioService.Delete(id);
                    return Ok(resultado.Result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
