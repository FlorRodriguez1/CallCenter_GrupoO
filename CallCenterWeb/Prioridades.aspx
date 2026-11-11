<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prioridades.aspx.cs" Inherits="CallCenterWeb.Prioridades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mt-4 mb-3">
        <h2>Prioridades</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="Nueva prioridad" CssClass="btn btn-primary" OnClick="btnNuevo_Click" />
    </div>

    <asp:GridView ID="dgvPrioridades" runat="server" AutoGenerateColumns="false"
        CssClass="table table-striped table-hover" GridLines="None" DataKeyNames="IdPrioridad">
        <Columns>
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
            <asp:HyperLinkField Text="Editar" DataNavigateUrlFields="IdPrioridad"
                DataNavigateUrlFormatString="PrioridadForm.aspx?id={0}" />
        </Columns>
    </asp:GridView>

</asp:Content>
