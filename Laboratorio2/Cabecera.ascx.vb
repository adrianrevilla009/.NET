Public Class Cabecera
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = "Nombre del socio: " + Session("nombre")
        Label2.Text = "Número de socios conectados: " +
        Application("numeroConectados").ToString
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Session.Abandon()
        Response.Redirect("Inicio.aspx")
    End Sub
End Class