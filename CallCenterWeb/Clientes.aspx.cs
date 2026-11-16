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
            lblInfo.Visible = false;
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
            } else if (ddlEstado.SelectedValue == "0")
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

            if (ddlFiltro.SelectedIndex == 0 || string.IsNullOrWhiteSpace(txtBuscar.Text)) {
                return;
            }

            List<Cliente> lista = new ClienteNegocio().Listar();
            List<Cliente> listaFiltrada = null;
            string busqueda = txtBuscar.Text.Trim();

            bool esNumero = Regex.IsMatch(busqueda, @"\d+");

            if (ddlFiltro.SelectedIndex == 1) {
                
                if (!esNumero)
                {
                    lblInfo.Text = "<strong>¡No!</strong> Debes insertar números.";
                    lblInfo.CssClass = "alert alert-danger p-3";
                    lblInfo.Visible = true;
                }

                listaFiltrada = lista.FindAll(x => x.DNI.Contains(busqueda));
            }

            if (ddlFiltro.SelectedIndex == 2) {

                if (esNumero)
                {
                    lblInfo.Text = "<strong>¡No!</strong> Debes insertar letras.";
                    lblInfo.CssClass = "alert alert-danger p-3";
                    lblInfo.Visible = true;
                }

                listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(busqueda.ToUpper()));
            }

            if(ddlFiltro.SelectedIndex == 3)
            {
                if (esNumero)
                {
                    lblInfo.Text = "<strong>¡No!</strong> Debes insertar letras.";
                    lblInfo.CssClass = "alert alert-danger p-3";
                    lblInfo.Visible = true;
                }

                listaFiltrada = lista.FindAll(x => x.Apellido.ToUpper().Contains(busqueda.ToUpper()));
            }

            if (listaFiltrada != null)
            {
                dgvClientes.DataSource = listaFiltrada;
                dgvClientes.DataBind();
            }
            return;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            if(lblInfo.Visible == true)
            {
                CargarGrilla();
            }
            return;
        }
    }
}