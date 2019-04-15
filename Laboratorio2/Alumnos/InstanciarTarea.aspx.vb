Imports System.Data.SqlClient

Public Class InstanciarTarea
    Inherits System.Web.UI.Page
    Dim bd As New Email.Datos
    Dim conexion As String
    Dim adapter As New SqlDataAdapter()
    Dim dataset As New DataSet
    Dim command As New SqlCommandBuilder()
    Dim tabla As New DataTable
    Dim fila As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not HttpContext.Current.User.Identity.IsAuthenticated) Then
            Response.Redirect("../Inicio.aspx")
        End If
        If Not Page.IsPostBack Then
            Dim codigo, horas, email As String
            codigo = Convert.ToString(Request.QueryString("codigo"))
            horas = Convert.ToString(Request.QueryString("HEstimadas"))
            email = Session("email")
            Label1.Text = email
            Label2.Text = codigo
            Label3.Text = horas

            conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
            adapter = New SqlDataAdapter("select * from EstudiantesTareas", conexion)
            command = New SqlCommandBuilder(adapter)        'obtenemos las tareas del estudiante con el adaptador
            adapter.Fill(dataset, "EstudiantesTareas")      'rellenamos el data set con los datos del adaptador

            Session("dataAdapterTarea") = adapter   'guardamos el adaptador y el data set en la sesion
            Session("dataSetTareas") = dataset      'Podriamos introducir esto en la consulta para que solo nos muestre las tareas del alumno: where Email='" & email & "'
        Else
            adapter = Session("dataAdapterTarea")
            dataset = Session("dataSetTareas")
        End If
        tabla = dataset.Tables("EstudiantesTareas")     'obtenemos la tabla con los datos para manipularla cuando clickemos el boton
        GridView1.DataSource = tabla
        GridView1.DataBind()

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim email, codigo, horas, horas2 As String
        conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
        email = Label1.Text 'obtenemos los parametros
        codigo = Label2.Text
        horas = Label3.Text
        horas2 = TextBox1.Text

        Dim valorEntero As Integer
        Dim bln As Boolean = Integer.TryParse(horas2, valorEntero)
        If Not bln Then
            Label4.Text = "No se ha introducido un numero"
        Else
            If (valorEntero > 0) Then
                'bd.insertarTarea(Session("Email"), Label1.Text, Label2.Text, Label3.Text)
                fila = tabla.NewRow()   'creamos una nueva fila en la tabla
                fila("Email") = email   'añadimos los parametros
                fila("CodTarea") = codigo
                fila("HEstimadas") = horas
                fila("HReales") = horas2
                tabla.Rows.Add(fila)    'guardamos la fila en la tabla

                adapter = Session("dataAdapterTarea") 'obtenemos el adaptador guardado
                adapter.Update(dataset, "EstudiantesTareas") 'actualizamos el adapter
                dataset.AcceptChanges()     'supongo que hacemos un commit
                Label4.Text = "Modificacion de la tabla correcta"
            Else
                Label4.Text = "Debes introducir un numero positivo"
            End If
        End If

    End Sub
End Class