Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class LoginTests

    <TestMethod>
    Public Sub CP_U12_Login_SoloConCredencialesValidas()
        Assert.IsTrue(Adaptador.Login(USUARIO_OK, CLAVE_OK), "Credenciales válidas deben ingresar")
        Assert.IsFalse(Adaptador.Login(USUARIO_OK, CLAVE_OK & "x"), "Contraseña errónea debe rechazarse")
        Assert.IsFalse(Adaptador.Login("", ""), "Campos vacíos deben rechazarse")
    End Sub

End Class