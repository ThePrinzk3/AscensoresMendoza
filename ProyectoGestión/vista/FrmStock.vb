Public Class FrmStock

    Private Sub FrmStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboCategoria.Items.Clear()
        cboCategoria.Items.AddRange(New String() {"Todos", "EPPs", "Herramientas", "Materiales", "Chatarra"})
        cboCategoria.SelectedIndex = 0

        txtCodigo.Text = ""
        txtCantidad.Text = ""
        txtNombre.Text = ""
        txtTrabajador.Text = ""

        ConfigurarGrid(dtvStock)
        ConfigurarGrid(dtvAlerta)
        ConfigurarGrid(dtvHistorial)

        dtvHistorial.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        dtvHistorial.DefaultCellStyle.WrapMode = DataGridViewTriState.False
        dtvHistorial.RowTemplate.Height = 22

        CargarTablas()
        CargarHistorialGeneral()
    End Sub

    Private Sub cboCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategoria.SelectedIndexChanged
        CargarTablas()
        CargarHistorialGeneral()
    End Sub

    Private Sub btnRegistrarEntrada_Click(sender As Object, e As EventArgs) Handles btnRegistrarEntrada.Click
        Try
            ValidarDatosParaMovimiento()
            If stockController.RegistrarEntrada(txtCodigo.Text.Trim(), CInt(txtCantidad.Text.Trim()), 1) Then
                MessageBox.Show("Entrada registrada.")
                CargarTablas()
                CargarHistorial(txtCodigo.Text.Trim())
                LimpiarCamposMovimiento()
                CargarHistorialGeneral()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRegistrarSalida_Click(sender As Object, e As EventArgs) Handles btnRegistrarSalida.Click
        Try
            ValidarDatosParaMovimiento()

            If String.IsNullOrWhiteSpace(txtTrabajador.Text) Then
                Throw New Exception("Debe ingresar el trabajador responsable para la salida.")
            End If

            If stockController.RegistrarSalida(txtCodigo.Text.Trim(), CInt(txtCantidad.Text.Trim()), 1, txtTrabajador.Text.Trim()) Then
                MessageBox.Show("Salida registrada.")
                CargarTablas()
                CargarHistorial(txtCodigo.Text.Trim())
                LimpiarCamposMovimiento()
                txtTrabajador.Text = ""
                CargarHistorialGeneral()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            If txtCodigo.Text.Trim() <> "" Then
                dtvStock.DataSource = stockController.BuscarStockPorCodigo(txtCodigo.Text.Trim())
                dtvHistorial.DataSource = stockController.BuscarHistorialPorCodigo(txtCodigo.Text.Trim())
                FormatearGrid(dtvStock)
                FormatearGrid(dtvHistorial)
                Return
            End If

            If txtNombre.Text.Trim() <> "" OrElse cboCategoria.Text <> "Todos" OrElse txtTrabajador.Text.Trim() <> "" Then
                Dim categoriaSeleccionada As String = cboCategoria.Text
                Dim nombreFiltro As String = txtNombre.Text.Trim()
                Dim codigoFiltro As String = txtCodigo.Text.Trim()
                Dim trabajadorFiltro As String = txtTrabajador.Text.Trim()

                dtvStock.DataSource = stockController.ListarStockGeneral(categoriaSeleccionada, nombreFiltro)
                dtvHistorial.DataSource = stockController.ListarHistorialGeneral(categoriaSeleccionada, nombreFiltro, codigoFiltro, trabajadorFiltro)

                FormatearGrid(dtvStock)
                FormatearGrid(dtvHistorial)
                Return
            End If

            MessageBox.Show("Ingrese un código, nombre, trabajador o seleccione una categoría para buscar.")
        Catch ex As Exception
            MessageBox.Show("Error al buscar: " & ex.Message)
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        txtCodigo.Text = ""
        txtCantidad.Text = ""
        txtNombre.Text = ""
        txtTrabajador.Text = ""
        cboCategoria.SelectedIndex = 0

        CargarTablas()
        CargarHistorialGeneral()
    End Sub

    ' ---------- LÓGICA INTERNA UI ----------
    Private Sub ValidarDatosParaMovimiento()
        If txtCodigo.Text.Trim() = "" Then
            Throw New Exception("Debe ingresar un código de producto.")
        End If
        Dim cantidadInt As Integer
        If Not Integer.TryParse(txtCantidad.Text.Trim(), cantidadInt) OrElse cantidadInt <= 0 Then
            Throw New Exception("Cantidad inválida.")
        End If
    End Sub

    Private Sub LimpiarCamposMovimiento()
        txtCodigo.Text = ""
        txtCantidad.Text = ""
    End Sub

    Private Sub CargarTablas()
        Dim categoriaSeleccionada As String = If(cboCategoria.SelectedItem IsNot Nothing, cboCategoria.SelectedItem.ToString(), "Todos")
        Dim filtroNombre As String = txtNombre.Text.Trim()

        dtvStock.DataSource = stockController.ListarStockGeneral(categoriaSeleccionada, filtroNombre)
        dtvAlerta.DataSource = stockController.ListarStockEnAlerta(categoriaSeleccionada)

        FormatearGrid(dtvStock)
        FormatearGrid(dtvAlerta)
    End Sub

    Private Sub CargarHistorial(Optional codigo As String = "")
        If String.IsNullOrWhiteSpace(codigo) Then
            dtvHistorial.DataSource = Nothing
            Return
        End If
        dtvHistorial.DataSource = stockController.BuscarHistorialPorCodigo(codigo)
        FormatearGrid(dtvHistorial)
    End Sub

    Private Sub CargarHistorialGeneral()
        Dim categoriaSeleccionada As String = If(cboCategoria.SelectedItem IsNot Nothing, cboCategoria.SelectedItem.ToString(), "Todos")
        Dim filtroNombre As String = txtNombre.Text.Trim()
        Dim filtroCodigo As String = txtCodigo.Text.Trim()
        Dim filtroTrabajador As String = txtTrabajador.Text.Trim()

        dtvHistorial.DataSource = stockController.ListarHistorialGeneral(categoriaSeleccionada, filtroNombre, filtroCodigo, filtroTrabajador)
        FormatearGrid(dtvHistorial)
    End Sub

    Private Sub ConfigurarGrid(dgv As DataGridView)
        If dgv Is Nothing Then Return
        dgv.AutoGenerateColumns = True
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False
        dgv.RowHeadersVisible = False
        dgv.AllowUserToResizeRows = False
        dgv.AllowUserToResizeColumns = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
    End Sub

    Private Sub FormatearGrid(dgv As DataGridView)
        If dgv Is Nothing OrElse dgv.Columns.Count = 0 Then Return

        If dgv.Columns.Contains("Fecha") Then
            dgv.Columns("Fecha").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
        End If
    End Sub

End Class
