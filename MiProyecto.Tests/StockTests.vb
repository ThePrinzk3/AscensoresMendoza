Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class StockTests

    <TestMethod>
    Public Sub CP_U01_SalidaNormal_DescuentaStock()
        FijarStock(MAT_COD, 20)
        Dim movimientos As Integer = ContarMovimientos(MAT_COD)
        Adaptador.Salida(MAT_COD, 5, PERSONAL)
        Assert.AreEqual(15, LeerStock(MAT_COD))
        Assert.AreEqual(movimientos + 1, ContarMovimientos(MAT_COD))
    End Sub

    <TestMethod>
    Public Sub CP_U02_SalidaIgualAlStock_DejaCero()
        FijarStock(MAT_COD, 20)
        Dim movimientos As Integer = ContarMovimientos(MAT_COD)
        Adaptador.Salida(MAT_COD, 20, PERSONAL)
        Assert.AreEqual(0, LeerStock(MAT_COD))
        Assert.AreEqual(movimientos + 1, ContarMovimientos(MAT_COD))
    End Sub

    <TestMethod>
    Public Sub CP_U03_SalidaMayorAlStock_SeRechaza()
        FijarStock(MAT_COD, 20)
        Dim movimientos As Integer = ContarMovimientos(MAT_COD)
        Adaptador.Salida(MAT_COD, 21, PERSONAL)
        Assert.AreEqual(20, LeerStock(MAT_COD))
        Assert.AreEqual(movimientos, ContarMovimientos(MAT_COD))
    End Sub

    <TestMethod>
    Public Sub CP_U04_CantidadCeroONegativa_SeRechaza()
        FijarStock(MAT_COD, 20)
        Dim movimientos As Integer = ContarMovimientos(MAT_COD)
        Adaptador.Salida(MAT_COD, 0, PERSONAL)
        Adaptador.Salida(MAT_COD, -3, PERSONAL)
        Adaptador.Entrada(MAT_COD, 0, PERSONAL)
        Adaptador.Entrada(MAT_COD, -3, PERSONAL)
        Assert.AreEqual(20, LeerStock(MAT_COD))
        Assert.AreEqual(movimientos, ContarMovimientos(MAT_COD))
    End Sub

    <TestMethod>
    Public Sub CP_U05_EntradaNormal_SumaStock()
        FijarStock(MAT_COD, 20)
        Dim movimientos As Integer = ContarMovimientos(MAT_COD)
        Adaptador.Entrada(MAT_COD, 10, PERSONAL)
        Assert.AreEqual(30, LeerStock(MAT_COD))
        Assert.AreEqual(movimientos + 1, ContarMovimientos(MAT_COD))
    End Sub

    Private Sub ProbarUmbral(codigo As String, nombre As String, enElMinimo As Integer)
        FijarStock(codigo, enElMinimo)
        Assert.IsFalse(Adaptador.AlertaActiva(nombre), "Con stock " & enElMinimo & " no debe haber alerta")
        FijarStock(codigo, enElMinimo - 1)
        Assert.IsTrue(Adaptador.AlertaActiva(nombre), "Con stock " & (enElMinimo - 1) & " debe haber alerta")
    End Sub

    <TestMethod>
    Public Sub CP_U06_AlertaEPP_MenorA5()
        ProbarUmbral(EPP_COD, EPP_NOM, 5)
    End Sub

    <TestMethod>
    Public Sub CP_U07_AlertaHerramienta_MenorA10()
        ProbarUmbral(HERR_COD, HERR_NOM, 10)
    End Sub

    <TestMethod>
    Public Sub CP_U08_AlertaMaterial_MenorA30()
        ProbarUmbral(MAT_COD, MAT_NOM, 30)
    End Sub

End Class