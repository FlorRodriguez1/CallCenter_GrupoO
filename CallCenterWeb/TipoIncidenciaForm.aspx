<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TipoIncidenciaForm.aspx.cs" Inherits="CallCenterWeb.TipoIncidenciaForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
    <div class="col-lg-6">

        <div class="d-flex justify-content-between align-items-center mt-4 mb-4">
            <h2>Tipo de incidencia</h2>
            <a class="btn btn-outline-secondary" runat="server" href="~/TiposIncidencia">← Volver al listado</a>
        </div>

        <div class="card shadow-sm mb-4">
            <div class="card-body">

                <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

                <div class="mb-3">
                    <label class="form-label">Descripción <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" />
                </div>

                <div class="form-check form-switch mb-4">
                    <input type="checkbox" id="chkActivo" runat="server" class="form-check-input" checked />
                    <label class="form-check-label fw-bold" for="chkActivo">Activo</label>
                </div>

                <div class="d-flex justify-content-between">
                    <a class="btn btn-outline-secondary" runat="server" href="~/TiposIncidencia">Cancelar</a>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                </div>

            </div>
        </div>

    </div>
    </div>
</asp:Content>
