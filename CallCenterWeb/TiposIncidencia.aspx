<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposIncidencia.aspx.cs" Inherits="CallCenterWeb.TiposIncidencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mt-4 mb-3">
        <h2>Tipos de incidencia</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo tipo" CssClass="btn btn-primary" OnClick="btnNuevo_Click" />
    </div>

    <asp:GridView ID="dgvTipos" runat="server" AutoGenerateColumns="false"
        CssClass="table table-striped table-hover" GridLines="None" DataKeyNames="IdTipo">
        <Columns>
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
            <asp:HyperLinkField Text="Editar" DataNavigateUrlFields="IdTipo"
                DataNavigateUrlFormatString="TipoIncidenciaForm.aspx?id={0}" />
        </Columns>
    </asp:GridView>

</asp:Content>
