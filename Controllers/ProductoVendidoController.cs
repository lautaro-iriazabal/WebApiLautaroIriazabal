using Microsoft.AspNetCore.Mvc;
using WebApiLautaroIriazabal.Models;
using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Service;
using WebApiLautaroIriazabal.DTO;

namespace WebApiLautaroIriazabal.Controllers
{
    // Definición de la clase ProductoVendidoController
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoVendidoController : Controller
    {
        // Atributo privado para el servicio de productos vendidos
        private readonly ProductoVendidoData productoVendidoService;

        // Constructor de la clase
        public ProductoVendidoController(ProductoVendidoData productoVendidoService)
        {
            // Inicialización del servicio de productos vendidos
            this.productoVendidoService = productoVendidoService;
        }

   


        // Método para obtener los productos vendidos por el ID de usuario
        [HttpGet("{idUsuario}")]
        public IActionResult ObtenerProductosVendidosPorIdDeUsuario(int idUsuario)
        {
            try
            {
                // Obtención de los productos vendidos por el ID de usuario
                var productosVendidos = this.productoVendidoService.ListarProductosVendidos;

                // Verificación de si se encontraron productos vendidos
                if (productosVendidos is not null)
                {
                    return Ok(productosVendidos);
                }
                else
                {
                    return NotFound(new { Message = "No se encontraron productos vendidos asociados a este Usuario", Status = "404" });
                }
            }
        
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}
