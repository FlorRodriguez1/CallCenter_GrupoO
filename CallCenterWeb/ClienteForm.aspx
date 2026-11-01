<%@ Page Title="Ficha de Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClienteForm.aspx.cs" Inherits="CallCenterWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
    <div class="col-lg-8">
    <!-- Encabezado -->
<div class="d-flex justify-content-between align-items-center mt-4 mb-4">
    <div>
        <h2>Ficha de cliente</h2>
        <p class="text-muted mb-0">Complete los datos de contacto e identidad del cliente.</p>
    </div>
    <a class="btn btn-outline-secondary" runat="server" href="~/Clientes">← Volver al listado</a>
</div>

<div class="card shadow-sm mb-4">
    <div class="card-body">
        

        <!-- Datos de identidad -->
        <h5 class="mb-3">Datos de identidad</h5>
        <div class="row mb-3">
            <div class="col-md-6">
                <label class="form-label">DNI <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" placeholder="Ej: 30123456" />
                <small class="text-muted">Sin puntos ni espacios</small>
            </div>
        </div>
        <div class="row mb-4">
            <div class="col-md-6">
                <label class="form-label">Nombre <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <label class="form-label">Apellido <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            </div>
        </div>

        <!-- Contacto -->
        <h5 class="mb-3">Datos de contacto</h5>
        <div class="row mb-4">
            <div class="col-md-6">
                <label class="form-label">Email <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="cliente@email.com" />
                <small class="text-muted">Acá le llegan los mails de sus incidencias</small>
            </div>
            <div class="col-md-6">
                <label class="form-label">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="11 1234-5678" />
            </div>
        </div>

        <!-- Estado -->
        <h5 class="mb-3">Estado de la cuenta</h5>
        <div class="bg-light rounded p-3 mb-4">
            <div class="form-check form-switch">
                <input type="checkbox" id="chkActivo" runat="server" class="form-check-input" checked />
                <label class="form-check-label fw-bold" for="chkActivo">Cliente activo</label>
            </div>
            <small class="text-muted">Si está inactivo no se le pueden cargar incidencias nuevas.</small>
        </div>

        <!-- Botones -->
        <div class="d-flex justify-content-between">
            <a class="btn btn-outline-secondary" runat="server" href="~/Clientes">Cancelar</a>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar cliente" CssClass="btn btn-primary" />
        </div>

    </div>
</div>

    </div>
</asp:Content>
