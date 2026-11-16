using System;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace CallCenterWeb
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            CargarGrilla();
            RellenarEstadisticas();
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            dgvClientes.DataSource = new ClienteNegocio().Listar();
            dgvClientes.DataBind();
        }

        private void RellenarEstadisticas()
        {
            ClienteEstadisticas stats = new ClienteNegocio().ObtenerEstadisticas();
            LitClientes.Text = stats.Total.ToString();
            LitActivos.Text = stats.Activos.ToString();
            LitReclamos.Text = stats.ConReclamos.ToString();
            LitNuevos.Text = stats.NuevosEsteMes.ToString();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstado.SelectedValue == "1") {
                dgvClientes.DataSource = new ClienteNegocio().ListarPorEstado(true);
            }else if (ddlEstado.SelectedValue == "0")
            {
                dgvClientes.DataSource = new ClienteNegocio().ListarPorEstado(false);
            }
            else
            {
                CargarGrilla();
                return; 
            }

            dgvClientes.DataBind();
        }
    }
}