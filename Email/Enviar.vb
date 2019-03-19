Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.NetworkCredentials

Public Class Enviar
    Public Function enviarEmail(ByVal random As Long, ByVal emailDestino As String, ByVal msg As String) As Boolean
        Try
            'Direccion de origen
            Dim from_address As New MailAddress("arevilla009@ikasle.ehu.eus", "Adrian Revilla")
            'Direccion de destino
            Dim to_address As New MailAddress(emailDestino)
            'Password de la cuenta
            Dim from_pass As String = "ADRI2606rev"
            'Objeto para el cliente smtp
            Dim smtp As New SmtpClient
            'Host en este caso el servidor de gmail
            smtp.Host = "smtp.ehu.eus"
            'Puerto
            smtp.Port = 587
            'SSL activado para que se manden correos de manera segura
            smtp.EnableSsl = True
            'No usar los credenciales por defecto ya que si no no funciona
            smtp.UseDefaultCredentials = False
            'Credenciales
            smtp.Credentials = New System.Net.NetworkCredential(from_address.Address, from_pass)
            'Creamos el mensaje con los parametros de origen y destino
            Dim message As New MailMessage(from_address, to_address)
            'Añadimos el asunto
            message.Subject = "Mensaje confirmacion"
            'Concatenamos el cuerpo del mensaje a la plantilla
            message.Body = "<html><head></head><body>" & msg & "</body></html>"
            'Definimos el cuerpo como html para poder escojer mejor como lo mandamos
            message.IsBodyHtml = True
            'Se envia el correo
            smtp.Send(message)
        Catch e As Exception
            Return False
        End Try
        Return True
    End Function
End Class


