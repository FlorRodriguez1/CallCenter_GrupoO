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

            if (inc.Estado != null)
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
                    Response.Redirect($"Incidencias.aspx?creada={idGenerado}", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                ManejarError(ex);
            }
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
