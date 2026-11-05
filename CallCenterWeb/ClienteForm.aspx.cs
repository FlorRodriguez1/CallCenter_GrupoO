using System;
using System.Data.SqlClient;
using Dominio;
using Negocio;

namespace CallCenterWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Si viene ?id=5 en la URL, la pantalla edita ese cliente; si no, da de alta uno nuevo
        private int IdCliente
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

            if (IdCliente > 0)
            {
                Cliente cliente = new ClienteNegocio().Obtener(IdCliente);
                if (cliente == null)
                {
                    Response.Redirect("Clientes.aspx", false);
                    return;
                }

                txtDni.Text = cliente.DNI;
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtEmail.Text = cliente.Email;
                txtTelefono.Text = cliente.Telefono;
                chkActivo.Checked = cliente.Activo;
            }
            else
            {
                // Un cliente nuevo siempre arranca activo (el SP de alta no recibe Activo)
                chkActivo.Checked = true;
                chkActivo.Disabled = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtDni.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MostrarError("Completá DNI, nombre, apellido y email.");
                return;
            }

            try
            {
                Cliente cliente = new Cliente();
                cliente.DNI = txtDni.Text.Trim();
                cliente.Nombre = txtNombre.Text.Trim();
                cliente.Apellido = txtApellido.Text.Trim();
                cliente.Email = txtEmail.Text.Trim();
                cliente.Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim();

                ClienteNegocio negocio = new ClienteNegocio();

                if (IdCliente > 0)
                {
                    cliente.IdCliente = IdCliente;
                    cliente.Activo = chkActivo.Checked;
                    negocio.Modificar(cliente);
                }
                else
                {
                    negocio.Agregar(cliente);
                }

                Response.Redirect("Clientes.aspx", false);
            }
            catch (SqlException ex)
            {
                // 2627 y 2601 = violación de UNIQUE (DNI repetido)
                if (ex.Number == 2627 || ex.Number == 2601)
                    MostrarError("Ya existe un cliente con ese DNI.");
                else
                    MostrarError("No se pudo guardar el cliente: " + ex.Message);
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo guardar el cliente: " + ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;
        }
    }
}