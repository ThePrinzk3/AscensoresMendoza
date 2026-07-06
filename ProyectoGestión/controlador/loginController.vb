Public Class LoginController

    ' Usuario autenticado (opcional, si quieres guardarlo en sesión)
    Public Shared Property UsuarioActual As Users

    ' ---------------------------------------------
    ' Maneja el intento de login desde el formulario
    ' ---------------------------------------------
    Public Shared Function IntentarLogin(nombreUsuario As String,
                                         contraseña As String,
                                         ByRef mensajeError As String) As Boolean

        ' Validaciones básicas
        If String.IsNullOrWhiteSpace(nombreUsuario) OrElse String.IsNullOrWhiteSpace(contraseña) Then
            mensajeError = "Usuario y contraseña son requeridos"
            Return False
        End If

        Try
            ' Verificar credenciales en el modelo
            If Users.VerificarCredenciales(nombreUsuario, contraseña) Then

                ' (Opcional) Cargar datos completos del usuario
                UsuarioActual = Users.ObtenerPorNombre(nombreUsuario)

                Return True
            Else
                mensajeError = "Credenciales incorrectas"
                Return False
            End If

        Catch ex As Exception
            mensajeError = "Error al conectar con la base de datos: " & ex.Message
            Return False
        End Try
    End Function
End Class
