Imports System.Data
Imports System.Data.SqlClient

Public Class Producto
    Public Property id_producto As Integer
    Public Property codigo As String
    Public Property nombre As String
    Public Property categoria As String
    Public Property descripcion As String
    Public Property costo As Decimal
    Public Property fecha_registro As DateTime
    Public Property usuario_id As Integer
    Public Property talla As String
    Public Property marca As String
    Public Property modelo As String
    Public Property medida As String

    ' ==================================================
    ' GUARDAR (INSERT) 
    ' ==================================================
    Public Function Guardar() As Boolean
        Try
            Using cn = Conexion.ObtenerConexion()
                ' Validar nombre único
                If Producto.ExisteNombreProducto(nombre.Trim()) Then
                    Throw New Exception("Ya existe un producto con ese nombre. Cambie el nombre antes de continuar.")
                End If

                ' Generar código
                codigo = GenerarCodigo(cn, categoria)

                Dim query As String =
                    "INSERT INTO Producto (codigo, nombre, categoria, descripcion, costo, usuario_id) 
                     VALUES (@codigo, @nombre, @categoria, @descripcion, @costo, @usuario_id);
                     SELECT SCOPE_IDENTITY();"

                Using cmd As New SqlCommand(query, cn)
                    cmd.Parameters.AddWithValue("@codigo", codigo)
                    cmd.Parameters.AddWithValue("@nombre", nombre)
                    cmd.Parameters.AddWithValue("@categoria", categoria)
                    cmd.Parameters.AddWithValue("@descripcion", descripcion)
                    cmd.Parameters.AddWithValue("@costo", costo)
                    cmd.Parameters.AddWithValue("@usuario_id", usuario_id)

                    id_producto = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                ' Insertar en tabla específica por categoría
                Select Case categoria
                    Case "EPPs"
                        Dim q = "INSERT INTO EPPs (producto_id, talla) VALUES (@id, @talla)"
                        Using cmdEpp As New SqlCommand(q, cn)
                            cmdEpp.Parameters.AddWithValue("@id", id_producto)
                            cmdEpp.Parameters.AddWithValue("@talla", talla)
                            cmdEpp.ExecuteNonQuery()
                        End Using

                    Case "Herramientas"
                        Dim q = "INSERT INTO Herramientas (producto_id, marca, modelo) VALUES (@id, @marca, @modelo)"
                        Using cmdHerr As New SqlCommand(q, cn)
                            cmdHerr.Parameters.AddWithValue("@id", id_producto)
                            cmdHerr.Parameters.AddWithValue("@marca", marca)
                            cmdHerr.Parameters.AddWithValue("@modelo", modelo)
                            cmdHerr.ExecuteNonQuery()
                        End Using

                    Case "Materiales"
                        Dim q = "INSERT INTO Materiales (producto_id, medida) VALUES (@id, @medida)"
                        Using cmdMat As New SqlCommand(q, cn)
                            cmdMat.Parameters.AddWithValue("@id", id_producto)
                            cmdMat.Parameters.AddWithValue("@medida", medida)
                            cmdMat.ExecuteNonQuery()
                        End Using

                    Case "Chatarra"
                        Dim q = "INSERT INTO Chatarra (producto_id) VALUES (@id)"
                        Using cmdChat As New SqlCommand(q, cn)
                            cmdChat.Parameters.AddWithValue("@id", id_producto)
                            cmdChat.ExecuteNonQuery()
                        End Using
                End Select
            End Using

            Return True
        Catch ex As Exception
            Throw New Exception("Error al guardar producto: " & ex.Message)
        End Try
    End Function

    ' Generar código automático según categoría (E / H / M / C)
    Private Function GenerarCodigo(cn As SqlConnection, categoria As String) As String
        Dim prefijo As String = ""
        Select Case categoria
            Case "EPPs" : prefijo = "E"
            Case "Herramientas" : prefijo = "H"
            Case "Materiales" : prefijo = "M"
            Case "Chatarra" : prefijo = "C"
        End Select

        Dim query As String =
            "SELECT ISNULL(MAX(CAST(SUBSTRING(codigo, 2, LEN(codigo)) AS INT)), 0) 
             FROM Producto 
             WHERE categoria = @categoria"

        Using cmd As New SqlCommand(query, cn)
            cmd.Parameters.AddWithValue("@categoria", categoria)
            Dim maxVal As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return prefijo & (maxVal + 1).ToString("D3")
        End Using
    End Function

    ' ==================================================
    ' BUSCAR POR CÓDIGO
    ' ==================================================
    Public Shared Function BuscarPorCodigo(codigo As String, categoria As String) As Producto
        Dim prod As New Producto()

        Try
            Using cn = Conexion.ObtenerConexion()
                Dim query As String =
                    "SELECT TOP 1 * 
                     FROM Producto 
                     WHERE codigo = @codigo AND categoria = @categoria"

                Using cmd As New SqlCommand(query, cn)
                    cmd.Parameters.AddWithValue("@codigo", codigo)
                    cmd.Parameters.AddWithValue("@categoria", categoria)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            prod.id_producto = Convert.ToInt32(reader("id_producto"))
                            prod.codigo = reader("codigo").ToString()
                            prod.nombre = reader("nombre").ToString()
                            prod.categoria = reader("categoria").ToString()
                            prod.descripcion = reader("descripcion").ToString()
                            prod.costo = Convert.ToDecimal(reader("costo"))
                            prod.fecha_registro = Convert.ToDateTime(reader("fecha_registro"))
                            prod.usuario_id = Convert.ToInt32(reader("usuario_id"))
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using

                ' Leer datos adicionales por categoría
                Select Case categoria
                    Case "EPPs"
                        Dim q = "SELECT talla FROM EPPs WHERE producto_id = @id"
                        Using cmd As New SqlCommand(q, cn)
                            cmd.Parameters.AddWithValue("@id", prod.id_producto)
                            prod.talla = Convert.ToString(cmd.ExecuteScalar())
                        End Using

                    Case "Herramientas"
                        Dim q = "SELECT marca, modelo FROM Herramientas WHERE producto_id = @id"
                        Using cmd As New SqlCommand(q, cn)
                            cmd.Parameters.AddWithValue("@id", prod.id_producto)
                            Using reader As SqlDataReader = cmd.ExecuteReader()
                                If reader.Read() Then
                                    prod.marca = reader("marca").ToString()
                                    prod.modelo = reader("modelo").ToString()
                                End If
                            End Using
                        End Using

                    Case "Materiales"
                        Dim q = "SELECT medida FROM Materiales WHERE producto_id = @id"
                        Using cmd As New SqlCommand(q, cn)
                            cmd.Parameters.AddWithValue("@id", prod.id_producto)
                            prod.medida = Convert.ToString(cmd.ExecuteScalar())
                        End Using
                End Select
            End Using

            Return prod
        Catch ex As Exception
            Throw New Exception("Error al buscar producto: " & ex.Message)
        End Try
    End Function

    ' ==================================================
    ' BUSCAR POR NOMBRE (LIKE)
    ' ==================================================
    Public Shared Function BuscarPorNombre(nombre As String, categoria As String) As DataTable
        Dim tabla As New DataTable()

        Try
            Using cn = Conexion.ObtenerConexion()
                Dim query As String =
                    "SELECT p.categoria AS Categoria,
                            p.codigo AS Codigo,
                            p.nombre AS Nombre,
                            p.costo AS Costo,
                            p.descripcion AS Descripcion,
                            p.fecha_registro AS Fecha
                     FROM Producto p
                     WHERE p.categoria = @categoria 
                       AND p.nombre LIKE @nombre
                     ORDER BY p.fecha_registro DESC"

                Using cmd As New SqlCommand(query, cn)
                    cmd.Parameters.AddWithValue("@categoria", categoria)
                    cmd.Parameters.AddWithValue("@nombre", "%" & nombre & "%")

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        tabla.Load(reader)
                    End Using
                End Using
            End Using

            Return tabla
        Catch ex As Exception
            Throw New Exception("Error al buscar por nombre: " & ex.Message)
        End Try
    End Function

    ' ==================================================
    ' ACTUALIZAR
    ' ==================================================
    Public Function Actualizar() As Boolean
        Try
            Using cn = Conexion.ObtenerConexion()
                ' Obtener id_producto
                Dim queryId = "SELECT id_producto FROM Producto WHERE codigo = @codigo AND categoria = @categoria"
                Using cmdId As New SqlCommand(queryId, cn)
                    cmdId.Parameters.AddWithValue("@codigo", codigo)
                    cmdId.Parameters.AddWithValue("@categoria", categoria)
                    id_producto = Convert.ToInt32(cmdId.ExecuteScalar())
                End Using

                ' Validar nombre único excluyendo el actual
                If Producto.ExisteNombreProducto(nombre.Trim(), id_producto) Then
                    Throw New Exception("Ya existe un producto con ese nombre. Cambie el nombre antes de continuar.")
                End If

                ' Actualizar tabla Producto
                Dim query As String =
                    "UPDATE Producto SET 
                        nombre = @nombre,
                        descripcion = @descripcion,
                        costo = @costo
                     WHERE codigo = @codigo AND categoria = @categoria"

                Using cmd As New SqlCommand(query, cn)
                    cmd.Parameters.AddWithValue("@nombre", nombre)
                    cmd.Parameters.AddWithValue("@descripcion", descripcion)
                    cmd.Parameters.AddWithValue("@costo", costo)
                    cmd.Parameters.AddWithValue("@codigo", codigo)
                    cmd.Parameters.AddWithValue("@categoria", categoria)

                    Dim filas = cmd.ExecuteNonQuery()
                    If filas = 0 Then
                        Throw New Exception("No se encontró el producto para actualizar.")
                    End If
                End Using

                ' Actualizar tabla específica
                Select Case categoria
                    Case "EPPs"
                        Dim q = "UPDATE EPPs SET talla = @talla WHERE producto_id = @id"
                        Using cmd As New SqlCommand(q, cn)
                            cmd.Parameters.AddWithValue("@talla", talla)
                            cmd.Parameters.AddWithValue("@id", id_producto)
                            cmd.ExecuteNonQuery()
                        End Using

                    Case "Herramientas"
                        Dim q = "UPDATE Herramientas 
                                 SET marca = @marca, modelo = @modelo 
                                 WHERE producto_id = @id"
                        Using cmd As New SqlCommand(q, cn)
                            cmd.Parameters.AddWithValue("@marca", marca)
                            cmd.Parameters.AddWithValue("@modelo", modelo)
                            cmd.Parameters.AddWithValue("@id", id_producto)
                            cmd.ExecuteNonQuery()
                        End Using

                    Case "Materiales"
                        Dim q = "UPDATE Materiales SET medida = @medida WHERE producto_id = @id"
                        Using cmd As New SqlCommand(q, cn)
                            cmd.Parameters.AddWithValue("@medida", medida)
                            cmd.Parameters.AddWithValue("@id", id_producto)
                            cmd.ExecuteNonQuery()
                        End Using
                End Select
            End Using

            Return True
        Catch ex As Exception
            Throw New Exception("Error al actualizar producto: " & ex.Message)
        End Try
    End Function

    ' ==================================================
    ' ELIMINAR
    ' ==================================================
    Public Function Eliminar() As Boolean
        Try
            Using cn = Conexion.ObtenerConexion()
                Dim query = "SELECT id_producto FROM Producto WHERE codigo = @codigo AND categoria = @categoria"
                Using cmd As New SqlCommand(query, cn)
                    cmd.Parameters.AddWithValue("@codigo", codigo)
                    cmd.Parameters.AddWithValue("@categoria", categoria)

                    Dim result = cmd.ExecuteScalar()
                    If result Is Nothing Then
                        Throw New Exception("Producto no encontrado.")
                    End If

                    id_producto = Convert.ToInt32(result)
                End Using

                Dim deleteQuery = "DELETE FROM Producto WHERE id_producto = @id"
                Using cmdDel As New SqlCommand(deleteQuery, cn)
                    cmdDel.Parameters.AddWithValue("@id", id_producto)
                    cmdDel.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            Throw New Exception("Error al eliminar producto: " & ex.Message)
        End Try
    End Function

    ' ==================================================
    ' LISTADOS
    ' ==================================================
    Public Shared Function ObtenerTodos() As DataTable
        Dim tabla As New DataTable()

        Try
            Using cn = Conexion.ObtenerConexion()
                Dim query As String =
                    "SELECT 
                        categoria AS Categoria,
                        codigo AS Codigo,
                        nombre AS Nombre,
                        costo AS Costo,
                        descripcion AS Descripcion,
                        fecha_registro AS Fecha
                     FROM Producto
                     ORDER BY fecha_registro DESC"

                Using cmd As New SqlCommand(query, cn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        tabla.Load(reader)
                    End Using
                End Using
            End Using

            Return tabla
        Catch ex As Exception
            Throw New Exception("Error al obtener productos: " & ex.Message)
        End Try
    End Function

    Public Shared Function ObtenerPorCategoria(categoria As String) As DataTable
        Dim tabla As New DataTable()

        Try
            Using cn = Conexion.ObtenerConexion()
                Dim query As String = ""

                Select Case categoria
                    Case "EPPs"
                        query =
                            "SELECT 
                                p.codigo AS Codigo,
                                p.nombre AS Nombre,
                                p.costo AS Costo,
                                e.talla AS Talla,
                                p.descripcion AS Descripcion,
                                p.fecha_registro AS Fecha
                             FROM Producto p
                             INNER JOIN EPPs e ON p.id_producto = e.producto_id
                             WHERE p.categoria = 'EPPs'
                             ORDER BY p.fecha_registro DESC"

                    Case "Herramientas"
                        query =
                            "SELECT 
                                p.codigo AS Codigo,
                                p.nombre AS Nombre,
                                p.costo AS Costo,
                                h.marca AS Marca,
                                h.modelo AS Modelo,
                                p.descripcion AS Descripcion,
                                p.fecha_registro AS Fecha
                             FROM Producto p
                             INNER JOIN Herramientas h ON p.id_producto = h.producto_id
                             WHERE p.categoria = 'Herramientas'
                             ORDER BY p.fecha_registro DESC"

                    Case "Materiales"
                        query =
                            "SELECT 
                                p.codigo AS Codigo,
                                p.nombre AS Nombre,
                                p.costo AS Costo,
                                m.medida AS Medida,
                                p.descripcion AS Descripcion,
                                p.fecha_registro AS Fecha
                             FROM Producto p
                             INNER JOIN Materiales m ON p.id_producto = m.producto_id
                             WHERE p.categoria = 'Materiales'
                             ORDER BY p.fecha_registro DESC"

                    Case "Chatarra"
                        query =
                            "SELECT 
                                p.codigo AS Codigo,
                                p.nombre AS Nombre,
                                p.descripcion AS Descripcion,
                                p.fecha_registro AS Fecha
                             FROM Producto p
                             INNER JOIN Chatarra c ON p.id_producto = c.producto_id
                             WHERE p.categoria = 'Chatarra'
                             ORDER BY p.fecha_registro DESC"

                    Case Else
                        Throw New Exception("Categoría no válida.")
                End Select

                Using cmd As New SqlCommand(query, cn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        tabla.Load(reader)
                    End Using
                End Using
            End Using

            Return tabla
        Catch ex As Exception
            Throw New Exception("Error al obtener productos por categoría: " & ex.Message)
        End Try
    End Function

    ' ==================================================
    ' VALIDACIÓN DE NOMBRES
    ' ==================================================
    Public Shared Function ExisteNombreProducto(nombre As String, Optional idProductoExcluir As Integer = 0) As Boolean
        Dim query As String =
            "SELECT COUNT(*) 
             FROM Producto 
             WHERE nombre = @nombre AND id_producto <> @id"

        Using cn = Conexion.ObtenerConexion()
            Using cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@nombre", nombre.Trim())
                cmd.Parameters.AddWithValue("@id", idProductoExcluir)
                Dim count As Integer = CInt(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

End Class
