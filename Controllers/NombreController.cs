using Microsoft.AspNetCore.Mvc;

namespace WebApiLautaroIriazabal.Controllers
{
    // La anotación ApiController indica que esta clase es un controlador de API.
    [ApiController]
    // La anotación Route define la ruta base para las acciones de este controlador.
    [Route("api/[controller]")]
    public class NombreController : Controller
    {
        // Método para obtener un nombre. Se accede a través de una petición GET.
        [HttpGet]
        public string ObtenerNombre()
        {
            // Devuelve un nombre fijo.
            return "Lautaro Iriazabal";
        }

        // Método para obtener un listado de nombres. Se accede a través de una petición GET a "api/Nombre/Listado".
        [HttpGet("Listado")]
        public List<String> ObtenerListadoDeNombres()
        {
            // Crea una lista de nombres y la devuelve.
            List<String> list = new List<String>() { "---Listado de nombres---" };
            return list;
        }

        // Método para obtener parámetros desde la query string de la URL. Se accede a través de una petición GET a "api/Nombre/QueryParam".
        [HttpGet("QueryParam")]
        public IActionResult Parametros([FromQuery] string nombre, [FromQuery] string edad)
        {
            // Devuelve un objeto anónimo con los parámetros recibidos.
            return base.Ok(new { parametro = new List<string> { nombre, edad } });
        }
    }

}
