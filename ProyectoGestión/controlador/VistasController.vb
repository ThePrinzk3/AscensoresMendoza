Imports System.Data.SqlClient
Imports Newtonsoft.Json

Public Class VistasController

    Private Shared Function EjecutarConsulta(sql As String) As DataTable
        Dim dt As New DataTable()
        Using conn = Conexion.ObtenerConexion()
            Using da As New SqlDataAdapter(sql, conn)
                da.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    Private Shared Function EjecutarProcedimiento(nombreProc As String, Optional params As Dictionary(Of String, Object) = Nothing) As DataTable
        Dim dt As New DataTable()
        Using conn = Conexion.ObtenerConexion()
            Using cmd As New SqlCommand(nombreProc, conn)
                cmd.CommandType = CommandType.StoredProcedure
                If params IsNot Nothing Then
                    For Each kv In params
                        cmd.Parameters.AddWithValue(kv.Key, kv.Value)
                    Next
                End If
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function ObtenerResumenGeneral() As ResumenGeneralModel
        Dim sql = "SELECT * FROM vw_ResumenGeneral"
        Dim dt = EjecutarConsulta(sql)
        If dt.Rows.Count = 0 Then Return Nothing
        Dim row = dt.Rows(0)
        Return New ResumenGeneralModel With {
            .RotacionAnual = Convert.ToDecimal(row("RotacionAnual")),
            .DiasCobertura = Convert.ToDecimal(row("DiasCobertura")),
            .NivelServicio = Convert.ToDecimal(row("NivelServicio")),
            .PrecisionStock = Convert.ToDecimal(row("PrecisionStock")),
            .StockPorCategoria = row("StockPorCategoria").ToString(),
            .ValorInventarioMensual = row("ValorInventarioMensual").ToString()
        }
    End Function

    Public Shared Function ObtenerStockCritico() As DataTable
        Return EjecutarConsulta("SELECT * FROM vw_StockCritico")
    End Function

    Public Shared Function ObtenerConsumoProyectos() As DataTable
        Return EjecutarConsulta("SELECT * FROM vw_ConsumoVsPresupuesto")
    End Function

    Public Shared Function ObtenerPronostico(productoId As Integer) As DataTable
        Dim sql = $"SELECT * FROM vw_PronosticoDemanda WHERE id_producto = {productoId} ORDER BY anio, mes"
        Return EjecutarConsulta(sql)
    End Function

    Public Shared Function ObtenerAlertas() As DataTable
        Return EjecutarProcedimiento("sp_ObtenerAlertas")
    End Function

    Public Shared Function ObtenerListaProductos() As DataTable
        Return EjecutarConsulta("SELECT id_producto, nombre FROM Producto ORDER BY nombre")
    End Function

End Class