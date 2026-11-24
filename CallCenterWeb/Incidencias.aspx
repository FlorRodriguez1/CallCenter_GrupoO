<%@ Page Title="Incidencias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Incidencias.aspx.cs" Inherits="CallCenterWeb.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Encabezado -->
<div class="d-flex justify-content-between align-items-center mt-4 mb-4">
    <div>
        <h2>Gestión de incidencias</h2>
        <p class="text-muted mb-0">Alta, asignación y seguimiento de los reclamos de los clientes.</p>
    </div>
    <a class="btn btn-primary" runat="server" href="~/IncidenciaForm">+ Nueva incidencia</a>
</div>

<!-- Tarjetas -->
<div class="row mb-4">
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">TOTAL INCIDENCIAS</p>
                <asp:Literal ID="litTotal" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">ABIERTAS</p>
                <asp:Literal ID="litAbiertas" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">EN ANÁLISIS</p>
                <asp:Literal ID="litAnalisis" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">RESUELTAS</p>
                <asp:Literal ID="litResueltas" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</div>

<!-- Filtros -->
<div class="card shadow-sm mb-4">
    <div class="card-body">
        <h6 class="mb-3">Búsqueda de incidencias</h6>
        <asp:Panel ID="pnlFiltros" runat="server" CssClass="row align-items-end" DefaultButton="btnFiltrar">
            <div class="col-md-3 mb-2">
                <label class="form-label small">N° de incidencia</label>
                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" placeholder="Ej: 15" />
            </div>
            <div class="col-md-3 mb-2">
                <label class="form-label small">Estado</label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" AppendDataBoundItems="true" DataTextField="Descripcion" DataValueField="IdEstado">
                    <asp:ListItem Text="Seleccione estado..." Value="" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2 mb-2">
                <label class="form-label small">Tipo</label>
                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select" AppendDataBoundItems="true" DataTextField="Descripcion" DataValueField="IdTipo">
                    <asp:ListItem Text="Seleccionar tipo..." Value="" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2 mb-2">
                <label class="form-label small">Prioridad</label>
                <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="form-select" AppendDataBoundItems="true" DataTextField="Descripcion" DataValueField="IdPrioridad">
                    <asp:ListItem Text="Seleccione prioridad..." Value="" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2 mb-2">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
            </div>
        </asp:Panel>
    </div>
</div>

<!-- Grilla -->
<div class="card shadow-sm mb-4">
    <div class="card-body">
        <div class="table-responsive">
            <asp:GridView ID="dgvIncidencias" runat="server" AutoGenerateColumns="false" CssClass="table table-hover align-middle mb-0" GridLines="None" DataKeyNames="IdIncidencia" AllowPaging="true" PageSize="10" EmptyDataText="No hay incidencias para mostrar." OnPageIndexChanging="dgvIncidencias_PageIndexChanging">
                <HeaderStyle CssClass="table-light" />
                <PagerStyle CssClass="pt-3" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField HeaderText="N°" DataField="IdIncidencia">
                        <ItemStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cliente" DataField="Cliente.Nombre" />
                    <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
                    <asp:TemplateField HeaderText="Prioridad">
                        <ItemTemplate>
                            <span class='<%# Convert.ToInt32(Eval("Prioridad.IdPrioridad")) == 1 ? "badge bg-danger px-3 py-2" : (Convert.ToInt32(Eval("Prioridad.IdPrioridad")) == 2 ? "badge bg-warning text-dark px-3 py-2" : "badge bg-secondary px-3 py-2") %>'>
                                <%# Eval("Prioridad.Descripcion") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='<%# Eval("Estado").ToString() == "EnAnalisis" ? "badge bg-info text-dark px-3 py-2" : (Eval("Estado").ToString() == "Reabierto" ? "badge bg-danger px-3 py-2" : (Eval("Estado").ToString() == "Abierto" ? "badge bg-primary px-3 py-2" : "badge bg-success px-3 py-2")) %>'>
                                <%# Eval("Estado").ToString() == "EnAnalisis" ? "En análisis" : Eval("Estado").ToString() %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Asignado a">
                        <ItemTemplate>
                            <%# string.IsNullOrWhiteSpace(Eval("UsuarioAsignado.Nombre") as string) ? "<span class=\"text-muted fst-italic\">Sin asignar</span>" : Eval("UsuarioAsignado.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField Text="Detalle" DataNavigateUrlFields="IdIncidencia" DataNavigateUrlFormatString="~/IncidenciaForm.aspx?id={0}" ControlStyle-CssClass="btn btn-outline-primary btn-sm">
                        <ItemStyle CssClass="text-end" Width="100px" />
                    </asp:HyperLinkField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:Label runat="server" ID="lblInfo" CssClass="d-block mt-2 text-muted small" Text=""></asp:Label>
    </div>
</div>
</asp:Content>
