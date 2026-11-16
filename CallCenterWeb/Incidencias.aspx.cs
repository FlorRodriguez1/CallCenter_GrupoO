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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            TipoIncidenciaNegocio tipoIncidenciaNegocio = new TipoIncidenciaNegocio();
            PrioridadNegocio prioridadNegocio = new PrioridadNegocio();

            try
            {
                ddlEstado.DataSource = Enum.GetValues(typeof(EstadoIncidencia))
                    .Cast<EstadoIncidencia>()
                    .Select(est => new
                    {
                        IdEstado = (int)est,
                        Descripcion = est == EstadoIncidencia.EnAnalisis ? "En análisis" : est.ToString()
                    })
                    .ToList();
                ddlEstado.DataBind();

                ddlTipo.DataSource = tipoIncidenciaNegocio.Listar();
                ddlTipo.DataBind();

                ddlPrioridad.DataSource = prioridadNegocio.Listar();
                ddlPrioridad.DataBind();

                RellenarEstadisticas();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private void RellenarEstadisticas()
        {
            IncidenciaEstadisticas stats = new TipoIncidenciaNegocio().ObtenerEstadisticas();
            litTotal.Text = stats.Total.ToString();
            litAbiertas.Text = stats.Abiertas.ToString();
            litAnalisis.Text = stats.Analisis.ToString();
            litResueltas.Text = stats.Resueltas.ToString();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            int? idIncidencia = int.TryParse(txtNumero.Text.Trim(), out int num) ? num : (int?)null;
            int? idEstado = int.TryParse(ddlEstado.SelectedValue, out int est) ? est : (int?)null;
            int? idTipo = int.TryParse(ddlTipo.SelectedValue, out int tip) ? tip : (int?)null;
            int? idPrioridad = int.TryParse(ddlPrioridad.SelectedValue, out int prio) ? prio : (int?)null;

            IncidenciaNegocio negocio = new IncidenciaNegocio();
            dgvIncidencias.DataSource = negocio.Filtrar(idIncidencia, idEstado, idTipo, idPrioridad);
            dgvIncidencias.DataBind();
        }
    }
}