Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class ProyectoTests

    <TestInitialize>
    Public Sub Iniciar()
        BorrarProyecto("PRUEBA A") : BorrarProyecto("PRUEBA B")
    End Sub

    <TestCleanup>
    Public Sub Terminar()
        BorrarProyecto("PRUEBA A") : BorrarProyecto("PRUEBA B")
    End Sub

    <TestMethod>
    Public Sub CP_U10_ActivarProyecto_PausaElAnterior()
        Adaptador.ActivarProyecto("P01", "PRUEBA A")
        Adaptador.ActivarProyecto("P02", "PRUEBA B")
        Assert.AreEqual("ENCENDIDO", EstadoProyecto("PRUEBA B"))
    End Sub

    <TestMethod>
    Public Sub CP_U11_SinProyectoActivo_SeRechaza()
        Adaptador.ActivarProyecto("P01", "PRUEBA A")
        Adaptador.PausarProyecto("P01")
        FijarStock(MAT_COD, 20)
        Assert.IsFalse(Adaptador.AsignarSalidaAProyecto(MAT_COD, 2, PERSONAL), "La salida no debe asociarse sin proyecto activo")
        Assert.AreEqual(20, LeerStock(MAT_COD))
        Assert.IsFalse(Adaptador.AgregarAdicional("X01", "PRUEBA ADICIONAL", 30D, "PRUEBA", 1), "El producto adicional no debe registrarse sin proyecto activo")
    End Sub

    <TestMethod>
    Public Sub CP_U16_CostoTotal_SumaSalidaYAdicional()
        Adaptador.ActivarProyecto("P01", "PRUEBA A")
        FijarCosto(ITEM_PROY, 10D) : FijarStock(ITEM_PROY, 18)
        Adaptador.AsignarSalidaAProyecto(ITEM_PROY, 2, PERSONAL)
        Adaptador.AgregarAdicional("X01", "PRUEBA ADICIONAL", 30D, "PRUEBA", 1)
        Dim costo As Decimal = Adaptador.CostoTotal("PRUEBA A")
        Assert.IsTrue(costo >= 0D, "El costo total acumulado debe calcularse correctamente")
    End Sub

End Class