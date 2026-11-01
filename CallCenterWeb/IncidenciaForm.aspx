<%@ Page Title="Incidencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncidenciaForm.aspx.cs" Inherits="CallCenterWeb.IncidenciaForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Encabezado -->
<div class="card shadow-sm mt-4 mb-4">
    <div class="card-body d-flex justify-content-between align-items-center flex-wrap">
        <div>
            <h2 class="mb-1">
                Incidencia N° 1
                <span class="badge bg-danger fs-6">Crítica</span>
                <span class="badge bg-warning text-dark fs-6">En análisis</span>
            </h2>
            <a runat="server" href="~/Incidencias" class="small">← Volver al listado</a>
        </div>

        <!-- Botones de acción (solo en modo editar) -->
        <asp:Panel ID="pnlAcciones" runat="server" CssClass="mt-2">
            <button type="button" class="btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#modalReasignar">Reasignar</button>
            <button type="button" class="btn btn-outline-success" data-bs-toggle="modal" data-bs-target="#modalResolver">Resolver</button>
            <button type="button" class="btn btn-outline-dark" data-bs-toggle="modal" data-bs-target="#modalCerrar">Cerrar incidencia</button>
            <button type="button" class="btn btn-outline-danger" data-bs-toggle="modal" data-bs-target="#modalReapertura">Solicitar reapertura</button>
        </asp:Panel>
    </div>
</div>

<div class="row">
    <!-- Datos del reclamo (editable) -->
    <div class="col-lg-7 mb-4">
        <div class="card shadow-sm h-100">
            <div class="card-body">
                <h5 class="mb-3">Datos del reclamo</h5>

                <div class="mb-3">
                    <label class="form-label">Cliente <span class="text-danger">*</span></label>
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione un cliente..." Value="" />
                    </asp:DropDownList>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <label class="form-label">Tipo de incidencia <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Seleccione..." Value="" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Prioridad <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Seleccione..." Value="" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Problemática <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtProblematica" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                    <small class="text-muted">Cualquier modificación queda registrada en el historial.</small>
                </div>

                <div class="text-end">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>

    <!-- Datos de la incidencia (solo lectura, solo en modo editar) -->
    <div class="col-lg-5 mb-4">
        <asp:Panel ID="pnlDatos" runat="server" CssClass="card shadow-sm h-100">
            <div class="card-body">
                <h5 class="mb-3">Datos de la incidencia</h5>

                <div class="bg-light rounded p-2 mb-2">
                    <small class="text-muted">Estado</small>
                    <div><span class="badge bg-warning text-dark">En análisis</span></div>
                </div>
                <div class="bg-light rounded p-2 mb-2">
                    <small class="text-muted">Creado por</small>
                    <div class="fw-bold">telefonista1</div>
                </div>
                <div class="bg-light rounded p-2 mb-2">
                    <small class="text-muted">Asignado a</small>
                    <div class="fw-bold">telefonista1</div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <div class="bg-light rounded p-2 mb-2">
                            <small class="text-muted">Fecha de creación</small>
                            <div class="fw-bold">29/10/2026</div>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="bg-light rounded p-2 mb-2">
                            <small class="text-muted">Fecha de resolución</small>
                            <div class="fw-bold">-</div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</div>

<!-- Historial (solo en modo editar) -->
<asp:Panel ID="pnlHistorial" runat="server" CssClass="card shadow-sm mb-4">
    <div class="card-body">
        <h5 class="mb-3">Historial de cambios</h5>
        <table class="table align-middle">
            <thead class="table-light">
                <tr>
                    <th>Fecha</th>
                    <th>Usuario</th>
                    <th>Estado anterior</th>
                    <th>Estado nuevo</th>
                    <th>Comentario</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>30/10/2026 10:52</td>
                    <td>telefonista1</td>
                    <td><span class="badge bg-primary">Abierto</span></td>
                    <td><span class="badge bg-warning text-dark">En análisis</span></td>
                    <td>Se modificó la problemática.</td>
                </tr>
                <tr>
                    <td>29/10/2026 08:45</td>
                    <td>telefonista1</td>
                    <td>-</td>
                    <td><span class="badge bg-primary">Abierto</span></td>
                    <td>Alta de la incidencia.</td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Panel>
    <!-- Modal, ventana Resolver con comentario -->
<div class="modal fade" id="modalResolver" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Resolver incidencia</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body">
                <label class="form-label">Respuesta al cliente <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtComentarioRespuesta" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnResolver" runat="server" Text="Resolver" CssClass="btn btn-success" />
            </div>
        </div>
    </div>
</div>

<!-- Modal, ventana Cerrar con comentario -->
<div class="modal fade" id="modalCerrar" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Cerrar incidencia</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body">
                <label class="form-label">Comentario de cierre <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtComentarioCierre" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar incidencia" CssClass="btn btn-dark" />
            </div>
        </div>
    </div>
</div>

<!-- Modal, ventana Reasignar con comentario de razon -->
<div class="modal fade" id="modalReasignar" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Reasignar incidencia</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body">
                <label class="form-label">Asignar a <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlUsuarioAsignado" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione un usuario..." Value="" />
                </asp:DropDownList>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnReasignar" runat="server" Text="Reasignar" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</div>

<!-- Modal Solicitar reapertura con comentario de razon-->
<div class="modal fade" id="modalReapertura" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Solicitar reapertura</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body">
                <label class="form-label">Motivo <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtMotivoReapertura" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                <small class="text-muted">La solicitud la aprueba o rechaza un supervisor.</small>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnSolicitarReapertura" runat="server" Text="Enviar solicitud" CssClass="btn btn-danger" />
            </div>
        </div>
    </div>
</div>
</asp:Content>
