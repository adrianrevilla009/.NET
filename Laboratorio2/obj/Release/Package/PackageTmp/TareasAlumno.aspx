<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TareasAlumno.aspx.vb" Inherits="Laboratorio2.Proyecto" %>
<link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 296px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Alumno conectado:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Logout" />
&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <br />
        <br />
        <div style="width: 608px">
        <h1>Seleccionar Asignatura</h1>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server"></asp:Label>
        
            <br />
        
        </div>
        <br  />
        <div style="width: 714px">
        <h1>Listado de tareas<br />


            </h1>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True" DataKeyNames="Codigo" EmptyDataText="No hay tareas para esta asignatura" AllowSorting="True">
                <HeaderStyle BackColor="#660066" />
                <RowStyle BackColor="Lime" />
                <SelectedRowStyle BackColor="Aqua" />
            </asp:GridView>
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server"></asp:Label>


        </div>
    </form>
</body>
</html>
