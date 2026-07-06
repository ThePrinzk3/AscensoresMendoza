Imports System.Data.SqlClient

Public Class ProyectoAdicional
    Public Property id_adicional As Integer
    Public Property proyecto_id As Integer
    Public Property codigo As String
    Public Property nombre As String
    Public Property descripcion As String
    Public Property categoria As String = "ADICIONAL"
    Public Property costo As Decimal
    Public Property cantidad As Integer

    Public Shared Function GenerarCodigo() As String
        Using cn = Conexion.ObtenerConexion()
            Dim cmd As New SqlCommand("SELECT ISNULL(MAX(codigo), 'A000') FROM ProyectoAdicional", cn)
            Dim lastCode As String = CStr(cmd.ExecuteScalar())
            Dim num As Integer = CInt(lastCode.Substring(1)) + 1
            Return "A" & num.ToString("000")
        End Using
    End Function

    Public Function Guardar() As Boolean
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String =
                "INSERT INTO ProyectoAdicional (proyecto_id, codigo, nombre, descripcion, categoria, costo, cantidad) " &
                "VALUES (@proyecto_id, @codigo, @nombre, @descripcion, @categoria, @costo, @cantidad)"
            Dim cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@proyecto_id", proyecto_id)
            cmd.Parameters.AddWithValue("@codigo", codigo)
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@descripcion", If(descripcion, CType(DBNull.Value, Object)))
            cmd.Parameters.AddWithValue("@categoria", categoria)
            cmd.Parameters.AddWithValue("@costo", costo)
            cmd.Parameters.AddWithValue("@cantidad", cantidad)
            Return cmd.ExecuteNonQuery() > 0
        End Using
    End Function

    Public Function Eliminar() As Boolean
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String = "DELETE FROM ProyectoAdicional WHERE id_adicional = @id"
            Dim cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", id_adicional)
            Return cmd.ExecuteNonQuery() > 0
        End Using
    End Function

    Public Function Actualizar() As Boolean
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String =
            "UPDATE ProyectoAdicional 
             SET nombre = @nombre,
                 descripcion = @descripcion,
                 costo = @costo,
                 cantidad = @cantidad
             WHERE id_adicional = @id"
            Dim cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", id_adicional)
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@descripcion", If(descripcion, CType(DBNull.Value, Object)))
            cmd.Parameters.AddWithValue("@costo", costo)
            cmd.Parameters.AddWithValue("@cantidad", cantidad)
            Return cmd.ExecuteNonQuery() > 0
        End Using
    End Function


    Public Shared Function ListarPorProyecto(idProyecto As Integer) As DataTable
        Dim dt As New DataTable()
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String =
                "SELECT id_adicional, codigo, nombre, descripcion, categoria, costo, cantidad " &
                "FROM ProyectoAdicional WHERE proyecto_id = @idProyecto"
            Dim da As New SqlDataAdapter(sql, cn)
            da.SelectCommand.Parameters.AddWithValue("@idProyecto", idProyecto)
            da.Fill(dt)
        End Using
        Return dt
    End Function
End Class
