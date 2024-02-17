using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApiLautaroIriazabal.Models;
using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Service;
using WebApiLautaroIriazabal.DTO;

namespace WebApiLautaroIriazabal.Controllers
{
    // La anotación ApiController indica que esta clase es un controlador de API.
    [ApiController]
    // La anotación Route define la ruta base para las acciones de este controlador.
    [Route("api/[controller]")]
    public class VentaController : Controller
    {
        // Variable privada para almacenar los datos de las ventas.
        private VentaData _ventaData;

        // Constructor que recibe los datos de las ventas como parámetro.
        public VentaController(VentaData ventaData)
        {
            // Verifica que los datos de las ventas no sean nulos antes de asignarlos.
            this._ventaData = ventaData ?? throw new ArgumentNullException(nameof(ventaData));
        }

    }

}
