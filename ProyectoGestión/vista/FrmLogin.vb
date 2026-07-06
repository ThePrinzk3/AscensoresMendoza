Public Class FrmLogin

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim nombreUsuario As String = txtUsuario.Text.Trim()
        Dim contraseña As String = txtContraseña.Text.Trim()
        Dim mensajeError As String = ""

        If LoginController.IntentarLogin(nombreUsuario, contraseña, mensajeError) Then
            ' Login correcto: abrir menú principal
            Dim menuForm As New Menu()
            menuForm.Show()
            Me.Hide()
        Else
            ' Mostrar mensaje de error
            MessageBox.Show(mensajeError, "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            txtUsuario.Clear()
            txtContraseña.Clear()
            txtUsuario.Focus()
        End If
    End Sub

    ' ----------------------------
    ' Botón Cancelar
    ' ----------------------------
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Application.Exit()
    End Sub

    ' ----------------------------
    ' Cerrar formulario = cerrar app
    ' ----------------------------
    Private Sub FrmLogin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Aquí podrías inicializar cosas visuales si quieres
    End Sub
End Class
