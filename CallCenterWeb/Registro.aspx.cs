using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data.SqlClient;
using Dominio;
using Negocio;

namespace CallCenterWeb
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MostrarError("Completá todos los campos obligatorios.");
                return;
            }

            if (txtClave.Text != txtConfirmarClave.Text)
            {
                MostrarError("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                Usuario nuevo = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    NombreUsuario = txtNombreUsuario.Text.Trim(),
                    Perfil = Perfil.Telefonista
                };

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Agregar(nuevo, txtClave.Text);

                // Loguea automáticamente al usuario recién creado
                Usuario logueado = negocio.Loguear(nuevo.NombreUsuario, txtClave.Text);
                Session["Usuario"] = logueado;

                Response.Redirect("Default.aspx", false);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    MostrarError("Ese nombre de usuario ya está en uso.");
                else
                    MostrarError("Error en base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo completar el registro: " + ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;
        }
    }
}