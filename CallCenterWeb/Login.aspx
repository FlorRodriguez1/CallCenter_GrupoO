<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CallCenterWeb.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Iniciar Sesión - CallCenter</title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center align-items-center vh-100">
                <div class="col-md-5 col-lg-4">
                    <div class="card shadow-sm">
                        <div class="card-body p-4">
                            <div class="text-center mb-4">
                                <h3 class="fw-bold text-primary mb-4">CallCenter</h3>
                                <div class="d-flex  gap-3">
    
    <h5 class="text-black mb-0">FibraNet</h5>
    <p class="text-info"><em>Internet de alta velocidad</em></p>
</div>


                            <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

                            <div class="mb-3">
                                <label class="form-label">Usuario</label>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Contraseña</label>
                                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>

                            <div class="d-grid gap-2">
                                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" />
                                <a href="Registro.aspx" class="btn btn-outline-secondary">Crear cuenta</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>