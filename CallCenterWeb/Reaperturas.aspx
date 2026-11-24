<%@ Page Title="Reaperturas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reaperturas.aspx.cs" Inherits="CallCenterWeb.Reaperturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Encabezado -->
<div class="mt-4 mb-4">
    <h2>Reaperturas pendientes</h2>
    <p class="text-muted mb-0">Solicitudes de reapertura enviadas por los telefonistas para aprobar o rechazar.</p>
</div>

<!-- Tarjetas -->
<div class="row mb-4">
    <div class="col-md-4 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">PENDIENTES DE REVISIÓN</p>
                <h3 class="text-danger"><asp:Literal ID="litPendientes" runat="server" /></h3>
                <p class="small mb-0">Esperan respuesta del supervisor</p>
            </div>
        </div>
    </div>
    <div class="col-md-4 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">APROBADAS</p>
                <h3 class="text-success"><asp:Literal ID="litAprobadas" runat="server" /></h3>
                <p class="small mb-0">Incidencias reabiertas</p>
            </div>
        </div>
    </div>
    <div class="col-md-4 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">RECHAZADAS</p>
                <h3><asp:Literal ID="litRechazadas" runat="server" /></h3>
                <p class="small mb-0">Con motivo registrado en el historial</p>
            </div>
        </div>
    </div>
</div>

<!-- Filtros -->
<div class="card shadow-sm mb-4">
    <div class="card-body">
        <div class="row align-items-end">
            <div class="col-md-5 mb-2">
                <label class="form-label small">N° de incidencia o cliente</label>
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Ej: 15 o Acosta" />
            </div>
            <div class="col-md-4 mb-2">
                <label class="form-label small">Estado de la solicitud</label>
                <asp:DropDownList ID="ddlEstadoSolicitud" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Pendiente" Value="1" />
                    <asp:ListItem Text="Aprobada" Value="2" />
                    <asp:ListItem Text="Rechazada" Value="3" />
                    <asp:ListItem Text="Todas" Value="" />
                </asp:DropDownList>
            </div>
            <div class="col-md-3 mb-2">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
            </div>
        </div>
    </div>
</div>

<!-- Grilla -->
<!-- Mensaje -->
<asp:Label ID="lblMensaje" runat="server" Visible="false" />

<!-- Recuadro para rechazar -->
<asp:Panel ID="pnlRechazar" runat="server" Visible="false" CssClass="card shadow-sm mb-4 border-danger">
    <div class="card-body">
        <h5 class="mb-3">Rechazar solicitud</h5>
        <asp:HiddenField ID="hfIdSolicitud" runat="server" />
        <label class="form-label">Motivo del rechazo <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtComentarioRechazo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="1000" />
        <small class="text-muted">La incidencia queda en su estado actual y el motivo se guarda en el historial.</small>
        <div class="mt-3">
            <asp:Button ID="btnConfirmarRechazo" runat="server" Text="Rechazar" CssClass="btn btn-danger" OnClick="btnConfirmarRechazo_Click" />
            <asp:Button ID="btnCancelarRechazo" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="btnCancelarRechazo_Click" />
        </div>
    </div>
</asp:Panel>

<!-- Grilla -->
<div class="card shadow-sm mb-4">
    <div class="card-body">
        <h5 class="mb-3">Solicitudes</h5>

        <asp:GridView ID="dgvSolicitudes" runat="server" AutoGenerateColumns="false"
            CssClass="table align-middle" GridLines="None"
            EmptyDataText="No hay solicitudes para mostrar."
            OnRowCommand="dgvSolicitudes_RowCommand">
            <HeaderStyle CssClass="table-light" />
            <Columns>
                <asp:TemplateField HeaderText="N°">
                    <ItemTemplate>
                        <a href='<%# "IncidenciaForm.aspx?id=" + Eval("Incidencia.IdIncidencia") %>' class="fw-bold">#<%# Eval("Incidencia.IdIncidencia") %></a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cliente">
                    <ItemTemplate>
                        <%# Eval("Incidencia.Cliente.Nombre") %> <%# Eval("Incidencia.Cliente.Apellido") %><br />
                        <small class="text-muted">DNI <%# Eval("Incidencia.Cliente.DNI") %></small>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Estado actual">
                    <ItemTemplate>
                        <span class='<%# ClaseEstado(Eval("Incidencia.Estado")) %>'><%# TextoEstado(Eval("Incidencia.Estado")) %></span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Solicitado por">
                    <ItemTemplate><%# Eval("Solicitante.NombreUsuario") %></ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Motivo" HeaderText="Motivo" ItemStyle-CssClass="small" />

                <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-CssClass="small" />

                <asp:TemplateField HeaderText="Solicitud">
                    <ItemTemplate>
                        <span class='<%# ClaseSolicitud(Eval("Estado")) %>'><%# Eval("Estado") %></span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-CssClass="text-nowrap">
                    <ItemTemplate>
                        <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" CssClass="btn btn-success btn-sm"
                            CommandName="Aprobar" CommandArgument='<%# Eval("IdSolicitud") %>'
                            Visible='<%# EsPendiente(Eval("Estado")) %>'
                            OnClientClick="return confirm('¿Aprobar la reapertura?');" />
                        <asp:Button ID="btnRechazarFila" runat="server" Text="Rechazar" CssClass="btn btn-outline-danger btn-sm"
                            CommandName="Rechazar" CommandArgument='<%# Eval("IdSolicitud") %>'
                            Visible='<%# EsPendiente(Eval("Estado")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>
</asp:Content>

