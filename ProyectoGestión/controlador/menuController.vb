Imports System.Windows.Forms
Imports System.Drawing

Public Class MenuController

    ' Cache de formularios MDI para reutilizarlos y no estar creando siempre nuevos
    Private Shared ReadOnly FormulariosCache As New Dictionary(Of Type, Form)

    ' --------------------------------------------------
    ' Cierra (oculta) todos los formularios hijos del MDI
    ' --------------------------------------------------
    Public Shared Sub CerrarFormulariosHijos(parentForm As Form)
        For Each frm As Form In parentForm.MdiChildren
            frm.Hide() ' Hide en lugar de Close() para ser más rápido y poder reutilizar
        Next
    End Sub

    ' --------------------------------------------------
    ' Método genérico para abrir un formulario MDI
    ' --------------------------------------------------
    Public Shared Sub AbrirFormularioMDI(Of T As {New, Form})(parentForm As Form)
        Dim tipo As Type = GetType(T)

        ' Si ya está en caché y visible, solo lo traemos al frente
        If FormulariosCache.ContainsKey(tipo) Then
            Dim formExistente As Form = FormulariosCache(tipo)
            If formExistente.Visible Then
                formExistente.BringToFront()
                Return
            End If
        End If

        ' Ocultar los demás formularios hijos
        CerrarFormulariosHijos(parentForm)

        Dim nuevoForm As Form

        ' Reutilizar si está en caché pero oculto
        If FormulariosCache.ContainsKey(tipo) Then
            nuevoForm = FormulariosCache(tipo)
        Else
            ' Crear una nueva instancia y configurarla como MDI Child
            nuevoForm = New T()
            nuevoForm.MdiParent = parentForm
            nuevoForm.StartPosition = FormStartPosition.Manual
            nuevoForm.Location = New Point(0, 0)
            FormulariosCache(tipo) = nuevoForm
        End If

        nuevoForm.Show()
        nuevoForm.BringToFront()
    End Sub

    ' --------------------------------------------------
    ' Métodos específicos para cada opción del menú
    ' --------------------------------------------------
    Public Shared Sub AbrirProducto(parentForm As Form)
        AbrirFormularioMDI(Of FrmProducto)(parentForm)
    End Sub

    Public Shared Sub AbrirStock(parentForm As Form)
        AbrirFormularioMDI(Of FrmStock)(parentForm)
    End Sub

    Public Shared Sub AbrirReporte(parentForm As Form)
        AbrirFormularioMDI(Of FrmReporte)(parentForm)
    End Sub

    Public Shared Sub AbrirProyecto(parentForm As Form)
        AbrirFormularioMDI(Of FrmProyecto)(parentForm)
    End Sub

    ' --------------------------------------------------
    ' Volver al login: limpiar caché y mostrar FrmLogin
    ' --------------------------------------------------
    Public Shared Sub VolverALogin(parentForm As Form)
        FormulariosCache.Clear()
        Dim loginForm As New FrmLogin()
        loginForm.Show()
        parentForm.Hide()
    End Sub

End Class
