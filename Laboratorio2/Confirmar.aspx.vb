Imports System.Data.SqlClient

Public Class Confirmar
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Valor, Valor2, valor3, valor4, valor5, mensaje1 As String
        Valor = Convert.ToString(Request.QueryString("mensaje1"))
        Valor2 = Convert.ToString(Request.QueryString("numero"))
        valor3 = Convert.ToString(Request.QueryString("mensaje2"))
        valor4 = Convert.ToString(Request.QueryString("usuarios"))
        valor5 = Convert.ToString(Request.QueryString("usuarios2"))
        'TextBox1.Text = Valor
        'TextBox2.Text = Valor2
        'TextBox3.Text = valor3
        'TextBox4.Text = valor4
        'TextBox5.Text = valor5
        Label2.Text = Valor
        Label3.Text = valor3
        Label4.Text = valor4
        Label5.Text = valor5


        Dim RS, RS2 As SqlDataReader
        Dim bd As New Email.Datos
        mensaje1 = bd.conectar()

        Try
            RS = bd.obtenerUsuariosConfirmados()
        Catch ex As Exception
            Label1.Text = ex.Message
            Exit Sub
        End Try
        ListBox1.Items.Clear()
        While RS.Read
            ListBox1.Items.Add("NOMBRE:" & RS.Item("nombre") & "++++++++   EMAIL:" & RS.Item("email"))
        End While
        RS.Close()

        Try
            RS2 = bd.obtenerUsuarios()
        Catch ex As Exception
            Label6.Text = ex.Message
            Exit Sub
        End Try
        ListBox2.Items.Clear()
        While RS2.Read
            ListBox2.Items.Add("NOMBRE:" & RS2.Item("nombre") & "++++++++   EMAIL:" & RS2.Item("email"))
        End While
        RS2.Close()



        mensaje1 = bd.cerrar()


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim numero, introducido, mensaje1, email As String
        numero = Convert.ToString(Request.QueryString("numero"))
        email = Convert.ToString(Request.QueryString("email"))
        introducido = TextBox2.Text

        Dim bd As New Email.Datos
        mensaje1 = bd.conectar()
        If introducido.ToString.Equals(numero) Then
            bd.actualizarClave(email)
            mensaje1 = bd.cerrar()
            Response.Redirect("Inicio.aspx")
        Else
            Label7.Text = "El numero no concuerda"
        End If



    End Sub
End Class