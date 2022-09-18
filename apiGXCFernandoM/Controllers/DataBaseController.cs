using apiGXCFernandoM.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiGXCFernandoM.Controllers
{
    [Route("api/[controller]")]
    public class DataBaseController : ControllerBase
    {
        private readonly ILogger<DataBaseController> _logger;

        ColegioContext dbcontext;

        public DataBaseController(ILogger<DataBaseController> logger, ColegioContext db)
        {
            this.dbcontext = db;
            this._logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult CreateDatabase()
        {
            _logger.LogDebug("Create Database");
            var creado = dbcontext.Database.EnsureCreated();
            if (creado)
                return Created("201", "Creado Exitosamente!!");
            else
                return Conflict("Error al crear");
        }

    }
}
