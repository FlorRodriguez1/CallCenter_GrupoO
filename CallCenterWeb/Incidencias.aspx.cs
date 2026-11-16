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

            RellenarEstadisticas();
        }

        private void RellenarEstadisticas()
        {
            IncidenciaEstadisticas stats = new TipoIncidenciaNegocio().ObtenerEstadisticas();
            litTotal.Text = stats.Total.ToString();
            litAbiertas.Text = stats.Abiertas.ToString();
            litAnalisis.Text = stats.Analisis.ToString();
            litResueltas.Text = stats.Resueltas.ToString();
        }
    }
}