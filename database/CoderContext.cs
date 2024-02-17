using System.Collections.Generic;
using System.Reflection.Emit;

namespace WebApiLautaroIriazabal.database
{
    // La clase CoderContext hereda de DbContext y se utiliza para interactuar con la base de datos.
    public partial class CoderContext : DbContext
    {
        // Constructor vacío
        public CoderContext()
        {
        }

        // Constructor que toma opciones de DbContext
        public CoderContext(DbContextOptions<CoderContext> options)
            : base(options)
        {
        }

        // Definición de las tablas de la base de datos
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Venta> Venta { get; set; } = null!;
        public virtual DbSet<ProductoVendido> ProductoVendidos { get; set; } = null!;

        // Método para configurar el modelo de la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario"); // Nombre de la tabla

                // Propiedades de la entidad
                entity.Property(e => e.Nombre).IsUnicode(false);
                entity.Property(e => e.Apellido).IsUnicode(false);
                entity.Property(e => e.NombreUsuario).IsUnicode(false);
                entity.Property(e => e.Mail).IsUnicode(false);
                entity.Property(e => e.Contraseña).IsUnicode(false);
            });

            // Configuración de la entidad Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto"); // Nombre de la tabla

                // Propiedades de la entidad
                entity.Property(e => e.Descripciones).IsUnicode(false);
                entity.Property(e => e.Costo).HasColumnType("money");
                entity.Property(e => e.PrecioVenta).HasColumnType("money");

                // Relación con la entidad Usuario
                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Producto_Usuario");
            });

            // Configuración de la entidad Venta
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.Property(e => e.Comentarios).IsUnicode(false);

                // Relación con la entidad Usuario
                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Venta_Usuario");
            });

            // Configuración de la entidad ProductoVendido
            modelBuilder.Entity<ProductoVendido>(entity =>
            {
                entity.ToTable("ProductoVendido"); // Nombre de la tabla

                // Relación con la entidad Producto
                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductoVendidos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProductoVendido_Producto");

                // Relación con la entidad Venta
                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.ProductoVendidos)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProductoVendido_Venta");
            });

            // Método parcial para configuraciones adicionales
            OnModelCreatingPartial(modelBuilder);
        }

        // Método parcial para ser sobrescrito en caso de necesitar configuraciones adicionales
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
