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
    public class MateriaController : ControllerBase
    {

        private readonly ILogger<MateriaController> _logger;
        IMateriaService _materiaService;

        public MateriaController (ILogger<MateriaController> logger, IMateriaService materiaService)
        {
            _logger = logger;
            _materiaService = materiaService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var materias = _materiaService.GetAll();
                return Ok(materias);
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
                var materia = _materiaService.GetById(id);
                return Ok(materia);
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
        public IActionResult Post([FromBody] Materia materia)
        {
            try
            {
                var resultado = _materiaService.Save(materia);
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
        public IActionResult Patch(int id, [FromBody] Materia materia)
        {
            try
            {
                var resultado = _materiaService.Update(id, materia);
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
                var resultado = _materiaService.Delete(id);
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
