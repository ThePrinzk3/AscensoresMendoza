' FrmProyecto.vb  (capa Vista)

Imports System.Data
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO


Public Class FrmProyecto

    Private proyectoController As New ProyectoController()
    Private idProyectoSeleccionado As Integer = 0

    '========================
    ' EVENTO LOAD DEL FORM
    '========================
    Private Sub FrmProyecto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarGrids()
        CargarProyectos()
        LimpiarProyecto()
        LimpiarAdicional()
        ActualizarProyectoActivoUI()

        dtvProyecto.RowHeadersVisible = False
        dtvAdicional.RowHeadersVisible = False

    End Sub

    '========================
    ' MÉTODOS DE APOYO
    '========================

    Private Sub ConfigurarGrids()
        ' Opcional: configuración básica
        dtvProyecto.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dtvProyecto.MultiSelect = False
        dtvProyecto.ReadOnly = True
        dtvProyecto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        dtvAdicional.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dtvAdicional.MultiSelect = False
        dtvAdicional.ReadOnly = True
        dtvAdicional.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub CargarProyectos()
        dtvProyecto.DataSource = Proyecto.Listar()

        ' Ocultar columna de ID y quitar fila / columna extra
        If dtvProyecto.Columns.Contains("id_proyecto") Then
            dtvProyecto.Columns("id_proyecto").Visible = False
        End If

        dtvProyecto.RowHeadersVisible = False
        dtvProyecto.AllowUserToAddRows = False
        dtvProyecto.ClearSelection()

        idProyectoSeleccionado = 0
    End Sub

    Private Sub CargarDetalleProyectoSeleccionado()
        If idProyectoSeleccionado <= 0 Then
            dtvAdicional.DataSource = Nothing
            txtValorProyecto.Clear()
            Exit Sub
        End If

        proyectoController.CargarDetalleProyecto(idProyectoSeleccionado, dtvAdicional)

        If dtvAdicional.Columns.Contains("OrdenOrigen") Then
            dtvAdicional.Columns("OrdenOrigen").Visible = False
        End If

        If dtvAdicional.Columns.Contains("id_adicional") Then
            dtvAdicional.Columns("id_adicional").Visible = False
        End If

        If dtvAdicional.Columns.Contains("id_stock_mov") Then
            dtvAdicional.Columns("id_stock_mov").Visible = False
        End If

        If dtvAdicional.Columns.Contains("id_producto") Then
            dtvAdicional.Columns("id_producto").Visible = False
        End If

        txtValorProyecto.Text = proyectoController.CalcularCostoTotal(idProyectoSeleccionado).ToString("0.00")
    End Sub


    Private Sub LimpiarProyecto()
        txtNombreProyecto.Clear()
        txtCodeProyecto.Clear()
        idProyectoSeleccionado = 0
        dtvProyecto.ClearSelection()
    End Sub

    Private Sub LimpiarAdicional()
        txtCodigoAdicional.Clear()
        txtNombreAdicional.Clear()
        txtDescripcionAdicional.Clear()
        txtCostoAdicional.Clear()
        txtCantidadAdicional.Clear()
    End Sub

    Private Sub ActualizarProyectoActivoUI()
        Dim activo = proyectoController.ObtenerProyectoActivo()
        If activo IsNot Nothing Then
            Label11.Text = $"Proyecto activo: {activo.codigo} - {activo.nombre}"
        Else
            Label11.Text = "Proyecto activo: ninguno"
        End If
    End Sub

    '========================
    ' EVENTOS DE PROYECTO
    '========================

    ' Seleccionar fila en dtvProyecto
    ' Seleccionar fila en dtvProyecto
    Private Sub dtvProyecto_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtvProyecto.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim fila As DataGridViewRow = dtvProyecto.Rows(e.RowIndex)

        ' Sacar el valor de id_proyecto desde el DataGridView
        Dim valorId As Object = Nothing

        ' 1) Intenta por nombre de columna
        If dtvProyecto.Columns.Contains("id_proyecto") Then
            valorId = fila.Cells("id_proyecto").Value
        Else
            ' 2) Si no existe esa columna, usa la primera columna
            valorId = fila.Cells(0).Value
        End If

        ' Si está vacío o es DBNull, no sigas
        If valorId Is Nothing OrElse valorId Is DBNull.Value OrElse valorId.ToString() = "" Then
            idProyectoSeleccionado = 0
            txtCodeProyecto.Clear()
            txtNombreProyecto.Clear()
            dtvAdicional.DataSource = Nothing
            txtValorProyecto.Clear()
            Exit Sub
        End If

        ' Ahora sí, convertir con seguridad
        idProyectoSeleccionado = Convert.ToInt32(valorId)

        ' Cargar textos
        ' Ajusta los nombres de columnas si fuera necesario
        txtCodeProyecto.Text = fila.Cells("codigo").Value.ToString()
        txtNombreProyecto.Text = fila.Cells("nombre").Value.ToString()

        ' Cargar detalle y costo del proyecto seleccionado
        CargarDetalleProyectoSeleccionado()
        ActualizarProyectoActivoUI()

    End Sub


    ' Registrar nuevo proyecto
    Private Sub btnRegistrarProyecto_Click(sender As Object, e As EventArgs) Handles btnRegistrarProyecto.Click
        If String.IsNullOrWhiteSpace(txtNombreProyecto.Text) Then
            MessageBox.Show("Ingrese un nombre de proyecto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim nombre As String = txtNombreProyecto.Text.Trim()

        ' 🔎 Validar que NO exista otro proyecto con el mismo nombre
        Try
            Using cn = Conexion.ObtenerConexion()
                Dim sqlVal As String = "SELECT COUNT(*) FROM Proyecto WHERE nombre = @nombre"
                Dim cmdVal As New SqlCommand(sqlVal, cn)
                cmdVal.Parameters.AddWithValue("@nombre", nombre)
                Dim existe As Integer = CInt(cmdVal.ExecuteScalar())

                If existe > 0 Then
                    MessageBox.Show("Ya existe un proyecto con ese nombre.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al validar nombre de proyecto: " & ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        If proyectoController.RegistrarProyecto(nombre) Then
            MessageBox.Show("Proyecto registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarProyectos()
            LimpiarProyecto()
            ActualizarProyectoActivoUI()
        Else
            MessageBox.Show("No se pudo registrar el proyecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    ' Actualizar proyecto seleccionado (solo nombre)
    Private Sub btnActualizarProyecto_Click(sender As Object, e As EventArgs) Handles btnActualizarProyecto.Click
        If idProyectoSeleccionado <= 0 Then
            MessageBox.Show("Seleccione un proyecto para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtNombreProyecto.Text) Then
            MessageBox.Show("Ingrese un nombre de proyecto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try
            Using cn = Conexion.ObtenerConexion()
                Dim sql As String = "UPDATE Proyecto SET nombre = @nombre WHERE id_proyecto = @id"
                Dim cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@nombre", txtNombreProyecto.Text.Trim())
                cmd.Parameters.AddWithValue("@id", idProyectoSeleccionado)
                Dim filas = cmd.ExecuteNonQuery()
                If filas > 0 Then
                    MessageBox.Show("Proyecto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CargarProyectos()
                Else
                    MessageBox.Show("No se pudo actualizar el proyecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al actualizar proyecto: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Eliminar proyecto seleccionado
    Private Sub btnEliminarProyecto_Click(sender As Object, e As EventArgs) Handles btnEliminarProyecto.Click
        If idProyectoSeleccionado <= 0 Then
            MessageBox.Show("Seleccione un proyecto para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If MessageBox.Show("¿Seguro que desea eliminar este proyecto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        Try
            Using cn = Conexion.ObtenerConexion()
                Dim sql As String = "DELETE FROM Proyecto WHERE id_proyecto = @id"
                Dim cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@id", idProyectoSeleccionado)
                Dim filas = cmd.ExecuteNonQuery()
                If filas > 0 Then
                    MessageBox.Show("Proyecto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CargarProyectos()
                    LimpiarProyecto()
                    dtvAdicional.DataSource = Nothing
                    txtValorProyecto.Clear()
                    ActualizarProyectoActivoUI()
                Else
                    MessageBox.Show("No se pudo eliminar el proyecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al eliminar proyecto: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Limpiar campos del proyecto
    Private Sub btnLimpiarProyecto_Click(sender As Object, e As EventArgs) Handles btnLimpiarProyecto.Click
        LimpiarProyecto()
    End Sub

    ' Buscar proyecto por código o nombre en el DataGridView
    Private Sub btnBuscarProyecto_Click(sender As Object, e As EventArgs) Handles btnBuscarProyecto.Click
        Dim criterio As String = txtCodeProyecto.Text.Trim()

        If String.IsNullOrWhiteSpace(criterio) Then
            criterio = txtNombreProyecto.Text.Trim()
        End If

        If String.IsNullOrWhiteSpace(criterio) Then
            MessageBox.Show("Ingrese código o nombre para buscar.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim encontrado As Boolean = False

        For Each fila As DataGridViewRow In dtvProyecto.Rows
            If fila.IsNewRow Then Continue For

            Dim cod As String = fila.Cells("codigo").Value.ToString()
            Dim nom As String = fila.Cells("nombre").Value.ToString()

            ' Búsqueda parcial
            If cod.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0 OrElse
           nom.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0 Then

                fila.Selected = True

                ' 👉 Buscar la primera columna visible
                Dim firstVisibleCol As Integer = -1
                For Each col As DataGridViewColumn In dtvProyecto.Columns
                    If col.Visible Then
                        firstVisibleCol = col.Index
                        Exit For
                    End If
                Next

                If firstVisibleCol >= 0 Then
                    dtvProyecto.CurrentCell = fila.Cells(firstVisibleCol)
                End If

                dtvProyecto.FirstDisplayedScrollingRowIndex = fila.Index

                ' Disparar el CellClick usando la primera columna visible
                Dim args As New DataGridViewCellEventArgs(
                If(firstVisibleCol >= 0, firstVisibleCol, 0),
                fila.Index
            )
                dtvProyecto_CellClick(dtvProyecto, args)

                encontrado = True
                Exit For
            End If
        Next

        If Not encontrado Then
            MessageBox.Show("No se encontró un proyecto con ese criterio.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub



    ' Pausar (apagar todos los proyectos)
    Private Sub btnPausa_Click(sender As Object, e As EventArgs) Handles btnPausa.Click
        proyectoController.ApagarTodos()
        MessageBox.Show("Todos los proyectos han sido apagados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        CargarProyectos()
        dtvAdicional.DataSource = Nothing
        txtValorProyecto.Clear()
        ActualizarProyectoActivoUI()
    End Sub

    ' Reanudar (encender solo el proyecto seleccionado)
    Private Sub btnReanudar_Click(sender As Object, e As EventArgs) Handles btnReanudar.Click
        If idProyectoSeleccionado <= 0 Then
            MessageBox.Show("Seleccione un proyecto para encender.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If proyectoController.EncenderProyecto(idProyectoSeleccionado) Then
            MessageBox.Show("Proyecto encendido. Solo este proyecto estará activo.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarProyectos()

            ' Volver a seleccionar el proyecto encendido
            For Each fila As DataGridViewRow In dtvProyecto.Rows
                If fila.IsNewRow Then Continue For
                Dim idFila As Integer = CInt(fila.Cells("id_proyecto").Value)
                If idFila = idProyectoSeleccionado Then
                    fila.Selected = True
                    dtvProyecto.CurrentCell = fila.Cells(0)
                    Dim args As New DataGridViewCellEventArgs(0, fila.Index)
                    dtvProyecto_CellClick(dtvProyecto, args)
                    Exit For
                End If
            Next

            ActualizarProyectoActivoUI()
        Else
            MessageBox.Show("No se pudo encender el proyecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    '========================
    ' EVENTOS DE ADICIONALES
    '========================

    Private Sub dtvAdicional_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtvAdicional.CellClick
        If e.RowIndex < 0 OrElse dtvAdicional.CurrentRow Is Nothing Then Exit Sub

        Dim fila = dtvAdicional.Rows(e.RowIndex)

        Try
            txtCodigoAdicional.Text = fila.Cells("codigo").Value.ToString()
            txtNombreAdicional.Text = fila.Cells("nombre").Value.ToString()
            txtDescripcionAdicional.Text = fila.Cells("descripcion").Value.ToString()
            txtCostoAdicional.Text = fila.Cells("costo").Value.ToString()
            txtCantidadAdicional.Text = fila.Cells("cantidad").Value.ToString()
        Catch
            ' Si alguna columna no existe, evitar error
        End Try
    End Sub


    ' Registrar un producto adicional para el proyecto activo
    Private Sub btnRegistrarAdicional_Click(sender As Object, e As EventArgs) Handles btnRegistrarAdicional.Click
        Dim proyectoActivo = proyectoController.ObtenerProyectoActivo()
        If proyectoActivo Is Nothing Then
            MessageBox.Show("No hay proyecto encendido. Encienda uno para agregar adicionales.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtNombreAdicional.Text) OrElse
       String.IsNullOrWhiteSpace(txtCostoAdicional.Text) OrElse
       String.IsNullOrWhiteSpace(txtCantidadAdicional.Text) Then

            MessageBox.Show("Complete nombre, costo y cantidad del adicional.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim costo As Decimal
        If Not Decimal.TryParse(txtCostoAdicional.Text, costo) Then
            MessageBox.Show("Costo no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cantidad As Integer
        If Not Integer.TryParse(txtCantidadAdicional.Text, cantidad) OrElse cantidad <= 0 Then
            MessageBox.Show("Cantidad no válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim ok = proyectoController.RegistrarAdicional(
        proyectoActivo,
        txtNombreAdicional.Text.Trim(),
        txtDescripcionAdicional.Text.Trim(),
        costo,
        cantidad
    )

        If ok Then
            MessageBox.Show("Producto adicional registrado en el proyecto activo.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)

            CargarDetalleProyectoSeleccionado()
            LimpiarAdicional()
            txtCantidadAdicional.Clear()
        Else
            MessageBox.Show("No se pudo registrar el producto adicional.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    ' Limpiar campos de adicional
    Private Sub btnLimpiarAdicional_Click(sender As Object, e As EventArgs) Handles btnLimpiarAdicional.Click
        LimpiarAdicional()
    End Sub

    ' Buscar adicional por código o nombre dentro del DataGridView
    Private Sub btnBuscarAdicional_Click(sender As Object, e As EventArgs) Handles btnBuscarAdicional.Click
        Dim criterio As String = txtCodigoAdicional.Text.Trim()

        If String.IsNullOrWhiteSpace(criterio) Then
            criterio = txtNombreAdicional.Text.Trim()
        End If

        If String.IsNullOrWhiteSpace(criterio) Then
            MessageBox.Show("Ingrese código o nombre para buscar un adicional.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If dtvAdicional.DataSource Is Nothing Then
            MessageBox.Show("No hay datos cargados en el detalle del proyecto.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim encontrado As Boolean = False

        For Each fila As DataGridViewRow In dtvAdicional.Rows
            If fila.IsNewRow Then Continue For

            Dim cod As String = ""
            Dim nom As String = ""

            If fila.Cells("codigo") IsNot Nothing AndAlso fila.Cells("codigo").Value IsNot Nothing Then
                cod = fila.Cells("codigo").Value.ToString()
            End If

            If fila.Cells("nombre") IsNot Nothing AndAlso fila.Cells("nombre").Value IsNot Nothing Then
                nom = fila.Cells("nombre").Value.ToString()
            End If

            ' Búsqueda parcial
            If cod.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0 OrElse
           nom.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0 Then

                fila.Selected = True

                ' 👉 Primera columna visible del dtvAdicional
                Dim firstVisibleCol As Integer = -1
                For Each col As DataGridViewColumn In dtvAdicional.Columns
                    If col.Visible Then
                        firstVisibleCol = col.Index
                        Exit For
                    End If
                Next

                If firstVisibleCol >= 0 Then
                    dtvAdicional.CurrentCell = fila.Cells(firstVisibleCol)
                End If

                dtvAdicional.FirstDisplayedScrollingRowIndex = fila.Index

                ' Llamar al CellClick con la columna visible
                Dim args As New DataGridViewCellEventArgs(
                If(firstVisibleCol >= 0, firstVisibleCol, 0),
                fila.Index
            )
                dtvAdicional_CellClick(dtvAdicional, args)

                encontrado = True
                Exit For
            End If
        Next

        If Not encontrado Then
            MessageBox.Show("No se encontró un producto en el detalle con ese criterio.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub



    ' Eliminar adicional (solo si es un registro ADICIONAL)
    Private Sub btnEliminarAdicional_Click(sender As Object, e As EventArgs) Handles btnEliminarAdicional.Click
        If dtvAdicional.CurrentRow Is Nothing OrElse dtvAdicional.CurrentRow.IsNewRow Then
            MessageBox.Show("Seleccione un registro adicional en la tabla de productos del proyecto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim fila = dtvAdicional.CurrentRow

        ' Verificar si es adicional (origen = ADICIONAL) – esto depende de que tu consulta lo incluya
        Dim origen As String = ""
        Try
            origen = fila.Cells("origen").Value.ToString()
        Catch
        End Try

        If Not origen.Equals("ADICIONAL", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("Solo puede eliminar registros de tipo ADICIONAL desde aquí.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Necesitamos el id_adicional en la consulta de CargarDetalleProyecto (ajústala para incluirlo)
        Dim idAdicional As Integer
        Try
            idAdicional = CInt(fila.Cells("id_adicional").Value)
        Catch
            MessageBox.Show("No se encontró el identificador del adicional. Ajuste la consulta para incluir id_adicional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        If MessageBox.Show("¿Seguro que desea eliminar este producto adicional?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        Try
            Using cn = Conexion.ObtenerConexion()
                Dim sql As String = "DELETE FROM ProyectoAdicional WHERE id_adicional = @id"
                Dim cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@id", idAdicional)
                Dim filas = cmd.ExecuteNonQuery()
                If filas > 0 Then
                    MessageBox.Show("Adicional eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CargarDetalleProyectoSeleccionado()
                Else
                    MessageBox.Show("No se pudo eliminar el adicional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al eliminar adicional: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Actualizar adicional (requiere que agregues id_adicional y un método Update en el modelo si quieres algo más elaborado)
    Private Sub btnActualizarAdicional_Click(sender As Object, e As EventArgs) Handles btnActualizarAdicional.Click
        If dtvAdicional.CurrentRow Is Nothing Then
            MessageBox.Show("Seleccione un adicional para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim fila = dtvAdicional.CurrentRow

        ' Intentar obtener id_adicional
        Dim idObj As Object = Nothing
        If dtvAdicional.Columns.Contains("id_adicional") Then
            idObj = fila.Cells("id_adicional").Value
        End If

        If idObj Is Nothing OrElse IsDBNull(idObj) OrElse idObj.ToString() = "" Then
            MessageBox.Show("El registro seleccionado no es un adicional (es del stock del proyecto).", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim idAdicional As Integer = Convert.ToInt32(idObj)

        ' Validar campos
        Dim costo As Decimal
        If Not Decimal.TryParse(txtCostoAdicional.Text, costo) Then
            MessageBox.Show("Costo inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cantidad As Integer
        If Not Integer.TryParse(txtCantidadAdicional.Text, cantidad) OrElse cantidad <= 0 Then
            MessageBox.Show("Cantidad inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim adicional As New ProyectoAdicional With {
            .id_adicional = idAdicional,
            .nombre = txtNombreAdicional.Text.Trim(),
            .descripcion = txtDescripcionAdicional.Text.Trim(),
            .costo = costo,
            .cantidad = cantidad
        }

        If adicional.Actualizar() Then
            MessageBox.Show("Adicional actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarDetalleProyectoSeleccionado()
        Else
            MessageBox.Show("No se pudo actualizar el adicional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    ' ----------------- EXPORTAR A PDF -----------------

    Private Sub btnPDFProyecto_Click(sender As Object, e As EventArgs) Handles btnPDFProyecto.Click
        Try
            Dim activo = proyectoController.ObtenerProyectoActivo()

            If activo Is Nothing Then
                MessageBox.Show("No hay proyecto ENCENDIDO. Encienda uno para exportar.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim dt As DataTable = proyectoController.ObtenerDetalleProyectoTabla(activo.id_proyecto)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                MessageBox.Show("El proyecto activo no tiene productos para exportar.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim total As Decimal = proyectoController.CalcularCostoTotal(activo.id_proyecto)

            ' ==============================
            '  📁 CARPETA Y RUTA AUTOMÁTICA
            ' ==============================
            Dim carpetaProyectos As String = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "Reportes_Proyectos"
        )

            ' Crear carpeta si no existe
            If Not Directory.Exists(carpetaProyectos) Then
                Directory.CreateDirectory(carpetaProyectos)
            End If

            ' Nombre de archivo con código + fecha/hora
            Dim nombreArchivo As String = $"{activo.codigo}_{activo.nombre}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            Dim rutaArchivo As String = Path.Combine(carpetaProyectos, nombreArchivo)

            ' ==============================
            '  📄 GENERAR PDF
            ' ==============================
            Dim doc As New Document(PageSize.A4, 40, 40, 40, 40)

            Using fs As New FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None)
                PdfWriter.GetInstance(doc, fs)
                doc.Open()

                ' Título
                Dim titulo As New Paragraph($"Reporte de Proyecto {activo.codigo}",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16))
                titulo.Alignment = Element.ALIGN_CENTER
                doc.Add(titulo)
                doc.Add(New Paragraph(" "))

                ' Datos de encabezado
                doc.Add(New Paragraph($"Nombre del proyecto: {activo.nombre}"))
                doc.Add(New Paragraph($"Fecha creación proyecto: {activo.fecha_creacion:dd/MM/yyyy HH:mm}"))
                doc.Add(New Paragraph($"Fecha de generación del archivo: {DateTime.Now:dd/MM/yyyy HH:mm}"))
                doc.Add(New Paragraph(" "))
                doc.Add(New Paragraph("Productos del proyecto:"))
                doc.Add(New Paragraph(" "))

                ' Columnas que se mostrarán
                Dim columnas = New List(Of String) From {
                "codigo", "nombre", "descripcion", "categoria", "costo", "cantidad", "origen"
            }

                Dim tabla As New PdfPTable(columnas.Count)
                tabla.WidthPercentage = 100

                ' Cabeceras
                For Each colName In columnas
                    Dim cell As New PdfPCell(New Phrase(colName.ToUpper())) With {
                    .BackgroundColor = BaseColor.LIGHT_GRAY
                }
                    tabla.AddCell(cell)
                Next

                ' Filas
                For Each row As DataRow In dt.Rows
                    For Each colName In columnas
                        tabla.AddCell(row(colName).ToString())
                    Next
                Next

                doc.Add(tabla)

                doc.Add(New Paragraph(" "))
                Dim parTotal As New Paragraph($"COSTO TOTAL DEL PROYECTO: {total:0.00}",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12))
                parTotal.Alignment = Element.ALIGN_RIGHT
                doc.Add(parTotal)

                doc.Close()
            End Using

            MessageBox.Show($"Archivo PDF generado correctamente en:{Environment.NewLine}{rutaArchivo}",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Abrir el explorador y resaltar el archivo recién creado
            Try
                Process.Start("explorer.exe", "/select,""" & rutaArchivo & """")
            Catch
            End Try

        Catch ex As Exception
            MessageBox.Show("Error al generar el PDF del proyecto: " & ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAjustarStockProyecto_Click(sender As Object, e As EventArgs) Handles btnAjustarStockProyecto.Click
        If dtvAdicional.CurrentRow Is Nothing OrElse dtvAdicional.CurrentRow.IsNewRow Then
            MessageBox.Show("Seleccione un registro del proyecto para ajustar el stock.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim fila = dtvAdicional.CurrentRow

        ' Solo permitir si es un registro de STOCK (no ADICIONAL)
        Dim origen As String = ""
        Try
            origen = fila.Cells("origen").Value.ToString()
        Catch
        End Try

        If Not origen.Equals("STOCK", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("Solo puede ajustar cantidades de los productos provenientes del STOCK.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Obtener datos necesarios
        Dim idMov As Integer
        Dim idProd As Integer
        Dim cantidadActual As Integer

        Try
            idMov = CInt(fila.Cells("id_stock_mov").Value)
            idProd = CInt(fila.Cells("id_producto").Value)
            cantidadActual = CInt(fila.Cells("cantidad").Value)
        Catch ex As Exception
            MessageBox.Show("No se pudo obtener la información del movimiento de stock: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        ' Pedir la nueva cantidad
        Dim input As String = InputBox($"Cantidad actual: {cantidadActual}{Environment.NewLine}Ingrese la nueva cantidad:",
                                       "Ajustar cantidad de stock",
                                       cantidadActual.ToString())

        If String.IsNullOrWhiteSpace(input) Then Exit Sub

        Dim nuevaCantidad As Integer
        If Not Integer.TryParse(input, nuevaCantidad) OrElse nuevaCantidad <= 0 Then
            MessageBox.Show("Cantidad nueva inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If nuevaCantidad = cantidadActual Then
            MessageBox.Show("La cantidad nueva es igual a la actual. No hay cambios que aplicar.",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Llamar al controlador
        Dim ok = proyectoController.ActualizarCantidadStockMovimiento(idMov, idProd, nuevaCantidad)

        If ok Then
            MessageBox.Show("Cantidad de stock ajustada correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' Recargar detalle y total del proyecto
            CargarDetalleProyectoSeleccionado()
        Else
            MessageBox.Show("No se pudo ajustar la cantidad de stock.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


End Class
