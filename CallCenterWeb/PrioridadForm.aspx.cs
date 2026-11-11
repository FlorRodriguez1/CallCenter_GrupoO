using System;
using Dominio;
using Negocio;

namespace CallCenterWeb
{
    public partial class PrioridadForm : System.Web.UI.Page
    {
        private int IdPrioridad
        {
            get
            {
                int id;
                return int.TryParse(Request.QueryString["id"], out id) ? id : 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            if (IdPrioridad > 0)
            {
                Prioridad prioridad = new PrioridadNegocio().Obtener(IdPrioridad);
                if (prioridad == null)
                {
                    Response.Redirect("Prioridades.aspx", false);
                    return;
                }

                txtDescripcion.Text = prioridad.Descripcion;
                chkActivo.Checked = prioridad.Activo;
            }
            else
            {
                // Una prioridad nueva siempre arranca activa
                chkActivo.Checked = true;
                chkActivo.Disabled = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblMensaje.Text = "Ingresá la descripción.";
                lblMensaje.Visible = true;
                return;
            }

            try
            {
                Prioridad prioridad = new Prioridad();
                prioridad.Descripcion = txtDescripcion.Text.Trim();

                PrioridadNegocio negocio = new PrioridadNegocio();

                if (IdPrioridad > 0)
                {
                    prioridad.IdPrioridad = IdPrioridad;
                    prioridad.Activo = chkActivo.Checked;
                    negocio.Modificar(prioridad);
                }
                else
                {
                    negocio.Agregar(prioridad);
                }

                Response.Redirect("Prioridades.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se pudo guardar: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }
    }
}