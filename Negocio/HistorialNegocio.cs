using System;
using System.Collections.Generic;
using Datos;
using Dominio;

namespace Negocio
{
    public class HistorialNegocio
    {
        // Guarda una fila en el historial
        public void Agregar(int idIncidencia, int idUsuario, EstadoIncidencia? estadoAnterior, EstadoIncidencia estadoNuevo, string comentario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO HistorialIncidencias (IdIncidencia, IdUsuario, IdEstadoAnterior, IdEstadoNuevo, Comentario) " +
                                     "VALUES (@IdIncidencia, @IdUsuario, @EstadoAnterior, @EstadoNuevo, @Comentario)");
                datos.SetearParametro("@IdIncidencia", idIncidencia);
                datos.SetearParametro("@IdUsuario", idUsuario);
                datos.SetearParametro("@EstadoAnterior", estadoAnterior.HasValue ? (object)(int)estadoAnterior.Value : null);
                datos.SetearParametro("@EstadoNuevo", (int)estadoNuevo);
                datos.SetearParametro("@Comentario", comentario);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        // Trae el historial de una incidencia, el más nuevo primero
        public List<HistorialIncidencia> Listar(int idIncidencia)
        {
            List<HistorialIncidencia> lista = new List<HistorialIncidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT h.Fecha, u.NombreUsuario, h.IdEstadoAnterior, h.IdEstadoNuevo, h.Comentario " +
                                     "FROM HistorialIncidencias h " +
                                     "INNER JOIN Usuarios u ON u.IdUsuario = h.IdUsuario " +
                                     "WHERE h.IdIncidencia = @IdIncidencia " +
                                     "ORDER BY h.Fecha DESC");
                datos.SetearParametro("@IdIncidencia", idIncidencia);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    HistorialIncidencia h = new HistorialIncidencia();
                    h.Fecha = (DateTime)datos.Lector["Fecha"];

                    h.Usuario = new Usuario();
                    h.Usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];

                    if (!(datos.Lector["IdEstadoAnterior"] is DBNull))
                        h.EstadoAnterior = (EstadoIncidencia)(int)datos.Lector["IdEstadoAnterior"];

                    h.EstadoNuevo = (EstadoIncidencia)(int)datos.Lector["IdEstadoNuevo"];
                    h.Comentario = datos.Lector["Comentario"] is DBNull ? "" : (string)datos.Lector["Comentario"];

                    lista.Add(h);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}