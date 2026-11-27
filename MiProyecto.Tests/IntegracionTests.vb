Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class IntegracionTests

    Private Const PROY_A As String = "PRUEBA_INT_A"
    Private Const PROY_B As String = "PRUEBA_INT_B"
    Private Const PROD_INT As String = "M990"

    <TestInitialize>
    Public Sub Iniciar()
        ' Limpieza previa de entorno de integración
        BorrarProyecto(PROY_A)
        BorrarProyecto(PROY_B)
        BorrarProducto(PROD_INT)
    End Sub

    <TestCleanup>
    Public Sub Terminar()
        ' Limpieza posterior
        BorrarProyecto(PROY_A)
        BorrarProyecto(PROY_B)
        BorrarProducto(PROD_INT)
    End Sub

    ' CP-I01: Integración Stock - Proyecto
    <TestMethod>
    Public Sub CP_I01_SalidaAsociadaAProyecto_ActualizaStockYCostoProyecto()
        Adaptador.ActivarProyecto("P100", PROY_A)
        FijarCosto(MAT_COD, 15D)
        FijarStock(MAT_COD, 50)

        ' Actuar: Registrar salida de 10 unidades
        Dim exitoSalida As Boolean = Adaptador.AsignarSalidaAProyecto(MAT_COD, 10, PERSONAL)

        ' Validar: El stock disminuye a 40 y el costo del proyecto incrementa en 150 (10 * 15D)
        Assert.IsTrue(exitoSalida, "La salida asociada al proyecto debe procesarse correctamente")
        Assert.AreEqual(40, LeerStock(MAT_COD), "El stock en BD debe actualizarse a 40")
        Assert.IsTrue(Adaptador.CostoTotal(PROY_A) >= 150D, "El costo acumulado del proyecto debe reflejar los S/. 150.00")
    End Sub

    ' CP-I02: Integración Producto - Categoría Especializada (Persistencia Relacional)
    <TestMethod>
    Public Sub CP_I02_RegistroProductoEspecializado_PersisteEnTablaPadreEHija()
        ' Actuar: Registrar un material con su medida
        Dim res As Boolean = Adaptador.RegistrarProducto("Materiales", PROD_INT, "PLACA DE ACERO", "PRUEBA INT", 45.5D, "", "", "", "1/4 PULGADA")

        ' Validar: El producto debe existir en la tabla general Producto y en la tabla especifica Materiales
        Assert.IsTrue(res, "El producto debe registrarse")
        Assert.AreEqual(1, ContarProductos(PROD_INT), "El registro debe existir en la tabla Producto")

        Dim countHija As Integer = Convert.ToInt32(BaseDatos.Escalar(
            "SELECT COUNT(*) FROM Materiales m INNER JOIN Producto p ON m.producto_id = p.id_producto WHERE p.codigo = @c",
            New SqlClient.SqlParameter("@c", PROD_INT)))
        Assert.AreEqual(1, countHija, "La relación con la tabla hija Materiales debe crearse correctamente")
    End Sub

    ' CP-I03: Integración Proyecto - Adicionales de Costo
    <TestMethod>
    Public Sub CP_I03_AgregarAdicionalAProyecto_SumaAlCostoTotalConsolidado()
        Adaptador.ActivarProyecto("P101", PROY_A)

        ' Actuar: Agregar dos adicionales al proyecto activo
        Adaptador.AgregarAdicional("A01", "FLETE DE TRANSPORTE", 120D, "SERVICIO", 1)
        Adaptador.AgregarAdicional("A02", "HORAS EXTRA TÉCNICO", 80D, "MANO DE OBRA", 2)

        ' Validar: Costo total debe ser al menos S/. 280.00 (120 + 80*2)
        Dim costoTotal As Decimal = Adaptador.CostoTotal(PROY_A)
        Assert.AreEqual(280D, Math.Round(costoTotal, 2), "El costo consolidado del proyecto debe sumar los adicionales")
    End Sub

    ' CP-I04: Integración Stock - Sistema de Alertas
    <TestMethod>
    Public Sub CP_I04_FlujoMovimientosStock_DisparaAlertaAutomaticamente()
        ' Fijar stock inicial de EPP en 10 (sin alerta)
        FijarStock(EPP_COD, 10)
        Assert.IsFalse(Adaptador.AlertaActiva(EPP_NOM), "No debe haber alerta activa con stock 10")

        ' Actuar: Realizar salida de 6 unidades dejando el stock en 4 (debajo del umbral de 5)
        Adaptador.Salida(EPP_COD, 6, PERSONAL)

        ' Validar: El módulo de alertas detecta el cambio de estado en BD
        Assert.IsTrue(Adaptador.AlertaActiva(EPP_NOM), "La alerta debe activarse automáticamente al quedar stock < 5")
    End Sub

    ' CP-I05: Integración Autenticación - Trazabilidad de Movimientos
    <TestMethod>
    Public Sub CP_I05_MovimientoStock_RegistraTrazabilidadDeUsuario()
        Adaptador.Login(USUARIO_OK, CLAVE_OK)
        FijarStock(MAT_COD, 30)

        ' Actuar: Registrar entrada de stock
        Adaptador.Entrada(MAT_COD, 5, PERSONAL)

        ' Validar: El último movimiento guardó el usuario_id correspondiente en BD
        Dim usuarioIdReg As Integer = Convert.ToInt32(BaseDatos.Escalar(
            "SELECT TOP 1 sm.usuario_id FROM StockMovimiento sm INNER JOIN Producto p ON sm.producto_id = p.id_producto WHERE p.codigo = @c ORDER BY sm.id_movimiento DESC",
            New SqlClient.SqlParameter("@c", MAT_COD)))

        Assert.IsTrue(usuarioIdReg > 0, "El movimiento de stock debe estar vinculado a un ID de usuario válido")
    End Sub

    ' CP-I06: Integración Conmutación de Proyectos - Asignación de Recursos
    <TestMethod>
    Public Sub CP_I06_ConmutacionDeProyectos_RedirigeSalidasAlProyectoActivo()
        ' Encender Proyecto A y luego Proyecto B
        Adaptador.ActivarProyecto("P100", PROY_A)
        Adaptador.ActivarProyecto("P200", PROY_B)

        FijarStock(MAT_COD, 50)

        ' Actuar: Asignar salida de stock
        Adaptador.AsignarSalidaAProyecto(MAT_COD, 5, PERSONAL)

        ' Validar: La salida debe quedar asociada al Proyecto B (activo) y no al Proyecto A
        Dim proyectoAsociado As String = BaseDatos.Escalar(
            "SELECT TOP 1 pr.nombre FROM StockMovimiento sm INNER JOIN Proyecto pr ON sm.proyecto_id = pr.id_proyecto ORDER BY sm.id_movimiento DESC").ToString()

        Assert.AreEqual(PROY_B, proyectoAsociado, "La salida de stock debe asociarse automáticamente al proyecto activo en ese instante")
    End Sub

    ' CP-I07: Integración Reporte de Inventario - Valorización Dinámica
    <TestMethod>
    Public Sub CP_I07_MovimientosMultiples_ActualizanValorizationEnReporte()
        Dim valorInicial As Decimal = Adaptador.ValorInventario("Materiales")

        FijarCosto(MAT_COD, 10D)
        FijarStock(MAT_COD, 20)

        ' Actuar: Registrar entrada de 10 unidades adicionales
        Adaptador.Entrada(MAT_COD, 10, PERSONAL)

        ' Validar: El valor reportado se incrementó tras el movimiento
        Dim valorFinal As Decimal = Adaptador.ValorInventario("Materiales")
        Assert.IsTrue(valorFinal > valorInicial, "El reporte de inventario debe reflejar el incremento de stock")
    End Sub

End Class