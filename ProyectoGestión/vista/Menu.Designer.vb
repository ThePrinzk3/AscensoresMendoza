<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Menu
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Menu))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.InicioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReporteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProyectoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InicioToolStripMenuItem, Me.ProductoToolStripMenuItem, Me.StockToolStripMenuItem, Me.ReporteToolStripMenuItem, Me.ProyectoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1344, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'InicioToolStripMenuItem
        '
        Me.InicioToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.InicioToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalirToolStripMenuItem})
        Me.InicioToolStripMenuItem.Image = Global.ProyectoGestión.My.Resources.Resources._3643769_building_home_house_main_menu_icon
        Me.InicioToolStripMenuItem.Name = "InicioToolStripMenuItem"
        Me.InicioToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
        Me.InicioToolStripMenuItem.Text = "Inicio"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Image = Global.ProyectoGestión.My.Resources.Resources._1564506_close_exit_logout_power_icon__1_1
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SalirToolStripMenuItem.Text = "Cerrar Sesión"
        '
        'ProductoToolStripMenuItem
        '
        Me.ProductoToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ProductoToolStripMenuItem.Image = Global.ProyectoGestión.My.Resources.Resources._3018569_alteration_bag_data_product_settings_icon
        Me.ProductoToolStripMenuItem.Name = "ProductoToolStripMenuItem"
        Me.ProductoToolStripMenuItem.Size = New System.Drawing.Size(84, 20)
        Me.ProductoToolStripMenuItem.Text = "Producto"
        '
        'StockToolStripMenuItem
        '
        Me.StockToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.StockToolStripMenuItem.Image = Global.ProyectoGestión.My.Resources.Resources._7217723_forex_trade_chart_stock_diagram_icon
        Me.StockToolStripMenuItem.Name = "StockToolStripMenuItem"
        Me.StockToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
        Me.StockToolStripMenuItem.Text = "Stock"
        '
        'ReporteToolStripMenuItem
        '
        Me.ReporteToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ReporteToolStripMenuItem.Image = Global.ProyectoGestión.My.Resources.Resources._2931174_clipboard_copy_paste_analysis_report_icon
        Me.ReporteToolStripMenuItem.Name = "ReporteToolStripMenuItem"
        Me.ReporteToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.ReporteToolStripMenuItem.Text = "Reporte"
        '
        'ProyectoToolStripMenuItem
        '
        Me.ProyectoToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ProyectoToolStripMenuItem.Image = Global.ProyectoGestión.My.Resources.Resources.ingeniero
        Me.ProyectoToolStripMenuItem.Name = "ProyectoToolStripMenuItem"
        Me.ProyectoToolStripMenuItem.Size = New System.Drawing.Size(82, 20)
        Me.ProyectoToolStripMenuItem.Text = "Proyecto"
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameLabel.ForeColor = System.Drawing.Color.White
        Me.UsernameLabel.Location = New System.Drawing.Point(378, 1)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(1127, 23)
        Me.UsernameLabel.TabIndex = 2
        Me.UsernameLabel.Text = "Elevadores Montacarga E.I.R.L. 2025 - El Agustino"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Menu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1344, 721)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Menu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menú - Elevadores Montacarga"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents InicioToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProductoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StockToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReporteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UsernameLabel As Label
    Friend WithEvents ProyectoToolStripMenuItem As ToolStripMenuItem
End Class
