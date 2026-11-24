using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace CallCenterWeb
{
    public partial class Reaperturas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Solo Supervisor y Administrador pueden entrar
            Usuario usuario = (Usuario)Session["Usuario"];
            if (usuario == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }
            if (usuario.Perfil != Perfil.Supervisor && usuario.Perfil != Perfil.Administrador)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                CargarGrilla();
                CargarTarjetas();
            }
        }

        private void CargarTarjetas()
        {
            SolicitudReaperturaNegocio negocio = new SolicitudReaperturaNegocio();
            litPendientes.Text = negocio.ContarPorEstado(EstadoSolicitud.Pendiente).ToString();
            litAprobadas.Text = negocio.ContarPorEstado(EstadoSolicitud.Aprobada).ToString();
            litRechazadas.Text = negocio.ContarPorEstado(EstadoSolicitud.Rechazada).ToString();
        }

        private void CargarGrilla()
        {
            int? idEstado = null;
            if (ddlEstadoSolicitud.SelectedValue != "")
                idEstado = int.Parse(ddlEstadoSolicitud.SelectedValue);

            List<SolicitudReapertura> lista = new SolicitudReaperturaNegocio().Listar(idEstado);

            // Búsqueda por número de incidencia o por nombre/apellido del cliente
            string busqueda = txtBuscar.Text.Trim().ToUpper();
            if (busqueda != "")
            {
                int numero;
                if (int.TryParse(busqueda, out numero))
                    lista = lista.FindAll(x => x.Incidencia.IdIncidencia == numero);
                else
                    lista = lista.FindAll(x => x.Incidencia.Cliente.Apellido.ToUpper().Contains(busqueda)
                                            || x.Incidencia.Cliente.Nombre.ToUpper().Contains(busqueda));
            }

            dgvSolicitudes.DataSource = lista;
            dgvSolicitudes.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
            pnlRechazar.Visible = false;
            CargarGrilla();
        }

        protected void dgvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idSolicitud = int.Parse(e.CommandArgument.ToString());
            Usuario supervisor = (Usuario)Session["Usuario"];
            lblMensaje.Visible = false;

            if (e.CommandName == "Aprobar")
            {
                try
                {
                    new SolicitudReaperturaNegocio().Aprobar(idSolicitud, supervisor.IdUsuario);
                    MostrarMensaje("Reapertura aprobada. La incidencia quedó en estado Reabierto.", "alert alert-success");
                }
                catch (Exception ex)
                {
                    MostrarMensaje("No se pudo aprobar: " + ex.Message, "alert alert-danger");
                }

                pnlRechazar.Visible = false;
                CargarGrilla();
                CargarTarjetas();
            }
            else if (e.CommandName == "Rechazar")
            {
                // Guarda qué solicitud se eligió y muestra el recuadro
                hfIdSolicitud.Value = idSolicitud.ToString();
                txtComentarioRechazo.Text = "";
                pnlRechazar.Visible = true;
            }
        }

        protected void btnConfirmarRechazo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtComentarioRechazo.Text))
            {
                MostrarMensaje("Escribí el motivo del rechazo.", "alert alert-warning");
                return;
            }

            try
            {
                Usuario supervisor = (Usuario)Session["Usuario"];
                int idSolicitud = int.Parse(hfIdSolicitud.Value);

                new SolicitudReaperturaNegocio().Rechazar(idSolicitud, supervisor.IdUsuario, txtComentarioRechazo.Text.Trim());
                MostrarMensaje("La solicitud fue rechazada.", "alert alert-secondary");

                pnlRechazar.Visible = false;
                CargarGrilla();
                CargarTarjetas();
            }
            catch (Exception ex)
            {
                MostrarMensaje("No se pudo rechazar: " + ex.Message, "alert alert-danger");
            }
        }

        protected void btnCancelarRechazo_Click(object sender, EventArgs e)
        {
            pnlRechazar.Visible = false;
            lblMensaje.Visible = false;
        }

        private void MostrarMensaje(string texto, string clase)
        {
            lblMensaje.Text = texto;
            lblMensaje.CssClass = clase + " d-block";
            lblMensaje.Visible = true;
        }

        // ----- Ayudas para la grilla -----

        protected bool EsPendiente(object estado)
        {
            return (EstadoSolicitud)estado == EstadoSolicitud.Pendiente;
        }

        protected string ClaseSolicitud(object estado)
        {
            switch ((EstadoSolicitud)estado)
            {
                case EstadoSolicitud.Pendiente: return "badge bg-warning text-dark";
                case EstadoSolicitud.Aprobada: return "badge bg-success";
                default: return "badge bg-secondary";
            }
        }

        protected string ClaseEstado(object estado)
        {
            switch ((EstadoIncidencia)estado)
            {
                case EstadoIncidencia.Resuelto: return "badge bg-success";
                case EstadoIncidencia.Cerrado: return "badge bg-dark";
                case EstadoIncidencia.Reabierto: return "badge bg-danger";
                default: return "badge bg-secondary";
            }
        }

        protected string TextoEstado(object estado)
        {
            EstadoIncidencia e = (EstadoIncidencia)estado;
            if (e == EstadoIncidencia.EnAnalisis)
                return "En análisis";
            return e.ToString();
        }
    }
}