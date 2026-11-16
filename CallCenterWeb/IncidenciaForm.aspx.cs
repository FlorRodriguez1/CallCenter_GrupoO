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
        protected void Page_Load(object sender, EventArgs e)
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            TipoIncidenciaNegocio tipoIncidenciaNegocio = new TipoIncidenciaNegocio();
            PrioridadNegocio prioridadNegocio = new PrioridadNegocio();
            try {

                if (!IsPostBack)
                {
                    ddlCliente.DataSource = clienteNegocio.Listar();
                    ddlTipo.DataSource = tipoIncidenciaNegocio.Listar();
                    ddlPrioridad.DataSource = prioridadNegocio.Listar();

                    ddlCliente.DataBind();
                    ddlTipo.DataBind();
                    ddlPrioridad.DataBind();
                }

            } catch(Exception Ex){ 
               
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlCliente.SelectedValue) ||
                string.IsNullOrEmpty(ddlTipo.SelectedValue) ||
                string.IsNullOrEmpty(ddlPrioridad.SelectedValue) ||
                string.IsNullOrWhiteSpace(txtProblematica.Text))
            {
                return;
            }

            Usuario usuarioLogueado = (Usuario)Session["Usuario"];

            Incidencia nueva = new Incidencia();
            nueva.Cliente = new Cliente { IdCliente = int.Parse(ddlCliente.SelectedValue) };
            nueva.Tipo = new TipoIncidencia { IdTipo = int.Parse(ddlTipo.SelectedValue) };
            nueva.Prioridad = new Prioridad { IdPrioridad = int.Parse(ddlPrioridad.SelectedValue) };
            nueva.Estado = EstadoIncidencia.Abierto;
            nueva.UsuarioCreador = new Usuario { IdUsuario = usuarioLogueado.IdUsuario };
            nueva.UsuarioAsignado = new Usuario { IdUsuario = usuarioLogueado.IdUsuario }; 
            nueva.Problematica = txtProblematica.Text.Trim();

            IncidenciaNegocio negocio = new IncidenciaNegocio();
            int idGenerado = negocio.Agregar(nueva);

            Response.Redirect($"Incidencias.aspx?creada={idGenerado}", false);
        }
    }
}