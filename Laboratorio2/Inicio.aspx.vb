Imports System.Data.SqlClient

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("Registro.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("Cambiar.aspx")
    End Sub

    Public Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim email, pass, mensaje1, tipo As String
        Dim bd As New Email.Datos
        email = TextBox1.Text
        pass = TextBox2.Text


        mensaje1 = bd.conectar()
        Dim RecordCount As Integer
        RecordCount = bd.usuarioLogeado(email, pass)
        tipo = bd.verTiposUsuario(email, pass)

        If RecordCount = 1 Then
            Label2.Text = "LOGIN CORRECTO"
            Session("email") = email
            If tipo.Equals("Alumno") Then
                Response.Redirect("Alumno.aspx")
            Else
                Response.Redirect("Profesor.aspx")
            End If
        ElseIf RecordCount > 1 Then
            Label2.Text = "LOGIN NO CORRECTO. USUARIO REPLICADO"
        Else
            Label2.Text = "LOGIN NO CORRECTO. NO EXISTE EL USUARIO"
        End If

        mensaje1 = bd.cerrar()

    End Sub


End Class