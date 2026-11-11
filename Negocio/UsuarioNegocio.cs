using System;
using Datos;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        // Devuelve el usuario si las credenciales son correctas y está activo; si no, null
        public Usuario Loguear(string nombreUsuario, string clave)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IdUsuario, NombreUsuario, Nombre, Apellido, Email, IdPerfil, Activo " +
                                     "FROM Usuarios " +
                                     "WHERE NombreUsuario = @nombreUsuario AND Clave = @clave AND Activo = 1");
                datos.SetearParametro("@nombreUsuario", nombreUsuario);
                datos.SetearParametro("@clave", clave);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.Apellido = (string)datos.Lector["Apellido"];
                    usuario.Email = (string)datos.Lector["Email"];
                    usuario.Perfil = (Perfil)(int)datos.Lector["IdPerfil"];
                    usuario.Activo = (bool)datos.Lector["Activo"];
                    return usuario;
                }

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}