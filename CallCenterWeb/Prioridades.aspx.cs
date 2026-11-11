using System;
using Negocio;

namespace CallCenterWeb
{
    public partial class Prioridades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dgvPrioridades.DataSource = new PrioridadNegocio().Listar();
                dgvPrioridades.DataBind();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrioridadForm.aspx", false);
        }
    }
}