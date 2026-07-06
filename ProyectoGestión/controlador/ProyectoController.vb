Imports System.Data
Imports System.Data.SqlClient

Public Class ProyectoController

    ' -------- PROYECTOS --------

    Public Sub CargarProyectos(grid As DataGridView)
        grid.DataSource = Proyecto.Listar()
    End Sub

    Public Function RegistrarProyecto(nombre As String) As Boolean
        Dim p As New Proyecto()
        p.codigo = Proyecto.GenerarCodigo()
        p.nombre = nombre
        p.estado = "APAGADO"
        Return p.Guardar()
    End Function

    Public Sub ApagarTodos()
        Using cn = Conexion.ObtenerConexion()
            Dim cmd As New SqlCommand("UPDATE Proyecto SET estado = 'APAGADO'", cn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Function EncenderProyecto(idProyecto As Integer) As Boolean
        Using cn = Conexion.ObtenerConexion()
            Dim trans = cn.BeginTransaction()
            Try
                Dim cmdOff As New SqlCommand("UPDATE Proyecto SET estado = 'APAGADO'", cn, trans)
                cmdOff.ExecuteNonQuery()

                Dim cmdOn As New SqlCommand("UPDATE Proyecto SET estado = 'ENCENDIDO' WHERE id_proyecto = @id", cn, trans)
                cmdOn.Parameters.AddWithValue("@id", idProyecto)
                cmdOn.ExecuteNonQuery()

                trans.Commit()
                Return True
            Catch
                trans.Rollback()
                Return False
            End Try
        End Using
    End Function

    Public Function ObtenerProyectoActivo() As Proyecto
        Return Proyecto.ObtenerProyectoActivo()
    End Function

    ' -------- ADICIONALES --------

    Public Function RegistrarAdicional(actual As Proyecto, nombre As String,
                                      descripcion As String, costo As Decimal,
                                      cantidad As Integer) As Boolean
        If actual Is Nothing OrElse actual.estado <> "ENCENDIDO" Then
            Return False
        End If

        Dim a As New ProyectoAdicional()
        a.proyecto_id = actual.id_proyecto
        a.codigo = ProyectoAdicional.GenerarCodigo()
        a.nombre = nombre
        a.descripcion = descripcion
        a.costo = costo
        a.cantidad = cantidad

        Return a.Guardar()
    End Function

    ' -------- DETALLE DEL PROYECTO (dtvAdicional / Exportar) --------
    ' NUEVO: devuelve el detalle del proyecto en un DataTable ya ORDENADO
    '   - Primero productos de STOCK
    '   - Luego ADICIONALES
    '   - Dentro de cada grupo, nombre de Z a A
    Public Function ObtenerDetalleProyectoTabla(idProyecto As Integer) As DataTable
        Dim dt As New DataTable()

        Using cn = Conexion.ObtenerConexion()
            Dim sql As String =
"SELECT *
 FROM (
    SELECT 
        0 AS OrdenOrigen,                 -- 0 = STOCK
        NULL AS id_adicional,
        sm.id_movimiento AS id_stock_mov, -- 👈 NUEVO
        p.id_producto,                    -- 👈 NUEVO
        p.codigo,
        p.nombre,
        p.descripcion,
        p.categoria,
        p.costo,
        sm.cantidad,
        'STOCK' AS origen
    FROM StockMovimiento sm
    INNER JOIN Producto p ON p.id_producto = sm.producto_id
    WHERE sm.proyecto_id = @idProyecto AND sm.tipo = 'salida'

    UNION ALL

    SELECT 
        1 AS OrdenOrigen,                 -- 1 = ADICIONAL
        pa.id_adicional,
        NULL AS id_stock_mov,             -- 👈 NUEVO
        NULL AS id_producto,              -- 👈 NUEVO
        pa.codigo,
        pa.nombre,
        pa.descripcion,
        pa.categoria,
        pa.costo,
        pa.cantidad,
        'ADICIONAL' AS origen
    FROM ProyectoAdicional pa
    WHERE pa.proyecto_id = @idProyecto
 ) x
 ORDER BY x.OrdenOrigen, x.nombre DESC;"

            Dim da As New SqlDataAdapter(sql, cn)
            da.SelectCommand.Parameters.AddWithValue("@idProyecto", idProyecto)
            da.Fill(dt)
        End Using

        Return dt
    End Function


    ' AHORA: CargarDetalleProyecto solo se apoya en la función anterior
    Public Sub CargarDetalleProyecto(idProyecto As Integer, dgv As DataGridView)
        Dim dt = ObtenerDetalleProyectoTabla(idProyecto)
        dgv.DataSource = dt
    End Sub

    ' -------- COSTO TOTAL DEL PROYECTO --------

    Public Function CalcularCostoTotal(idProyecto As Integer) As Decimal
        Dim total As Decimal = 0D
        Using cn = Conexion.ObtenerConexion()
            Dim sql As String =
                "SELECT SUM(p.costo * sm.cantidad) " &
                "FROM StockMovimiento sm " &
                "INNER JOIN Producto p ON sm.producto_id = p.id_producto " &
                "WHERE sm.proyecto_id = @idProyecto AND sm.tipo = 'salida'; " &
                "SELECT SUM(pa.costo * pa.cantidad) " &
                "FROM ProyectoAdicional pa " &
                "WHERE pa.proyecto_id = @idProyecto;"

            Dim cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@idProyecto", idProyecto)
            Dim reader = cmd.ExecuteReader()

            Dim total1 As Decimal = 0D
            Dim total2 As Decimal = 0D

            If reader.Read() AndAlso Not reader.IsDBNull(0) Then
                total1 = reader.GetDecimal(0)
            End If

            If reader.NextResult() AndAlso reader.Read() AndAlso Not reader.IsDBNull(0) Then
                total2 = reader.GetDecimal(0)
            End If

            total = total1 + total2
        End Using
        Return total
    End Function


    ' -------- AJUSTE DE STOCK EN MOVIMIENTOS DEL PROYECTO --------

    ' -------- AJUSTE DE STOCK EN MOVIMIENTOS DEL PROYECTO --------
    ' Ajusta la cantidad de un movimiento de stock y recalcula el stock
    ' de todos los movimientos de ese producto.
    Public Function ActualizarCantidadStockMovimiento(
        idMovimiento As Integer,
        idProducto As Integer,
        nuevaCantidad As Integer
    ) As Boolean
        Using cn = Conexion.ObtenerConexion()
            Dim trans = cn.BeginTransaction()
            Try
                ' 1) Actualizar la cantidad en el movimiento específico
                Dim sqlMov As String = "UPDATE StockMovimiento 
                                    SET cantidad = @nuevaCantidad
                                    WHERE id_movimiento = @idMovimiento"

                Using cmdMov As New SqlCommand(sqlMov, cn, trans)
                    cmdMov.Parameters.AddWithValue("@nuevaCantidad", nuevaCantidad)
                    cmdMov.Parameters.AddWithValue("@idMovimiento", idMovimiento)
                    cmdMov.ExecuteNonQuery()
                End Using

                ' 2) Recalcular el stock de TODOS los movimientos de ese producto
                '    usando una suma acumulativa (window function)
                Dim sqlRecalc As String =
    "WITH Movs AS (
    SELECT 
        id_movimiento,
        SUM(CASE WHEN tipo = 'entrada' THEN cantidad ELSE -cantidad END)
            OVER (PARTITION BY producto_id 
                  ORDER BY fecha, id_movimiento
                  ROWS UNBOUNDED PRECEDING) AS nuevoStock
    FROM StockMovimiento
    WHERE producto_id = @idProducto
)
UPDATE sm
SET stock = m.nuevoStock
FROM StockMovimiento sm
INNER JOIN Movs m
    ON sm.id_movimiento = m.id_movimiento;"

                Using cmdRecalc As New SqlCommand(sqlRecalc, cn, trans)
                    cmdRecalc.Parameters.AddWithValue("@idProducto", idProducto)
                    cmdRecalc.ExecuteNonQuery()
                End Using

                trans.Commit()
                Return True

            Catch ex As Exception
                trans.Rollback()
                ' 👇 Te recomiendo dejar esto mientras pruebas para ver el error real
                MessageBox.Show("Error en ActualizarCantidadStockMovimiento: " & ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Using
    End Function





End Class
