
Imports System.Web.SessionState

        Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Dim listaAlumnos As New ListBox
        Dim listaProfesores As New ListBox
        Application.Contents("listaAlumnos") = listaAlumnos
        Application.Contents("listaProfesores") = listaProfesores
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al comienzo de cada solicitud
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al intentar autenticar el uso
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena cuando se produce un error
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        Dim email = Session("email")
        If Session("tipo") = "Alumno" Then
            Application.Lock()
            Dim listaAlumnos As ListBox = Application.Contents("listaAlumnos")
            listaAlumnos.Items.Remove(email)
            Application.Contents("listaAlumnos") = listaAlumnos
            Application.UnLock()
        ElseIf Session("tipo") = "Profesor" Then
            Application.Lock()
            Dim listaProfesores As ListBox = Application.Contents("listaProfesores")
            listaProfesores.Items.Remove(email)
            Application.Contents("listaProfesores") = listaProfesores
            Application.UnLock()
        End If

    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena cuando finaliza la aplicación

    End Sub

End Class