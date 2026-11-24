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
            CargarRecientes();
        }

        private void RellenarEstadisticas()
        {
            IncidenciaEstadisticasGuardia stats = new TipoIncidenciaNegocio().ObtenerEstadisticasGuardia();
            litAbiertas.Text = stats.Abiertas.ToString();
            litAnalisis.Text = stats.Analisis.ToString();
            litResueltas.Text = stats.Resueltas.ToString();
            litReaperturas.Text = stats.Reaperturas.ToString();
        }
        private void CargarRecientes()
        {
            List<Incidencia> lista = new IncidenciaNegocio().Listar();
            List<Incidencia> recientes = new List<Incidencia>();
            string busqueda = txtBuscar.Text.Trim().ToUpper();

            foreach (Incidencia inc in lista)
            {
                // las cerradas no se muestran en el inicio
                if (inc.Estado == EstadoIncidencia.Cerrado)
                    continue;

                if (busqueda != "")
                {
                    int numero;
                    if (int.TryParse(busqueda, out numero))
                    {
                        if (inc.IdIncidencia != numero)
                            continue;
                    }
                    else if (!inc.Cliente.Nombre.ToUpper().Contains(busqueda))
                    {
                        continue;
                    }
                }

                recientes.Add(inc);

                if (recientes.Count == 10)   // como máximo 10
                    break;
            }

            dgvRecientes.DataSource = recientes;
            dgvRecientes.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarRecientes();
        }

        protected string ClasePrioridad(object descripcion)
        {
            switch (descripcion.ToString())
            {
                case "Urgente": return "badge bg-danger";
                case "Alta": return "badge bg-warning text-dark";
                case "Media": return "badge bg-info text-dark";
                default: return "badge bg-secondary";
            }
        }

        protected string ClaseEstado(object estado)
        {
            switch ((EstadoIncidencia)estado)
            {
                case EstadoIncidencia.Abierto: return "badge bg-primary";
                case EstadoIncidencia.Asignado: return "badge bg-secondary";
                case EstadoIncidencia.EnAnalisis: return "badge bg-info text-dark";
                case EstadoIncidencia.Resuelto: return "badge bg-success";
                case EstadoIncidencia.Reabierto: return "badge bg-danger";
                default: return "badge bg-dark";
            }
        }

        protected string TextoEstado(object estado)
        {
            EstadoIncidencia e = (EstadoIncidencia)estado;
            if (e == EstadoIncidencia.EnAnalisis)
                return "En análisis";
            return e.ToString();
        }
    }
}