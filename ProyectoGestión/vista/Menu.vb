Public Class Menu

    Private Sub FrmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuración inicial del formulario MDI
        Me.IsMdiContainer = True

        ' 1. Le quitamos a Windows el control de centrar la ventana
        Me.StartPosition = FormStartPosition.Manual

        ' 2. Lo pegamos exactamente en la esquina superior izquierda del monitor
        Me.Location = New Point(0, 0)

        ' 3. Obtenemos el tamaño exacto del monitor actual (restando la barra de tareas)
        Dim resolucionMonitor As Size = Screen.PrimaryScreen.WorkingArea.Size

        ' 4. Forzamos al formulario a tomar esas dimensiones exactas para que se cuadre solo
        Me.Size = resolucionMonitor
    End Sub

    ' 🌟 ESTE ES EL NUEVO MÉTODO QUE CORRIGE EL PROBLEMA 🌟
    ' Escucha activamente cada vez que la barra blanca se mueve
    Private Sub FrmMenu_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        ' Si la ventana intenta salir de la esquina superior izquierda (0,0)
        If Me.Location.X <> 0 OrElse Me.Location.Y <> 0 Then
            ' La forzamos magnéticamente a regresar de inmediato al origen
            Me.Location = New Point(0, 0)
        End If
    End Sub

    Private Sub ProductoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductoToolStripMenuItem.Click
        MenuController.AbrirProducto(Me)
    End Sub

    Private Sub StockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockToolStripMenuItem.Click
        MenuController.AbrirStock(Me)
    End Sub

    Private Sub ReporteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReporteToolStripMenuItem.Click
        MenuController.AbrirReporte(Me)
    End Sub

    Private Sub ProyectoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProyectoToolStripMenuItem.Click
        MenuController.AbrirProyecto(Me)
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        MenuController.VolverALogin(Me)
    End Sub

    Private Sub FrmMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

End Class
