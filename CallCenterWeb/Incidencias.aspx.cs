using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CallCenterWeb
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

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

                ddlTipo.DataSource = new TipoIncidenciaNegocio().Listar();
                ddlTipo.DataBind();

                ddlPrioridad.DataSource = new PrioridadNegocio().Listar();
                ddlPrioridad.DataBind();

                CargarGrilla();
                RellenarEstadisticas();
            }
            catch (Exception ex)
            {
                lblInfo.Text = "No se pudieron cargar las incidencias: " + ex.Message;
            }
        }

        // Si el usuario es telefonista, deja solo las incidencias asignadas a él
        private List<Incidencia> SoloLasDelUsuario(List<Incidencia> lista)
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Perfil != Perfil.Telefonista)
                return lista;

            List<Incidencia> resultado = new List<Incidencia>();
            foreach (Incidencia inc in lista)
            {
                if (inc.UsuarioAsignado.IdUsuario == usuario.IdUsuario)
                    resultado.Add(inc);
            }
            return resultado;
        }

        private void RellenarEstadisticas()
        {
            List<Incidencia> lista = SoloLasDelUsuario(new IncidenciaNegocio().Listar());
            int abiertas = 0;
            int analisis = 0;
            int resueltas = 0;

            foreach (Incidencia inc in lista)
            {
                if (inc.Estado == EstadoIncidencia.Abierto ||
                    inc.Estado == EstadoIncidencia.Asignado ||
                    inc.Estado == EstadoIncidencia.Reabierto)
                    abiertas++;
                else if (inc.Estado == EstadoIncidencia.EnAnalisis)
                    analisis++;
                else if (inc.Estado == EstadoIncidencia.Resuelto)
                    resueltas++;
            }

            litTotal.Text = lista.Count.ToString();
            litAbiertas.Text = abiertas.ToString();
            litAnalisis.Text = analisis.ToString();
            litResueltas.Text = resueltas.ToString();
        }

        // Carga la grilla usando lo que esté elegido en los filtros
        private void CargarGrilla()
        {
            int? idIncidencia = int.TryParse(txtNumero.Text.Trim(), out int num) ? num : (int?)null;
            int? idEstado = int.TryParse(ddlEstado.SelectedValue, out int est) ? est : (int?)null;
            int? idTipo = int.TryParse(ddlTipo.SelectedValue, out int tip) ? tip : (int?)null;
            int? idPrioridad = int.TryParse(ddlPrioridad.SelectedValue, out int prio) ? prio : (int?)null;

            List<Incidencia> lista = new IncidenciaNegocio().Filtrar(idIncidencia, idEstado, idTipo, idPrioridad);
            dgvIncidencias.DataSource = SoloLasDelUsuario(lista);
            dgvIncidencias.DataBind();
        }

        protected void dgvIncidencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvIncidencias.PageIndex = e.NewPageIndex;
            CargarGrilla();   // vuelve a aplicar los filtros, así no se pierden
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            dgvIncidencias.PageIndex = 0;   // al filtrar, volver a la primera página
            CargarGrilla();
        }

        protected string ClasePrioridad(object descripcion)
        {
            switch (descripcion.ToString())
            {
                case "Urgente": return "badge bg-danger px-3 py-2";
                case "Alta": return "badge bg-warning text-dark px-3 py-2";
                case "Media": return "badge bg-info text-dark px-3 py-2";
                default: return "badge bg-secondary px-3 py-2";
            }
        }

        protected string ClaseEstado(object estado)
        {
            switch ((EstadoIncidencia)estado)
            {
                case EstadoIncidencia.Abierto: return "badge bg-primary px-3 py-2";
                case EstadoIncidencia.Asignado: return "badge bg-secondary px-3 py-2";
                case EstadoIncidencia.EnAnalisis: return "badge bg-info text-dark px-3 py-2";
                case EstadoIncidencia.Resuelto: return "badge bg-success px-3 py-2";
                case EstadoIncidencia.Reabierto: return "badge bg-danger px-3 py-2";
                default: return "badge bg-dark px-3 py-2";
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