Public Class Datos
    Private conexion As New SqlConnection
    Private comando As New SqlCommand
    Private dataAdapter, dataAdapter2 As New SqlDataAdapter()
    Private commandBuilder, commandBuilder2 As New SqlCommandBuilder()
    Private dataSet, dataSet2 As New DataSet



    Public Function conectar() As String
        Try
            conexion.ConnectionString = "Server=tcp:hads18-revilla.database.windows.net,1433;Initial Catalog=HADS18-REVILLA;Persist Security Info=False;User ID=Revilla@hads18-revilla;Password=ADRI2606rev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
            conexion.Open()
        Catch ex As Exception
            Return "ERROR" + ex.Message
        End Try
        Return "OK"
    End Function

    Public Function cerrar() As String
        conexion.Close()
        Return "OK"
    End Function

    Public Function insertar(ByVal nombre As String, ByVal apellidos As String, ByVal pass As String, ByVal email As String, ByVal tipo As String, ByVal numconfir As Integer, ByVal confirmado As Integer, ByVal codpass As Integer) As String
        Dim st = "insert into Usuarios(email,nombre,apellidos,numconfir,confirmado,tipo,pass,codpass) values ('" & email & "','" & nombre & "','" & apellidos & "','" & numconfir & "','" & confirmado & "','" & tipo & "','" & pass & "','" & codpass & "')"
        Dim numregs As Integer
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteNonQuery()
        Catch ex As Exception
            Return ex.Message
        End Try
        Return (numregs & " registro(s) insertado(s) en la BD ")
    End Function

    Public Function contar() As String
        Dim st = "select count(*) from Usuarios"
        comando = New SqlCommand(st, conexion)
        Return comando.ExecuteScalar()
    End Function

    Public Function contarRegistrados() As String
        Dim st = "select count(*) from Usuarios where confirmado=1"
        comando = New SqlCommand(st, conexion)
        Return comando.ExecuteScalar()
    End Function

    Public Function usuarioLogeado(ByVal email As String, ByVal pass As String) As String
        Dim st = "select count(*) from Usuarios where email= '" & email & "' and pass= '" & pass & "' and confirmado = 1"
        comando = New SqlCommand(st, conexion)
        Return comando.ExecuteScalar()
    End Function

    Public Function obtenerUsuariosConfirmados() As SqlDataReader
        Dim st = "select * from Usuarios where confirmado=1"    'booleano = true se pone con el 1
        comando = New SqlCommand(st, conexion)
        Return (comando.ExecuteReader())
    End Function

    Public Function obtenerUsuarios() As SqlDataReader
        Dim st = "select * from Usuarios"    'booleano = true se pone con el 1
        comando = New SqlCommand(st, conexion)
        Return (comando.ExecuteReader())
    End Function

    Public Function actualizarClave(ByVal email As String) As String
        Dim st = "update Usuarios set confirmado = 1 where email = '" & email & "'"
        Dim numregs As Integer
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteNonQuery()
        Catch ex As Exception
            Return ex.Message
        End Try
        Return (numregs & " registro(s) actualizado(s) en la BD ")
    End Function

    Public Function correoRepetido(ByVal email As String) As String
        Dim st = "select count(*) from Usuarios where email='" & email & "'"
        comando = New SqlCommand(st, conexion)
        Return comando.ExecuteScalar()
    End Function

    Public Function actualizarContraseña(ByVal email As String, ByVal passVieja As String, ByVal passNueva As String) As String
        Dim st = "update Usuarios set pass = '" & passNueva & "' where email = '" & email & "'and pass= '" & passVieja & "' and confirmado = 1"
        Dim numregs As Integer
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteNonQuery()
        Catch ex As Exception
            Return ex.Message
        End Try
        Return (numregs & " registro(s) actualizado(s) en la BD ")
    End Function

    Public Function obtenerNumero(ByVal email As String, ByVal pass As String) As Integer
        Dim st = "select numConfir from Usuarios where email= '" & email & "' and pass= '" & pass & "' and confirmado = 1"
        comando = New SqlCommand(st, conexion)
        Return comando.ExecuteScalar()
    End Function

    ' A partir de aqui funciones del lab3
    Public Function verTiposUsuario(ByVal email As String, ByVal pass As String) As String
        Dim st = "select tipo from Usuarios where email= '" & email & "' and pass= '" & pass & "' and confirmado = 1"
        comando = New SqlCommand(st, conexion)
        Return comando.ExecuteScalar()
    End Function

    Public Function obtenerAsignaturas(ByVal email As String, ByVal conect As String, ByRef da As SqlDataAdapter) As DataSet
        Dim st = "select GruposClase.codigoasig from EstudiantesGrupo inner join GruposClase on EstudiantesGrupo.Grupo=GruposClase.codigo and EstudiantesGrupo.Email='" & email & "' "
        dataAdapter = New SqlDataAdapter(st, conect)    'creamos el adaptador con la query y la conexion
        da = dataAdapter
        commandBuilder = New SqlCommandBuilder(dataAdapter) 'creamos el comando para que se pueda ejecutar
        dataAdapter.Fill(dataSet, "GruposClase")    'realiza el comando y llena la tabla "gruposclase" que se la asigna a un dataset
        Return dataSet 'devolvemos el dataset
    End Function

    Public Function obtenerTareas(ByVal email As String, ByVal conect As String, ByVal codigo As String, ByRef da As SqlDataAdapter) As DataSet
        Dim st = "select TareasGenericas.Codigo, TareasGenericas.Descripcion, TareasGenericas.HEstimadas, TareasGenericas.TipoTarea from TareasGenericas where TareasGenericas.CodAsig='" & codigo & "' and TareasGenericas.Explotacion='True' and TareasGenericas.Codigo not in (select EstudiantesTareas.CodTarea from EstudiantesTareas  where EstudiantesTareas.Email='" & email & "')"
        ' aparte de obtener las tareas con el codigo que esten en explotacion, hay que mirar que el alumno no tenga ya asignada esa tarea -> mediante su email y su codigo de la asignatura
        dataAdapter2 = New SqlDataAdapter(st, conect)
        da = dataAdapter2
        commandBuilder2 = New SqlCommandBuilder(dataAdapter2)
        dataAdapter2.Fill(dataSet2, "TareasGenericas")
        Return dataSet2
    End Function


    Public Function insertarTarea(ByVal email As String, ByVal codigo As String, ByVal hPrevistas As String, ByVal hReales As String) As String
        Dim st = "insert into EstudiantesTareas(Email,CodTarea,HEstimadas,HReales) values ('" & email & "','" & codigo & "','" & hPrevistas & "')"
        Dim numregs As Integer
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteNonQuery()
        Catch ex As Exception
            Return ex.Message
        End Try
        Return (numregs & " registro(s) insertado(s) en la BD ")
    End Function
End Class