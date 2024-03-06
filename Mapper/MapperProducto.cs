using WebApiLautaroIriazabal.DTO;
using WebApiLautaroIriazabal.Models;

namespace WebApiLautaroIriazabal.Mapper
{
    public class MapperProducto
    {
        // Método para mapear un ProductoDTO a un Producto
        public Producto MapearToProducto(ProductoDTO dto)
        {
            // Crea un nuevo objeto Producto
            Producto producto = new Producto();

            // Asigna los valores del DTO al Producto
            producto.Id = dto.Id;
            producto.Descripciones = dto.Descripciones;
            producto.Costo = dto.Costo;
            producto.Stock = dto.Stock;
            producto.PrecioVenta = dto.PrecioVenta;
            producto.IdUsuario = dto.IdUsuario;

            // Retorna el Producto mapeado
            return producto;
        }

        // Método para mapear un Producto a un ProductoDTO
        public ProductoDTO MapearToDTO(Producto producto)
        {
            // Crea un nuevo objeto ProductoDTO
            ProductoDTO dto = new ProductoDTO();

            // Asigna los valores del Producto al DTO
            dto.Id = producto.Id;
            dto.Descripciones = producto.Descripciones;
            dto.Costo = producto.Costo;
            dto.Stock = producto.Stock;
            dto.PrecioVenta = producto.PrecioVenta;
            dto.IdUsuario = producto.IdUsuario;

            // Retorna el DTO mapeado
            return dto;
        }
    }
}
