Public Class reporteController
    Public Shared Function MostrarTodos() As DataTable
        Return reporte.ObtenerTodosLosProductos()
    End Function

    Public Shared Function MostrarPorCategoria(categoria As String) As DataTable
        Return reporte.ObtenerPorCategoria(categoria)
    End Function

    Public Shared Function GenerarReporteCompletoAvanzado() As DataTable
        Return reporte.GenerarReporteCompletoAvanzado()
    End Function

    Public Shared Function GenerarReportePorCategoria(categoria As String) As DataTable
        Return reporte.GenerarReportePorCategoria(categoria)
    End Function
End Class
