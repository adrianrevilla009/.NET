<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Alumno.aspx.vb" Inherits="Laboratorio2.Alumno" %>
<link href="../StyleSheet1.css" rel="stylesheet" type="text/css" />


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            width: 1026px;
            height: 325px;
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 335px; margin-top: 35px; width: 172px;">
    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Alumnos/TareasAlumno.aspx">Tareas Genericas</asp:HyperLink>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Logout" />
        <br />
    
    <div style="height: 176px; margin-top: 0px; width: 700px; margin-left: 256px;">
        <h1 style="margin-left: 80px">Gestión Web de Tareas - Dedicación</h1>
        <br />
        <h1 style="margin-left: 280px">Alumnos</h1>
    
    <div style="height: 50px; margin-top: 49px; width: 366px; margin-left: 0px;">
        
    
        Alumno conextado:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server"></asp:Label>
        
    
        <br />
        <br />
        Cookie registrada:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server"></asp:Label>
        
    
    </div>

        <br />
        <br />
    
    </div>
    
    </div>

    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <p>
        Usuarios logeados:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Alumnos:<asp:Label ID="Label3" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Profesores:<asp:Label ID="Label4" runat="server"></asp:Label>
    </p>

                <asp:Timer ID="Timer1" runat="server" Interval="2000">
                </asp:Timer>

                <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" BackColor="Aqua" Font-Bold="True"></asp:ListBox>
                <asp:ListBox ID="ListBox2" runat="server" AutoPostBack="True" BackColor="Fuchsia" Font-Bold="True"></asp:ListBox>
                <br />
        </ContentTemplate>
        </asp:UpdatePanel>

    </form>
    </body>
</html>
