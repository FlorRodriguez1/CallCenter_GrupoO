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
    }
}