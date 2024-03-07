using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Models;
using WebApiLautaroIriazabal.Mapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using WebApiLautaroIriazabal.DTO;

namespace WebApiLautaroIriazabal.Service
{
    public class ProductoVendidoData
    {
        // Definición de las propiedades de la clase
        private CoderContext context;
        private MapperProductoVendido productoVendidoMapper;

        // Constructor de la clase
        public ProductoVendidoData(CoderContext coderContext, MapperProductoVendido productoVendidoMapper)
        {
            this.context = coderContext;
            this.productoVendidoMapper = productoVendidoMapper;
        }

        // Método para obtener los productos vendidos por ID de usuario
        public List<ProductoVendidoDTO> ObtenerProductosVendidosPorIdUsuario(int idUsuario)
        {
            List<Producto>? productos = this.context.Productos
                .Include(p => p.ProductoVendidos).Where(p => p.IdUsuario == idUsuario).ToList();

            List<ProductoVendido?>? pVendidos = productos.Select(p => p.ProductoVendidos.ToList().
            Find(pv => pv.IdProducto == p.Id)).Where(p => !object.ReferenceEquals(p, null)).ToList();

            List<ProductoVendidoDTO> dto = pVendidos.Select(p => this.productoVendidoMapper.MapearToDTO(p)).ToList();

            return dto;
        }

        // Método para agregar un producto vendido
        public bool AgregarUnProductoVendido(ProductoVendidoDTO productoVendidoDTO)
        {
            ProductoVendido ProductoVendido = this.productoVendidoMapper.MapearToProdcutoVendido(productoVendidoDTO);
            EntityEntry<ProductoVendido>? resultado = this.context.ProductoVendidos.Add(ProductoVendido);
            resultado.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();

            return true;
        }

        // Método para obtener un producto vendido por su ID
        public ProductoVendido ObtenerProductoVendido(int id)
        {
            ProductoVendido productoBuscadoVendido = context.ProductoVendidos.Where(u => u.Id == id).FirstOrDefault();

            return productoBuscadoVendido;
        }

        // Método para listar todos los productos vendidos
        public List<ProductoVendido> ListarProductosVendidos()
        {
            List<ProductoVendido> productoVendido = context.ProductoVendidos.ToList();

            return productoVendido;
        }

        // Método para crear un producto vendido
        public bool CrearProductoVendido(ProductoVendido productoVendido)
        {
            context.ProductoVendidos.Add(productoVendido);
            context.SaveChanges();

            return true;
        }

        // Método para modificar un producto vendido
        public bool ModificarProductoVendido(ProductoVendido productoVendido, int id)
        {
            ProductoVendido ProductoVendidoBuscado = context.ProductoVendidos.Where(pv => pv.Id == id).FirstOrDefault();

            ProductoVendidoBuscado.Stock = productoVendido.Stock;
            ProductoVendidoBuscado.IdProducto = productoVendido.IdProducto;
            ProductoVendidoBuscado.IdVenta = productoVendido.IdVenta;

            context.ProductoVendidos.Add(ProductoVendidoBuscado);
            context.SaveChanges();

            return true;
        }

        // Método para eliminar un producto vendido
        public bool EliminarProductoVendido(int id)
        {
            ProductoVendido productoABorradoVendido = context.ProductoVendidos.Where(pv => pv.Id == id).FirstOrDefault();

            if (productoABorradoVendido is not null)
            {
                context.ProductoVendidos.Remove(productoABorradoVendido);
                context.SaveChanges();
                return true;
            }

            return false;
        }
    }

}
