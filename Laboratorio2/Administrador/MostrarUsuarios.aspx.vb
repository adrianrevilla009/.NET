Imports System.Data.SqlClient

Public Class EliminarUsuario1
    Inherits System.Web.UI.Page
    Dim conexion As String
    Dim dataset As New DataSet
    Dim bd As New Email.Datos
    Dim dataAdapter As New SqlDataAdapter
    Dim commandBuilder As SqlCommandBuilder
    Dim tabla As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not HttpContext.Current.User.Identity.IsAuthenticated) Then
            Response.Redirect("../Inicio.aspx")
        End If
        Label2.Text = Session("email")
        Label3.Text = HttpContext.Current.User.Identity.Name
        If Not Page.IsPostBack Then
            Dim email As String = Session("email")
            conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"

            Try
                Dim st = "select * from Usuarios "
                dataAdapter = New SqlDataAdapter(st, conexion)
                commandBuilder = New SqlCommandBuilder(dataAdapter)
                dataAdapter.Fill(dataset, "Usuarios")
            Catch ex As Exception
                Label1.Text = "Error: " & ex.Message
            End Try

            tabla = dataset.Tables("Usuarios")
            GridView1.DataSource = tabla
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FormsAuthentication.SignOut()
        Session.Abandon()
        Response.Redirect("../Inicio.aspx")
    End Sub
End Class