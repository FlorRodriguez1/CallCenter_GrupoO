using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

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

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            List<Cliente> lista = new ClienteNegocio().Listar();

            bool esNumero = Regex.IsMatch(txtBuscar.Text, @"\d+");

            if (esNumero)
            {
                List<Cliente> listaFiltrada = lista.FindAll(x => x.DNI.ToUpper().Contains(txtBuscar.Text.ToUpper()));
                dgvClientes.DataSource = listaFiltrada;
                dgvClientes.DataBind();
            }
            else
            {
                List<Cliente> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper()));
                dgvClientes.DataSource = listaFiltrada;
                dgvClientes.DataBind(); 
            }


        }
    }
}