Imports System.Data.SqlClient

Public Class reporte
    Public Shared Function ObtenerTodosLosProductos() As DataTable
        Dim tabla As New DataTable()

        Using cn = Conexion.ObtenerConexion()
            Dim query As String = "
                SELECT 
                    p.codigo AS Codigo,
                    p.nombre AS Nombre,
                    p.categoria AS Categoria,
                    p.costo AS Costo,
                    ISNULL(m.stock, 0) AS Stock
                FROM Producto p
                LEFT JOIN (
                    SELECT m1.producto_id, m1.stock
                    FROM StockMovimiento m1
                    INNER JOIN (
                        SELECT producto_id, MAX(fecha) AS max_fecha
                        FROM StockMovimiento
                        GROUP BY producto_id
                    ) m2 ON m1.producto_id = m2.producto_id AND m1.fecha = m2.max_fecha
                ) m ON p.id_producto = m.producto_id
                ORDER BY p.fecha_registro DESC"

            Using cmd As New SqlCommand(query, cn)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using

        Return tabla
    End Function


    Public Shared Function ObtenerPorCategoria(categoria As String) As DataTable
        Dim tabla As New DataTable()

        Using cn = Conexion.ObtenerConexion()
            Dim query As String = "
                SELECT 
                    p.codigo AS Codigo,
                    p.nombre AS Nombre,
                    p.categoria AS Categoria,
                    p.costo AS Costo,
                    ISNULL(m.stock, 0) AS Stock
                FROM Producto p
                LEFT JOIN (
                    SELECT m1.producto_id, m1.stock
                    FROM StockMovimiento m1
                    INNER JOIN (
                        SELECT producto_id, MAX(fecha) AS max_fecha
                        FROM StockMovimiento
                        GROUP BY producto_id
                    ) m2 ON m1.producto_id = m2.producto_id AND m1.fecha = m2.max_fecha
                ) m ON p.id_producto = m.producto_id
                WHERE p.categoria = @categoria
                ORDER BY p.fecha_registro DESC"

            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@categoria", categoria)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using

        Return tabla
    End Function



    Public Shared Function GenerarReporteCompletoAvanzado() As DataTable
        Dim tabla As New DataTable()
        Using cn = Conexion.ObtenerConexion()
            Dim query As String = "
        SELECT 
            p.codigo AS Codigo,
            p.nombre AS Producto,
            p.categoria AS Categoria,
            p.costo AS Costo,
            ISNULL(m.stock, 0) AS [Stock Actual],
            m.fecha AS [Fecha Ult. Movimiento],
            (p.costo * ISNULL(m.stock, 0)) AS [Valor Neto]
        FROM Producto p
        LEFT JOIN (
            SELECT m1.producto_id, m1.stock, m1.fecha
            FROM StockMovimiento m1
            INNER JOIN (
                SELECT producto_id, MAX(fecha) AS max_fecha
                FROM StockMovimiento
                GROUP BY producto_id
            ) m2 ON m1.producto_id = m2.producto_id AND m1.fecha = m2.max_fecha
        ) m ON p.id_producto = m.producto_id
        ORDER BY p.codigo ASC, p.categoria ASC"

            Using cmd As New SqlCommand(query, cn)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using
        Return tabla
    End Function


    Public Shared Function GenerarReportePorCategoria(categoria As String) As DataTable
        Dim tabla As New DataTable()
        Using cn = Conexion.ObtenerConexion()
            Dim query As String = "
        SELECT 
            p.codigo AS Codigo,
            p.nombre AS Producto,
            p.categoria AS Categoria,
            p.costo AS Costo,
            ISNULL(m.stock, 0) AS [Stock Actual],
            m.fecha AS [Fecha Ult. Movimiento],
            (p.costo * ISNULL(m.stock, 0)) AS [Valor Neto]
        FROM Producto p
        LEFT JOIN (
            SELECT m1.producto_id, m1.stock, m1.fecha
            FROM StockMovimiento m1
            INNER JOIN (
                SELECT producto_id, MAX(fecha) AS max_fecha
                FROM StockMovimiento
                GROUP BY producto_id
            ) m2 ON m1.producto_id = m2.producto_id AND m1.fecha = m2.max_fecha
        ) m ON p.id_producto = m.producto_id
        WHERE p.categoria = @categoria
        ORDER BY p.codigo ASC, p.categoria ASC"

            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@categoria", categoria)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using
        Return tabla
    End Function



    Public Shared Function CalcularTotales(Optional categoria As String = "Todos") As Dictionary(Of String, Decimal)
        Dim resultado As New Dictionary(Of String, Decimal)()

        Dim query As String = "
        SELECT 
            p.categoria,
            p.costo,
            ISNULL(m.stock, 0) AS stock,
            (p.costo * ISNULL(m.stock, 0)) AS valor_neto
        FROM Producto p
        LEFT JOIN (
            SELECT m1.producto_id, m1.stock
            FROM StockMovimiento m1
            INNER JOIN (
                SELECT producto_id, MAX(fecha) AS max_fecha
                FROM StockMovimiento
                GROUP BY producto_id
            ) m2 ON m1.producto_id = m2.producto_id AND m1.fecha = m2.max_fecha
        ) m ON p.id_producto = m.producto_id
        WHERE p.costo IS NOT NULL"

        If categoria <> "Todos" AndAlso categoria <> "Chatarra" Then
            query &= " AND p.categoria = @categoria"
        ElseIf categoria = "Chatarra" Then
            ' No calcular nada, devolver diccionario vacío
            Return resultado
        End If

        Dim tabla As New DataTable()
        Using cn = Conexion.ObtenerConexion()
            Using cmd As New SqlCommand(query, cn)
                If categoria <> "Todos" AndAlso categoria <> "Chatarra" Then
                    cmd.Parameters.AddWithValue("@categoria", categoria)
                End If
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using

        If categoria = "Todos" Then
            resultado("Total General") = tabla.AsEnumerable().Sum(Function(r) Convert.ToDecimal(r("valor_neto")))
            resultado("EPPs") = tabla.AsEnumerable().Where(Function(r) r("categoria").ToString() = "EPPs").Sum(Function(r) Convert.ToDecimal(r("valor_neto")))
            resultado("Herramientas") = tabla.AsEnumerable().Where(Function(r) r("categoria").ToString() = "Herramientas").Sum(Function(r) Convert.ToDecimal(r("valor_neto")))
            resultado("Materiales") = tabla.AsEnumerable().Where(Function(r) r("categoria").ToString() = "Materiales").Sum(Function(r) Convert.ToDecimal(r("valor_neto")))
        ElseIf categoria <> "Chatarra" Then
            resultado(categoria) = tabla.AsEnumerable().Sum(Function(r) Convert.ToDecimal(r("valor_neto")))
        End If

        Return resultado
    End Function


End Class
