<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReporte
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReporte))
        Me.dtvReporte = New System.Windows.Forms.DataGridView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboFiltro = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtValorInventario = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtValorEPP = New System.Windows.Forms.TextBox()
        Me.txtValorHerramienta = New System.Windows.Forms.TextBox()
        Me.txtValorMaterial = New System.Windows.Forms.TextBox()
        Me.chartDashboard = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.cboPeriodo = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnBackup = New System.Windows.Forms.Button()
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox()
        Me.btnPDF = New System.Windows.Forms.Button()
        Me.btnExcel = New System.Windows.Forms.Button()
        CType(Me.dtvReporte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartDashboard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtvReporte
        '
        Me.dtvReporte.AllowUserToAddRows = False
        Me.dtvReporte.AllowUserToDeleteRows = False
        Me.dtvReporte.AllowUserToResizeColumns = False
        Me.dtvReporte.AllowUserToResizeRows = False
        Me.dtvReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dtvReporte.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtvReporte.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dtvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtvReporte.Location = New System.Drawing.Point(475, 12)
        Me.dtvReporte.Name = "dtvReporte"
        Me.dtvReporte.ReadOnly = True
        Me.dtvReporte.Size = New System.Drawing.Size(1413, 931)
        Me.dtvReporte.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(176, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 22)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Imprimir"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(169, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 22)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Categoría:"
        '
        'cboFiltro
        '
        Me.cboFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFiltro.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFiltro.ForeColor = System.Drawing.Color.Black
        Me.cboFiltro.FormattingEnabled = True
        Me.cboFiltro.Location = New System.Drawing.Point(274, 42)
        Me.cboFiltro.Name = "cboFiltro"
        Me.cboFiltro.Size = New System.Drawing.Size(162, 30)
        Me.cboFiltro.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(37, 196)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(231, 22)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Valor total del inventario:"
        '
        'txtValorInventario
        '
        Me.txtValorInventario.Enabled = False
        Me.txtValorInventario.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorInventario.ForeColor = System.Drawing.Color.Black
        Me.txtValorInventario.Location = New System.Drawing.Point(274, 193)
        Me.txtValorInventario.Name = "txtValorInventario"
        Me.txtValorInventario.Size = New System.Drawing.Size(137, 30)
        Me.txtValorInventario.TabIndex = 35
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(55, 243)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(135, 22)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Valor de EPPs:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(55, 279)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(213, 22)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Valor de Herramientas:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(55, 315)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(184, 22)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Valor de Materiales:"
        '
        'txtValorEPP
        '
        Me.txtValorEPP.Enabled = False
        Me.txtValorEPP.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorEPP.ForeColor = System.Drawing.Color.Black
        Me.txtValorEPP.Location = New System.Drawing.Point(274, 240)
        Me.txtValorEPP.Name = "txtValorEPP"
        Me.txtValorEPP.Size = New System.Drawing.Size(137, 30)
        Me.txtValorEPP.TabIndex = 39
        '
        'txtValorHerramienta
        '
        Me.txtValorHerramienta.Enabled = False
        Me.txtValorHerramienta.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorHerramienta.ForeColor = System.Drawing.Color.Black
        Me.txtValorHerramienta.Location = New System.Drawing.Point(274, 276)
        Me.txtValorHerramienta.Name = "txtValorHerramienta"
        Me.txtValorHerramienta.Size = New System.Drawing.Size(137, 30)
        Me.txtValorHerramienta.TabIndex = 40
        '
        'txtValorMaterial
        '
        Me.txtValorMaterial.Enabled = False
        Me.txtValorMaterial.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorMaterial.ForeColor = System.Drawing.Color.Black
        Me.txtValorMaterial.Location = New System.Drawing.Point(274, 312)
        Me.txtValorMaterial.Name = "txtValorMaterial"
        Me.txtValorMaterial.Size = New System.Drawing.Size(137, 30)
        Me.txtValorMaterial.TabIndex = 41
        '
        'chartDashboard
        '
        ChartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent
        ChartArea3.AxisY.LineColor = System.Drawing.Color.Transparent
        ChartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray
        ChartArea3.Name = "ChartArea1"
        Me.chartDashboard.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.chartDashboard.Legends.Add(Legend3)
        Me.chartDashboard.Location = New System.Drawing.Point(23, 476)
        Me.chartDashboard.Name = "chartDashboard"
        Series3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        Series3.BackSecondaryColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Series3.BorderColor = System.Drawing.Color.Black
        Series3.BorderWidth = 2
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
        Series3.Legend = "Legend1"
        Series3.LegendText = "Valor De Inventario"
        Series3.MarkerBorderColor = System.Drawing.Color.Black
        Series3.MarkerSize = 8
        Series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle
        Series3.Name = "Series1"
        Me.chartDashboard.Series.Add(Series3)
        Me.chartDashboard.Size = New System.Drawing.Size(424, 467)
        Me.chartDashboard.TabIndex = 42
        Me.chartDashboard.Text = "Chart1"
        '
        'cboPeriodo
        '
        Me.cboPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPeriodo.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPeriodo.ForeColor = System.Drawing.Color.Black
        Me.cboPeriodo.FormattingEnabled = True
        Me.cboPeriodo.Location = New System.Drawing.Point(293, 538)
        Me.cboPeriodo.Name = "cboPeriodo"
        Me.cboPeriodo.Size = New System.Drawing.Size(118, 30)
        Me.cboPeriodo.TabIndex = 43
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(160, 373)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(149, 22)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "Generar Backup"
        '
        'btnBackup
        '
        Me.btnBackup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackup.Image = Global.ProyectoGestión.My.Resources.Resources.backup
        Me.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBackup.Location = New System.Drawing.Point(180, 404)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(92, 38)
        Me.btnBackup.TabIndex = 45
        Me.btnBackup.Text = "Backup"
        Me.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBackup.UseVisualStyleBackColor = True
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), System.Drawing.Image)
        Me.LogoPictureBox.Location = New System.Drawing.Point(12, 12)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(134, 155)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoPictureBox.TabIndex = 33
        Me.LogoPictureBox.TabStop = False
        '
        'btnPDF
        '
        Me.btnPDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPDF.Image = Global.ProyectoGestión.My.Resources.Resources._8664943_file_pdf_document_icon
        Me.btnPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPDF.Location = New System.Drawing.Point(358, 90)
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Size = New System.Drawing.Size(70, 30)
        Me.btnPDF.TabIndex = 10
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPDF.UseVisualStyleBackColor = True
        '
        'btnExcel
        '
        Me.btnExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExcel.Image = Global.ProyectoGestión.My.Resources.Resources._9023622_microsoft_excel_logo_fill_icon
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExcel.Location = New System.Drawing.Point(274, 90)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(78, 30)
        Me.btnExcel.TabIndex = 7
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'FrmReporte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1900, 965)
        Me.Controls.Add(Me.btnBackup)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboPeriodo)
        Me.Controls.Add(Me.chartDashboard)
        Me.Controls.Add(Me.txtValorMaterial)
        Me.Controls.Add(Me.txtValorHerramienta)
        Me.Controls.Add(Me.txtValorEPP)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtValorInventario)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LogoPictureBox)
        Me.Controls.Add(Me.cboFiltro)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnPDF)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.dtvReporte)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmReporte"
        Me.Text = "FrmReporte"
        CType(Me.dtvReporte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartDashboard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dtvReporte As DataGridView
    Friend WithEvents btnPDF As Button
    Friend WithEvents btnExcel As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cboFiltro As ComboBox
    Friend WithEvents LogoPictureBox As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtValorInventario As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtValorEPP As TextBox
    Friend WithEvents txtValorHerramienta As TextBox
    Friend WithEvents txtValorMaterial As TextBox
    Friend WithEvents chartDashboard As DataVisualization.Charting.Chart
    Friend WithEvents cboPeriodo As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btnBackup As Button
End Class
