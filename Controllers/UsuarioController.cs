using Microsoft.AspNetCore.Mvc;

namespace WebApiLautaroIriazabal.Controllers
{
    // La anotación ApiController indica que esta clase es un controlador de API.
    [ApiController]
    // La anotación Route define la ruta base para las acciones de este controlador.
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        // Variable privada para almacenar los datos de los usuarios.
        private UsuarioData _usuarioData;

        // Constructor que recibe los datos de los usuarios como parámetro.
        public UsuarioController(UsuarioData usuarioData)
        {
            // Verifica que los datos de los usuarios no sean nulos antes de asignarlos.
            this._usuarioData = usuarioData ?? throw new ArgumentNullException(nameof(usuarioData));
        }

        // Método para obtener una lista de usuarios. Se accede a través de una petición GET a "api/Usuario/ListadoDeUsuarios".
        [HttpGet("ListadoDeUsuarios")]
        public ActionResult<List<Usuario>> ObtenerListadoDeUsuarios()
        {
            var usuarios = this._usuarioData.ListarUsuarios();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("No se encontraron usuarios.");
            }
            return usuarios;
        }
    }

}
}
