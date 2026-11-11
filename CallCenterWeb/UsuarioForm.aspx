<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuarioForm.aspx.cs" Inherits="CallCenterWeb.UsuarioForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
    <div class="col-lg-8">

        <div class="d-flex justify-content-between align-items-center mt-4 mb-4">
            <h2>Ficha de usuario</h2>
            <a class="btn btn-outline-secondary" runat="server" href="~/Usuarios">← Volver al listado</a>
        </div>

        <div class="card shadow-sm mb-4">
            <div class="card-body">

                <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

                <h5 class="mb-3">Datos personales</h5>
                <div class="row mb-3">
                    <div class="col-md-6">
                        <label class="form-label">Nombre <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Apellido <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-md-12">
                        <label class="form-label">Email <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" MaxLength="100" />
                    </div>
                </div>

                <h5 class="mb-3">Acceso al sistema</h5>
                <div class="row mb-3">
                    <div class="col-md-6">
                        <label class="form-label">Nombre de usuario <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Perfil <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-select" />
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-md-6">
                        <label class="form-label">Clave <asp:Label ID="lblClaveObligatoria" runat="server" CssClass="text-danger" Text="*" /></label>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" MaxLength="100" />
                        <asp:Label ID="lblAyudaClave" runat="server" CssClass="text-muted small" Visible="false"
                            Text="Dejala vacía para mantener la actual." />
                    </div>
                </div>

                <h5 class="mb-3">Estado</h5>
                <div class="bg-light rounded p-3 mb-4">
                    <div class="form-check form-switch">
                        <input type="checkbox" id="chkActivo" runat="server" class="form-check-input" checked />
                        <label class="form-check-label fw-bold" for="chkActivo">Usuario activo</label>
                    </div>
                    <small class="text-muted">Un usuario inactivo no puede iniciar sesión.</small>
                </div>

                <div class="d-flex justify-content-between">
                    <a class="btn btn-outline-secondary" runat="server" href="~/Usuarios">Cancelar</a>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar usuario" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                </div>

            </div>
        </div>

    </div>
    </div>
</asp:Content>
