using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiLautaroIriazabal.Service
{
    public class VentaData
    {
        private CoderContext context;

        public VentaData(CoderContext coderContext)
        {
            // Verifica que el contexto no sea nulo antes de asignarlo.
            this.context = coderContext ?? throw new ArgumentNullException(nameof(coderContext));
        }

        public Venta ObtenerVenta(int id)
        {
            // Verifica que el id sea válido antes de buscar la venta.
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }

            Venta ventaBuscado = context.Venta.Where(u => u.Id == id).FirstOrDefault();

            return ventaBuscado;
        }

        public List<Venta> ListarVenta()
        {
            List<Venta> venta = context.Venta.ToList();

            return venta;
        }

        public bool CrearVenta(Venta venta)
        {
            // Verifica que la venta no sea nula antes de crearla.
            if (venta == null)
            {
                throw new ArgumentNullException(nameof(venta));
            }

            context.Venta.Add(venta);
            context.SaveChanges();

            return true;
        }

        public bool ModificarVenta(Venta venta, int id)
        {
            // Verifica que el id y la venta sean válidos antes de modificar la venta.
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }
            if (venta == null)
            {
                throw new ArgumentNullException(nameof(venta));
            }

            Venta VentaBuscada = context.Venta.Where(v => v.Id == id).FirstOrDefault();

            VentaBuscada.Comentarios = venta.Comentarios;
            VentaBuscada.IdUsuario = venta.IdUsuario;

            context.Venta.Update(VentaBuscada);
            context.SaveChanges();

            return true;
        }

        public bool EliminarVenta(int id)
        {
            // Verifica que el id sea válido antes de eliminar la venta.
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }

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
