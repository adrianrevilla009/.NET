Imports System.Data.SqlClient

Public Class Proyecto
    Inherits System.Web.UI.Page

    Dim dataset, dataset2, dataset3 As New DataSet
    Dim bd As New Email.Datos
    Dim tabla, tabla2, tabla3 As New DataTable
    Dim conexion As String
    Dim dataAdapter, dataAdapter2 As New SqlDataAdapter
    Dim vista As DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label2.Text = Session("email")

        If Not Page.IsPostBack Then
            Dim email As String = Session("email")
            conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"

            Try
                dataset = bd.obtenerAsignaturas(email, conexion, dataAdapter) 'obtenemos los datos y guardamos en el dataset
                Session("dataAdapterAsignatura") = dataAdapter  'necesitamos pasarle un data adapter como referencia para que nos devuelva el que usa en
            Catch ex As Exception                               'el componente para meterlo en la sesion, ya que directamente desde el componente no nos deja
                Label1.Text = "Error: " & ex.Message
            End Try

            tabla = dataset.Tables("GruposClase")   'obtenemos la tabla "gruposclase" del dataset
            If tabla.Rows.Count.Equals(0) Then
                Label1.Text = "No hay asignaturas para este alumno"
            Else
                DropDownList1.DataSource = tabla             'asignamos los datos de la tabla al dropdownlist
                DropDownList1.DataTextField = "codigoasig"  'cogemos la columna codigoasig
                DropDownList1.DataBind()
            End If

            Session("dataSetAsignaturas") = dataset
        Else
            dataset = Session("dataSetAsignaturas")
            'Session("dataAdapterAsignatura") = dataAdapter
            dataset2 = Session("dataSetTareas")
            'Session("dataAdapterTarea") = dataAdapter2
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Dim email As String = Session("email")
        conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
        Dim codigoDrop As String
        codigoDrop = DropDownList1.SelectedItem.Value       'obtenemos el valor del desplegable(codigo asignatura)

        Try
            dataset2 = bd.obtenerTareas(email, conexion, codigoDrop, dataAdapter2)      'obtenemos las tareas de esa asignatura en una tabla del dataset
            Session("dataAdapterTarea") = dataAdapter2
        Catch ex As Exception
            Label3.Text = "Error: " & ex.Message
        End Try

        tabla2 = dataset2.Tables("TareasGenericas")     'obtenemos la tabla del dataset
        GridView1.DataSource = tabla2                   'mostramos la tabla en el gridview
        GridView1.DataBind()

        Session("dataSetTareas") = dataset2
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Response.Redirect("./InstanciarTarea.aspx?codigo=" & GridView1.SelectedRow.Cells(1).Text & "&HEstimadas=" & GridView1.SelectedRow.Cells(3).Text)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Session.Abandon()
        Response.Redirect("Inicio.aspx")
    End Sub

    Private Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        'no consigo que funcione correctamente 
        dataset3 = Session("dataSetTareas") 'obtenemos el dataset de las tareas y la tabla
        tabla3 = dataset3.Tables("TareasGenericas")

        vista = New DataView(tabla3)    'creamos la vista de la tabla
        vista.Sort = e.SortExpression   'ordenamos la vista

        GridView1.DataSource = vista    'ligamos la vista al gridview
        GridView1.DataBind()

    End Sub
End Class