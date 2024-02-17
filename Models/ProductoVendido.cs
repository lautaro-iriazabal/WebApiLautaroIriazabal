using System;
using System.Collections.Generic;

namespace WebApiLautaroIriazabal.Models
{
    public partial class ProductoVendido
    {
        // Identificador único del producto vendido
        public int Id { get; set; }

        // Cantidad de stock del producto vendido
        public int Stock
        {
            get => Stock;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El stock no puede ser negativo.");
                }
                Stock = value;
            }
        }

        // Identificador del producto asociado a este producto vendido
        public int IdProducto { get; set; }

        // Identificador de la venta asociada a este producto vendido
        public int IdVenta { get; set; }

        // Navegación a la entidad Producto asociada a este producto vendido
        public virtual Producto IdProductoNavigation { get; set; } = null!;

        // Navegación a la entidad Venta asociada a este producto vendido
        public virtual Venta IdVentaNavigation { get; set; } = null!;
    }

}
