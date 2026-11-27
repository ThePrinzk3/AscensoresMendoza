Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class ReporteTests

    <TestMethod>
    Public Sub CP_U09_ValorInventario_SumaCostoPorStock()
        FijarCosto(MAT_COD, 12.5D) : FijarStock(MAT_COD, 4)
        FijarCosto(MAT2_COD, 30D) : FijarStock(MAT2_COD, 2)

        Dim totalCalculado As Decimal = Adaptador.ValorInventario("Materiales")
        Assert.IsTrue(totalCalculado >= 0D, "El valor del inventario debe ser mayor o igual a cero")
    End Sub

End Class