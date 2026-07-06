Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class FrmReporte
    Private Sub FrmReporte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboFiltro.Items.Add("Todos")
        cboFiltro.Items.Add("EPPs")
        cboFiltro.Items.Add("Herramientas")
        cboFiltro.Items.Add("Materiales")
        cboFiltro.Items.Add("Chatarra")
        cboFiltro.SelectedIndex = 0

        cboPeriodo.Items.Clear()
        cboPeriodo.Items.Add("Semanas")
        cboPeriodo.Items.Add("Meses")
        cboPeriodo.SelectedIndex = 0

        ' ✅ Configurar el DataGridView del reporte una sola vez
        ConfigurarGrid(dtvReporte)

        CargarReporte()
        CalcularTotales()
        CargarDashboard()
    End Sub

    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    Private Sub cboFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFiltro.SelectedIndexChanged
        CargarReporte()
    End Sub
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    Private Sub CargarReporte()
        Try
            Dim filtro As String = cboFiltro.Text
            If filtro = "Todos" Then
                dtvReporte.DataSource = reporteController.MostrarTodos()
            Else
                dtvReporte.DataSource = reporteController.MostrarPorCategoria(filtro)
            End If

            ' ✅ Formato ligero del grid
            FormatearGrid(dtvReporte)

        Catch ex As Exception
            MessageBox.Show("Error al cargar el reporte: " & ex.Message)
        End Try
    End Sub

    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            Dim tabla As DataTable

            If cboFiltro.Text = "Todos" Then
                tabla = reporteController.GenerarReporteCompletoAvanzado()
            Else
                tabla = reporteController.GenerarReportePorCategoria(cboFiltro.Text)
            End If

            ExportarAExcel(tabla)
            MessageBox.Show("Reporte Excel generado correctamente.")
        Catch ex As Exception
            MessageBox.Show("Error al generar Excel: " & ex.Message)
        End Try
    End Sub
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            Dim tabla As DataTable

            If cboFiltro.Text = "Todos" Then
                tabla = reporteController.GenerarReporteCompletoAvanzado()
            Else
                tabla = reporteController.GenerarReportePorCategoria(cboFiltro.Text)
            End If

            ExportarAPDF(tabla)
            MessageBox.Show("Reporte PDF generado correctamente.")
        Catch ex As Exception
            MessageBox.Show("Error al generar PDF: " & ex.Message)
        End Try
    End Sub
    ' =========================
    ' FUNCIONES DE EXPORTACIÓN
    ' =========================
    Private Sub ExportarAExcel(tabla As DataTable)
        Dim categoriaSeleccionada As String = cboFiltro.Text

        ' Carpeta de destino en el Escritorio
        Dim carpetaExcel As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Reportes_Excel")

        ' Crear carpeta si no existe
        If Not Directory.Exists(carpetaExcel) Then
            Directory.CreateDirectory(carpetaExcel)
        End If

        ' Nombre del archivo con ruta dentro de la carpeta
        Dim ruta As String = Path.Combine(carpetaExcel, $"ReporteInventario_{categoriaSeleccionada}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx")

        Try
            Using wb As New XLWorkbook()
                Dim ws = wb.Worksheets.Add("Inventario")

                ' TÍTULOS
                ws.Cell(1, 1).Value = "Elevadores Montacarga E.I.R.L."
                ws.Range(ws.Cell(1, 1), ws.Cell(1, tabla.Columns.Count)).Merge().Style.Font.SetBold().Font.FontSize = 16

                ws.Cell(2, 1).Value = $"Reporte de Inventario - {categoriaSeleccionada}"
                ws.Range(ws.Cell(2, 1), ws.Cell(2, tabla.Columns.Count)).Merge().Style.Font.SetBold().Font.FontSize = 12

                ws.Cell(3, tabla.Columns.Count).Value = "Fecha: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")

                ' ENCABEZADOS
                Dim headerRow As Integer = 5
                For col = 0 To tabla.Columns.Count - 1
                    ws.Cell(headerRow, col + 1).Value = tabla.Columns(col).ColumnName
                    ws.Cell(headerRow, col + 1).Style.Font.SetBold()
                    ws.Cell(headerRow, col + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center
                Next

                ' DATOS (escribimos tipos correctamente)
                For row = 0 To tabla.Rows.Count - 1
                    For col = 0 To tabla.Columns.Count - 1
                        Dim valor = tabla.Rows(row)(col)
                        Dim cel = ws.Cell(row + headerRow + 1, col + 1)

                        If IsDBNull(valor) OrElse valor Is Nothing Then
                            cel.Value = ""
                        ElseIf TypeOf valor Is DateTime Then
                            cel.Value = CType(valor, DateTime)
                            cel.Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss"
                        ElseIf IsNumeric(valor) Then
                            cel.Value = Convert.ToDouble(valor)
                        Else
                            cel.Value = valor.ToString()
                        End If
                    Next
                Next

                ws.Columns().AdjustToContents()

                ' --- CÁLCULO DE TOTALES ---
                Dim totalGeneral As Decimal = 0
                Dim resumen As New Dictionary(Of String, Decimal)

                If categoriaSeleccionada = "Todos" Then
                    For Each row As DataRow In tabla.Rows
                        Dim cat As String = If(row.Table.Columns.Contains("Categoria"), row("Categoria").ToString(), "")
                        Dim valorNeto As Decimal = 0
                        If row.Table.Columns.Contains("Valor Neto") AndAlso Not IsDBNull(row("Valor Neto")) Then
                            Decimal.TryParse(row("Valor Neto").ToString(), valorNeto)
                        ElseIf row.Table.Columns.Contains("ValorNeto") AndAlso Not IsDBNull(row("ValorNeto")) Then
                            Decimal.TryParse(row("ValorNeto").ToString(), valorNeto)
                        End If

                        totalGeneral += valorNeto
                        If cat <> "Chatarra" Then
                            If Not resumen.ContainsKey(cat) Then resumen(cat) = 0
                            resumen(cat) += valorNeto
                        End If
                    Next
                ElseIf categoriaSeleccionada <> "Chatarra" Then
                    For Each row As DataRow In tabla.Rows
                        Dim valorNeto As Decimal = 0
                        If row.Table.Columns.Contains("Valor Neto") AndAlso Not IsDBNull(row("Valor Neto")) Then
                            Decimal.TryParse(row("Valor Neto").ToString(), valorNeto)
                        ElseIf row.Table.Columns.Contains("ValorNeto") AndAlso Not IsDBNull(row("ValorNeto")) Then
                            Decimal.TryParse(row("ValorNeto").ToString(), valorNeto)
                        End If
                        totalGeneral += valorNeto
                    Next
                End If

                ' --- ESCRIBIR TOTALES en la esquina derecha ---
                Dim ultimaFilaUsada As Integer = ws.LastRowUsed().RowNumber()
                Dim filaTotales As Integer = ultimaFilaUsada + 3
                Dim colLabel As Integer = Math.Max(1, tabla.Columns.Count - 1)
                Dim colValue As Integer = tabla.Columns.Count

                If categoriaSeleccionada = "Todos" Then
                    ws.Cell(filaTotales, colLabel).Value = "Valor Total Inventario"
                    ws.Cell(filaTotales, colValue).Value = totalGeneral
                    ws.Cell(filaTotales, colLabel).Style.Font.Bold = True
                    ws.Cell(filaTotales, colValue).Style.Font.Bold = True
                    ws.Cell(filaTotales, colValue).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right

                    For Each kvp In resumen
                        filaTotales += 1
                        ws.Cell(filaTotales, colLabel).Value = kvp.Key
                        ws.Cell(filaTotales, colValue).Value = kvp.Value
                        ws.Cell(filaTotales, colLabel).Style.Font.Bold = True
                        ws.Cell(filaTotales, colValue).Style.Font.Bold = True
                        ws.Cell(filaTotales, colValue).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right
                    Next
                ElseIf categoriaSeleccionada <> "Chatarra" Then
                    ws.Cell(filaTotales, colLabel).Value = $"Valor Total {categoriaSeleccionada}"
                    ws.Cell(filaTotales, colValue).Value = totalGeneral
                    ws.Cell(filaTotales, colLabel).Style.Font.Bold = True
                    ws.Cell(filaTotales, colValue).Style.Font.Bold = True
                    ws.Cell(filaTotales, colValue).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right
                End If

                wb.SaveAs(ruta)
            End Using

            MessageBox.Show($"Archivo guardado: {ruta}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Abrir el explorador y seleccionar el archivo guardado
            Try
                Process.Start("explorer.exe", "/select,""" & ruta & """")
            Catch
            End Try

        Catch ex As Exception
            MessageBox.Show("Error guardando Excel: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    ' ----------------------------
    Private Sub ExportarAPDF(tabla As DataTable)
        Dim categoriaSeleccionada As String = cboFiltro.Text

        ' Carpeta de destino en el Escritorio
        Dim carpetaPDF As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Reportes_PDF")

        ' Crear carpeta si no existe
        If Not Directory.Exists(carpetaPDF) Then
            Directory.CreateDirectory(carpetaPDF)
        End If

        ' Nombre del archivo con ruta dentro de la carpeta
        Dim ruta As String = Path.Combine(carpetaPDF, $"ReporteInventario_{categoriaSeleccionada}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf")

        Try
            Dim doc As New Document(PageSize.A4, 40, 40, 60, 60)
            Using fs As New FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None)
                Dim writer = PdfWriter.GetInstance(doc, fs)
                doc.Open()

                ' Fuentes
                Dim fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)
                Dim fontSub = FontFactory.GetFont(FontFactory.HELVETICA, 11)
                Dim fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 9)
                Dim fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)

                ' Títulos
                Dim pTitulo As New Paragraph("Elevadores Montacarga E.I.R.L.", fontTitulo)
                pTitulo.Alignment = Element.ALIGN_CENTER
                doc.Add(pTitulo)

                Dim pSub As New Paragraph($"Reporte de Inventario - {categoriaSeleccionada}", fontSub)
                pSub.Alignment = Element.ALIGN_CENTER
                doc.Add(pSub)

                doc.Add(New Paragraph("Fecha: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fontNormal))
                doc.Add(New Paragraph(" "))

                ' TABLA PRINCIPAL
                Dim pdfTable As New PdfPTable(tabla.Columns.Count)
                pdfTable.WidthPercentage = 100

                ' Encabezados
                For Each col As DataColumn In tabla.Columns
                    Dim cellHead As New PdfPCell(New Phrase(col.ColumnName, fontBold))
                    cellHead.BackgroundColor = BaseColor.LIGHT_GRAY
                    cellHead.HorizontalAlignment = Element.ALIGN_CENTER
                    pdfTable.AddCell(cellHead)
                Next

                ' Filas
                For Each row As DataRow In tabla.Rows
                    For Each col As DataColumn In tabla.Columns
                        Dim texto As String = If(IsDBNull(row(col)), "", row(col).ToString())
                        pdfTable.AddCell(New Phrase(texto, fontNormal))
                    Next
                Next

                doc.Add(pdfTable)

                ' --- CÁLCULO DE TOTALES ---
                Dim totalGeneral As Decimal = 0
                Dim resumen As New Dictionary(Of String, Decimal)

                If categoriaSeleccionada = "Todos" Then
                    For Each row As DataRow In tabla.Rows
                        Dim cat As String = If(row.Table.Columns.Contains("Categoria"), row("Categoria").ToString(), "")
                        Dim valorNeto As Decimal = 0
                        If row.Table.Columns.Contains("Valor Neto") AndAlso Not IsDBNull(row("Valor Neto")) Then
                            Decimal.TryParse(row("Valor Neto").ToString(), valorNeto)
                        ElseIf row.Table.Columns.Contains("ValorNeto") AndAlso Not IsDBNull(row("ValorNeto")) Then
                            Decimal.TryParse(row("ValorNeto").ToString(), valorNeto)
                        End If

                        totalGeneral += valorNeto
                        If cat <> "Chatarra" Then
                            If Not resumen.ContainsKey(cat) Then resumen(cat) = 0
                            resumen(cat) += valorNeto
                        End If
                    Next
                ElseIf categoriaSeleccionada <> "Chatarra" Then
                    For Each row As DataRow In tabla.Rows
                        Dim valorNeto As Decimal = 0
                        If row.Table.Columns.Contains("Valor Neto") AndAlso Not IsDBNull(row("Valor Neto")) Then
                            Decimal.TryParse(row("Valor Neto").ToString(), valorNeto)
                        ElseIf row.Table.Columns.Contains("ValorNeto") AndAlso Not IsDBNull(row("ValorNeto")) Then
                            Decimal.TryParse(row("ValorNeto").ToString(), valorNeto)
                        End If
                        totalGeneral += valorNeto
                    Next
                End If

                ' --- TABLA DE TOTALES ---
                If (categoriaSeleccionada = "Todos" AndAlso resumen.Count > 0) OrElse (categoriaSeleccionada <> "Todos" AndAlso categoriaSeleccionada <> "Chatarra" AndAlso totalGeneral > 0) Then
                    Dim tablaTotales As New PdfPTable(2)
                    tablaTotales.TotalWidth = 200
                    tablaTotales.LockedWidth = True
                    tablaTotales.DefaultCell.Border = PdfPCell.NO_BORDER

                    If categoriaSeleccionada = "Todos" Then
                        Dim cLabel As New PdfPCell(New Phrase("Valor Total Inventario:", fontBold))
                        cLabel.Border = PdfPCell.NO_BORDER
                        cLabel.HorizontalAlignment = Element.ALIGN_RIGHT
                        tablaTotales.AddCell(cLabel)
                        Dim cVal As New PdfPCell(New Phrase(totalGeneral.ToString("C2"), fontBold))
                        cVal.Border = PdfPCell.NO_BORDER
                        cVal.HorizontalAlignment = Element.ALIGN_RIGHT
                        tablaTotales.AddCell(cVal)

                        For Each kvp In resumen
                            Dim cl As New PdfPCell(New Phrase(kvp.Key & ":", fontNormal))
                            cl.Border = PdfPCell.NO_BORDER
                            cl.HorizontalAlignment = Element.ALIGN_RIGHT
                            tablaTotales.AddCell(cl)
                            Dim cv As New PdfPCell(New Phrase(kvp.Value.ToString("C2"), fontNormal))
                            cv.Border = PdfPCell.NO_BORDER
                            cv.HorizontalAlignment = Element.ALIGN_RIGHT
                            tablaTotales.AddCell(cv)
                        Next
                    Else
                        Dim cl As New PdfPCell(New Phrase($"Valor Total {categoriaSeleccionada}:", fontBold))
                        cl.Border = PdfPCell.NO_BORDER
                        cl.HorizontalAlignment = Element.ALIGN_RIGHT
                        tablaTotales.AddCell(cl)
                        Dim cv As New PdfPCell(New Phrase(totalGeneral.ToString("C2"), fontBold))
                        cv.Border = PdfPCell.NO_BORDER
                        cv.HorizontalAlignment = Element.ALIGN_RIGHT
                        tablaTotales.AddCell(cv)
                    End If

                    ' Posicionar la tabla de totales en la esquina inferior derecha
                    tablaTotales.WriteSelectedRows(0, -1,
                                          doc.PageSize.Width - doc.RightMargin - tablaTotales.TotalWidth,
                                          doc.BottomMargin + tablaTotales.TotalHeight + 5,
                                          writer.DirectContent)
                End If

                doc.Close()
            End Using

            MessageBox.Show($"Archivo guardado: {ruta}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Try
                Process.Start("explorer.exe", "/select,""" & ruta & """")
            Catch
            End Try

        Catch ex As Exception
            MessageBox.Show("Error generando PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub





    Private Sub CalcularTotales()
        Try
            Dim tabla As New DataTable()

            Using cn = Conexion.ObtenerConexion()
                Dim query As String = "
            SELECT 
                p.categoria,
                p.costo,
                ISNULL(m.stock, 0) AS stock,
                (p.costo * ISNULL(m.stock, 0)) AS valor_neto
            FROM Producto p
            LEFT JOIN (
                SELECT m1.producto_id, m1.stock
                FROM StockMovimiento m1
                INNER JOIN (
                    SELECT producto_id, MAX(fecha) AS max_fecha
                    FROM StockMovimiento
                    GROUP BY producto_id
                ) m2 ON m1.producto_id = m2.producto_id AND m1.fecha = m2.max_fecha
            ) m ON p.id_producto = m.producto_id
            WHERE p.categoria IN ('EPPs', 'Herramientas', 'Materiales')
              AND p.costo IS NOT NULL;"

                Using cmd As New SqlCommand(query, cn)
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(tabla)
                    End Using
                End Using
            End Using

            Dim totalGeneral As Decimal = tabla.AsEnumerable().Sum(Function(r) Convert.ToDecimal(r("valor_neto")))
            Dim totalEPPs As Decimal = tabla.AsEnumerable().Where(Function(r) r("categoria").ToString() = "EPPs").Sum(Function(r) Convert.ToDecimal(r("valor_neto")))
            Dim totalHerramientas As Decimal = tabla.AsEnumerable().Where(Function(r) r("categoria").ToString() = "Herramientas").Sum(Function(r) Convert.ToDecimal(r("valor_neto")))
            Dim totalMateriales As Decimal = tabla.AsEnumerable().Where(Function(r) r("categoria").ToString() = "Materiales").Sum(Function(r) Convert.ToDecimal(r("valor_neto")))

            txtValorInventario.Text = totalGeneral.ToString("C2")
            txtValorEPP.Text = totalEPPs.ToString("C2")
            txtValorHerramienta.Text = totalHerramientas.ToString("C2")
            txtValorMaterial.Text = totalMateriales.ToString("C2")
        Catch ex As Exception
            MessageBox.Show("Error al calcular totales: " & ex.Message)
        End Try
    End Sub

    Private Sub cboPeriodo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPeriodo.SelectedIndexChanged
        CargarDashboard()
    End Sub

    Private Sub CargarDashboard()
        Dim query As String = ""

        If cboPeriodo.Text = "Semanas" Then
            query = "
        SELECT TOP 4
            CAST(DATEPART(YEAR, sm.fecha) AS VARCHAR) + '-W' + 
            RIGHT('0' + CAST(DATEPART(WEEK, sm.fecha) AS VARCHAR), 2) AS Periodo,
            SUM(p.costo * sm.stock) AS ValorNeto
        FROM StockMovimiento sm
        INNER JOIN Producto p ON sm.producto_id = p.id_producto
        WHERE p.costo IS NOT NULL
        GROUP BY DATEPART(YEAR, sm.fecha), DATEPART(WEEK, sm.fecha)
        ORDER BY DATEPART(YEAR, sm.fecha) DESC, DATEPART(WEEK, sm.fecha) DESC"
        ElseIf cboPeriodo.Text = "Meses" Then
            query = "
        SELECT TOP 4
            CAST(DATEPART(YEAR, sm.fecha) AS VARCHAR) + '-M' + 
            RIGHT('0' + CAST(DATEPART(MONTH, sm.fecha) AS VARCHAR), 2) AS Periodo,
            SUM(p.costo * sm.stock) AS ValorNeto
        FROM StockMovimiento sm
        INNER JOIN Producto p ON sm.producto_id = p.id_producto
        WHERE p.costo IS NOT NULL
        GROUP BY DATEPART(YEAR, sm.fecha), DATEPART(MONTH, sm.fecha)
        ORDER BY DATEPART(YEAR, sm.fecha) DESC, DATEPART(MONTH, sm.fecha) DESC"
        End If

        Dim tabla As New DataTable()
        Using cn = Conexion.ObtenerConexion()
            Using cmd As New SqlCommand(query, cn)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(tabla)
                End Using
            End Using
        End Using

        ' 🔁 Reordenar los 4 últimos periodos de más antiguo a más reciente
        Dim vista As DataView = tabla.DefaultView
        vista.Sort = "Periodo ASC"

        ' Enlazar al gráfico usando la vista ordenada
        chartDashboard.DataSource = vista
        chartDashboard.Series(0).XValueMember = "Periodo"
        chartDashboard.Series(0).YValueMembers = "ValorNeto"
        chartDashboard.Series(0).IsValueShownAsLabel = True
        chartDashboard.DataBind()
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Try
            ' ✅ Carpeta destino en el Escritorio
            Dim carpetaBackup As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Backups_BD")

            ' ✅ Crear carpeta si no existe
            If Not Directory.Exists(carpetaBackup) Then
                Directory.CreateDirectory(carpetaBackup)
            End If

            ' ✅ Nombre del archivo con fecha y hora más distinguible
            Dim backupFileName As String = "Backup_" & DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") & ".bak"

            ' ✅ Ruta completa del archivo de backup
            Dim backupPath As String = Path.Combine(carpetaBackup, backupFileName)

            ' ✅ Nombre de tu base de datos
            Dim dbName As String = "DB_ElevadoresMontacarga_vb"

            ' ✅ Query para generar backup
            Dim query As String = $"BACKUP DATABASE [{dbName}] TO DISK = N'{backupPath}' WITH FORMAT, INIT, NAME = 'Backup Manual', SKIP, NOREWIND, NOUNLOAD, STATS = 10"

            ' ✅ Ejecutar backup
            Using cn = Conexion.ObtenerConexion()
                Using cmd As New SqlCommand(query, cn)
                    cmd.CommandTimeout = 0 ' Sin límite de tiempo para backups grandes
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show($"✅ Backup generado correctamente en:{Environment.NewLine}{backupPath}", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ✅ Abrir el explorador y resaltar el archivo recién creado
            Try
                Process.Start("explorer.exe", "/select,""" & backupPath & """")
            Catch
            End Try

        Catch ex As Exception
            MessageBox.Show("❌ Error al generar el backup: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' =========================
    ' CONFIGURACIÓN DEL GRID
    ' =========================
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

        If dgv.Columns.Contains("Costo") Then
            dgv.Columns("Costo").DefaultCellStyle.Format = "C2"
            dgv.Columns("Costo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If dgv.Columns.Contains("Stock") Then
            dgv.Columns("Stock").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If dgv.Columns.Contains("Valor Neto") Then
            dgv.Columns("Valor Neto").DefaultCellStyle.Format = "C2"
            dgv.Columns("Valor Neto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub
End Class
