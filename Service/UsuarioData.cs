using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiLautaroIriazabal.DTO;
using WebApiLautaroIriazabal.Mapper;

namespace WebApiLautaroIriazabal.Service
{
    public class UsuarioData
    {
        // Definición de las propiedades de la clase
        private CoderContext context;
        private UsuarioMapper usuarioMapper;

        // Constructor de la clase
        public UsuarioData(CoderContext coderContext, UsuarioMapper usuarioMapper)
        {
            this.context = coderContext;
            this.usuarioMapper = usuarioMapper;
        }

        // Método para obtener un usuario por su nombre de usuario
        public Usuario ObtenerUsuarioPorNombreDeUsuario(string nombreDeUsuario)
        {
            Usuario? usuarioBuscado = context.Usuarios.Where(u => u.NombreUsuario == nombreDeUsuario).FirstOrDefault();
            return usuarioBuscado;
        }

        // Método para obtener un usuario por su nombre de usuario y contraseña
        public Usuario ObtenerUsuarioYPasswordDeUsuario(string Usuario, string Password)
        {
            Usuario? usuarioBuscado = context.Usuarios.Where(u => u.NombreUsuario == Usuario && u.Contraseña == Password).FirstOrDefault();
            return usuarioBuscado;
        }

        // Método para obtener un usuario por su ID
        public Usuario ObtenerUsuario(int id)
        {
            Usuario usuarioBuscado = context.Usuarios.Where(u => u.Id == id).FirstOrDefault();
            return usuarioBuscado;
        }

        // Método para listar todos los usuarios
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> usuarios = context.Usuarios.ToList();
            return usuarios;
        }

        // Método para crear un usuario
        public bool CrearUsuario(UsuarioDTO dto)
        {
            Usuario u = usuarioMapper.MapearToUsuario(dto);

            context.Usuarios.Add(u);
            context.SaveChanges();

            return true;
        }

        // Método para modificar un usuario
        public bool ModificarUsuario(UsuarioDTO usuarioDTO)
        {
            Usuario usuarioBuscado = usuarioMapper.MapearToUsuario(usuarioDTO);

            context.Usuarios.Update(usuarioBuscado);
            context.SaveChanges();

            return true;
        }

        // Método para eliminar un usuario
        public bool EliminarUsuario(int id)
        {
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
