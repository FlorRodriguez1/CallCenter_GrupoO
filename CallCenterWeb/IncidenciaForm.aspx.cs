using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallCenterWeb
{
    public partial class IncidenciaForm : System.Web.UI.Page
    {
        private int? IdIncidenciaActual
        {
            get { return ViewState["IdIncidenciaActual"] as int?; }
            set { ViewState["IdIncidenciaActual"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarDesplegables();

                    string idParam = Request.QueryString["id"];
                    if (!string.IsNullOrWhiteSpace(idParam) && int.TryParse(idParam, out int idIncidencia) && idIncidencia > 0)
                    {
                        CargarIncidencia(idIncidencia);
                    }
                    else
                    {
                        IdIncidenciaActual = null;
                        btnGuardar.Text = "Crear incidencia";
                        litTituloIncidencia.Text = "Nueva incidencia";
                        litBadgePrioridad.Text = string.Empty;
                        litBadgeEstado.Text = string.Empty;
                        pnlAcciones.Visible = false;
                        pnlDatos.Visible = false;
                        pnlHistorial.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ManejarError(ex);
            }
        }

        private void CargarDesplegables()
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            TipoIncidenciaNegocio tipoIncidenciaNegocio = new TipoIncidenciaNegocio();
            PrioridadNegocio prioridadNegocio = new PrioridadNegocio();

            ddlCliente.DataSource = clienteNegocio.Listar();
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "IdCliente";
            ddlCliente.DataBind();

            ddlTipo.DataSource = tipoIncidenciaNegocio.Listar();
            ddlTipo.DataTextField = "Descripcion";
            ddlTipo.DataValueField = "IdTipo";
            ddlTipo.DataBind();

            ddlPrioridad.DataSource = prioridadNegocio.Listar();
            ddlPrioridad.DataTextField = "Descripcion";
            ddlPrioridad.DataValueField = "IdPrioridad";
            ddlPrioridad.DataBind();

            ddlUsuarioAsignado.DataSource = new UsuarioNegocio().Listar();
            ddlUsuarioAsignado.DataTextField = "NombreUsuario";
            ddlUsuarioAsignado.DataValueField = "IdUsuario";
            ddlUsuarioAsignado.DataBind();
        }

        private void CargarIncidencia(int idIncidencia)
        {
            IncidenciaNegocio negocio = new IncidenciaNegocio();
            Incidencia inc = negocio.Listar(idIncidencia);

            if (inc == null)
            {
                Response.Redirect("Incidencias.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (inc.Prioridad != null)
            {
                string cssPrioridad = ObtenerCssPrioridad(inc.Prioridad.IdPrioridad);
                litBadgePrioridad.Text = $"<span class=\"badge {cssPrioridad} fs-6\">{inc.Prioridad.Descripcion}</span> ";
            }


            {
                string estadoTexto = inc.Estado.ToString();
                string cssEstado = ObtenerCssEstado(estadoTexto);
                string textoMostrar = estadoTexto == "EnAnalisis" ? "En análisis" : estadoTexto;
                litBadgeEstado.Text = $"<span class=\"badge {cssEstado} fs-6\">{textoMostrar}</span>";
            }

            litTituloIncidencia.Text = $"Incidencia N° {inc.IdIncidencia} ";
            IdIncidenciaActual = inc.IdIncidencia;
            btnGuardar.Text = "Guardar cambios";

            SeleccionarValor(ddlCliente, inc.Cliente?.IdCliente);
            SeleccionarValor(ddlTipo, inc.Tipo?.IdTipo);
            SeleccionarValor(ddlPrioridad, inc.Prioridad?.IdPrioridad);

            txtProblematica.Text = inc.Problematica ?? string.Empty;
            litEstado.Text = litBadgeEstado.Text;
            litCreador.Text = inc.UsuarioCreador.Nombre;
            litAsignado.Text = inc.UsuarioAsignado.Nombre;
            litFechaCreacion.Text = inc.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
            litFechaResolucion.Text = inc.FechaResolucion.HasValue ? inc.FechaResolucion.Value.ToString("dd/MM/yyyy") : "-";
            dgvHistorial.DataSource = new HistorialNegocio().Listar(inc.IdIncidencia);
            dgvHistorial.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlCliente.SelectedValue) ||
                    string.IsNullOrEmpty(ddlTipo.SelectedValue) ||
                    string.IsNullOrEmpty(ddlPrioridad.SelectedValue) ||
                    string.IsNullOrWhiteSpace(txtProblematica.Text))
                {
                    lblMensaje.Text = "Completá cliente, tipo, prioridad y problemática.";
                    lblMensaje.Visible = true;
                    return;
                }

                Usuario usuarioLogueado = Session["Usuario"] as Usuario;
                if (usuarioLogueado == null)
                {
                    Response.Redirect("Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                IncidenciaNegocio negocio = new IncidenciaNegocio();

                if (IdIncidenciaActual.HasValue)
                {
                    Incidencia existente = negocio.Listar(IdIncidenciaActual.Value);
                    if (existente == null)
                    {
                        Response.Redirect("Incidencias.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }

                    existente.Cliente = new Cliente { IdCliente = int.Parse(ddlCliente.SelectedValue) };
                    existente.Tipo = new TipoIncidencia { IdTipo = int.Parse(ddlTipo.SelectedValue) };
                    existente.Prioridad = new Prioridad { IdPrioridad = int.Parse(ddlPrioridad.SelectedValue) };
                    existente.Problematica = txtProblematica.Text.Trim();

                    negocio.Modificar(existente);
                    new HistorialNegocio().Agregar(existente.IdIncidencia, usuarioLogueado.IdUsuario,
                    existente.Estado, existente.Estado, "Se modificaron los datos del reclamo.");
                    Response.Redirect($"Incidencias.aspx?editada={existente.IdIncidencia}", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Incidencia nueva = new Incidencia
                    {
                        Cliente = new Cliente { IdCliente = int.Parse(ddlCliente.SelectedValue) },
                        Tipo = new TipoIncidencia { IdTipo = int.Parse(ddlTipo.SelectedValue) },
                        Prioridad = new Prioridad { IdPrioridad = int.Parse(ddlPrioridad.SelectedValue) },
                        Estado = EstadoIncidencia.Abierto,
                        UsuarioCreador = new Usuario { IdUsuario = usuarioLogueado.IdUsuario },
                        UsuarioAsignado = new Usuario { IdUsuario = usuarioLogueado.IdUsuario },
                        Problematica = txtProblematica.Text.Trim()
                    };

                    int idGenerado = negocio.Agregar(nueva);
                    new HistorialNegocio().Agregar(idGenerado, usuarioLogueado.IdUsuario,
                    null, EstadoIncidencia.Abierto, "Alta de la incidencia.");
                    Response.Redirect($"Incidencias.aspx?creada={idGenerado}", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }


            catch (Exception ex)
            {
                ManejarError(ex);
            }
        }

        protected void btnResolver_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtComentarioRespuesta.Text))
            {
                MostrarMensaje("Para resolver hay que escribir la respuesta al cliente.");
                return;
            }

            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                Incidencia inc = new IncidenciaNegocio().Listar(IdIncidenciaActual.Value);

                new IncidenciaNegocio().Resolver(inc.IdIncidencia, txtComentarioRespuesta.Text.Trim());
                new HistorialNegocio().Agregar(inc.IdIncidencia, usuario.IdUsuario,
                    inc.Estado, EstadoIncidencia.Resuelto, "Resuelta: " + txtComentarioRespuesta.Text.Trim());

                RecargarPagina();
            }
            catch (Exception ex)
            {
                ManejarError(ex);
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtComentarioCierre.Text))
            {
                MostrarMensaje("Para cerrar hay que escribir un comentario de cierre.");
                return;
            }

            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                Incidencia inc = new IncidenciaNegocio().Listar(IdIncidenciaActual.Value);

                new IncidenciaNegocio().Cerrar(inc.IdIncidencia, txtComentarioCierre.Text.Trim());
                new HistorialNegocio().Agregar(inc.IdIncidencia, usuario.IdUsuario,
                    inc.Estado, EstadoIncidencia.Cerrado, "Cerrada: " + txtComentarioCierre.Text.Trim());

                RecargarPagina();
            }
            catch (Exception ex)
            {
                ManejarError(ex);
            }
        }

        protected void btnReasignar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlUsuarioAsignado.SelectedValue))
            {
                MostrarMensaje("Elegí a qué usuario reasignar la incidencia.");
                return;
            }

            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                Incidencia inc = new IncidenciaNegocio().Listar(IdIncidenciaActual.Value);
                int idNuevo = int.Parse(ddlUsuarioAsignado.SelectedValue);

                new IncidenciaNegocio().Reasignar(inc.IdIncidencia, idNuevo);
                new HistorialNegocio().Agregar(inc.IdIncidencia, usuario.IdUsuario,
                    inc.Estado, EstadoIncidencia.Asignado, "Reasignada a " + ddlUsuarioAsignado.SelectedItem.Text);

                RecargarPagina();
            }
            catch (Exception ex)
            {
                ManejarError(ex);
            }
        }

        protected void btnSolicitarReapertura_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMotivoReapertura.Text))
            {
                MostrarMensaje("Escribí el motivo de la reapertura.");
                return;
            }

            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                Incidencia inc = new IncidenciaNegocio().Listar(IdIncidenciaActual.Value);

                // Solo se puede pedir reapertura de una incidencia resuelta o cerrada
                if (inc.Estado != EstadoIncidencia.Resuelto && inc.Estado != EstadoIncidencia.Cerrado)
                {
                    MostrarMensaje("Solo se puede pedir reapertura de una incidencia resuelta o cerrada.");
                    return;
                }

                new SolicitudReaperturaNegocio().Solicitar(inc.IdIncidencia, usuario.IdUsuario, txtMotivoReapertura.Text.Trim());
                new HistorialNegocio().Agregar(inc.IdIncidencia, usuario.IdUsuario,
                    inc.Estado, inc.Estado, "Solicitud de reapertura: " + txtMotivoReapertura.Text.Trim());

                RecargarPagina();
            }
            catch (Exception ex)
            {
                ManejarError(ex);
            }
        }

        // Vuelve a abrir la misma incidencia para ver los cambios
        private void RecargarPagina()
        {
            Response.Redirect("IncidenciaForm.aspx?id=" + IdIncidenciaActual.Value, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;
        }

        private void SeleccionarValor(DropDownList ddl, int? id)
        {
            if (id.HasValue)
            {
                string valor = id.Value.ToString();
                if (ddl.Items.FindByValue(valor) != null)
                {
                    ddl.SelectedValue = valor;
                }
            }
        }

        private string ObtenerCssPrioridad(int idPrioridad)
        {
            switch (idPrioridad)
            {
                case 1:
                    return "bg-danger";
                case 2:
                    return "bg-warning text-dark";
                default:
                    return "bg-secondary";
            }
        }

        private string ObtenerCssEstado(string estado)
        {
            switch (estado)
            {
                case "EnAnalisis":
                    return "bg-info text-dark";
                case "Reabierto":
                    return "bg-danger";
                case "Abierto":
                    return "bg-primary";
                case "Resuelto":
                case "Cerrado":
                    return "bg-success";
                default:
                    return "bg-secondary";
            }
        }

        private void ManejarError(Exception ex)
        {
            Session["Error"] = ex.ToString();
            Response.Redirect("Error.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
