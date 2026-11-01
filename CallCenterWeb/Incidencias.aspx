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
                <h3>3</h3>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">ABIERTAS</p>
                <h3 class="text-primary">1</h3>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">EN ANÁLISIS</p>
                <h3 class="text-warning">1</h3>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">RESUELTAS</p>
                <h3 class="text-success">1</h3>
            </div>
        </div>
    </div>
</div>

<!-- Filtros -->
<div class="card shadow-sm mb-4">
    <div class="card-body">
        <h6 class="mb-3">Búsqueda de incidencias</h6>
        <div class="row align-items-end">
            <div class="col-md-3 mb-2">
                <label class="form-label small">N° de incidencia</label>
                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" placeholder="Ej: 15" />
            </div>
            <div class="col-md-3 mb-2">
                <label class="form-label small">Estado</label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Todos los estados" Value="" />
                    <asp:ListItem Text="Abierto" Value="1" />
                    <asp:ListItem Text="Asignado" Value="2" />
                    <asp:ListItem Text="En análisis" Value="3" />
                    <asp:ListItem Text="Resuelto" Value="4" />
                    <asp:ListItem Text="Reabierto" Value="5" />
                    <asp:ListItem Text="Cerrado" Value="6" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2 mb-2">
                <label class="form-label small">Tipo</label>
                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Todos los tipos" Value="" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2 mb-2">
                <label class="form-label small">Prioridad</label>
                <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Todas" Value="" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2 mb-2">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" />
            </div>
        </div>
    </div>
</div>

<!-- Grilla -->
<div class="card shadow-sm mb-4">
    <div class="card-body">
        <table class="table table-hover align-middle">
            <thead class="table-light">
                <tr>
                    <th>N°</th>
                    <th>Cliente</th>
                    <th>Tipo</th>
                    <th>Prioridad</th>
                    <th>Estado</th>
                    <th>Asignado a</th>
                    <th>Fecha</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <%--reemplazar por un GridView que lea de la vista VW_Incidencias--%>
                    <td class="fw-bold">#1</td>
                    <td>Valeria Acosta<br /><small class="text-muted">DNI 30123456</small></td>
                    <td>Facturación</td>
                    <td><span class="badge bg-danger">Crítica</span></td>
                    <td><span class="badge bg-warning text-dark">En análisis</span></td>
                    <td>telefonista1</td>
                    <td><small>29/10/2026</small></td>
                    <td><a class="btn btn-outline-primary btn-sm" runat="server" href="~/IncidenciaForm.aspx?id=1">Ver</a></td>
                </tr>
                <tr>
                    <td class="fw-bold">#2</td>
                    <td>Juan Pérez<br /><small class="text-muted">DNI 28555444</small></td>
                    <td>Técnico</td>
                    <td><span class="badge bg-warning text-dark">Alta</span></td>
                    <td><span class="badge bg-primary">Abierto</span></td>
                    <td>telefonista2</td>
                    <td><small>30/10/2026</small></td>
                    <td><a class="btn btn-outline-primary btn-sm" runat="server" href="~/IncidenciaForm.aspx?id=2">Ver</a></td>
                </tr>
                <tr>
                    <td class="fw-bold">#3</td>
                    <td>María Gómez<br /><small class="text-muted">DNI 35111222</small></td>
                    <td>Reclamo</td>
                    <td><span class="badge bg-secondary">Media</span></td>
                    <td><span class="badge bg-success">Resuelto</span></td>
                    <td>telefonista1</td>
                    <td><small>31/10/2026</small></td>
                    <td><a class="btn btn-outline-primary btn-sm" runat="server" href="~/IncidenciaForm.aspx?id=3">Ver</a></td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
</asp:Content>
