using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace CallCenterWeb
{
    public partial class UsuarioForm : System.Web.UI.Page
    {
        private int IdUsuario
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

            // Los valores del desplegable son los mismos IDs del enum y de la tabla Perfiles
            foreach (Perfil perfil in Enum.GetValues(typeof(Perfil)))
                ddlPerfil.Items.Add(new ListItem(perfil.ToString(), ((int)perfil).ToString()));

            if (IdUsuario > 0)
            {
                Usuario usuario = new UsuarioNegocio().Obtener(IdUsuario);
                if (usuario == null)
                {
                    Response.Redirect("Usuarios.aspx", false);
                    return;
                }

                txtNombre.Text = usuario.Nombre;
                txtApellido.Text = usuario.Apellido;
                txtEmail.Text = usuario.Email;
                txtNombreUsuario.Text = usuario.NombreUsuario;
                ddlPerfil.SelectedValue = ((int)usuario.Perfil).ToString();
                chkActivo.Checked = usuario.Activo;

                // Al editar, la clave es opcional
                lblClaveObligatoria.Visible = false;
                lblAyudaClave.Visible = true;
            }
            else
            {
                // Un usuario nuevo siempre arranca activo
                chkActivo.Checked = true;
                chkActivo.Disabled = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MostrarError("Completá nombre, apellido, email y nombre de usuario.");
                return;
            }

            // La clave es obligatoria solo en el alta
            if (IdUsuario == 0 && string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MostrarError("Ingresá una clave para el nuevo usuario.");
                return;
            }

            try
            {
                Usuario usuario = new Usuario();
                usuario.NombreUsuario = txtNombreUsuario.Text.Trim();
                usuario.Nombre = txtNombre.Text.Trim();
                usuario.Apellido = txtApellido.Text.Trim();
                usuario.Email = txtEmail.Text.Trim();
                usuario.Perfil = (Perfil)int.Parse(ddlPerfil.SelectedValue);

                UsuarioNegocio negocio = new UsuarioNegocio();

                if (IdUsuario > 0)
                {
                    usuario.IdUsuario = IdUsuario;
                    usuario.Activo = chkActivo.Checked;
                    negocio.Modificar(usuario);

                    if (!string.IsNullOrWhiteSpace(txtClave.Text))
                        negocio.CambiarClave(IdUsuario, txtClave.Text);
                }
                else
                {
                    negocio.Agregar(usuario, txtClave.Text);
                }

                Response.Redirect("Usuarios.aspx", false);
            }
            catch (SqlException ex)
            {
                // 2627 y 2601 = violación de UNIQUE (nombre de usuario repetido)
                if (ex.Number == 2627 || ex.Number == 2601)
                    MostrarError("Ya existe un usuario con ese nombre de usuario.");
                else
                    MostrarError("No se pudo guardar el usuario: " + ex.Message);
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo guardar el usuario: " + ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;
        }
    }
}