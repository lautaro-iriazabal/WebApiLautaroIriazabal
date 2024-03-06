using WebApiLautaroIriazabal.Models;
using WebApiLautaroIriazabal.Service;
using WebApiLautaroIriazabal.DTO;

namespace WebApiLautaroIriazabal.Mapper
{
    public class MapperProductoVendido
    {
        public ProductoVendido MapearToProdcutoVendido(ProductoVendidoDTO dto)
        {
            ProductoVendido productoVendido = new ProductoVendido();

            productoVendido.Id = dto.Id;
            productoVendido.Stock = dto.Stock;
            productoVendido.IdProducto = dto.IdProducto;
            productoVendido.IdVenta = dto.IdVenta;
            return productoVendido;
        }

        public ProductoVendidoDTO MapearToDTO(ProductoVendido productoVendido)
        {
            ProductoVendidoDTO dto = new ProductoVendidoDTO();

            dto.Id = productoVendido.Id;
            dto.Stock = productoVendido.Stock;
            dto.IdProducto = productoVendido.IdProducto;
            dto.IdVenta = productoVendido.IdVenta;

            return dto;
        }
    }
}
