Imports System.Text.RegularExpressions
Imports System.Data

Public Class FrmProducto

    Private Sub FrmProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboCategoria.Items.AddRange(New String() {"EPPs", "Herramientas", "Materiales", "Chatarra"})

        ' Deshabilitar todos los campos al inicio
        HabilitarCampos(False)

        ' Configurar DataGridView una sola vez
        ConfigurarGrid(dtvProducto)
    End Sub

    ' ==================================================
    ' CAMBIO DE CATEGORÍA
    ' ==================================================
    Private Sub cboCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategoria.SelectedIndexChanged
        CargarProductosPorCategoria()

        If cboCategoria.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una categoría.")
            Exit Sub
        End If

        HabilitarCampos(False)
        LimpiarFormulario()

        Select Case cboCategoria.Text
            Case "EPPs"
                txtNombre.Enabled = True
                txtCodigo.Enabled = True
                txtDescripcion.Enabled = True
                txtCosto.Enabled = True
                txtTalla.Enabled = True

            Case "Herramientas"
                txtNombre.Enabled = True
                txtCodigo.Enabled = True
                txtDescripcion.Enabled = True
                txtCosto.Enabled = True
                txtMarca.Enabled = True
                txtModelo.Enabled = True

            Case "Materiales"
                txtNombre.Enabled = True
                txtCodigo.Enabled = True
                txtDescripcion.Enabled = True
                txtCosto.Enabled = True
                txtMedida.Enabled = True

            Case "Chatarra"
                txtNombre.Enabled = True
                txtCodigo.Enabled = True
                txtDescripcion.Enabled = True
        End Select
    End Sub

    Private Sub HabilitarCampos(habilitar As Boolean)
        txtNombre.Enabled = habilitar
        txtCodigo.Enabled = habilitar
        txtDescripcion.Enabled = habilitar
        txtCosto.Enabled = habilitar
        txtTalla.Enabled = habilitar
        txtMarca.Enabled = habilitar
        txtModelo.Enabled = habilitar
        txtMedida.Enabled = habilitar
    End Sub

    ' ==================================================
    ' REGISTRAR
    ' ==================================================
    Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        ' Validación: el código no se escribe en registro
        If txtCodigo.Text.Trim() <> "" Then
            MessageBox.Show("El campo 'Código' solo se puede usar para buscar o eliminar productos. El código se asigna automáticamente al registrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCodigo.Text = ""
            Exit Sub
        End If

        If Not ValidarCampos(True) Then Exit Sub

        Try
            Dim p As New Producto With {
                .nombre = txtNombre.Text.Trim(),
                .categoria = cboCategoria.Text,
                .descripcion = If(txtDescripcion.Text.Trim() = "No hay descripción", "", txtDescripcion.Text.Trim()),
                .usuario_id = 1
            }

            ' Costo (excepto chatarra)
            If cboCategoria.Text <> "Chatarra" Then
                Dim costoVal As Decimal
                If Not Decimal.TryParse(txtCosto.Text.Trim(), costoVal) OrElse costoVal <= 0D Then
                    MessageBox.Show("Ingrese un costo válido mayor a 0.")
                    Exit Sub
                End If
                p.costo = costoVal
            Else
                p.costo = 0D
            End If

            ' Campos extra
            Select Case p.categoria
                Case "EPPs"
                    p.talla = txtTalla.Text.Trim()
                Case "Herramientas"
                    p.marca = txtMarca.Text.Trim()
                    p.modelo = txtModelo.Text.Trim()
                Case "Materiales"
                    p.medida = txtMedida.Text.Trim()
            End Select

            If ProductoController.RegistrarProducto(p) Then
                MessageBox.Show("Producto registrado con código: " & p.codigo)
                CargarProductosPorCategoria()
                LimpiarFormulario()
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' ==================================================
    ' BUSCAR
    ' ==================================================
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        If cboCategoria.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una categoría para buscar.", "Aviso")
            Exit Sub
        End If

        Dim categoria As String = cboCategoria.Text

        If txtCodigo.Text.Trim() <> "" Then
            ' Buscar por código
            Try
                Dim prod As Producto = ProductoController.BuscarProducto(txtCodigo.Text.Trim(), categoria)
                If prod IsNot Nothing Then
                    ' Sincronizar categoría
                    cboCategoria.SelectedItem = prod.categoria
                    cboCategoria_SelectedIndexChanged(Nothing, Nothing)

                    ' Datos base
                    txtCodigo.Text = prod.codigo
                    txtNombre.Text = prod.nombre
                    txtCosto.Text = prod.costo.ToString("0.00")
                    txtDescripcion.Text = If(String.IsNullOrWhiteSpace(prod.descripcion), "No hay descripción", prod.descripcion)

                    ' Extras
                    Select Case prod.categoria
                        Case "EPPs"
                            txtTalla.Text = prod.talla
                        Case "Herramientas"
                            txtMarca.Text = prod.marca
                            txtModelo.Text = prod.modelo
                        Case "Materiales"
                            txtMedida.Text = prod.medida
                        Case "Chatarra"
                            ' Sin campos extra
                    End Select
                Else
                    MessageBox.Show("Producto no encontrado con ese código y categoría.", "Sin resultado")
                End If

            Catch ex As Exception
                MessageBox.Show("Error al buscar: " & ex.Message)
            End Try

        ElseIf txtNombre.Text.Trim() <> "" Then
            ' Buscar por nombre parcial
            Try
                Dim resultados As DataTable = ProductoController.BuscarProductoPorNombre(txtNombre.Text.Trim(), categoria)
                If resultados.Rows.Count > 0 Then
                    dtvProducto.DataSource = resultados
                Else
                    MessageBox.Show("No se encontraron productos con ese nombre parcial.", "Sin resultado")
                End If
            Catch ex As Exception
                MessageBox.Show("Error al buscar por nombre: " & ex.Message)
            End Try

        Else
            MessageBox.Show("Debe ingresar un código o un nombre para buscar.", "Aviso")
        End If
    End Sub

    ' ==================================================
    ' ACTUALIZAR
    ' ==================================================
    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If cboCategoria.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una categoría para actualizar.", "Aviso")
            Exit Sub
        End If

        If txtCodigo.Text.Trim() = "" Then
            MessageBox.Show("Ingrese el código del producto a actualizar.", "Aviso")
            Exit Sub
        End If

        If Not ValidarCampos(False) Then Exit Sub

        Dim descripcionFinal As String =
            If(txtDescripcion.Text.Trim() = "No hay descripción", "", txtDescripcion.Text.Trim())

        Dim costoVal As Decimal = 0D
        If cboCategoria.Text <> "Chatarra" Then
            If Not Decimal.TryParse(txtCosto.Text.Trim(), costoVal) OrElse costoVal <= 0D Then
                MessageBox.Show("Ingrese un costo válido mayor a 0.")
                Exit Sub
            End If
        End If

        Try
            Dim p As New Producto With {
                .codigo = txtCodigo.Text.Trim(),
                .categoria = cboCategoria.Text,
                .nombre = txtNombre.Text.Trim(),
                .descripcion = descripcionFinal,
                .costo = costoVal
            }

            Select Case p.categoria
                Case "EPPs"
                    p.talla = txtTalla.Text.Trim()
                Case "Herramientas"
                    p.marca = txtMarca.Text.Trim()
                    p.modelo = txtModelo.Text.Trim()
                Case "Materiales"
                    p.medida = txtMedida.Text.Trim()
            End Select

            If ProductoController.ActualizarProducto(p) Then
                MessageBox.Show("Producto actualizado correctamente.")
                CargarProductosPorCategoria()
                LimpiarFormulario()
            End If

        Catch ex As Exception
            MessageBox.Show("Error al actualizar: " & ex.Message)
        End Try
    End Sub

    ' ==================================================
    ' ELIMINAR
    ' ==================================================
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If cboCategoria.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una categoría para eliminar.", "Aviso")
            Exit Sub
        End If

        If txtCodigo.Text.Trim() = "" Then
            MessageBox.Show("Ingrese el código del producto a eliminar.", "Aviso")
            Exit Sub
        End If

        Dim confirmacion = MessageBox.Show("¿Está seguro que desea eliminar el producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If confirmacion = DialogResult.No Then Exit Sub

        Try
            Dim p As New Producto With {
                .codigo = txtCodigo.Text.Trim(),
                .categoria = cboCategoria.Text
            }

            If ProductoController.EliminarProducto(p) Then
                MessageBox.Show("Producto eliminado correctamente.")
                LimpiarFormulario()
                CargarProductosPorCategoria()
            End If

        Catch ex As Exception
            MessageBox.Show("Error al eliminar: " & ex.Message)
        End Try
    End Sub

    ' ==================================================
    ' CARGA DE PRODUCTOS AL GRID
    ' ==================================================
    Private Sub CargarProductosPorCategoria()
        Try
            If cboCategoria.SelectedIndex = -1 Then
                dtvProducto.DataSource = Nothing
                dtvProducto.Columns.Clear()
                dtvProducto.AutoGenerateColumns = True
                Exit Sub
            End If

            Dim categoriaSeleccionada = cboCategoria.Text
            dtvProducto.DataSource = ProductoController.ListarPorCategoria(categoriaSeleccionada)

            FormatearGrid(dtvProducto)

        Catch ex As Exception
            MessageBox.Show("Error al cargar productos: " & ex.Message)
        End Try
    End Sub

    Private Sub LimpiarFormulario()
        txtCodigo.Text = ""
        txtNombre.Text = ""
        txtDescripcion.Text = ""
        txtCosto.Text = ""
        txtTalla.Text = ""
        txtMarca.Text = ""
        txtModelo.Text = ""
        txtMedida.Text = ""
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        LimpiarFormulario()
    End Sub

    ' ==================================================
    ' VALIDACIONES DE UI
    ' ==================================================
    Private Function ValidarCampos(Optional esRegistro As Boolean = True) As Boolean
        If cboCategoria.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una categoría.")
            Return False
        End If

        Dim soloLetrasEspacios As New Regex("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")
        Dim letrasNumerosEspacios As New Regex("^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]+$")
        Dim decimalValido As New Regex("^\d+(\.\d{1,2})?$")

        ' Nombre
        If esRegistro Then
            If Not soloLetrasEspacios.IsMatch(txtNombre.Text.Trim()) Then
                MessageBox.Show("El nombre solo debe contener letras y espacios.")
                Return False
            End If
        Else
            If txtNombre.Text.Trim() <> "" AndAlso Not soloLetrasEspacios.IsMatch(txtNombre.Text.Trim()) Then
                MessageBox.Show("El nombre solo debe contener letras y espacios.")
                Return False
            End If
        End If

        Select Case cboCategoria.Text
            Case "EPPs"
                If Not decimalValido.IsMatch(txtCosto.Text.Trim()) OrElse Decimal.Parse(txtCosto.Text) <= 0 Then
                    MessageBox.Show("El costo debe ser un número decimal válido mayor a 0 (máximo 2 decimales).")
                    Return False
                End If
                If txtTalla.Text.Trim() <> "" AndAlso Not letrasNumerosEspacios.IsMatch(txtTalla.Text.Trim()) Then
                    MessageBox.Show("La talla solo debe contener letras, números o espacios.")
                    Return False
                End If

            Case "Herramientas"
                If Not decimalValido.IsMatch(txtCosto.Text.Trim()) OrElse Decimal.Parse(txtCosto.Text) <= 0 Then
                    MessageBox.Show("El costo debe ser un número decimal válido mayor a 0 (máximo 2 decimales).")
                    Return False
                End If
                If txtMarca.Text.Trim() <> "" AndAlso Not letrasNumerosEspacios.IsMatch(txtMarca.Text.Trim()) Then
                    MessageBox.Show("La marca solo debe contener letras, números o espacios.")
                    Return False
                End If
                If txtModelo.Text.Trim() <> "" AndAlso Not letrasNumerosEspacios.IsMatch(txtModelo.Text.Trim()) Then
                    MessageBox.Show("El modelo solo debe contener letras, números o espacios.")
                    Return False
                End If

            Case "Materiales"
                If Not decimalValido.IsMatch(txtCosto.Text.Trim()) OrElse Decimal.Parse(txtCosto.Text) <= 0 Then
                    MessageBox.Show("El costo debe ser un número decimal válido mayor a 0 (máximo 2 decimales).")
                    Return False
                End If

            Case "Chatarra"
                ' Sin costo obligatorio
        End Select

        Return True
    End Function

    ' ==================================================
    ' CONFIGURACIÓN Y FORMATO DEL GRID
    ' ==================================================
    Private Sub ConfigurarGrid(dgv As DataGridView)
        If dgv Is Nothing Then Return
        dgv.AutoGenerateColumns = True
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.[False]
        dgv.RowHeadersVisible = False
        dgv.AllowUserToResizeRows = False
        dgv.AllowUserToResizeColumns = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
    End Sub

    Private Sub FormatearGrid(dgv As DataGridView)
        If dgv Is Nothing OrElse dgv.Columns.Count = 0 Then Return

        If dgv.Columns.Contains("Costo") Then
            dgv.Columns("Costo").DefaultCellStyle.Format = "C2"
            dgv.Columns("Costo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If dgv.Columns.Contains("Fecha") Then
            dgv.Columns("Fecha").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
            dgv.Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If
    End Sub

End Class
