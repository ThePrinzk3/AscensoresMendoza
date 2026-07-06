<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProyecto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProyecto))
        Me.dtvProyecto = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNombreProyecto = New System.Windows.Forms.TextBox()
        Me.btnRegistrarProyecto = New System.Windows.Forms.Button()
        Me.btnEliminarProyecto = New System.Windows.Forms.Button()
        Me.btnActualizarProyecto = New System.Windows.Forms.Button()
        Me.btnLimpiarProyecto = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtCantidadAdicional = New System.Windows.Forms.TextBox()
        Me.txtCostoAdicional = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnBuscarAdicional = New System.Windows.Forms.Button()
        Me.btnRegistrarAdicional = New System.Windows.Forms.Button()
        Me.btnEliminarAdicional = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnActualizarAdicional = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnLimpiarAdicional = New System.Windows.Forms.Button()
        Me.txtDescripcionAdicional = New System.Windows.Forms.TextBox()
        Me.txtNombreAdicional = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCodigoAdicional = New System.Windows.Forms.TextBox()
        Me.txtCodeProyecto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnPausa = New System.Windows.Forms.Button()
        Me.btnReanudar = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnBuscarProyecto = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtvAdicional = New System.Windows.Forms.DataGridView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtValorProyecto = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnPDFProyecto = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnAjustarStockProyecto = New System.Windows.Forms.Button()
        CType(Me.dtvProyecto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dtvAdicional, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtvProyecto
        '
        Me.dtvProyecto.BackgroundColor = System.Drawing.Color.White
        Me.dtvProyecto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtvProyecto.Location = New System.Drawing.Point(29, 62)
        Me.dtvProyecto.Name = "dtvProyecto"
        Me.dtvProyecto.Size = New System.Drawing.Size(395, 598)
        Me.dtvProyecto.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(22, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 22)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Nombre:"
        '
        'txtNombreProyecto
        '
        Me.txtNombreProyecto.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreProyecto.ForeColor = System.Drawing.Color.Black
        Me.txtNombreProyecto.Location = New System.Drawing.Point(114, 36)
        Me.txtNombreProyecto.Name = "txtNombreProyecto"
        Me.txtNombreProyecto.Size = New System.Drawing.Size(205, 30)
        Me.txtNombreProyecto.TabIndex = 7
        '
        'btnRegistrarProyecto
        '
        Me.btnRegistrarProyecto.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRegistrarProyecto.ForeColor = System.Drawing.Color.Black
        Me.btnRegistrarProyecto.Image = Global.ProyectoGestión.My.Resources.Resources._8201360_add_plus_new_ui_icon__2_1
        Me.btnRegistrarProyecto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRegistrarProyecto.Location = New System.Drawing.Point(333, 32)
        Me.btnRegistrarProyecto.Name = "btnRegistrarProyecto"
        Me.btnRegistrarProyecto.Size = New System.Drawing.Size(130, 36)
        Me.btnRegistrarProyecto.TabIndex = 15
        Me.btnRegistrarProyecto.Text = "Registrar"
        Me.btnRegistrarProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRegistrarProyecto.UseVisualStyleBackColor = True
        '
        'btnEliminarProyecto
        '
        Me.btnEliminarProyecto.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarProyecto.ForeColor = System.Drawing.Color.Black
        Me.btnEliminarProyecto.Image = CType(resources.GetObject("btnEliminarProyecto.Image"), System.Drawing.Image)
        Me.btnEliminarProyecto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarProyecto.Location = New System.Drawing.Point(469, 32)
        Me.btnEliminarProyecto.Name = "btnEliminarProyecto"
        Me.btnEliminarProyecto.Size = New System.Drawing.Size(115, 36)
        Me.btnEliminarProyecto.TabIndex = 18
        Me.btnEliminarProyecto.Text = "Eliminar"
        Me.btnEliminarProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarProyecto.UseVisualStyleBackColor = True
        '
        'btnActualizarProyecto
        '
        Me.btnActualizarProyecto.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnActualizarProyecto.ForeColor = System.Drawing.Color.Black
        Me.btnActualizarProyecto.Image = CType(resources.GetObject("btnActualizarProyecto.Image"), System.Drawing.Image)
        Me.btnActualizarProyecto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnActualizarProyecto.Location = New System.Drawing.Point(590, 32)
        Me.btnActualizarProyecto.Name = "btnActualizarProyecto"
        Me.btnActualizarProyecto.Size = New System.Drawing.Size(130, 36)
        Me.btnActualizarProyecto.TabIndex = 17
        Me.btnActualizarProyecto.Text = "Actualizar"
        Me.btnActualizarProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnActualizarProyecto.UseVisualStyleBackColor = True
        '
        'btnLimpiarProyecto
        '
        Me.btnLimpiarProyecto.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiarProyecto.ForeColor = System.Drawing.Color.Black
        Me.btnLimpiarProyecto.Image = CType(resources.GetObject("btnLimpiarProyecto.Image"), System.Drawing.Image)
        Me.btnLimpiarProyecto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLimpiarProyecto.Location = New System.Drawing.Point(726, 32)
        Me.btnLimpiarProyecto.Name = "btnLimpiarProyecto"
        Me.btnLimpiarProyecto.Size = New System.Drawing.Size(111, 36)
        Me.btnLimpiarProyecto.TabIndex = 16
        Me.btnLimpiarProyecto.Text = "Limpiar"
        Me.btnLimpiarProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLimpiarProyecto.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAjustarStockProyecto)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtCantidadAdicional)
        Me.GroupBox1.Controls.Add(Me.txtCostoAdicional)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.btnBuscarAdicional)
        Me.GroupBox1.Controls.Add(Me.btnRegistrarAdicional)
        Me.GroupBox1.Controls.Add(Me.btnEliminarAdicional)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.btnActualizarAdicional)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnLimpiarAdicional)
        Me.GroupBox1.Controls.Add(Me.txtDescripcionAdicional)
        Me.GroupBox1.Controls.Add(Me.txtNombreAdicional)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtCodigoAdicional)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(523, 176)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(795, 184)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Productos adicionales"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(475, 80)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(92, 22)
        Me.Label14.TabIndex = 33
        Me.Label14.Text = "Cantidad:"
        '
        'txtCantidadAdicional
        '
        Me.txtCantidadAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadAdicional.ForeColor = System.Drawing.Color.Black
        Me.txtCantidadAdicional.Location = New System.Drawing.Point(573, 77)
        Me.txtCantidadAdicional.Name = "txtCantidadAdicional"
        Me.txtCantidadAdicional.Size = New System.Drawing.Size(102, 30)
        Me.txtCantidadAdicional.TabIndex = 32
        '
        'txtCostoAdicional
        '
        Me.txtCostoAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostoAdicional.ForeColor = System.Drawing.Color.Black
        Me.txtCostoAdicional.Location = New System.Drawing.Point(665, 33)
        Me.txtCostoAdicional.Name = "txtCostoAdicional"
        Me.txtCostoAdicional.Size = New System.Drawing.Size(102, 30)
        Me.txtCostoAdicional.TabIndex = 31
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(595, 36)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(64, 22)
        Me.Label13.TabIndex = 30
        Me.Label13.Text = "Costo:"
        '
        'btnBuscarAdicional
        '
        Me.btnBuscarAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarAdicional.ForeColor = System.Drawing.Color.Black
        Me.btnBuscarAdicional.Image = CType(resources.GetObject("btnBuscarAdicional.Image"), System.Drawing.Image)
        Me.btnBuscarAdicional.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBuscarAdicional.Location = New System.Drawing.Point(419, 126)
        Me.btnBuscarAdicional.Name = "btnBuscarAdicional"
        Me.btnBuscarAdicional.Size = New System.Drawing.Size(99, 36)
        Me.btnBuscarAdicional.TabIndex = 29
        Me.btnBuscarAdicional.Text = "Buscar"
        Me.btnBuscarAdicional.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBuscarAdicional.UseVisualStyleBackColor = True
        '
        'btnRegistrarAdicional
        '
        Me.btnRegistrarAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRegistrarAdicional.ForeColor = System.Drawing.Color.Black
        Me.btnRegistrarAdicional.Image = Global.ProyectoGestión.My.Resources.Resources._8201360_add_plus_new_ui_icon__2_1
        Me.btnRegistrarAdicional.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRegistrarAdicional.Location = New System.Drawing.Point(26, 126)
        Me.btnRegistrarAdicional.Name = "btnRegistrarAdicional"
        Me.btnRegistrarAdicional.Size = New System.Drawing.Size(130, 36)
        Me.btnRegistrarAdicional.TabIndex = 24
        Me.btnRegistrarAdicional.Text = "Registrar"
        Me.btnRegistrarAdicional.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRegistrarAdicional.UseVisualStyleBackColor = True
        '
        'btnEliminarAdicional
        '
        Me.btnEliminarAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarAdicional.ForeColor = System.Drawing.Color.Black
        Me.btnEliminarAdicional.Image = CType(resources.GetObject("btnEliminarAdicional.Image"), System.Drawing.Image)
        Me.btnEliminarAdicional.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminarAdicional.Location = New System.Drawing.Point(162, 126)
        Me.btnEliminarAdicional.Name = "btnEliminarAdicional"
        Me.btnEliminarAdicional.Size = New System.Drawing.Size(115, 36)
        Me.btnEliminarAdicional.TabIndex = 27
        Me.btnEliminarAdicional.Text = "Eliminar"
        Me.btnEliminarAdicional.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarAdicional.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(22, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 22)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Descripción:"
        '
        'btnActualizarAdicional
        '
        Me.btnActualizarAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnActualizarAdicional.ForeColor = System.Drawing.Color.Black
        Me.btnActualizarAdicional.Image = CType(resources.GetObject("btnActualizarAdicional.Image"), System.Drawing.Image)
        Me.btnActualizarAdicional.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnActualizarAdicional.Location = New System.Drawing.Point(283, 126)
        Me.btnActualizarAdicional.Name = "btnActualizarAdicional"
        Me.btnActualizarAdicional.Size = New System.Drawing.Size(130, 36)
        Me.btnActualizarAdicional.TabIndex = 26
        Me.btnActualizarAdicional.Text = "Actualizar"
        Me.btnActualizarAdicional.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnActualizarAdicional.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(283, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 22)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Nombre:"
        '
        'btnLimpiarAdicional
        '
        Me.btnLimpiarAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiarAdicional.ForeColor = System.Drawing.Color.Black
        Me.btnLimpiarAdicional.Image = CType(resources.GetObject("btnLimpiarAdicional.Image"), System.Drawing.Image)
        Me.btnLimpiarAdicional.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLimpiarAdicional.Location = New System.Drawing.Point(524, 126)
        Me.btnLimpiarAdicional.Name = "btnLimpiarAdicional"
        Me.btnLimpiarAdicional.Size = New System.Drawing.Size(111, 36)
        Me.btnLimpiarAdicional.TabIndex = 25
        Me.btnLimpiarAdicional.Text = "Limpiar"
        Me.btnLimpiarAdicional.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLimpiarAdicional.UseVisualStyleBackColor = True
        '
        'txtDescripcionAdicional
        '
        Me.txtDescripcionAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcionAdicional.ForeColor = System.Drawing.Color.Black
        Me.txtDescripcionAdicional.Location = New System.Drawing.Point(146, 80)
        Me.txtDescripcionAdicional.Name = "txtDescripcionAdicional"
        Me.txtDescripcionAdicional.Size = New System.Drawing.Size(306, 30)
        Me.txtDescripcionAdicional.TabIndex = 26
        '
        'txtNombreAdicional
        '
        Me.txtNombreAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreAdicional.ForeColor = System.Drawing.Color.Black
        Me.txtNombreAdicional.Location = New System.Drawing.Point(374, 33)
        Me.txtNombreAdicional.Name = "txtNombreAdicional"
        Me.txtNombreAdicional.Size = New System.Drawing.Size(203, 30)
        Me.txtNombreAdicional.TabIndex = 26
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(22, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 22)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Código:"
        '
        'txtCodigoAdicional
        '
        Me.txtCodigoAdicional.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigoAdicional.ForeColor = System.Drawing.Color.Black
        Me.txtCodigoAdicional.Location = New System.Drawing.Point(113, 36)
        Me.txtCodigoAdicional.Name = "txtCodigoAdicional"
        Me.txtCodigoAdicional.Size = New System.Drawing.Size(159, 30)
        Me.txtCodigoAdicional.TabIndex = 24
        '
        'txtCodeProyecto
        '
        Me.txtCodeProyecto.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodeProyecto.ForeColor = System.Drawing.Color.Black
        Me.txtCodeProyecto.Location = New System.Drawing.Point(114, 80)
        Me.txtCodeProyecto.Name = "txtCodeProyecto"
        Me.txtCodeProyecto.Size = New System.Drawing.Size(159, 30)
        Me.txtCodeProyecto.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(22, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 22)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Código:"
        '
        'btnPausa
        '
        Me.btnPausa.BackColor = System.Drawing.Color.Red
        Me.btnPausa.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPausa.ForeColor = System.Drawing.Color.Black
        Me.btnPausa.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPausa.Location = New System.Drawing.Point(447, 76)
        Me.btnPausa.Name = "btnPausa"
        Me.btnPausa.Size = New System.Drawing.Size(84, 36)
        Me.btnPausa.TabIndex = 22
        Me.btnPausa.Text = "Pausa"
        Me.btnPausa.UseVisualStyleBackColor = False
        '
        'btnReanudar
        '
        Me.btnReanudar.BackColor = System.Drawing.Color.YellowGreen
        Me.btnReanudar.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReanudar.ForeColor = System.Drawing.Color.Black
        Me.btnReanudar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReanudar.Location = New System.Drawing.Point(537, 76)
        Me.btnReanudar.Name = "btnReanudar"
        Me.btnReanudar.Size = New System.Drawing.Size(108, 36)
        Me.btnReanudar.TabIndex = 23
        Me.btnReanudar.Text = "Reanudar"
        Me.btnReanudar.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnBuscarProyecto)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.btnReanudar)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.btnPausa)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtNombreProyecto)
        Me.GroupBox2.Controls.Add(Me.txtCodeProyecto)
        Me.GroupBox2.Controls.Add(Me.btnLimpiarProyecto)
        Me.GroupBox2.Controls.Add(Me.btnActualizarProyecto)
        Me.GroupBox2.Controls.Add(Me.btnRegistrarProyecto)
        Me.GroupBox2.Controls.Add(Me.btnEliminarProyecto)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(453, 26)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(865, 133)
        Me.GroupBox2.TabIndex = 28
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Proyecto"
        '
        'btnBuscarProyecto
        '
        Me.btnBuscarProyecto.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarProyecto.ForeColor = System.Drawing.Color.Black
        Me.btnBuscarProyecto.Image = CType(resources.GetObject("btnBuscarProyecto.Image"), System.Drawing.Image)
        Me.btnBuscarProyecto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBuscarProyecto.Location = New System.Drawing.Point(333, 76)
        Me.btnBuscarProyecto.Name = "btnBuscarProyecto"
        Me.btnBuscarProyecto.Size = New System.Drawing.Size(99, 36)
        Me.btnBuscarProyecto.TabIndex = 28
        Me.btnBuscarProyecto.Text = "Buscar"
        Me.btnBuscarProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBuscarProyecto.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(22, 82)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(0, 22)
        Me.Label7.TabIndex = 27
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(22, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(0, 22)
        Me.Label8.TabIndex = 25
        '
        'dtvAdicional
        '
        Me.dtvAdicional.BackgroundColor = System.Drawing.Color.White
        Me.dtvAdicional.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtvAdicional.Location = New System.Drawing.Point(453, 398)
        Me.dtvAdicional.Name = "dtvAdicional"
        Me.dtvAdicional.Size = New System.Drawing.Size(865, 216)
        Me.dtvAdicional.TabIndex = 29
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Cambria", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(23, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(142, 32)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "Proyectos:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(447, 363)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(305, 32)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Productos del proyecto:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cambria", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(447, 628)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(152, 32)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "Costo total:"
        '
        'txtValorProyecto
        '
        Me.txtValorProyecto.Enabled = False
        Me.txtValorProyecto.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorProyecto.ForeColor = System.Drawing.Color.Black
        Me.txtValorProyecto.Location = New System.Drawing.Point(605, 630)
        Me.txtValorProyecto.Name = "txtValorProyecto"
        Me.txtValorProyecto.Size = New System.Drawing.Size(137, 30)
        Me.txtValorProyecto.TabIndex = 44
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(892, 636)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(0, 22)
        Me.Label11.TabIndex = 47
        '
        'btnPDFProyecto
        '
        Me.btnPDFProyecto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPDFProyecto.Image = Global.ProyectoGestión.My.Resources.Resources._8664943_file_pdf_document_icon
        Me.btnPDFProyecto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPDFProyecto.Location = New System.Drawing.Point(1248, 631)
        Me.btnPDFProyecto.Name = "btnPDFProyecto"
        Me.btnPDFProyecto.Size = New System.Drawing.Size(70, 30)
        Me.btnPDFProyecto.TabIndex = 46
        Me.btnPDFProyecto.Text = "PDF"
        Me.btnPDFProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPDFProyecto.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Cambria", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(770, 628)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(101, 32)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "Actual:"
        '
        'btnAjustarStockProyecto
        '
        Me.btnAjustarStockProyecto.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAjustarStockProyecto.ForeColor = System.Drawing.Color.Black
        Me.btnAjustarStockProyecto.Image = CType(resources.GetObject("btnAjustarStockProyecto.Image"), System.Drawing.Image)
        Me.btnAjustarStockProyecto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAjustarStockProyecto.Location = New System.Drawing.Point(641, 126)
        Me.btnAjustarStockProyecto.Name = "btnAjustarStockProyecto"
        Me.btnAjustarStockProyecto.Size = New System.Drawing.Size(115, 36)
        Me.btnAjustarStockProyecto.TabIndex = 34
        Me.btnAjustarStockProyecto.Text = "Corregir"
        Me.btnAjustarStockProyecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAjustarStockProyecto.UseVisualStyleBackColor = True
        '
        'FrmProyecto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1340, 691)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btnPDFProyecto)
        Me.Controls.Add(Me.txtValorProyecto)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.dtvAdicional)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dtvProyecto)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmProyecto"
        Me.Text = "Form1"
        CType(Me.dtvProyecto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dtvAdicional, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Friend WithEvents dtvProyecto As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNombreProyecto As TextBox
    Friend WithEvents btnRegistrarProyecto As Button
    Friend WithEvents btnEliminarProyecto As Button
    Friend WithEvents btnActualizarProyecto As Button
    Friend WithEvents btnLimpiarProyecto As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtCodeProyecto As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnPausa As Button
    Friend WithEvents btnReanudar As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtDescripcionAdicional As TextBox
    Friend WithEvents txtNombreAdicional As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtCodigoAdicional As TextBox
    Friend WithEvents btnRegistrarAdicional As Button
    Friend WithEvents btnEliminarAdicional As Button
    Friend WithEvents btnActualizarAdicional As Button
    Friend WithEvents btnLimpiarAdicional As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents dtvAdicional As DataGridView
    Friend WithEvents Label9 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtValorProyecto As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents btnPDFProyecto As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents btnBuscarAdicional As Button
    Friend WithEvents btnBuscarProyecto As Button

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Friend WithEvents txtCostoAdicional As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents txtCantidadAdicional As TextBox
    Friend WithEvents btnAjustarStockProyecto As Button
End Class
