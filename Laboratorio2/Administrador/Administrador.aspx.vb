Public Class EliminarUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not HttpContext.Current.User.Identity.IsAuthenticated) Then
            Response.Redirect("../Inicio.aspx")
        End If
        Dim email As String = Session("email")
        Label1.Text = email
        Label2.Text = HttpContext.Current.User.Identity.Name
    End Sub



End Class