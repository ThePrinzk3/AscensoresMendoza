Imports System.Data.SqlClient

Public Class Users
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Contraseña As String

    ' ----------------------------
    ' Verificar credenciales en BD
    ' ----------------------------
    Public Shared Function VerificarCredenciales(usuario As String, contraseña As String) As Boolean
        Dim query As String = "SELECT COUNT(1) FROM Usuario WHERE Nombre = @Usuario AND Contraseña = @Contraseña"

        Using cn As SqlConnection = Conexion.ObtenerConexion()
            Using comando As New SqlCommand(query, cn)
                comando.Parameters.AddWithValue("@Usuario", usuario)
                comando.Parameters.AddWithValue("@Contraseña", contraseña)

                Dim resultado As Integer = CInt(comando.ExecuteScalar())
                Return resultado > 0
            End Using
        End Using
    End Function

    ' --------------------------------------------
    ' Obtener un usuario por nombre desde la BD
    ' --------------------------------------------
    Public Shared Function ObtenerPorNombre(nombreUsuario As String) As Users
        Dim usuario As Users = Nothing
        Dim query As String = "SELECT Id, Nombre, Contraseña FROM Usuario WHERE Nombre = @Nombre"

        Using cn As SqlConnection = Conexion.ObtenerConexion()
            Using comando As New SqlCommand(query, cn)
                comando.Parameters.AddWithValue("@Nombre", nombreUsuario)

                Using reader As SqlDataReader = comando.ExecuteReader()
                    If reader.Read() Then
                        usuario = New Users() With {
                            .Id = Convert.ToInt32(reader("Id")),
                            .Nombre = reader("Nombre").ToString(),
                            .Contraseña = reader("Contraseña").ToString()
                        }
                    End If
                End Using
            End Using
        End Using

        Return usuario
    End Function
End Class
