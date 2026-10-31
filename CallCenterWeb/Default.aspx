<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CallCenterWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Encabezado -->
<div class="d-flex justify-content-between align-items-center mt-4 mb-4">
    <div>
        <h2>Resumen operativo</h2>
        <p class="text-muted mb-0">Estado de los reclamos del turno actual.</p>
    </div>
    <a class="btn btn-primary" runat="server" href="~/IncidenciaForm">+ Nueva incidencia</a>
</div>

<!-- Tarjetas -->
<div class="row mb-4">
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">INCIDENCIAS ABIERTAS</p>
                <h3 class="text-primary">42</h3>
                <p class="small mb-0">Requieren primera respuesta</p>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">EN ANÁLISIS</p>
                <h3>19</h3>
                <p class="small mb-0">En revisión por el equipo</p>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm">
            <div class="card-body">
                <p class="text-muted small mb-1">RESUELTAS EN EL TURNO</p>
                <h3 class="text-success">138</h3>
                <p class="small mb-0">Esperando cierre</p>
            </div>
        </div>
    </div>
    <div class="col-md-3 mb-3">
        <div class="card shadow-sm border-danger">
            <div class="card-body">
                <p class="text-muted small mb-1">REAPERTURAS PENDIENTES</p>
                <h3 class="text-danger">7</h3>
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
            <input type="text" class="form-control w-25" placeholder="Buscar por número o cliente..." />
        </div>

        <table class="table table-hover align-middle">
            <thead class="table-light">
                <tr>
                    <th>N°</th>
                    <th>Cliente</th>
                    <th>Tipo</th>
                    <th>Prioridad</th>
                    <th>Estado</th>
                    <th>Asignado a</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>1</td>
                    <td>Valeria Acosta</td>
                    <td>Facturación</td>
                    <td><span class="badge bg-danger">Crítica</span></td>
                    <td><span class="badge bg-info text-dark">En análisis</span></td>
                    <td>telefonista1</td>
                    <td><a class="btn btn-outline-primary btn-sm" runat="server" href="~/IncidenciaForm">Detalle</a></td>
                </tr>
                <tr>
                    <td>2</td>
                    <td>Juan Pérez</td>
                    <td>Técnico</td>
                    <td><span class="badge bg-warning text-dark">Alta</span></td>
                    <td><span class="badge bg-danger">Reabierto</span></td>
                    <td>telefonista2</td>
                    <td><a class="btn btn-outline-primary btn-sm" runat="server" href="~/IncidenciaForm">Detalle</a></td>
                </tr>
                <tr>
                    <td>3</td>
                    <td>María Gómez</td>
                    <td>Reclamo</td>
                    <td><span class="badge bg-secondary">Media</span></td>
                    <td><span class="badge bg-primary">Abierto</span></td>
                    <td><em class="text-muted">Sin asignar</em></td>
                    <td><a class="btn btn-outline-primary btn-sm" runat="server" href="~/IncidenciaForm">Detalle</a></td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
</asp:Content>
