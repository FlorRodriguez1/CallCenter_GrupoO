<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="CallCenterWeb.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mt-4 mb-3">Clientes</h2>

    <div class="row mb-3">
        <div class="col-md-6">
            <div class="input-group">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por DNI o nombre" />
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" />
            </div>
        </div>
        <div class="col-md-6 text-end">
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo cliente" CssClass="btn btn-primary" />
        </div>
    </div>

    <asp:GridView ID="dgvClientes" runat="server" AutoGenerateColumns="false"
        CssClass="table table-striped table-hover" GridLines="None" DataKeyNames="IdCliente">
        <Columns>
            <asp:BoundField HeaderText="DNI" DataField="DNI" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
            <asp:BoundField HeaderText="Email" DataField="Email" />
            <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
        </Columns>
    </asp:GridView>

</asp:Content>