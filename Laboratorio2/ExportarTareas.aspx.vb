Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml
Imports Newtonsoft.Json

Public Class ExportarTareas
    Inherits System.Web.UI.Page
    Dim conexion As String
    Dim dataAdapter, dataAdapter1 As SqlDataAdapter
    Dim commandBuilder, commandBuilder1 As SqlCommandBuilder
    Dim DataSet, DataSet1, DataSet2 As New DataSet

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click 'opcional 4
        'primero descargamos e importamos Newtonsoft.Json
        DataSet2 = Session("setTareasProfeExport")
        tabla2 = DataSet2.Tables("TareasProfe")
        If tabla2.Rows.Count.Equals(0) Then
            Label2.Text = "No se pueden exportar tareas para esta asignatura"
        Else    'importamos System.IO para poder usar la propiedad File
            Dim conversion As String = JsonConvert.SerializeObject(tabla2, formatting:=Newtonsoft.Json.Formatting.Indented)   'convierte la tabla de datos a un string
            File.WriteAllText(Server.MapPath("App_Data/" & DropDownList1.SelectedValue & ".json"), conversion)  'con esto creamos el archivo .json y escribimos en el el string que habiamos obtenido
            Label2.Text = "Tareas exportadas a json"
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Session.Abandon()
        Response.Redirect("Inicio.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataSet2 = Session("setTareasProfeExport")
        tabla2 = DataSet2.Tables("TareasProfe")
        If tabla2.Rows.Count.Equals(0) Then
            Label2.Text = "No se pueden exportar tareas para esta asignatura"
        Else
            'creamos el documento como 
            Dim documento As New XmlDocument
            Dim cabecera = documento.CreateXmlDeclaration("1.0", "UTF-8", "yes")    'creamos la cabecera del documento XML
            documento.AppendChild(cabecera)
            Dim tareas As XmlElement

            tareas = documento.CreateElement("tareas") 'creamos etiqueta tareas
            tareas.SetAttribute("xmlns:has", "http://ji.ehu.es/has")   'añadimos el atributo de la etiqueta tareas  OPCIONAL 2
            documento.AppendChild(tareas)
            For i = 0 To tabla2.Rows.Count - 1          'recorremos las filas de la tabla 
                Dim row As DataRow = tabla2.Rows(i)     'obtenemos la fila i para obtener sus datos
                Dim tarea As XmlElement                 'creamos los objetos de tipo elemento y atributo
                Dim codigo As XmlAttribute
                Dim descripcion As XmlElement
                Dim hestimadas As XmlElement
                Dim explotacion As XmlElement
                Dim tipotarea As XmlElement

                tarea = documento.CreateElement("tarea")        'instanciamos esos elementos/atributos en el documento 
                codigo = documento.CreateAttribute("codigo")
                descripcion = documento.CreateElement("descripcion")
                hestimadas = documento.CreateElement("hestimadas")
                explotacion = documento.CreateElement("explotacion")
                tipotarea = documento.CreateElement("tipotarea")

                Dim codigoValor As String = row.Item("codigo")          'obtenemos los datos de la fila
                Dim descripcionValor As String = row.Item("descripcion")
                Dim hestimadasValor As String = row.Item("hestimadas")
                Dim explotacionValor As String = row.Item("explotacion")
                Dim tipotareaValor As String = row.Item("tipotarea")

                Dim codigoTexto As XmlText                          'creamos los objetos de tipo texto
                Dim descripcionTexto As XmlText
                Dim hestimadasTexto As XmlText
                Dim explotacionTexto As XmlText
                Dim tipotareaTexto As XmlText

                codigoTexto = documento.CreateTextNode(codigoValor)         'les damos un valor a los objetos de tipo texto con los datos de la fila
                descripcionTexto = documento.CreateTextNode(descripcionValor)
                hestimadasTexto = documento.CreateTextNode(hestimadasValor)
                explotacionTexto = documento.CreateTextNode(explotacionValor)
                tipotareaTexto = documento.CreateTextNode(tipotareaValor)

                codigo.AppendChild(codigoTexto)                         'insertamos el texto obtenido a los objetos de tipo elemento/atributo
                descripcion.AppendChild(descripcionTexto)
                hestimadas.AppendChild(hestimadasTexto)
                explotacion.AppendChild(explotacionTexto)
                tipotarea.AppendChild(tipotareaTexto)

                tarea.Attributes.Append(codigo)                           'completamos el elemento tarea con sus respectivos elementos y atributo
                tarea.AppendChild(descripcion)
                tarea.AppendChild(hestimadas)
                tarea.AppendChild(explotacion)
                tarea.AppendChild(tipotarea)

                documento.DocumentElement.AppendChild(tarea)        'añadimos la tarea como elemento principal del documento
            Next
            Try 'HABRIA QUE HACER UN CONTROL DE DOCUMENTOS PARA NO SOBREESCRIBIRLOS?
                documento.Save(Server.MapPath("App_Data/" & DropDownList1.SelectedValue & ".xml"))      'guardamos el documento
            Catch ex As Exception
                Label2.Text = "Error en el guardado del documento XML"
            End Try
            Label2.Text = "Documento exportado!"
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        DataSet1.Tables.Clear() 'primero limpio la tabla si no me la duplica en el grid view
        conexion = "Server=tcp: hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
        Dim st1 As String = "SELECT TareasGenericas.Codigo, TareasGenericas.Descripcion, TareasGenericas.CodAsig, TareasGenericas.HEstimadas, TareasGenericas.Explotacion, TareasGenericas.TipoTarea FROM TareasGenericas INNER JOIN GruposClase ON TareasGenericas.CodAsig = GruposClase.codigoasig INNER JOIN ProfesoresGrupo ON GruposClase.codigo = ProfesoresGrupo.codigogrupo WHERE (TareasGenericas.CodAsig = '" & DropDownList1.SelectedValue & "') AND (ProfesoresGrupo.email = '" & Session("email") & "')"
        dataAdapter1 = New SqlDataAdapter(st1, conexion)
        commandBuilder1 = New SqlCommandBuilder(dataAdapter1)
        dataAdapter1.Fill(DataSet1, "TareasProfe")
        tabla1 = DataSet1.Tables("TareasProfe")
        If tabla1.Rows.Count.Equals(0) Then
            Label2.Text = "No hay tareas para esta asignatura"
        Else
            GridView1.DataSource = tabla1
            GridView1.DataBind()
        End If
        Session("adapterTareasProfeExport") = dataAdapter1
        Session("setTareasProfeExport") = DataSet1
    End Sub

    Dim tabla, tabla1, tabla2 As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = Session("email")
        If Not Page.IsPostBack Then
            conexion = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
            Dim st As String = "SELECT Asignaturas.codigo FROM Asignaturas  INNER JOIN GruposClase ON GruposClase.codigoasig = Asignaturas.codigo INNER JOIN ProfesoresGrupo ON ProfesoresGrupo.codigogrupo = GruposClase.codigo WHERE ProfesoresGrupo.email ='" & Session("email") & "'"
            dataAdapter = New SqlDataAdapter(st, conexion)
            commandBuilder = New SqlCommandBuilder(dataAdapter)
            dataAdapter.Fill(DataSet, "AsignaturasProfe")
            tabla = DataSet.Tables("AsignaturasProfe")
            If tabla.Rows.Count.Equals(0) Then
                Label2.Text = "No hay asignaturas para este profe"
            Else
                DropDownList1.DataSource = tabla
                DropDownList1.DataTextField = "codigo"
                DropDownList1.DataBind()
            End If
            Dim st1 As String = "SELECT TareasGenericas.Codigo, TareasGenericas.Descripcion, TareasGenericas.CodAsig, TareasGenericas.HEstimadas, TareasGenericas.Explotacion, TareasGenericas.TipoTarea FROM TareasGenericas INNER JOIN GruposClase ON TareasGenericas.CodAsig = GruposClase.codigoasig INNER JOIN ProfesoresGrupo ON GruposClase.codigo = ProfesoresGrupo.codigogrupo WHERE (TareasGenericas.CodAsig = '" & DropDownList1.SelectedValue & "') AND (ProfesoresGrupo.email = '" & Session("email") & "')"
            dataAdapter1 = New SqlDataAdapter(st1, conexion)
            commandBuilder1 = New SqlCommandBuilder(dataAdapter1)
            dataAdapter1.Fill(DataSet1, "TareasProfe")
            tabla1 = DataSet1.Tables("TareasProfe")
            If tabla1.Rows.Count.Equals(0) Then
                Label2.Text = "No hay tareas para esta asignatura"
            Else
                GridView1.DataSource = tabla1
                GridView1.DataBind()
            End If
            Session("adapterAsigProfeExport") = dataAdapter
            Session("setAsigProfeExport") = DataSet
            Session("adapterTareasProfeExport") = dataAdapter1
            Session("setTareasProfeExport") = DataSet1
        Else
            dataAdapter = Session("adapterAsigProfeExport")
            DataSet = Session("setAsigProfeExport")
            dataAdapter1 = Session("adapterTareasProfeExport")
            DataSet1 = Session("setTareasProfeExport")
        End If
    End Sub
End Class