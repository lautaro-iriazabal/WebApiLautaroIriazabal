namespace WebApiLautaroIriazabal.DTO
{
    public class ProductoDTO
    {
        // Identificador único del producto
        public int Id { get; set; }

        // Descripción del producto
        public string Descripciones { get; set; } = null!;

        // Costo del producto. Puede ser nulo si el costo no está definido.
        public decimal? Costo { get; set; }

        // Precio de venta del producto
        public decimal PrecioVenta { get; set; }

        // Cantidad de stock disponible para el producto
        public int Stock { get; set; }

        // Identificador del usuario que creó el producto
        public int IdUsuario { get; set; }
    }

}
