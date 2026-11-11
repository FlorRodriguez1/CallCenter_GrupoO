<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="CallCenterWeb.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mt-4 mb-3">
        <h2>Usuarios</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo usuario" CssClass="btn btn-primary" OnClick="btnNuevo_Click" />
    </div>

    <asp:GridView ID="dgvUsuarios" runat="server" AutoGenerateColumns="false"
        CssClass="table table-striped table-hover" GridLines="None" DataKeyNames="IdUsuario">
        <Columns>
            <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario" />
            <asp:BoundField HeaderText="Nombre" DataField="NombreCompleto" />
            <asp:BoundField HeaderText="Email" DataField="Email" />
            <asp:BoundField HeaderText="Perfil" DataField="Perfil" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
            <asp:HyperLinkField Text="Editar" DataNavigateUrlFields="IdUsuario"
                DataNavigateUrlFormatString="UsuarioForm.aspx?id={0}" />
        </Columns>
    </asp:GridView>

</asp:Content>
