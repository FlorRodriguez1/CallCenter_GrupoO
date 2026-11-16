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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            RellenarEstadisticas();
        }

        private void RellenarEstadisticas()
        {
            IncidenciaEstadisticasGuardia stats = new TipoIncidenciaNegocio().ObtenerEstadisticasGuardia();
            litAbiertas.Text = stats.Abiertas.ToString();
            litAnalisis.Text = stats.Analisis.ToString();
            litResueltas.Text = stats.Resueltas.ToString();
            litReaperturas.Text = stats.Reaperturas.ToString();
        }
    }
}