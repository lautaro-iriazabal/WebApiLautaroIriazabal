namespace WebApiLautaroIriazabal.Models
{
    public partial class Usuario
    {
        // Constructor de la clase Usuario
        public Usuario()
        {
            // Inicialización de las colecciones de productos y ventas
            Productos = new HashSet<Producto>();
            Venta = new HashSet<Venta>();
        }

        // Identificador único del usuario
        public int Id { get; set; }

        // Nombre del usuario
        public string Nombre
        {
            get => Nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre no puede estar vacío.");
                }
                Nombre = value;
            }
        }

        // Apellido del usuario
        public string Apellido
        {
            get => Apellido;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El apellido no puede estar vacío.");
                }
                Apellido = value;
            }
        }

        // Nombre de usuario
        public string NombreUsuario
        {
            get => NombreUsuario;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre de usuario no puede estar vacío.");
                }
                NombreUsuario = value;
            }
        }

        // Contraseña del usuario
        public string Contraseña
        {
            get => Contraseña;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La contraseña no puede estar vacía.");
                }
                Contraseña = value;
            }
        }

        // Correo electrónico del usuario
        public string Mail
        {
            get => Mail;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El correo electrónico no puede estar vacío.");
                }
                Mail = value;
            }
        }

        // Colección de productos asociados a este usuario
        public virtual ICollection<Producto> Productos { get; set; }

        // Colección de ventas asociadas a este usuario
        public virtual ICollection<Venta> Venta { get; set; }
    }

}
