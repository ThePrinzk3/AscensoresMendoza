Imports System.Data
Imports ProyectoGestión

Friend Module Adaptador

    Private Const UsuarioId As Integer = 1

    ' 1. Registrar Salida de Stock
    Friend Function Salida(codigo As String, cantidad As Integer, personal As String) As Boolean
        Try
            Return stockController.RegistrarSalida(codigo, cantidad, UsuarioId, personal)
        Catch
            Return False
        End Try
    End Function

    ' 2. Registrar Entrada de Stock
    Friend Function Entrada(codigo As String, cantidad As Integer, personal As String) As Boolean
        Try
            Return stockController.RegistrarEntrada(codigo, cantidad, UsuarioId)
        Catch
            Return False
        End Try
    End Function

    ' 3. Alertas de Stock
    Friend Function AlertaActiva(nombre As String) As Boolean
        Try
            Dim dt As DataTable = stockController.ListarStockEnAlerta("Todos")
            If dt IsNot Nothing Then
                For Each row As DataRow In dt.Rows
                    Dim nomCol As String = If(dt.Columns.Contains("nombre"), row("nombre").ToString(), "")
                    Dim codCol As String = If(dt.Columns.Contains("codigo"), row("codigo").ToString(), "")
                    If nomCol.Trim().Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase) OrElse
                       codCol.Trim().Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase) Then
                        Return True
                    End If
                Next
            End If
            Return False
        Catch
            Return False
        End Try
    End Function

    ' 4. Valor de Inventario por Categoría
    Friend Function ValorInventario(categoria As String) As Decimal
        Try
            Dim dt As DataTable = reporteController.GenerarReportePorCategoria(categoria)
            Dim total As Decimal = 0D
            If dt IsNot Nothing Then
                For Each row As DataRow In dt.Rows
                    If dt.Columns.Contains("valor_total") AndAlso Not IsDBNull(row("valor_total")) Then
                        total += Convert.ToDecimal(row("valor_total"))
                    ElseIf dt.Columns.Contains("costo") AndAlso dt.Columns.Contains("cantidad") Then
                        total += Convert.ToDecimal(row("costo")) * Convert.ToDecimal(row("cantidad"))
                    ElseIf dt.Columns.Contains("costo") AndAlso dt.Columns.Contains("stock") Then
                        total += Convert.ToDecimal(row("costo")) * Convert.ToDecimal(row("stock"))
                    End If
                Next
            End If
            Return total
        Catch
            Return 0D
        End Try
    End Function

    ' 5. Login
    Friend Function Login(usuario As String, clave As String) As Boolean
        Try
            Dim msgError As String = ""
            Return LoginController.IntentarLogin(usuario, clave, msgError)
        Catch
            Return False
        End Try
    End Function

    ' 6. Registrar Producto
    ' 6. Registrar Producto
    Friend Function RegistrarProducto(categoria As String, codigo As String, nombre As String, descripcion As String, costo As Decimal, talla As String, marca As String, modelo As String, medida As String) As Boolean
        Try
            ' 1. Asegurar sesión activa para el controlador
            Dim msg As String = ""
            LoginController.IntentarLogin(USUARIO_OK, CLAVE_OK, msg)

            ' 2. Instanciar y llenar producto
            Dim p As New Producto()
            p.categoria = categoria
            p.codigo = codigo
            p.nombre = nombre
            p.descripcion = descripcion
            p.costo = costo
            p.usuario_id = 1

            ' 3. Asignar atributos específicos por reflexión o propiedad directa
            Try : CallByName(p, "talla", CallType.Let, talla) : Catch : End Try
            Try : CallByName(p, "marca", CallType.Let, marca) : Catch : End Try
            Try : CallByName(p, "modelo", CallType.Let, modelo) : Catch : End Try
            Try : CallByName(p, "medida", CallType.Let, medida) : Catch : End Try

            ' 4. Invocar al controlador
            Return ProductoController.RegistrarProducto(p)
        Catch
            Return False
        End Try
    End Function

    ' 7. Activar Proyecto
    Friend Function ActivarProyecto(codigo As String, nombre As String) As Boolean
        Try
            Dim pc As New ProyectoController()
            pc.RegistrarProyecto(nombre)

            Dim dt As DataTable = Proyecto.Listar()
            If dt IsNot Nothing Then
                For Each row As DataRow In dt.Rows
                    Dim c As String = row("codigo").ToString()
                    Dim n As String = row("nombre").ToString()
                    If c = codigo OrElse n = nombre Then
                        Dim idProy As Integer = Convert.ToInt32(row("id_proyecto"))
                        Return pc.EncenderProyecto(idProy)
                    End If
                Next
            End If
            Return False
        Catch
            Return False
        End Try
    End Function

    ' 8. Pausar Proyecto
    Friend Sub PausarProyecto(codigo As String)
        Try
            Dim pc As New ProyectoController()
            pc.ApagarTodos()
        Catch
        End Try
    End Sub

    ' 9. Asignar Salida a Proyecto Activo
    Friend Function AsignarSalidaAProyecto(codigoItem As String, cantidad As Integer, personal As String) As Boolean
        Try
            Dim pc As New ProyectoController()
            Dim pActivo As Proyecto = pc.ObtenerProyectoActivo()
            If pActivo Is Nothing OrElse pActivo.estado <> "ENCENDIDO" Then
                Return False
            End If
            Return stockController.RegistrarSalida(codigoItem, cantidad, UsuarioId, personal)
        Catch
            Return False
        End Try
    End Function

    ' 10. Agregar Adicional a Proyecto
    Friend Function AgregarAdicional(codigo As String, nombre As String, costo As Decimal, descripcion As String, cantidad As Integer) As Boolean
        Try
            Dim pc As New ProyectoController()
            Dim pActivo As Proyecto = pc.ObtenerProyectoActivo()
            If pActivo Is Nothing OrElse pActivo.estado <> "ENCENDIDO" Then
                Return False
            End If
            Return pc.RegistrarAdicional(pActivo, nombre, descripcion, costo, cantidad)
        Catch
            Return False
        End Try
    End Function

    ' 11. Costo Total de Proyecto
    Friend Function CostoTotal(codigoProyecto As String) As Decimal
        Try
            Dim pc As New ProyectoController()
            Dim dt As DataTable = Proyecto.Listar()
            If dt IsNot Nothing Then
                For Each row As DataRow In dt.Rows
                    If row("codigo").ToString() = codigoProyecto OrElse row("nombre").ToString() = codigoProyecto Then
                        Dim idProy As Integer = Convert.ToInt32(row("id_proyecto"))
                        Return pc.CalcularCostoTotal(idProy)
                    End If
                Next
            End If
            Return 0D
        Catch
            Return 0D
        End Try
    End Function

End Module