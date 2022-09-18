using apiGXCFernandoM.Exeptions;
using apiGXCFernandoM.Models;
using apiGXCFernandoM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace apiGXCFernandoM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly ILogger<CalificacionesController> _logger;
        ICalificacionService _calificacionService;

        public CalificacionesController (ILogger<CalificacionesController> logger, ICalificacionService calificacionService)
        {
            _logger = logger;
            _calificacionService = calificacionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var calificaciones = _calificacionService.GetAll();
                return Ok(calificaciones);
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
                var calificaciones = _calificacionService.GetById(id);
                return Ok(calificaciones);
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
        public IActionResult Post([FromBody] Calificaciones calificaciones)
        {
            try
            {
                var resultado = _calificacionService.Save(calificaciones);
                return Created("Creado Exitosamente!", resultado.Result);
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
        public IActionResult Patch(int id, [FromBody] Calificaciones calificaciones)
        {
            try
            {
                var resultado = _calificacionService.Update(id, calificaciones);
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
                var resultado = _calificacionService.Delete(id);
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
