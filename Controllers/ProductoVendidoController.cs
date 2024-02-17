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
    public class ProductoVendidoController : Controller
    {
        // Variable privada para almacenar los datos de los productos vendidos.
        private ProductoVendidoData _productoVendidoData;

        // Constructor que recibe los datos de los productos vendidos como parámetro.
        public ProductoVendidoController(ProductoVendidoData productoVendidoData)
        {
            // Verifica que los datos de los productos vendidos no sean nulos antes de asignarlos.
            this._productoVendidoData = productoVendidoData ?? throw new ArgumentNullException(nameof(productoVendidoData));
        }

    }

}
