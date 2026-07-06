Imports System.Data.SqlClient

Public Class Conexion
    ' Cadena de conexión con autenticación de Windows (integrada)
    Private Shared ReadOnly _cadena As String = "Server=localhost;Database=DB_ElevadoresMontacarga_vb;Integrated Security=True;"

    ' Propiedad pública para acceder a la cadena de conexión
    Public Shared ReadOnly Property CadenaConexion As String
        Get
            Return _cadena
        End Get
    End Property

    ' Método para obtener una conexión abierta
    Public Shared Function ObtenerConexion() As SqlConnection
        Dim conexion As New SqlConnection(_cadena)
        Try
            conexion.Open()
            Return conexion
        Catch ex As Exception
            Throw New Exception("Error al conectar: " & ex.Message)
        End Try
    End Function

    ' Método opcional para probar la conexión
    Public Shared Sub ProbarConexion()
        Try
            Using conexion As SqlConnection = ObtenerConexion()
                MessageBox.Show("Conexión exitosa a SQL Server", "Conectado")
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al conectar: " & ex.Message, "Error")
        End Try
    End Sub
End Class