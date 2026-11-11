using System;
using System.Collections.Generic;
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
                    return LeerUsuario(datos);

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_ListarUsuarios");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                    lista.Add(LeerUsuario(datos));

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Usuario Obtener(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_ObtenerUsuario");
                datos.SetearParametro("@IdUsuario", idUsuario);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return LeerUsuario(datos);

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Usuario nuevo, string clave)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_AgregarUsuario");
                datos.SetearParametro("@NombreUsuario", nuevo.NombreUsuario);
                datos.SetearParametro("@Clave", clave);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@Email", nuevo.Email);
                datos.SetearParametro("@IdPerfil", (int)nuevo.Perfil);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_ModificarUsuario");
                datos.SetearParametro("@IdUsuario", usuario.IdUsuario);
                datos.SetearParametro("@NombreUsuario", usuario.NombreUsuario);
                datos.SetearParametro("@Nombre", usuario.Nombre);
                datos.SetearParametro("@Apellido", usuario.Apellido);
                datos.SetearParametro("@Email", usuario.Email);
                datos.SetearParametro("@IdPerfil", (int)usuario.Perfil);
                datos.SetearParametro("@Activo", usuario.Activo);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void CambiarClave(int idUsuario, string clave)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_CambiarClaveUsuario");
                datos.SetearParametro("@IdUsuario", idUsuario);
                datos.SetearParametro("@Clave", clave);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        // La clave nunca se carga en el objeto
        private Usuario LeerUsuario(AccesoDatos datos)
        {
            Usuario aux = new Usuario();
            aux.IdUsuario = (int)datos.Lector["IdUsuario"];
            aux.NombreUsuario = (string)datos.Lector["NombreUsuario"];
            aux.Nombre = (string)datos.Lector["Nombre"];
            aux.Apellido = (string)datos.Lector["Apellido"];
            aux.Email = (string)datos.Lector["Email"];
            aux.Perfil = (Perfil)(int)datos.Lector["IdPerfil"];
            aux.Activo = (bool)datos.Lector["Activo"];
            return aux;
        }
    }
}