Public Class Menu

    Private Sub FrmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuración inicial del formulario MDI
        Me.IsMdiContainer = True
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

    Private Sub VistasToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        MenuController.AbrirVistas(Me)
    End Sub
End Class
