using Microsoft.AspNetCore.Mvc;
using System;
using WebApiLautaroIriazabal.Models;
using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Service;
using WebApiLautaroIriazabal.DTO;


namespace WebApiLautaroIriazabal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private ProductoData _productoData;

        public ProductoController(ProductoData productoData)
        {
            this._productoData = productoData ?? throw new ArgumentNullException(nameof(productoData));
        }

        [HttpGet("ListadoDeProductos")]
        public ActionResult<List<Producto>> ObtenerListaDeProductos()
        {
            var productos = this._productoData.ListarProductos();
            if (productos == null || !productos.Any())
            {
                return NotFound("No se encontraron productos.");
            }
            return productos;
        }

        [HttpPost("AgregadoDeProducto")]
        public IActionResult AgregarUnProducto([FromBody] ProductoDTO producto)
        {
            if (producto == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            if (this._productoData.CrearProducto(producto))
            {
                return CreatedAtAction(nameof(AgregarUnProducto), new { id = producto.Id }, producto);
            }
            else
            {
                return Conflict("No se pudo agregar el producto.");
            }
        }

        [HttpPut("ActualizarProducto")]
        public IActionResult ModificarProducto(int id, ProductoDTO productoDTO)
        {
            if (id <= 0)
            {
                return BadRequest("El id no puede ser negativo o cero.");
            }

            if (productoDTO == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            if (this._productoData.ModificarProducto(id, productoDTO))
            {
                return Ok(new { mensaje = "Producto Actualizado", productoDTO });
            }

            return NotFound("No se encontró el producto para actualizar.");
        }

        [HttpDelete("BorradoDeProducto")]
        public IActionResult BorrarProducto(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El id no puede ser negativo o cero.");
            }

            if (this._productoData.EliminarProducto(id))
            {
                return Ok(new { mensaje = "Producto borrado con éxito" });
            }
            else
            {
                return NotFound("No se encontró el producto para borrar.");
            }
        }
    }

}
