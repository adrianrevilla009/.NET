Public Class Registro
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim email As New Email.Enviar
        Dim bd As New Email.Datos
        Dim emailDestino, nombre, apellido, password, radio, mensajeConexion, mensajeConexion2, msg As String
        Dim NumConf As Long
        Dim confirmado, codpass, usuarios, usuarios2, NumConf2 As Integer
        Dim enviado As Boolean


        emailDestino = TextBox1.Text
        nombre = TextBox2.Text
        apellido = TextBox6.Text
        password = TextBox4.Text
        If RadioButton1.Checked Then
            radio = "Alumno"
        End If
        If RadioButton2.Checked Then
            radio = "Profesor"
        End If
        confirmado = 0  '0 si no se ha confirmado, 1 si si se ha hecho
        codpass = 0 'un atributo del usuario que se pone a 1 cuando se cambia la contraseña

        NumConf = CLng(Rnd() * 9000000) + 1000000
        NumConf2 = CInt(NumConf)

        mensajeConexion = bd.conectar()

        Dim int As Integer
        int = bd.correoRepetido(emailDestino)
        If int >= 1 Then
            Label1.Text = "CORREO REPETIDO, INTENTALO DE NUEVO"
        Else

            usuarios = bd.contar()
            'AQUI VA LA FUNCIÓN HASH



            bd.insertar(nombre, apellido, password, emailDestino, radio, NumConf2, confirmado, codpass)
            usuarios2 = bd.contarRegistrados()

            mensajeConexion2 = bd.cerrar()

            msg = "http://localhost:49782/Confirmar.aspx?mensaje1=" & mensajeConexion & "&numero=" & NumConf & "&mensaje2=" & mensajeConexion2 & "&usuarios=" & usuarios & "&usuarios2=" & usuarios2 & "&email=" & emailDestino
            'Response.Redirect("Confirmar.aspx?mensaje1=" & mensajeConexion & "&numero=" & NumConf & "&mensaje2=" & mensajeConexion2 & "&usuarios=" & usuarios & "&usuarios2=" & usuarios2 & "&email=" & emailDestino)

            enviado = email.enviarEmail(NumConf, emailDestino, msg)
            If enviado = True Then
                Label1.Text = "CORREO DE CONFIRMACION ENVIADO"
                Response.Redirect("Inicio.aspx")
            Else
                Label1.Text = "CORREO DE CONFIRMACION NO ENVIADO"
            End If

        End If

        bd.cerrar()






    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("Inicio.aspx")
    End Sub
End Class