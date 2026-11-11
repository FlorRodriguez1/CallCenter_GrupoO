using System;
using Dominio;
using Negocio;

namespace CallCenterWeb
{
    public partial class TipoIncidenciaForm : System.Web.UI.Page
    {
        private int IdTipo
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

            if (IdTipo > 0)
            {
                TipoIncidencia tipo = new TipoIncidenciaNegocio().Obtener(IdTipo);
                if (tipo == null)
                {
                    Response.Redirect("TiposIncidencia.aspx", false);
                    return;
                }

                txtDescripcion.Text = tipo.Descripcion;
                chkActivo.Checked = tipo.Activo;
            }
            else
            {
                // Un tipo nuevo siempre arranca activo
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
                TipoIncidencia tipo = new TipoIncidencia();
                tipo.Descripcion = txtDescripcion.Text.Trim();

                TipoIncidenciaNegocio negocio = new TipoIncidenciaNegocio();

                if (IdTipo > 0)
                {
                    tipo.IdTipo = IdTipo;
                    tipo.Activo = chkActivo.Checked;
                    negocio.Modificar(tipo);
                }
                else
                {
                    negocio.Agregar(tipo);
                }

                Response.Redirect("TiposIncidencia.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se pudo guardar: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }
    }
}