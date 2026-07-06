Imports System.Data

Public Class stockController

    Public Shared Function RegistrarEntrada(codigo As String,
                                            cantidad As Integer,
                                            usuario_id As Integer) As Boolean
        Dim s As New stock With {
            .codigo = codigo,
            .cantidad = cantidad,
            .tipo = "entrada",
            .usuario_id = usuario_id
        }
        Return s.RegistrarMovimiento()
    End Function

    Public Shared Function RegistrarSalida(codigo As String,
                                           cantidad As Integer,
                                           usuario_id As Integer,
                                           trabajador As String) As Boolean
        Dim s As New stock With {
            .codigo = codigo,
            .cantidad = cantidad,
            .tipo = "salida",
            .usuario_id = usuario_id,
            .personal_trabajador = trabajador
        }
        Return s.RegistrarMovimiento()
    End Function

    Public Shared Function ListarStockGeneral(Optional categoria As String = "Todos",
                                              Optional nombreFiltro As String = "") As DataTable
        Return stock.ObtenerStockGeneral(categoria, nombreFiltro)
    End Function

    Public Shared Function ListarStockEnAlerta(Optional categoria As String = "Todos") As DataTable
        Return stock.ObtenerStockEnAlerta(categoria)
    End Function

    Public Shared Function BuscarHistorialPorCodigo(codigo As String) As DataTable
        Return stock.ObtenerHistorial(codigo)
    End Function

    Public Shared Function ListarHistorialGeneral(Optional categoria As String = "Todos",
                                                  Optional nombreFiltro As String = "",
                                                  Optional codigoFiltro As String = "",
                                                  Optional trabajadorFiltro As String = "") As DataTable
        Return stock.ObtenerHistorialGeneral(categoria, nombreFiltro, codigoFiltro, trabajadorFiltro)
    End Function

    Public Shared Function BuscarStockPorCodigo(codigo As String) As DataTable
        Return stock.ObtenerStockPorCodigo(codigo)
    End Function

End Class
