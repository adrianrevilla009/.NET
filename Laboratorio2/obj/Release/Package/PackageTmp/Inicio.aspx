<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Inicio.aspx.vb" Inherits="Laboratorio2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="BIENVENIDOS AL PROYECTO 2 DE HADS"></asp:Label>
        <br />
        <br />
    
    </div>
        <asp:Button ID="Button2" runat="server" Text="Cambiar contraseña" />
        <p>
        <asp:Button ID="Button1" runat="server" Text="Registro" />
        </p>
        <p>
            SI QUIERES HACER LOGIN INTRODUCE TUS DATOS</p>
        <p>
&nbsp;Nombre:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server" style="margin-bottom: 0px"></asp:TextBox>
        </p>
        <p>
            Password:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <div style="margin-left: 40px">
            <asp:Button ID="Button3" runat="server" Text="Login" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server"></asp:Label>
        </div>
        <div style="margin-left: 40px">
        </div>
    </form>
</body>
</html>
