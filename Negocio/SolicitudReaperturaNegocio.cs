using System;
using System.Collections.Generic;
using Datos;
using Dominio;

namespace Negocio
{
    public class SolicitudReaperturaNegocio
    {
        // Si idEstado es null trae todas
        public List<SolicitudReapertura> Listar(int? idEstado)
        {
            List<SolicitudReapertura> lista = new List<SolicitudReapertura>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta =
                    "SELECT S.IdSolicitud, S.Motivo, S.FechaSolicitud, S.IdEstadoSolicitud, " +
                    "I.IdIncidencia, I.IdEstado, " +
                    "C.Nombre, C.Apellido, C.DNI, " +
                    "U.IdUsuario, U.NombreUsuario " +
                    "FROM SolicitudesReapertura S " +
                    "INNER JOIN Incidencias I ON I.IdIncidencia = S.IdIncidencia " +
                    "INNER JOIN Clientes C ON C.IdCliente = I.IdCliente " +
                    "INNER JOIN Usuarios U ON U.IdUsuario = S.IdUsuarioSolicitante ";

                if (idEstado != null)
                {
                    consulta += "WHERE S.IdEstadoSolicitud = @IdEstado ";
                    datos.SetearParametro("@IdEstado", idEstado.Value);
                }

                consulta += "ORDER BY S.FechaSolicitud DESC";

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    SolicitudReapertura aux = new SolicitudReapertura();
                    aux.IdSolicitud = (int)datos.Lector["IdSolicitud"];
                    aux.Motivo = (string)datos.Lector["Motivo"];
                    aux.FechaSolicitud = (DateTime)datos.Lector["FechaSolicitud"];
                    aux.Estado = (EstadoSolicitud)(int)datos.Lector["IdEstadoSolicitud"];

                    aux.Incidencia = new Incidencia();
                    aux.Incidencia.IdIncidencia = (int)datos.Lector["IdIncidencia"];
                    aux.Incidencia.Estado = (EstadoIncidencia)(int)datos.Lector["IdEstado"];

                    aux.Incidencia.Cliente = new Cliente();
                    aux.Incidencia.Cliente.Nombre = (string)datos.Lector["Nombre"];
                    aux.Incidencia.Cliente.Apellido = (string)datos.Lector["Apellido"];
                    aux.Incidencia.Cliente.DNI = (string)datos.Lector["DNI"];

                    aux.Solicitante = new Usuario();
                    aux.Solicitante.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.Solicitante.NombreUsuario = (string)datos.Lector["NombreUsuario"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public int ContarPorEstado(EstadoSolicitud estado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM SolicitudesReapertura WHERE IdEstadoSolicitud = @IdEstado");
                datos.SetearParametro("@IdEstado", (int)estado);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0];

                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Aprobar(int idSolicitud, int idSupervisor)
        {
            Incidencia incidencia = ObtenerIncidenciaDeSolicitud(idSolicitud);
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // 1) la solicitud pasa a Aprobada (2)
                // 2) la incidencia pasa a Reabierto (5) y se borran las fechas de resolución y cierre
                // 3) se guarda el cambio en el historial
                datos.SetearConsulta(
                    "UPDATE SolicitudesReapertura SET IdEstadoSolicitud = 2, IdUsuarioSupervisor = @IdSupervisor, FechaRespuesta = GETDATE() " +
                    "WHERE IdSolicitud = @IdSolicitud; " +
                    "UPDATE Incidencias SET IdEstado = 5, FechaResolucion = NULL, FechaCierre = NULL " +
                    "WHERE IdIncidencia = @IdIncidencia; " +
                    "INSERT INTO HistorialIncidencias (IdIncidencia, IdUsuario, IdEstadoAnterior, IdEstadoNuevo, Comentario) " +
                    "VALUES (@IdIncidencia, @IdSupervisor, @EstadoAnterior, 5, @Comentario)");
                datos.SetearParametro("@IdSolicitud", idSolicitud);
                datos.SetearParametro("@IdSupervisor", idSupervisor);
                datos.SetearParametro("@IdIncidencia", incidencia.IdIncidencia);
                datos.SetearParametro("@EstadoAnterior", (int)incidencia.Estado);
                datos.SetearParametro("@Comentario", "Reapertura aprobada");
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Solicitar(int idIncidencia, int idUsuario, string motivo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO SolicitudesReapertura (IdIncidencia, IdUsuarioSolicitante, Motivo) VALUES (@IdIncidencia, @IdUsuario, @Motivo)");
                datos.SetearParametro("@IdIncidencia", idIncidencia);
                datos.SetearParametro("@IdUsuario", idUsuario);
                datos.SetearParametro("@Motivo", motivo);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Rechazar(int idSolicitud, int idSupervisor, string comentario)
        {
            Incidencia incidencia = ObtenerIncidenciaDeSolicitud(idSolicitud);
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // la solicitud pasa a Rechazada (3); la incidencia queda en su mismo estado
                datos.SetearConsulta(
                    "UPDATE SolicitudesReapertura SET IdEstadoSolicitud = 3, IdUsuarioSupervisor = @IdSupervisor, " +
                    "FechaRespuesta = GETDATE(), ComentarioRespuesta = @Comentario " +
                    "WHERE IdSolicitud = @IdSolicitud; " +
                    "INSERT INTO HistorialIncidencias (IdIncidencia, IdUsuario, IdEstadoAnterior, IdEstadoNuevo, Comentario) " +
                    "VALUES (@IdIncidencia, @IdSupervisor, @Estado, @Estado, @ComentarioHistorial)");
                datos.SetearParametro("@IdSolicitud", idSolicitud);
                datos.SetearParametro("@IdSupervisor", idSupervisor);
                datos.SetearParametro("@Comentario", comentario);
                datos.SetearParametro("@IdIncidencia", incidencia.IdIncidencia);
                datos.SetearParametro("@Estado", (int)incidencia.Estado);
                datos.SetearParametro("@ComentarioHistorial", "Reapertura rechazada: " + comentario);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        // Trae el número y el estado actual de la incidencia de esa solicitud
        private Incidencia ObtenerIncidenciaDeSolicitud(int idSolicitud)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(
                    "SELECT I.IdIncidencia, I.IdEstado FROM SolicitudesReapertura S " +
                    "INNER JOIN Incidencias I ON I.IdIncidencia = S.IdIncidencia " +
                    "WHERE S.IdSolicitud = @IdSolicitud");
                datos.SetearParametro("@IdSolicitud", idSolicitud);
                datos.EjecutarLectura();

                Incidencia aux = new Incidencia();
                if (datos.Lector.Read())
                {
                    aux.IdIncidencia = (int)datos.Lector["IdIncidencia"];
                    aux.Estado = (EstadoIncidencia)(int)datos.Lector["IdEstado"];
                }
                return aux;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}