using WebApiLautaroIriazabal.DTO;
using WebApiLautaroIriazabal.Models;

namespace WebApiLautaroIriazabal.Mapper
{
    public class MapperVenta
    {
        // Método para mapear un objeto VentaDTO a un objeto Venta
        public static Venta MapearToVenta(VentaDTO dto)
        {
            // Crear una nueva instancia de Venta
            Venta venta = new Venta();

            // Asignar los valores del dto al objeto Venta
            venta.Id = dto.Id;
            venta.Comentarios = dto.Comentarios;
            venta.IdUsuario = dto.IdUsuario;

            // Devolver el objeto Venta
            return venta;
        }

        // Método para mapear un objeto Venta a un objeto VentaDTO
        public VentaDTO MapearToDto(Venta venta)
        {
            // Crear una nueva instancia de VentaDTO
            VentaDTO dto = new VentaDTO();

            // Asignar los valores del objeto Venta al dto
            dto.Id = venta.Id;
            dto.Comentarios = venta.Comentarios;
            dto.IdUsuario = venta.IdUsuario;

            // Devolver el dto
            return dto;
        }

    }
}
