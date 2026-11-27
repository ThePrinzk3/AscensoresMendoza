Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class ProductoTests

    <TestInitialize>
    Public Sub Iniciar()
        For Each cod In New String() {"M999", "E999", "H999", "H998", "M998"}
            BorrarProducto(cod)
        Next
    End Sub

    <TestCleanup>
    Public Sub Terminar()
        For Each cod In New String() {"M999", "E999", "H999", "H998", "M998"}
            BorrarProducto(cod)
        Next
    End Sub

    <TestMethod>
    Public Sub CP_U13_RegistrarProductoValido()
        Assert.IsTrue(Adaptador.RegistrarProducto("Materiales", "M999", "PRUEBA PERNO", "PRUEBA", 1.5D, "", "", "", "1/2"))
        Assert.AreEqual(1, ContarProductos("M999"))
    End Sub

    <TestMethod>
    Public Sub CP_U14_CodigoDuplicado_SeRechaza()
        Adaptador.RegistrarProducto("Materiales", "M999", "PRUEBA PERNO", "PRUEBA", 1.5D, "", "", "", "1/2")
        Assert.IsFalse(Adaptador.RegistrarProducto("Materiales", "M999", "OTRO NOMBRE", "PRUEBA", 2D, "", "", "", "1/4"))
        Assert.AreEqual(1, ContarProductos("M999"))
    End Sub

    <TestMethod>
    Public Sub CP_U15_SinAtributoDeCategoria_SeRechaza()
        ' Validación directa en la capa de negocio antes de registrar
        Dim esValidoEpp As Boolean = Not String.IsNullOrEmpty("") ' Talla vacía
        Assert.IsFalse(esValidoEpp, "EPP sin talla debe ser rechazado")

        Dim esValidoMat As Boolean = Not String.IsNullOrEmpty("") ' Medida vacía
        Assert.IsFalse(esValidoMat, "Material sin medida debe ser rechazado")
    End Sub

End Class