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
    public class ColegioController : ControllerBase
    {
        private readonly ILogger<ColegioController> _logger;
        IColegioService _colegioService;

        public ColegioController (ILogger<ColegioController> logger, IColegioService colegioService)
        {
            _logger = logger;
            _colegioService = colegioService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var colegios = _colegioService.GetAll();
                return Ok(colegios);
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
                var colegios = _colegioService.GetById(id);
                return Ok(colegios);
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
        public IActionResult Post([FromBody] Colegio colegio)
        {
            try
            {
                var resultado = _colegioService.Save(colegio);
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
        public IActionResult Patch(int id, [FromBody] Colegio colegio)
        {
            try
            {
                var resultado = _colegioService.Update(id, colegio);
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
                var resultado = _colegioService.Delete(id);
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
