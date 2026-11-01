<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="CallCenterWeb.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Encabezado -->
<div class="d-flex justify-content-between align-items-center mt-4 mb-4">
    <div>
        <h2>Gestión de clientes</h2>
        <p class="text-muted mb-0">Clientes registrados y estado de sus cuentas.</p>
    </div>
    <a class="btn btn-primary" runat="server" href="~/ClienteForm">+ Nuevo cliente</a>
</div>

<!-- Tarjetas -->
<div class="row mb-4">
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">TOTAL CLIENTES</p>
                
                <h3>5</h3>
             
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">CLIENTES ACTIVOS</p>
                <h3 class="text-success">5</h3>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">CON RECLAMOS ABIERTOS</p>
                <h3 class="text-danger">3</h3>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">NUEVOS ESTE MES</p>
                <h3>2</h3>
            </div>
        </div>
    </div>
</div>

<!-- Buscador, filtro y grilla -->
<div class="card shadow-sm mb-4">
    <div class="card-body">
        <div class="row mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por DNI, nombre o apellido..." />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" />
                </div>
            </div>
            <div class="col-md-3 ms-auto">
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Todos los estados" Value="" />
                    <asp:ListItem Text="Activos" Value="1" />
                    <asp:ListItem Text="Inactivos" Value="0" />
                </asp:DropDownList>
            </div>
        </div>

        <asp:GridView ID="dgvClientes" runat="server" AutoGenerateColumns="false"
            CssClass="table table-hover align-middle" GridLines="None" DataKeyNames="IdCliente" 
            AllowPaging="true" PageSize="10" OnPageIndexChanging="dgvClientes_PageIndexChanging">
            <HeaderStyle CssClass="table-light" />
            <Columns>
                <asp:BoundField HeaderText="DNI" DataField="DNI" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <span class='<%# (bool)Eval("Activo") ? "badge bg-success" : "badge bg-secondary" %>'>
                            <%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField Text="Editar" DataNavigateUrlFields="IdCliente"
                    DataNavigateUrlFormatString="~/ClienteForm.aspx?id={0}"
                    ControlStyle-CssClass="btn btn-outline-primary btn-sm" />
            </Columns>
        </asp:GridView>
    </div>
</div>

</asp:Content>