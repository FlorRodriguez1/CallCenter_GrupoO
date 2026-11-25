using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class IncidenciaNegocio
    {


        public int Agregar(Incidencia nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_AgregarIncidencia");
                datos.SetearParametro("@IdCliente", nueva.Cliente.IdCliente);
                datos.SetearParametro("@IdTipo", nueva.Tipo.IdTipo);
                datos.SetearParametro("@IdPrioridad", nueva.Prioridad.IdPrioridad);
                datos.SetearParametro("@IdEstado", (int)nueva.Estado);
                datos.SetearParametro("@IdUsuarioCreador", nueva.UsuarioCreador.IdUsuario);
                datos.SetearParametro("@IdUsuarioAsignado", nueva.UsuarioAsignado.IdUsuario);
                datos.SetearParametro("@Problematica", nueva.Problematica);

                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IdIncidencia"];
                }

                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Incidencia> Listar()
        {
            List<Incidencia> lista = new List<Incidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT * FROM VW_Incidencias ORDER BY IdIncidencia DESC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(LeerIncidencia(datos));
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Incidencia Listar(int? idIncidencia)
        {

            Incidencia inc = new Incidencia();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_FiltrarIncidencias");
                datos.SetearParametro("@IdIncidencia", (object)idIncidencia ?? DBNull.Value);

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    inc = LeerIncidencia(datos);
                }

                return inc;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public List<Incidencia> Filtrar(int? idIncidencia, int? idEstado, int? idTipo, int? idPrioridad)
        {
            List<Incidencia> lista = new List<Incidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_FiltrarIncidencias");
                datos.SetearParametro("@IdIncidencia", (object)idIncidencia ?? DBNull.Value);
                datos.SetearParametro("@IdEstado", (object)idEstado ?? DBNull.Value);
                datos.SetearParametro("@IdTipo", (object)idTipo ?? DBNull.Value);
                datos.SetearParametro("@IdPrioridad", (object)idPrioridad ?? DBNull.Value);

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(LeerIncidencia(datos));
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public int Modificar(Incidencia inc)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_ModificarIncidencia");
                datos.SetearParametro("@IdCliente", inc.Cliente.IdCliente);
                datos.SetearParametro("@IdTipo", inc.Tipo.IdTipo);
                datos.SetearParametro("@IdPrioridad", inc.Prioridad.IdPrioridad);
                datos.SetearParametro("@IdEstado", (int)inc.Estado);
                datos.SetearParametro("@IdIncidencia", inc.IdIncidencia);
                datos.SetearParametro("@Problematica", inc.Problematica);

                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IdIncidencia"];
                }

                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Resolver(int idIncidencia, string respuesta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Incidencias SET IdEstado = 4, FechaResolucion = GETDATE(), ComentarioRespuesta = @Comentario WHERE IdIncidencia = @IdIncidencia");
                datos.SetearParametro("@Comentario", respuesta);
                datos.SetearParametro("@IdIncidencia", idIncidencia);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Cerrar(int idIncidencia, string comentario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Incidencias SET IdEstado = 6, FechaCierre = GETDATE(), ComentarioCierre = @Comentario WHERE IdIncidencia = @IdIncidencia");
                datos.SetearParametro("@Comentario", comentario);
                datos.SetearParametro("@IdIncidencia", idIncidencia);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Reasignar(int idIncidencia, int idUsuarioNuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Incidencias SET IdEstado = 2, IdUsuarioAsignado = @IdUsuario WHERE IdIncidencia = @IdIncidencia");
                datos.SetearParametro("@IdUsuario", idUsuarioNuevo);
                datos.SetearParametro("@IdIncidencia", idIncidencia);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private Incidencia LeerIncidencia(AccesoDatos datos)
        {
            Incidencia aux = new Incidencia();
            aux.IdIncidencia = (int)datos.Lector["IdIncidencia"];
            aux.Problematica = datos.Lector["Problematica"] is DBNull ? string.Empty : (string)datos.Lector["Problematica"];
            aux.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
            aux.Estado = (EstadoIncidencia)(int)datos.Lector["IdEstado"];

            if (!(datos.Lector["FechaResolucion"] is DBNull))
                aux.FechaResolucion = (DateTime)datos.Lector["FechaResolucion"];

            if (!(datos.Lector["FechaCierre"] is DBNull))
                aux.FechaCierre = (DateTime)datos.Lector["FechaCierre"];

            if (!(datos.Lector["ComentarioRespuesta"] is DBNull))
                aux.ComentarioRespuesta = (string)datos.Lector["ComentarioRespuesta"];

            if (!(datos.Lector["ComentarioCierre"] is DBNull))
                aux.ComentarioCierre = (string)datos.Lector["ComentarioCierre"];

            aux.Cliente = new Cliente();
            aux.Cliente.IdCliente = (int)datos.Lector["IdCliente"];
            aux.Cliente.Nombre = datos.Lector["Cliente"] is DBNull ? string.Empty : (string)datos.Lector["Cliente"];
            aux.Cliente.Email = datos.Lector["EmailCliente"] is DBNull ? string.Empty : (string)datos.Lector["EmailCliente"];

            aux.Tipo = new TipoIncidencia();
            aux.Tipo.IdTipo = (int)datos.Lector["IdTipo"];
            aux.Tipo.Descripcion = datos.Lector["Tipo"] is DBNull ? string.Empty : (string)datos.Lector["Tipo"];

            aux.Prioridad = new Prioridad();
            aux.Prioridad.IdPrioridad = (int)datos.Lector["IdPrioridad"];
            aux.Prioridad.Descripcion = datos.Lector["Prioridad"] is DBNull ? string.Empty : (string)datos.Lector["Prioridad"];

            aux.UsuarioCreador = new Usuario();
            aux.UsuarioCreador.IdUsuario = (int)datos.Lector["IdUsuarioCreador"];
            aux.UsuarioCreador.Nombre = datos.Lector["UsuarioCreador"] is DBNull ? string.Empty : (string)datos.Lector["UsuarioCreador"];

            aux.UsuarioAsignado = new Usuario();
            aux.UsuarioAsignado.IdUsuario = (int)datos.Lector["IdUsuarioAsignado"];
            aux.UsuarioAsignado.Nombre = datos.Lector["UsuarioAsignado"] is DBNull ? string.Empty : (string)datos.Lector["UsuarioAsignado"];

            return aux;
        }

    }
}
