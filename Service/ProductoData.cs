using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using WebApiLautaroIriazabal.DTO;
using Microsoft.EntityFrameworkCore;


namespace WebApiLautaroIriazabal.Service
{
    public class ProductoData
    {
        private CoderContext context;

        public ProductoData(CoderContext coderContext)
        {
            // Verifica que el contexto no sea nulo antes de asignarlo.
            this.context = coderContext ?? throw new ArgumentNullException(nameof(coderContext));
        }

        public Producto ObtenerProducto(int id)
        {
            // Verifica que el id sea válido antes de buscar el producto.
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }

            Producto productoBuscado = context.Productos.Where(u => u.Id == id).FirstOrDefault();

            // Verifica que se haya encontrado un producto con el id proporcionado.
            if (productoBuscado == null)
            {
                throw new ArgumentException($"No se encontró ningún producto con el id {id}.");
            }

            return productoBuscado;
        }


        public List<Producto> ListarProductos()
        {
            List<Producto> productos = context.Productos.ToList();

            return productos;
        }

        public bool CrearProducto(ProductoDTO dto)
        {
            // Verifica que el DTO no sea nulo antes de crear el producto.
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            Producto p = new Producto();
            p.Id = dto.Id;
            p.Descripciones = dto.Descripciones;
            p.Costo = dto.Costo;
            p.Stock = dto.Stock;
            p.PrecioVenta = dto.PrecioVenta;
            p.IdUsuario = dto.IdUsuario;

            context.Productos.Add(p);
            context.SaveChanges();

            return true;
        }

        public bool ModificarProducto(int id, ProductoDTO productoDTO)
        {
            // Verifica que el id y el DTO sean válidos antes de modificar el producto.
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }
            if (productoDTO == null)
            {
                throw new ArgumentNullException(nameof(productoDTO));
            }

            Producto? ProductoBuscado = context.Productos.Where(p => p.Id == id).FirstOrDefault();
            if (ProductoBuscado is not null)
            {
                ProductoBuscado.Descripciones = productoDTO.Descripciones;
                ProductoBuscado.Costo = productoDTO.Costo;
                ProductoBuscado.PrecioVenta = productoDTO.PrecioVenta;
                ProductoBuscado.Stock = productoDTO.Stock;
                ProductoBuscado.IdUsuario = productoDTO.IdUsuario;

                context.Productos.Update(ProductoBuscado);
                context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool EliminarProducto(int id)
        {
            // Verifica que el id sea válido antes de eliminar el producto.
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }

            Producto productoABorrado = context.Productos.Include(p => p.ProductoVendidos).Where(p => p.Id == id).FirstOrDefault();

            if (productoABorrado is not null)
            {
                context.Productos.Remove(productoABorrado);
                context.SaveChanges();
                return true;
            }

            return false;
        }
    }

}
