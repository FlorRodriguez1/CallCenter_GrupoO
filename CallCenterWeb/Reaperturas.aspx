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
                <h3 class="text-danger">2</h3>
                <p class="small mb-0">Esperan respuesta del supervisor</p>
            </div>
        </div>
    </div>
    <div class="col-md-4 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">APROBADAS</p>
                <h3 class="text-success">1</h3>
                <p class="small mb-0">Incidencias reabiertas</p>
            </div>
        </div>
    </div>
    <div class="col-md-4 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">RECHAZADAS</p>
                <h3>0</h3>
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
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" />
            </div>
        </div>
    </div>
</div>

<!-- Grilla -->
<div class="card shadow-sm mb-4">
    <div class="card-body">
        <h5 class="mb-3">Solicitudes pendientes</h5>
        <table class="table align-middle">
            <thead class="table-light">
                <tr>
                    <th>N°</th>
                    <th>Cliente</th>
                    <th>Estado actual</th>
                    <th>Solicitado por</th>
                    <th>Motivo</th>
                    <th>Fecha</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td><a runat="server" href="~/IncidenciaForm.aspx?id=3" class="fw-bold">#3</a></td>
                    <td>María Gómez<br /><small class="text-muted">DNI 35111222</small></td>
                    <td><span class="badge bg-success">Resuelto</span></td>
                    <td>telefonista1</td>
                    <td class="small">El cliente volvió a llamar: el problema sigue igual wetc etc...</td>
                    <td><small>31/10/2026<br />10:15</small></td>
                    <td class="text-nowrap">
                        <button type="button" class="btn btn-success btn-sm">Aprobar</button>
                        <button type="button" class="btn btn-outline-danger btn-sm" data-bs-toggle="modal" data-bs-target="#modalRechazar">Rechazar</button>
                    </td>
                </tr>
                <tr>
                    <td><a runat="server" href="~/IncidenciaForm.aspx?id=1" class="fw-bold">#1</a></td>
                    <td>Valeria Acosta<br /><small class="text-muted">DNI 30123456</small></td>
                    <td><span class="badge bg-dark">Cerrado</span></td>
                    <td>telefonista2</td>
                    <td class="small">Se cerró asdasddd..........</td>
                    <td><small>01/11/2026<br />09:30</small></td>
                    <td class="text-nowrap">
                        <button type="button" class="btn btn-success btn-sm">Aprobar</button>
                        <button type="button" class="btn btn-outline-danger btn-sm" data-bs-toggle="modal" data-bs-target="#modalRechazar">Rechazar</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>

<!-- Modal Rechazar -->
<div class="modal fade" id="modalRechazar" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Rechazar solicitud</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body">
                <label class="form-label">Motivo del rechazo <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtComentarioRechazo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                <small class="text-muted">La incidencia queda en su estado actual y el motivo se guarda en el historial.</small>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" CssClass="btn btn-danger" />
            </div>
        </div>
    </div>
</div>
</asp:Content>
