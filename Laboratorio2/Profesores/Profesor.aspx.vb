Public Class Profesor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not HttpContext.Current.User.Identity.IsAuthenticated) Then
            Response.Redirect("../Inicio.aspx")
        End If
        Dim email As String = Session("email")
        Label2.Text = HttpContext.Current.User.Identity.Name
        Label1.Text = email
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'FormsAuthentication.SignOut()
        Session.Abandon()
        Response.Redirect("../Inicio.aspx")
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim listaProfes As New ListBox
        listaProfes = Application.Contents("listaProfesores")
        Dim listaAlumnos As New ListBox
        listaAlumnos = Application.Contents("listaAlumnos")
        Label3.Text = listaAlumnos.Items.Count
        Label4.Text = listaProfes.Items.Count

        ListBox1.Items.Clear()
        If ListBox1.Items.Count < listaAlumnos.Items.Count Then
            For i = 0 To listaAlumnos.Items.Count - 1
                Dim b As Boolean = False
                For j = 0 To ListBox1.Items.Count - 1
                    If (ListBox1.Items(j).ToString().Equals(listaAlumnos.Items(i).ToString())) Then
                        b = True
                    End If
                Next
                If b = False Then
                    ListBox1.Items.Add(listaAlumnos.Items(i).ToString())
                End If
            Next
            ListBox1.DataBind()
        End If

        ListBox2.Items.Clear()
        If ListBox2.Items.Count < listaProfes.Items.Count Then
            For i = 0 To listaProfes.Items.Count - 1
                Dim b As Boolean = False
                For j = 0 To ListBox2.Items.Count - 1
                    If (ListBox2.Items(j).ToString().Equals(listaProfes.Items(i).ToString())) Then
                        b = True
                    End If
                Next
                If b = False Then
                    ListBox2.Items.Add(listaProfes.Items(i).ToString())
                End If
            Next
            ListBox2.DataBind()
        End If
    End Sub
End Class