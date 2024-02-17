using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiLautaroIriazabal.Service
{
    public class UsuarioData
    {
        private CoderContext context;

        public UsuarioData(CoderContext coderContext)
        {
            // Verifica que el contexto no sea nulo antes de asignarlo.
            this.context = coderContext ?? throw new ArgumentNullException(nameof(coderContext));
        }

        public Usuario ObtenerUsuario(int id)
        {
            // Verifica que el id sea válido antes de buscar el usuario.
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }

            Usuario usuarioBuscado = context.Usuarios.Where(u => u.Id == id).FirstOrDefault();

            return usuarioBuscado;
        }

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> usuarios = context.Usuarios.ToList();

            return usuarios;
        }

        public bool CrearUsuario(Usuario usuario)
        {
            // Verifica que el usuario no sea nulo antes de crearlo.
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            context.Usuarios.Add(usuario);
            context.SaveChanges();

            return true;
        }

        public bool ModificarUsuario(Usuario usuario, int id)
        {
            // Verifica que el id y el usuario sean válidos antes de modificar el usuario.
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            Usuario usuarioBuscado = context.Usuarios.Where(u => u.Id == id).FirstOrDefault();

            usuarioBuscado.Nombre = usuario.Nombre;
            usuarioBuscado.Apellido = usuario.Apellido;
            usuarioBuscado.Contraseña = usuario.Contraseña;
            usuarioBuscado.NombreUsuario = usuario.NombreUsuario;
            usuarioBuscado.Mail = usuario.Mail;

            context.Usuarios.Update(usuarioBuscado);
            context.SaveChanges();

            return true;
        }

        public bool EliminarUsuario(int id)
        {
            // Verifica que el id sea válido antes de eliminar el usuario.
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }

            Usuario usuarioBorrado = context.Usuarios.Where(u => u.Id == id).FirstOrDefault();

            if (usuarioBorrado is not null)
            {
                context.Usuarios.Remove(usuarioBorrado);
                context.SaveChanges();
                return true;
            }

            return false;
        }
    }

}
