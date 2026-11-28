<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CallCenterWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Encabezado -->
    <div class="d-flex justify-content-between align-items-center mt-4 mb-4">
        <div>
            <h2>Resumen operativo</h2>
            <p class="text-muted mb-0">Estado de los reclamos del turno actual.</p>
        </div>
    </div>

    <!-- Tarjetas -->
    <div class="row mb-4">
        <div class="col-md-3 mb-3">
            <div class="card shadow-sm">
                <div class="card-body">
                    <p class="text-muted small mb-1">INCIDENCIAS ABIERTAS</p>
                    <asp:Literal ID="litAbiertas" runat="server"></asp:Literal>
                    <p class="small mb-0">Requieren primera respuesta</p>
                </div>
            </div>
        </div>
        <div class="col-md-3 mb-3">
            <div class="card shadow-sm">
                <div class="card-body">
                    <p class="text-muted small mb-1">EN ANÁLISIS</p>
                    <asp:Literal ID="litAnalisis" runat="server"></asp:Literal>
                    <p class="small mb-0">En revisión por el equipo</p>
                </div>
            </div>
        </div>
        <div class="col-md-3 mb-3">
            <div class="card shadow-sm">
                <div class="card-body">
                    <p class="text-muted small mb-1">RESUELTAS EN EL TURNO</p>
                    <asp:Literal ID="litResueltas" runat="server"></asp:Literal>
                    <p class="small mb-0">Esperando cierre</p>
                </div>
            </div>
        </div>
        <div class="col-md-3 mb-3">
            <div class="card shadow-sm border-danger">
                <div class="card-body">
                    <p class="text-muted small mb-1">REAPERTURAS PENDIENTES</p>
                    <asp:Literal ID="litReaperturas" runat="server"></asp:Literal>
                    <p class="small mb-0">Esperan aprobación del supervisor</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Incidencias recientes -->
    <div class="card shadow-sm mb-4">
        <div class="card-body">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <div>
                    <h5 class="mb-0">Incidencias recientes y casos críticos</h5>
                    <small class="text-muted">Requieren seguimiento o reasignación</small>
                </div>
                <asp:Panel ID="pnlBuscar" runat="server" CssClass="d-flex w-25" DefaultButton="btnBuscar">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control me-2" placeholder="Número o cliente..." />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-primary" OnClick="btnBuscar_Click" />
                </asp:Panel>
            </div>
            <asp:GridView ID="dgvRecientes" runat="server" AutoGenerateColumns="false"
                CssClass="table table-hover align-middle" GridLines="None"
                EmptyDataText="No hay incidencias para mostrar.">
                <HeaderStyle CssClass="table-light" />
                <Columns>
                    <asp:BoundField DataField="IdIncidencia" HeaderText="N°" />

                    <asp:TemplateField HeaderText="Cliente">
                        <ItemTemplate><%# Eval("Cliente.Nombre") %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate><%# Eval("Tipo.Descripcion") %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Prioridad">
                        <ItemTemplate>
                            <span class='<%# ClasePrioridad(Eval("Prioridad.Descripcion")) %>'><%# Eval("Prioridad.Descripcion") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='<%# ClaseEstado(Eval("Estado")) %>'><%# TextoEstado(Eval("Estado")) %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Asignado a">
                        <ItemTemplate><%# Eval("UsuarioAsignado.Nombre") %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:HyperLinkField Text="Detalle" DataNavigateUrlFields="IdIncidencia"
                        DataNavigateUrlFormatString="~/IncidenciaForm.aspx?id={0}"
                        ControlStyle-CssClass="btn btn-outline-primary btn-sm" />
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
