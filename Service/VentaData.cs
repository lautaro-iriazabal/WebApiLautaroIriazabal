using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiLautaroIriazabal.DTO;
using WebApiLautaroIriazabal.Mapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApiLautaroIriazabal.Service;

namespace WebApiLautaroIriazabal.Service
{
    public class VentaData
    {
        // Definición de las propiedades de la clase
        private CoderContext context;
        private ProductoVendidoData productoVendidoData;
        private ProductoData productoData;
        private MapperVenta ventaMapper;

        // Constructor de la clase
        public VentaData(CoderContext coderContext, ProductoVendidoData productoVendidoData, ProductoData productoData, MapperVenta ventaMapper)
        {
            this.context = coderContext;
            this.productoVendidoData = productoVendidoData;
            this.ventaMapper = ventaMapper;
            this.productoData = productoData;
        }

        // Método para obtener las ventas por ID de usuario
        public List<VentaDTO> ObtenerVentasPorIdUsuario(int idUsuario)
        {
            return this.context.Venta.Where(v => v.IdUsuario == idUsuario)
                .Select(v => this.ventaMapper.MapearToDto(v)).ToList();
        }

        // Método para agregar una nueva venta
        public bool AgregarNuevaVenta(int idUsuario, List<ProductoDTO> productosDTO)
        {
            Venta venta = new Venta();
            List<string> nombredeProductos = productosDTO.Select(p => p.Descripciones).ToList();
            string comentarios = string.Join(",", nombredeProductos);
            venta.Comentarios = comentarios;
            venta.IdUsuario = idUsuario;

            EntityEntry<Venta>? resultado = this.context.Venta.Add(venta);
            resultado.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            this.context.SaveChanges();

            this.MarcarComoProductosVendidos(productosDTO, venta.Id);

            this.ActualizarStockDeProductosVendidos(productosDTO);

            return true;
        }

        // Método para marcar productos como vendidos
        private void MarcarComoProductosVendidos(List<ProductoDTO> productoDTOs, int idVenta)
        {
            productoDTOs.ForEach(p =>
            {
                ProductoVendidoDTO productoVendidoDTO = new ProductoVendidoDTO();
                productoVendidoDTO.IdProducto = p.Id;
                productoVendidoDTO.Stock = p.Stock;
                productoVendidoDTO.IdVenta = idVenta;

                this.productoVendidoData.AgregarUnProductoVendido(productoVendidoDTO);

            });
        }

        // Método para actualizar el stock de los productos vendidos
        private void ActualizarStockDeProductosVendidos(List<ProductoDTO> productoDTOs)
        {

            productoDTOs.ForEach(p =>
            {
                ProductoDTO productoActual = this.productoData.ObtenerProdcutosPorIdProducto(p.Id);
                productoActual.Stock -= p.Stock;
                this.productoData.ActualizarProducto(productoActual);

            });
        }

        // Método para obtener una venta por su ID
        public Venta ObtenerVenta(int id)
        {
            Venta ventaBuscado = context.Venta.Where(u => u.Id == id).FirstOrDefault();

            return ventaBuscado;
        }

        // Método para listar todas las ventas
        public List<Venta> ListarVenta()
        {
            List<Venta> venta = context.Venta.ToList();

            return venta;
        }

        // Método para crear una venta
        public bool CrearVenta(VentaDTO dto)
        {
            Venta v = VentaMapper.MapearToVenta(dto);

            context.Venta.Add(v);
            context.SaveChanges();

            return true;
        }

        // Método para modificar una venta
        public bool ModificarVenta(int id, VentaDTO ventaDTO)
        {
            Venta VentaBuscada = context.Venta.Where(v => v.Id == id).FirstOrDefault();

            VentaBuscada.Comentarios = ventaDTO.Comentarios;
            VentaBuscada.IdUsuario = ventaDTO.IdUsuario;

            context.Venta.Update(VentaBuscada);
            context.SaveChanges();

            return true;
        }

        // Método para eliminar una venta
        public bool EliminarVenta(int id)
        {
            Venta ventaABorrado = context.Venta.Include(p => p.ProductoVendidos).Where(v => v.Id == id).FirstOrDefault();

            if (ventaABorrado is not null)
            {
                context.Venta.Remove(ventaABorrado);
                context.SaveChanges();
                return true;
            }

            return false;
        }
    }


}
