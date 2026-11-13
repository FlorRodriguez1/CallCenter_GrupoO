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
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClienteNegocio negocio = new ClienteNegocio();
                dgvClientes.DataSource = negocio.Listar();
                dgvClientes.DataBind();


                
            }
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            dgvClientes.PageIndex = e.NewPageIndex;
            dgvClientes.DataSource = negocio.Listar();
            dgvClientes.DataBind();
        }

        protected void rellenarEstadisticas()
        {
            ClienteNegocio negocio = new ClienteNegocio();
            ClienteEstadisticas stats = new ClienteEstadisticas();

            stats = negocio.ObtenerEstadisticas();

            lblClientes.Text = stats.Total.ToString();
            lblClientesActivos.Text = stats.Activos.ToString();
            //LitClientesReclamos.Text = stats.ConReclamos.ToString();

        }
    }
}