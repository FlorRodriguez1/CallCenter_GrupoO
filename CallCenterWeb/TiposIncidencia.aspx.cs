using System;
using Negocio;

namespace CallCenterWeb
{
    public partial class TiposIncidencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dgvTipos.DataSource = new TipoIncidenciaNegocio().Listar();
                dgvTipos.DataBind();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("TipoIncidenciaForm.aspx", false);
        }
    }
}