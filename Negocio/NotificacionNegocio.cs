using System;
using System.Net;
using Datos;

namespace Negocio
{
    public class NotificacionNegocio
    {
        // Devuelve true si el mail salió. Nunca lanza excepción: un fallo del mail
        // no tendria que deshacer ni frenar el cambio de estado.
        public bool NotificarCambioEstado(int idIncidencia)
        {
            try
            {
                string email = null, cliente = null, tipo = null, estado = null, comentario = null;
                int idEstado = 0;

                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.SetearProcedimiento("SP_ObtenerIncidencia");
                    datos.SetearParametro("@IdIncidencia", idIncidencia);
                    datos.EjecutarLectura();

                    if (!datos.Lector.Read())
                        return false;

                    email = (string)datos.Lector["EmailCliente"];
                    cliente = (string)datos.Lector["Cliente"];
                    tipo = (string)datos.Lector["Tipo"];
                    estado = (string)datos.Lector["Estado"];
                    idEstado = (int)datos.Lector["IdEstado"];

                    if (idEstado == 4)          // Resuelto
                        comentario = datos.Lector["ComentarioRespuesta"] as string;
                    else if (idEstado == 6)     // Cerrado
                        comentario = datos.Lector["ComentarioCierre"] as string;
                }
                finally
                {
                    datos.CerrarConexion();
                }

                string cuerpo = "<p>Hola " + WebUtility.HtmlEncode(cliente) + ",</p>" +
                                "<p>Tu incidencia <b>N° " + idIncidencia + "</b> (" + WebUtility.HtmlEncode(tipo) +
                                ") cambió de estado a <b>" + WebUtility.HtmlEncode(estado) + "</b>.</p>";

                if (!string.IsNullOrWhiteSpace(comentario))
                    cuerpo += "<p><b>Comentario:</b> " + WebUtility.HtmlEncode(comentario) + "</p>";

                cuerpo += "<p>Gracias por comunicarte con nosotros.</p>";

                EmailService servicio = new EmailService();
                servicio.ArmarCorreo(email, "Incidencia N° " + idIncidencia + ": " + estado, cuerpo);
                servicio.EnviarEmail();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("No se pudo enviar el mail: " + ex.Message);
                return false;
            }
        }
    }
}