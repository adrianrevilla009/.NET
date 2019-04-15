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
            If email = "admin@ehu.es" And pass = "admin" Then
                'para el administrador no hacemos control de aplicacion
                FormsAuthentication.SetAuthCookie("Administrador", True)
                Response.Redirect("Administrador/Administrador.aspx")
            End If
            If tipo.Equals("Alumno") Then
                'añadimos un alumno
                Application.Lock()
                Dim ListBox1 As ListBox = Application.Contents("listaAlumnos")
                ListBox1.Items.Add(email)
                ListBox1.DataBind()
                Application.Contents("listaAlumnos") = ListBox1
                Application.UnLock()
                Session("tipo") = "Alumno"

                FormsAuthentication.SetAuthCookie("Alumno", True)
                Response.Redirect("Alumnos/Alumno.aspx")
            Else
                'añadimos un profesor
                Application.Lock()
                Dim ListBox2 As ListBox = Application.Contents("listaProfesores")
                ListBox2.Items.Add(email)
                ListBox2.DataBind()
                Application.Contents("listaProfesores") = ListBox2
                Application.UnLock()
                Session("tipo") = "Profesor"

                If email = "vadillo@ehu.es" Then
                    FormsAuthentication.SetAuthCookie("Vadillo", True)
                    Response.Redirect("Profesores/Profesor.aspx")
                Else
                    FormsAuthentication.SetAuthCookie("Profesor", True)
                    Response.Redirect("Profesores/Profesor.aspx")
                End If
            End If
        ElseIf RecordCount > 1 Then
            Label2.Text = "LOGIN NO CORRECTO. USUARIO REPLICADO"
        Else
            Label2.Text = "LOGIN NO CORRECTO. NO EXISTE EL USUARIO"
        End If
        mensaje1 = bd.cerrar()
    End Sub
End Class