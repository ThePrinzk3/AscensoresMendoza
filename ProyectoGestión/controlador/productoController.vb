Imports System.Data

Public Class ProductoController

    Public Shared Function RegistrarProducto(p As Producto) As Boolean
        Return p.Guardar()
    End Function

    Public Shared Function BuscarProducto(codigo As String, categoria As String) As Producto
        Return Producto.BuscarPorCodigo(codigo, categoria)
    End Function

    Public Shared Function BuscarProductoPorNombre(nombre As String, categoria As String) As DataTable
        Return Producto.BuscarPorNombre(nombre, categoria)
    End Function

    Public Shared Function ActualizarProducto(p As Producto) As Boolean
        Return p.Actualizar()
    End Function

    Public Shared Function EliminarProducto(p As Producto) As Boolean
        Return p.Eliminar()
    End Function

    Public Shared Function ListarPorCategoria(categoria As String) As DataTable
        Return Producto.ObtenerPorCategoria(categoria)
    End Function

    Public Shared Function ListarTodos() As DataTable
        Return Producto.ObtenerTodos()
    End Function

End Class
