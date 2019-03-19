Public Class CambiarContraseña
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim email, passVieja, passNueva, mensaje1, mensaje2, introducido As String
        Dim numero As Integer
        Dim bd As New Email.Datos

        email = TextBox1.Text
        passVieja = TextBox2.Text
        passNueva = TextBox3.Text
        introducido = TextBox5.Text
        mensaje1 = bd.conectar()


        Dim RecordCount As Integer
        RecordCount = bd.usuarioLogeado(email, passVieja)
        If RecordCount = 1 Then
            numero = bd.obtenerNumero(email, passVieja)
            If numero.ToString.Equals(introducido) Then
                Label1.Text = bd.actualizarContraseña(email, passVieja, passNueva)
                mensaje2 = bd.cerrar()
                Response.Redirect("Inicio.aspx")
            Else
                Label1.Text = "ERROR EN LA ACTUALIZACION DE CONTRASEÑA, EL NUMERO NO COINCIDE"
            End If
        Else
            Label1.Text = "ERROR EN LA ACTUALIZACION DE CONTRASEÑA"
        End If

        mensaje2 = bd.cerrar()




    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("Inicio.aspx")
    End Sub
End Class