<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="CallCenterWeb.Registro" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Registro - CallCenter</title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center align-items-center min-vh-100 py-4">
                <div class="col-md-6 col-lg-5">
                    <div class="card shadow-sm">
                        <div class="card-body p-4">
                            <h3 class="text-center fw-bold text-primary mb-4">Registro de Usuario</h3>

                            <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label class="form-label">Nombre *</label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" />
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Apellido *</label>
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" />
                                </div>
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Email *</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" MaxLength="100" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Nombre de usuario *</label>
                                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" MaxLength="50" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Contraseña *</label>
                                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" MaxLength="100" />
                            </div>

                            <div class="mb-4">
                                <label class="form-label">Repetir contraseña *</label>
                                <asp:TextBox ID="txtConfirmarClave" runat="server" CssClass="form-control" TextMode="Password" MaxLength="100" />
                            </div>

                            <div class="d-grid gap-2">
                                <asp:Button ID="btnRegistrar" runat="server" Text="Registrarme" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                                <a href="Login.aspx" class="btn btn-outline-secondary">Volver al Login</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>