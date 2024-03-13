using Microsoft.AspNetCore.Mvc;

namespace WebApiLautaroIriazabal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreController : Controller
    {
        // Constructor de la clase
        public NombreController()
        {
        }

        // Método para obtener el nombre
        [HttpGet]
        public string ObtenerNombre()
        {
            return "Panel de control";
        }
    }


}
