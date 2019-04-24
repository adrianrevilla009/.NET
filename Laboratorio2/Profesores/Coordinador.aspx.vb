Public Class Coordinador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not HttpContext.Current.User.Identity.IsAuthenticated) Then
            Response.Redirect("../Inicio.aspx")
        End If
        If Not Session("email") = "vadillo@ehu.es" Then
            Response.Redirect("../Inicio.aspx")
        End If
        Label1.Text = Session("email")
        Label2.Text = HttpContext.Current.User.Identity.Name
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FormsAuthentication.SignOut()
        Session.Abandon()
        Response.Redirect("../Inicio.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DropDownList1.SelectedValue = "" Then
            Label3.Text = "Selecciona una asignatura"
        Else
            Dim a As New obtenerHoras.ServicioCoordinador
            Dim drop As String = DropDownList1.SelectedValue.ToString
            Try
                Dim res As String = a.numeroHoras(drop)
                Label3.Text = res.ToString & " horas"
            Catch ex As Exception
                Label3.Text = "No hay tareas instanciadas"
            End Try
        End If
    End Sub
End Class