using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Dominio;

namespace CallCenterWeb
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            Usuario usuario = (Usuario)Session["Usuario"];
            lblNombreUsuario.Text = usuario.NombreCompleto;
            lblPerfil.Text = usuario.Perfil.ToString();

            AplicarPermisosMenu(usuario.Perfil);
        }

        private void AplicarPermisosMenu(Perfil perfil)
        {
            // Administración (Usuarios, Tipos, Prioridades): solo Administrador
            liAdministracion.Visible = (perfil == Perfil.Administrador);

            // Reaperturas: Supervisor o Administrador
            liReaperturas.Visible = (perfil == Perfil.Supervisor || perfil == Perfil.Administrador);
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx", false);
        }
    }
}