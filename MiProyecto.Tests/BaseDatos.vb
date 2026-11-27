Imports System.Configuration
Imports System.Data.SqlClient

Friend Module BaseDatos

    Private Function Conectar() As SqlConnection
        Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("DB_ElevadoresMontacarga_vb").ConnectionString)
        cn.Open()
        Return cn
    End Function

    Friend Function Escalar(sql As String, ParamArray p As SqlParameter()) As Object
        Using cn As SqlConnection = Conectar()
            Using cmd As New SqlCommand(sql, cn)
                If p IsNot Nothing Then cmd.Parameters.AddRange(p)
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Friend Sub Ejecutar(sql As String, ParamArray p As SqlParameter())
        Using cn As SqlConnection = Conectar()
            Using cmd As New SqlCommand(sql, cn)
                If p IsNot Nothing Then cmd.Parameters.AddRange(p)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' ---------- Stock y Movimientos ----------
    Friend Function LeerStock(codigo As String) As Integer
        Dim sql As String = "SELECT ISNULL(SUM(CASE WHEN sm.tipo = 'entrada' THEN sm.cantidad ELSE -sm.cantidad END), 0) " &
                            "FROM StockMovimiento sm " &
                            "INNER JOIN Producto p ON sm.producto_id = p.id_producto " &
                            "WHERE p.codigo = @c"
        Dim res = Escalar(sql, New SqlParameter("@c", codigo))
        Return If(res IsNot DBNull.Value AndAlso res IsNot Nothing, Convert.ToInt32(res), 0)
    End Function

    Friend Sub FijarStock(codigo As String, cantidad As Integer)
        ' Limpia movimientos previos e inserta la entrada inicial requerida para la prueba
        Dim sqlFix As String = "DECLARE @pid INT; " &
                               "SELECT @pid = id_producto FROM Producto WHERE codigo = @c; " &
                               "IF @pid IS NOT NULL BEGIN " &
                               "    DELETE FROM StockMovimiento WHERE producto_id = @pid; " &
                               "    IF @cant > 0 BEGIN " &
                               "        INSERT INTO StockMovimiento (producto_id, usuario_id, tipo, cantidad, personal_trabajador) " &
                               "        VALUES (@pid, 1, 'entrada', @cant, NULL); " &
                               "    END " &
                               "END"
        Ejecutar(sqlFix, New SqlParameter("@c", codigo), New SqlParameter("@cant", cantidad))
    End Sub

    Friend Function ContarMovimientos(codigo As String) As Integer
        Dim sql As String = "SELECT COUNT(*) FROM StockMovimiento sm INNER JOIN Producto p ON sm.producto_id = p.id_producto WHERE p.codigo = @c"
        Return Convert.ToInt32(Escalar(sql, New SqlParameter("@c", codigo)))
    End Function

    ' ---------- Productos ----------
    Friend Function LeerCosto(codigo As String) As Decimal
        Dim res = Escalar("SELECT ISNULL(costo, 0) FROM Producto WHERE codigo = @c", New SqlParameter("@c", codigo))
        Return If(res IsNot DBNull.Value AndAlso res IsNot Nothing, Convert.ToDecimal(res), 0D)
    End Function

    Friend Sub FijarCosto(codigo As String, costo As Decimal)
        Ejecutar("UPDATE Producto SET costo = @n WHERE codigo = @c", New SqlParameter("@n", costo), New SqlParameter("@c", codigo))
    End Sub

    Friend Function ContarProductos(codigo As String) As Integer
        Return Convert.ToInt32(Escalar("SELECT COUNT(*) FROM Producto WHERE codigo = @c", New SqlParameter("@c", codigo)))
    End Function

    Friend Sub BorrarProducto(codigo As String)
        ' Gracias a las Claves Foráneas ON DELETE CASCADE, se borran automáticamente sus tablas hijas
        Ejecutar("DELETE FROM Producto WHERE codigo = @c", New SqlParameter("@c", codigo))
    End Sub

    ' ---------- Proyectos ----------
    Friend Function EstadoProyecto(codigo As String) As String
        Dim res = Escalar("SELECT estado FROM Proyecto WHERE codigo = @c OR nombre = @c", New SqlParameter("@c", codigo))
        Return If(res IsNot Nothing, res.ToString().Trim().ToUpper(), "")
    End Function

    Friend Sub BorrarProyecto(codigo As String)
        Ejecutar("DELETE FROM Proyecto WHERE codigo = @c OR nombre = @c", New SqlParameter("@c", codigo))
    End Sub

End Module