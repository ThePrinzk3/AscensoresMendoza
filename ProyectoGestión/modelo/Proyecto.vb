Imports System.Data.SqlClient

Public Class Proyecto
    Public Property id_proyecto As Integer
    Public Property codigo As String
    Public Property nombre As String
    Public Property estado As String
    Public Property fecha_creacion As DateTime

    ' Generar código: P001, P002, ...
    Public Shared Function GenerarCodigo() As String
        Using cn = Conexion.ObtenerConexion()
            Dim cmd As New SqlCommand("SELECT ISNULL(MAX(codigo), 'P000') FROM Proyecto", cn)
            Dim lastCode As String = CStr(cmd.ExecuteScalar())
            Dim num As Integer = CInt(lastCode.Substring(1)) + 1
            Return "P" & num.ToString("000")
        End Using
    End Function

    Public Function Guardar() As Boolean
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String = "INSERT INTO Proyecto (codigo, nombre, estado) VALUES (@codigo, @nombre, @estado)"
            Dim cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@codigo", codigo)
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@estado", estado)
            Return cmd.ExecuteNonQuery() > 0
        End Using
    End Function

    Public Function Actualizar() As Boolean
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String = "UPDATE Proyecto SET nombre = @nombre, estado = @estado WHERE id_proyecto = @id"
            Dim cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@estado", estado)
            cmd.Parameters.AddWithValue("@id", id_proyecto)
            Return cmd.ExecuteNonQuery() > 0
        End Using
    End Function

    Public Function Eliminar() As Boolean
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String = "DELETE FROM Proyecto WHERE id_proyecto = @id"
            Dim cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", id_proyecto)
            Return cmd.ExecuteNonQuery() > 0
        End Using
    End Function

    Public Shared Function Listar() As DataTable
        Dim dt As New DataTable()
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String = "SELECT id_proyecto, codigo, nombre, estado FROM Proyecto ORDER BY id_proyecto DESC"
            Dim da As New SqlDataAdapter(sql, cn)
            da.Fill(dt)
        End Using
        Return dt
    End Function

    Public Shared Function ObtenerProyectoActivo() As Proyecto
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String = "SELECT TOP 1 * FROM Proyecto WHERE estado = 'ENCENDIDO'"
            Dim cmd As New SqlCommand(sql, cn)
            Using dr = cmd.ExecuteReader()
                If dr.Read() Then
                    Dim p As New Proyecto()
                    p.id_proyecto = CInt(dr("id_proyecto"))
                    p.codigo = dr("codigo").ToString()
                    p.nombre = dr("nombre").ToString()
                    p.estado = dr("estado").ToString()
                    p.fecha_creacion = CDate(dr("fecha_creacion"))
                    Return p
                End If
            End Using
        End Using
        Return Nothing
    End Function
End Class
