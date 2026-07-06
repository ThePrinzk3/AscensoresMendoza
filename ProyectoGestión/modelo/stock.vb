Imports System.Data
Imports System.Data.SqlClient

Public Class stock
    Public Property id_movimiento As Integer
    Public Property producto_id As Integer
    Public Property codigo As String
    Public Property usuario_id As Integer
    Public Property tipo As String ' entrada o salida
    Public Property personal_trabajador As String
    Public Property cantidad As Integer
    Public Property stock_actual As Integer
    Public Property fecha As DateTime

    ' ============================
    ' OBTENER ID DE PRODUCTO
    ' ============================
    Public Shared Function ObtenerProductoId(codigo As String) As Integer
        Using cn = Conexion.ObtenerConexion()
            Dim query = "SELECT id_producto FROM Producto WHERE codigo = @codigo"
            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@codigo", codigo)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Return Convert.ToInt32(result)
                Else
                    Throw New Exception("Producto no encontrado.")
                End If
            End Using
        End Using
    End Function

    ' ============================
    ' OBTENER STOCK ACTUAL
    ' ============================
    Public Shared Function ObtenerStockActual(producto_id As Integer) As Integer
        Using cn = Conexion.ObtenerConexion()
            Dim query = "SELECT TOP 1 stock FROM StockMovimiento WHERE producto_id = @id ORDER BY fecha DESC"
            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@id", producto_id)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Return Convert.ToInt32(result)
                Else
                    Return 0
                End If
            End Using
        End Using
    End Function

    ' ============================
    ' REGISTRAR MOVIMIENTO
    ' ============================
    Public Function RegistrarMovimiento() As Boolean
        If cantidad <= 0 Then
            Throw New Exception("La cantidad debe ser mayor que 0.")
        End If

        Using cn = Conexion.ObtenerConexion()
            ' 1) Obtener producto
            producto_id = ObtenerProductoId(codigo)
            Dim stockAnterior = ObtenerStockActual(producto_id)

            ' 2) Validación de stock
            If tipo = "salida" AndAlso cantidad > stockAnterior Then
                Throw New Exception("No hay suficiente stock disponible.")
            End If

            ' 3) Validación de trabajador en salidas
            If tipo = "salida" Then
                If String.IsNullOrWhiteSpace(personal_trabajador) Then
                    Throw New Exception("Debe registrar el personal responsable en una salida.")
                End If
            Else
                personal_trabajador = Nothing ' en entradas siempre NULL
            End If

            ' 4) Calcular stock (el trigger recalcula, pero enviamos el valor)
            stock_actual = If(tipo = "entrada", stockAnterior + cantidad, stockAnterior - cantidad)

            ' 5) Obtener proyecto activo
            Dim proyectoActivo As Proyecto = Proyecto.ObtenerProyectoActivo()
            Dim proyectoIdValue As Object

            If proyectoActivo Is Nothing OrElse Not proyectoActivo.estado.Equals("ENCENDIDO", StringComparison.OrdinalIgnoreCase) Then
                proyectoIdValue = DBNull.Value
            Else
                proyectoIdValue = proyectoActivo.id_proyecto
            End If

            ' 6) Insertar en StockMovimiento
            Dim query = "INSERT INTO StockMovimiento " &
                        "(producto_id, usuario_id, tipo, cantidad, stock, fecha, personal_trabajador, proyecto_id) " &
                        "VALUES (@producto_id, @usuario_id, @tipo, @cantidad, @stock, GETDATE(), @personal_trabajador, @proyecto_id)"

            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@producto_id", producto_id)
                cmd.Parameters.AddWithValue("@usuario_id", usuario_id)
                cmd.Parameters.AddWithValue("@tipo", tipo)
                cmd.Parameters.AddWithValue("@cantidad", cantidad)
                cmd.Parameters.AddWithValue("@stock", stock_actual)

                If personal_trabajador IsNot Nothing Then
                    cmd.Parameters.AddWithValue("@personal_trabajador", personal_trabajador)
                Else
                    cmd.Parameters.AddWithValue("@personal_trabajador", DBNull.Value)
                End If

                cmd.Parameters.AddWithValue("@proyecto_id", proyectoIdValue)

                cmd.ExecuteNonQuery()
            End Using
        End Using

        Return True
    End Function

    ' ============================
    ' LISTADO STOCK GENERAL
    ' ============================
    Public Shared Function ObtenerStockGeneral(Optional categoria As String = "Todos",
                                               Optional nombreFiltro As String = "") As DataTable
        Dim tabla As New DataTable()

        Using cn = Conexion.ObtenerConexion()
            Dim query As String = "
                SELECT 
                    p.codigo AS Codigo,
                    p.nombre AS Nombre,
                    p.categoria AS Categoria,
                    ISNULL(sm.stock, 0) AS Stock_Actual,
                    sm.fecha AS Fecha
                FROM Producto p
                LEFT JOIN (
                    SELECT sm1.producto_id, sm1.stock, sm1.fecha
                    FROM StockMovimiento sm1
                    WHERE sm1.fecha = (
                        SELECT MAX(sm2.fecha) 
                        FROM StockMovimiento sm2 
                        WHERE sm2.producto_id = sm1.producto_id
                    )
                ) sm ON sm.producto_id = p.id_producto
                WHERE (@categoria = 'Todos' OR p.categoria = @categoria)
                  AND (@nombreFiltro = '' OR p.nombre LIKE '%' + @nombreFiltro + '%')
                ORDER BY p.nombre ASC"

            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@categoria", categoria)
                cmd.Parameters.AddWithValue("@nombreFiltro", nombreFiltro)

                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using

        Return tabla
    End Function

    ' ============================
    ' STOCK EN ALERTA
    ' ============================
    Public Shared Function ObtenerStockEnAlerta(Optional categoria As String = "Todos") As DataTable
        Dim tabla As New DataTable()

        Using cn = Conexion.ObtenerConexion()
            Dim query As String = "
                SELECT 
                    p.categoria AS Categoria,
                    p.nombre AS Nombre,
                    ISNULL(sm.stock, 0) AS Stock_Actual
                FROM Producto p
                LEFT JOIN (
                    SELECT sm1.producto_id, sm1.stock
                    FROM StockMovimiento sm1
                    WHERE sm1.fecha = (
                        SELECT MAX(sm2.fecha) 
                        FROM StockMovimiento sm2 
                        WHERE sm2.producto_id = sm1.producto_id
                    )
                ) sm ON sm.producto_id = p.id_producto
                WHERE (@categoria = 'Todos' OR p.categoria = @categoria)
                  AND (
                        (p.categoria = 'EPPs' AND ISNULL(sm.stock,0) < 5) OR
                        (p.categoria = 'Materiales' AND ISNULL(sm.stock,0) < 30) OR
                        (p.categoria = 'Herramientas' AND ISNULL(sm.stock,0) < 10) OR
                        (p.categoria = 'Chatarra' AND 1 = 0)
                      )
                ORDER BY p.nombre ASC"

            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@categoria", categoria)

                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using

        Return tabla
    End Function

    ' ============================
    ' HISTORIAL POR CÓDIGO
    ' ============================
    Public Shared Function ObtenerHistorial(codigo As String) As DataTable
        Dim tabla As New DataTable()

        Using cn = Conexion.ObtenerConexion()
            Dim query = "
                SELECT 
                    p.codigo AS Codigo,
                    p.nombre AS Nombre,
                    p.categoria AS Categoria,
                    sm.tipo AS Tipo_Movimiento,
                    sm.cantidad AS Cantidad,
                    sm.stock AS Stock_Actual,
                    sm.fecha AS Fecha,
                    CASE WHEN sm.tipo = 'salida' THEN sm.personal_trabajador ELSE '' END AS Personal_Trabajador
                FROM StockMovimiento sm
                JOIN Producto p ON p.id_producto = sm.producto_id
                WHERE p.codigo = @codigo
                ORDER BY sm.fecha DESC"

            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@codigo", codigo)

                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using

        Return tabla
    End Function

    ' ============================
    ' HISTORIAL GENERAL
    ' ============================
    Public Shared Function ObtenerHistorialGeneral(Optional categoria As String = "Todos",
                                                   Optional nombreFiltro As String = "",
                                                   Optional codigoFiltro As String = "",
                                                   Optional trabajadorFiltro As String = "") As DataTable
        Dim tabla As New DataTable()

        Using cn = Conexion.ObtenerConexion()
            Dim query = "
                SELECT TOP 200
                    p.codigo AS Codigo,
                    p.nombre AS Nombre,
                    p.categoria AS Categoria,
                    sm.tipo AS Tipo_Movimiento,
                    sm.cantidad AS Cantidad,
                    sm.stock AS Stock_Actual,
                    sm.fecha AS Fecha,
                    CASE WHEN sm.tipo = 'salida' THEN sm.personal_trabajador ELSE '' END AS Personal
                FROM StockMovimiento sm
                JOIN Producto p ON p.id_producto = sm.producto_id
                WHERE (@categoria = 'Todos' OR p.categoria = @categoria)
                  AND (@nombreFiltro = '' OR p.nombre LIKE '%' + @nombreFiltro + '%')
                  AND (@codigoFiltro = '' OR p.codigo LIKE '%' + @codigoFiltro + '%')
                  AND (@trabajadorFiltro = '' OR (sm.tipo = 'salida' AND sm.personal_trabajador LIKE '%' + @trabajadorFiltro + '%'))
                ORDER BY sm.fecha DESC"

            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@categoria", categoria)
                cmd.Parameters.AddWithValue("@nombreFiltro", nombreFiltro)
                cmd.Parameters.AddWithValue("@codigoFiltro", codigoFiltro)
                cmd.Parameters.AddWithValue("@trabajadorFiltro", trabajadorFiltro)

                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using

        Return tabla
    End Function

    ' ============================
    ' STOCK POR CÓDIGO (RESUMIDO)
    ' ============================
    Public Shared Function ObtenerStockPorCodigo(codigo As String) As DataTable
        Dim tabla As New DataTable()

        Using cn = Conexion.ObtenerConexion()
            Dim query = "
                SELECT 
                    p.codigo AS Codigo,
                    p.nombre AS Nombre,
                    p.categoria AS Categoria,
                    ISNULL(sm.stock, 0) AS Stock_Actual,
                    sm.fecha AS Fecha
                FROM Producto p
                LEFT JOIN (
                    SELECT producto_id, stock, fecha
                    FROM StockMovimiento sm1
                    WHERE sm1.fecha = (
                        SELECT MAX(fecha) 
                        FROM StockMovimiento 
                        WHERE producto_id = sm1.producto_id
                    )
                ) sm ON sm.producto_id = p.id_producto
                WHERE p.codigo = @codigo"

            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@codigo", codigo)

                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using

        Return tabla
    End Function

End Class
