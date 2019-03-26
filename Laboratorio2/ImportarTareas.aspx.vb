Imports System.Data.SqlClient
Imports System.Xml

Public Class ImportarTareas
    Inherits System.Web.UI.Page

    Dim conexion As String
    Dim dataAdapter, dataAdapter1, dataAdapter3 As SqlDataAdapter
    Dim commandBuilder, commandBuilder1, commandBuilder3 As SqlCommandBuilder
    Dim dataSet, dataSet1, dataSet3 As New DataSet
    Dim tabla, tabla1, tabla3 As New DataTable
    Dim xml As New XmlDocument
    Dim nodos As XmlNodeList
    Dim fila As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = Session("email")

        If Not Page.IsPostBack Then
            'en el caso de los alumnos podriamos recuperar el dato de la sesion, pero en este caso debemos obtener los datos
            conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
            'obtenemos codigos para el dropdown
            Dim st As String = "SELECT Asignaturas.codigo FROM Asignaturas  INNER JOIN GruposClase ON GruposClase.codigoasig = Asignaturas.codigo INNER JOIN ProfesoresGrupo ON ProfesoresGrupo.codigogrupo = GruposClase.codigo WHERE ProfesoresGrupo.email ='" & Session("email") & "'"
            dataAdapter = New SqlDataAdapter(st, conexion)
            commandBuilder = New SqlCommandBuilder(dataAdapter)
            dataAdapter.Fill(dataSet, "AsignaturasProfe")
            tabla = dataSet.Tables("AsignaturasProfe")
            If tabla.Rows.Count.Equals(0) Then
                Label2.Text = "No hay asignaturas para este profe"
            Else
                DropDownList1.DataSource = tabla
                DropDownList1.DataTextField = "codigo"
                DropDownList1.DataBind()

                'ESTA PARTE DEL ADAPTADOR NO HACE FALTA !!!
                'obtenemos tareas del profe del dropdown seleccionado
                Dim st1 As String = "SELECT TareasGenericas.Codigo, TareasGenericas.Descripcion, TareasGenericas.CodAsig ,TareasGenericas.HEstimadas, TareasGenericas.Explotacion, TareasGenericas.TipoTarea FROM TareasGenericas INNER JOIN GruposClase ON TareasGenericas.CodAsig = GruposClase.codigoasig INNER JOIN ProfesoresGrupo ON GruposClase.codigo = ProfesoresGrupo.codigogrupo WHERE (TareasGenericas.CodAsig = '" & DropDownList1.SelectedValue & "') AND (ProfesoresGrupo.email = '" & Session("email") & "')"
                dataAdapter1 = New SqlDataAdapter(st1, conexion)
                commandBuilder1 = New SqlCommandBuilder(dataAdapter1)
                dataAdapter1.Fill(dataSet1, "TareasProfe")
                tabla1 = dataSet1.Tables("TareasProfe")
                If tabla1.Rows.Count.Equals(0) Then
                    Label2.Text = "No hay tareas para este profe"
                End If
            End If


            If System.IO.File.Exists(Server.MapPath("App_Data/" & DropDownList1.SelectedValue & ".xml")) Then   'para sacar los datos nada mas cargar debemos verificar que existe el fichero si no nos esta dando errores
                Xml1.DocumentSource = Server.MapPath("App_Data/" & DropDownList1.SelectedValue & ".xml")    'ahora mostraremos los datos en el componente XML
                Xml1.TransformSource = Server.MapPath("App_Data/XSLTFile.xsl")      'MOSTRAR DATOS
                Button2.Visible = True  'si el fichero existe podemos importar
                Label3.Text = ""
            Else
                Button2.Visible = False 'ocultamos el boton de importar si el fichero no existe
                Label3.Text = "No hay fichero XML para esta asignatura"
            End If

            Session("adapterAsigProfe") = dataAdapter
            Session("setAsigProfe") = dataSet
            Session("adapterTareasProfe") = dataAdapter1
            Session("setTareasProfe") = dataSet1

        Else
            dataAdapter = Session("adapterAsigProfe")
            dataSet = Session("setAsigProfe")
            dataAdapter1 = Session("adapterTareasProfe")
            dataSet1 = Session("setTareasProfe")
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Session.Abandon()
        Response.Redirect("Inicio.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'obtenemos todas las tareas para despues coger las del XML y guardarlas todas
        conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
        Dim seleccionado As String = DropDownList1.SelectedValue
        Dim st As String = "SELECT * FROM TareasGenericas WHERE 0=1"
        dataAdapter3 = New SqlDataAdapter(st, conexion)
        commandBuilder3 = New SqlCommandBuilder(dataAdapter3)
        dataAdapter3.Fill(dataSet3, "Pepe")
        tabla3 = dataSet3.Tables("Pepe")


        If seleccionado.Equals("") Then
            Label2.Text = "No hay ninguna asignatura seleccionada"
        Else
            xml.Load(Server.MapPath("App_Data/" & DropDownList1.SelectedValue & ".xml")) 'cargamos el documento XML correspondiente al codigo del desplegable
            nodos = xml.GetElementsByTagName("tarea") 'obtenemos los elementos de tipo tarea definidos en el XML cargado en forma de nodo'
            For i = 0 To nodos.Count - 1    'vamos a recorrer todos los nodos
                fila = dataSet3.Tables("Pepe").NewRow    'por cada nodo vamos a crear una fila
                fila("Codigo") = nodos(i).Attributes(0).Value                          'a esta fila le vamos a añadir los parametros que queremos sacar por pantalla
                fila("Descripcion") = nodos(i).ChildNodes(0).ChildNodes(0).Value        ' el atributo de la tarea la cogemos con .Atribute(0)
                fila("CodAsig") = DropDownList1.SelectedValue
                fila("HEstimadas") = nodos(i).ChildNodes(1).ChildNodes(0).Value         ' el primer child node es la posicion en el xml 0,1,2 .. y el segundo es el dato ligado a ese nodo
                fila("Explotacion") = nodos(i).ChildNodes(2).ChildNodes(0).Value
                fila("TipoTarea") = nodos(i).ChildNodes(3).ChildNodes(0).Value
                dataSet3.Tables("Pepe").Rows.Add(fila)       'añadimos la fila en la tabla 
            Next
            Try
                Dim num As Integer = dataAdapter3.Update(dataSet3, "Pepe")
                dataSet3.AcceptChanges()
                If num > 0 Then
                    Label3.Text = "Tareas importadas a la BD."
                Else
                    Label3.Text = "No se añadio nada nuevo"
                End If
            Catch ex As Exception
                Label3.Text = ex.Message
                Label2.Text = "Tareas ya en la BD, no se pueden repetir las claves"
            End Try
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        'vamos a repetir el codigo anterior para cargar los datos (solo en caso de que exista el fichero) en el componente XML
        If System.IO.File.Exists(Server.MapPath("App_Data/" & DropDownList1.SelectedValue & ".xml")) Then
            Xml1.DocumentSource = Server.MapPath("App_Data/" & DropDownList1.SelectedValue & ".xml")
            Xml1.TransformSource = Server.MapPath("App_Data/XSLTFile.xsl")
            Button2.Visible = True
            Label3.Text = ""
        Else
            Button2.Visible = False
            Label3.Text = "No hay fichero XML para esta asignatura"
        End If
    End Sub
End Class