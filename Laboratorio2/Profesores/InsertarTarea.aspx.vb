Imports System.Data.SqlClient

Public Class InsertarTarea
    Inherits System.Web.UI.Page
    Dim adap As SqlDataAdapter
    Dim dataset As New DataSet
    Dim tabla As New DataTable
    Dim com As SqlCommandBuilder
    Dim fila As DataRow
    Dim conexion As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not HttpContext.Current.User.Identity.IsAuthenticated) Then
            Response.Redirect("../Inicio.aspx")
        End If
        Label1.Text = Session("email")
        Label3.Text = HttpContext.Current.User.Identity.Name
        'primero crearemos el adapter con el data set... para obtener las tareas y luego usarlo en el boton
        If Not Page.IsPostBack Then
            conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
            Dim codigo, descripcion, asignatura, horas, tipo As String
            codigo = TextBox1.Text
            descripcion = TextBox2.Text
            asignatura = DropDownList1.SelectedValue
            horas = TextBox3.Text
            tipo = DropDownList2.SelectedValue
            'esta vez vamos a usar el adaptador desde esta clase -> sin llamar al componente
            adap = New SqlDataAdapter("SELECT * FROM TareasGenericas WHERE CodAsig = '" & asignatura & "'", conexion)
            com = New SqlCommandBuilder(adap)
            Try
                adap.Fill(dataset, "TareasProfesor")        'obtenemos todas las tareas de la asignatura

                tabla = dataset.Tables("TareasProfesor")        'metemos en la tabla las tareas y comprobamos si existe alguna
                'If tabla.Rows.Count.Equals(0) Then
                'Label2.Text = "No existen tareas para la asignatura seleccioada. Crea la primera!"
                'Else
                'Session("tablaProfesorTareas") = tabla
                'End If
            Catch ex As Exception
                Label2.Text = "Error: " & ex.Message
            End Try
            Session("dataAdapterProfesorTareas") = adap     'si es la primera vez que se carga la pagina se crea los objetos
            Session("dataSetProfesorTareas") = dataset

        Else
            adap = Session("dataAdapterProfesorTareas") 'sino se obtienen directamente
            dataset = Session("dataSetProfesorTareas")
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FormsAuthentication.SignOut()
        Session.Abandon()
        Response.Redirect("../Inicio.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim codigo, descripcion, asignatura, horas, tipo As String
        conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
        codigo = TextBox1.Text
        descripcion = TextBox2.Text
        asignatura = DropDownList1.SelectedValue
        horas = TextBox3.Text
        tipo = DropDownList2.SelectedValue

        tabla = dataset.Tables("TareasProfesor")    'obtenemos la tabla con las tareas para poder actualizarla y que se guarde en BD
        fila = tabla.NewRow()                       'rellenamos los datos de la tabla TareasGenericas en la fila
        fila("Codigo") = codigo
        fila("Descripcion") = descripcion
        fila("CodAsig") = asignatura
        fila("HEstimadas") = horas
        fila("Explotacion") = "True"
        fila("TipoTarea") = tipo
        tabla.Rows.Add(fila)

        adap = Session("dataAdapterProfesorTareas")         'actualizamos el adapter y guardamos los cambios
        adap.Update(dataset, "TareasProfesor")
        dataset.AcceptChanges()
        Label2.Text = "Salvados los cambios en la BD"
        Response.Redirect("TareasProfesor.aspx")
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        'debemos modificar el adapter en funcion a la asignatura seleccionada
        Dim asignatura As String
        conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
        asignatura = DropDownList1.SelectedValue

        adap = New SqlDataAdapter("SELECT * FROM TareasGenericas WHERE CodAsig = '" & asignatura & "'", conexion)
        com = New SqlCommandBuilder(adap)
        Try
            adap.Fill(dataset, "TareasProfesor")        'obtenemos todas las tareas de la asignatura

            tabla = dataset.Tables("TareasProfesor")        'metemos en la tabla las tareas y comprobamos si existe alguna
            'If tabla.Rows.Count.Equals(0) Then
            'Label2.Text = "No existen tareas para la asignatura seleccioada. Crea la primera!"
            'Else
            'Session("tablaProfesorTareas") = tabla
            'End If
        Catch ex As Exception
            Label2.Text = "Error: " & ex.Message
        End Try

        Session("dataAdapterProfesorTareas") = adap     'si es la primera vez que se carga la pagina se crea los objetos
        Session("dataSetProfesorTareas") = dataset
    End Sub
End Class