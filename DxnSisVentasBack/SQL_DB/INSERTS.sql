-- ==============================================
-- INSERTS DE PRODUCTOS
-- ==============================================
call insertar_producto(@id, 'Arroz', 5.5, 100, 1, 'gramos', 'Comestible', 10);
call insertar_producto(@id, 'Azucar', 3.5, 100, 1, 'gramos', 'Comestible', 10);
call insertar_producto(@id, 'Aceite', 7.5, 100, 1, 'mililitros', 'Comestible', 10);
call insertar_producto(@id, 'Shampoo', 10.5, 100, 1, 'mililitros', 'CuidadoPersonal', 10);
call insertar_producto(@id, 'Acondicionador', 10.5, 100, 1, 'mililitros', 'CuidadoPersonal', 10);
call insertar_producto(@id, 'Crema', 10.5, 100, 1, 'mililitros', 'CuidadoPersonal', 10);
call insertar_producto(@id, 'Desodorante', 10.5, 100, 1, 'mililitros', 'CuidadoPersonal', 10);
call insertar_producto(@id, 'Jamón', 15.5, 100, 1, 'gramos', 'Comestible', 10);
call insertar_producto(@id, 'Queso', 20.5, 100, 1, 'gramos', 'Comestible', 10);
call insertar_producto(@id, 'Leche', 8.5, 100, 1, 'mililitros', 'Comestible', 10);
call insertar_producto(@id, 'Yogurt', 5.5, 100, 1, 'mililitros', 'Comestible', 10);
call insertar_producto(@id, 'Galletas', 4.5, 100, 1, 'gramos', 'Comestible', 10);
call insertar_producto(@id, 'Pan', 2.5, 100, 1, 'gramos', 'Comestible', 10);

-- ==============================================
-- INSERTS DE EMPLEADOS Y CUENTAS_EMPLEADOS
-- ==============================================

CALL insertar_empleado(@out_value, '74368258', 'Fernando', 'Candia', 'Aroni', 2500.0, 'Administrador');
CALL insertar_empleado(@out_value, '76841888', 'Jorge', 'Alejandro', 'Contreras', 1500.0, 'Administrador');
CALL insertar_empleado(@out_value, '70402080', 'Jhairt', 'Vega', 'Quino', 3500.0, 'Repartidor');
CALL insertar_empleado(@out_value, '72845864', 'Gian', 'Luca', 'Lujan', 3500.0, 'Repartidor');
CALL insertar_empleado(@out_value, '35264357', 'Yayito', 'Lujan', 'Carrion', 3500.0, 'EncargadoAlmacen');
CALL insertar_empleado(@out_value, '95483726', 'Maria', 'Ramos', 'Cantu', 3500.0, 'EncargadoVentas');
CALL insertar_empleado(@out_value, '12345678', 'Juan', 'Perez', 'Gonzales', 3500.0, 'EncargadoAlmacen');
CALL insertar_empleado(@out_value, '87654321', 'Luis', 'Rodriguez', 'Fernandez', 3500.0, 'EncargadoVentas');
CALL insertar_empleado(@out_value, '11223344', 'Ana', 'Martinez', 'Garcia', 3500.0, 'Administrador');
CALL insertar_empleado(@out_value, '55667788', 'Carlos', 'Diaz', 'Torres', 3500.0, 'Repartidor');

CALL insertar_cuenta_empleado(@id_cuenta, "fernando.candia", "74368258", 1);
CALL insertar_cuenta_empleado(@id_cuenta, "jorge.alejandro", "23456789", 2);
CALL insertar_cuenta_empleado(@id_cuenta, "jhairt.vega", "70402080", 3);
CALL insertar_cuenta_empleado(@id_cuenta, "gian.luca", "72845864", 4);

-- ==============================================
-- INSERTS DE CLIENTES Y CUENTAS_CLIENTES
-- ==============================================

CALL insertar_cliente(@out_value, '72947932', 'Juan', 'Casas', 'Gonzales', 100, 0, NULL, NULL, 'Av. Los Pinos 123', NULL);
CALL insertar_cliente(@out_value, '12345678', 'Maria', 'Perez', 'Lopez', 200, 10, '12345678901', 'Empresa XYZ', 'Calle Falsa 123', 1);
CALL insertar_cliente(@out_value, '87654321', 'Luis', 'Rodriguez', 'Fernandez', 150, 20, NULL, NULL, 'Jiron La Union 456', 2);
CALL insertar_cliente(@out_value, '11223344', 'Ana', 'Martinez', 'Garcia', 300, 0, '10987654321', 'Comercio ABC', 'Av. Larco 789', 1);
CALL insertar_cliente(@out_value, '55667788', 'Carlos', 'Diaz', 'Torres', 50, 5, NULL, NULL, 'Calle Real 321', NULL);
CALL insertar_cliente(@out_value, '99887766', 'Elena', 'Sanchez', 'Ramirez', 400, 25, '10293847561', 'Servicios PQR', 'Av. Brasil 159', 2);
CALL insertar_cliente(@out_value, '44556677', 'Jorge', 'Mendoza', 'Vasquez', 75, 15, NULL, NULL, 'Jiron Tarapaca 678', 2);
CALL insertar_cliente(@out_value, '33445566', 'Patricia', 'Flores', 'Ruiz', 120, 10, '19876543210', 'Industria LMN', 'Av. Salaverry 852', 1);
CALL insertar_cliente(@out_value, '22114433', 'Miguel', 'Gomez', 'Castillo', 90, 5, NULL, NULL, 'Calle Las Lomas 987', NULL);
CALL insertar_cliente(@out_value, '66778899', 'Teresa', 'Gutierrez', 'Hernandez', 250, 0, '10765432198', 'Consultoria OPQ', 'Jiron Amazonas 145', 3);
CALL insertar_cliente(@out_value, '77665544', 'Ricardo', 'Herrera', 'Chavez', 130, 20, NULL, NULL, 'Av. Grau 741', 4);
CALL insertar_cliente(@out_value, '55443322', 'Isabel', 'Vargas', 'Diaz', 110, 30, '12309845677', 'Almacen RST', 'Calle San Martin 555', 7);

CALL insertar_cuenta_cliente(@id_cuenta, "juan.casas", "72947932", 1);
CALL insertar_cuenta_cliente(@id_cuenta, "maria.perez", "12345678", 2);
CALL insertar_cuenta_cliente(@id_cuenta, "luis.rodriguez", "87654321", 3);
CALL insertar_cuenta_cliente(@id_cuenta, "ana.martinez", "11223344", 4);
CALL insertar_cuenta_cliente(@id_cuenta, "carlos.diaz", "55667788", 5);
CALL insertar_cuenta_cliente(@id_cuenta, "elena.sanchez", "99887766", 6);
CALL insertar_cuenta_cliente(@id_cuenta, "jorge.mendoza", "44556677", 7);
CALL insertar_cuenta_cliente(@id_cuenta, "patricia.flores", "33445566", 8);
CALL insertar_cuenta_cliente(@id_cuenta, "miguel.gomez", "22114433", 9);
CALL insertar_cuenta_cliente(@id_cuenta, "teresa.gutierrez", "66778899", 10);
CALL insertar_cuenta_cliente(@id_cuenta, "ricardo.herrera", "77665544", 11);
CALL insertar_cuenta_cliente(@id_cuenta, "isabel.vargas", "55443322", 12);

-- ==============================================
-- INSERTS DE ORDENCOMPRA, LINEAORDEN Y COMPROBANTE
-- ==============================================

-- Las ordenes de compra son registros sobre pedidos que realiza la empresa a sus proveedores
-- Las lineas de orden son los productos que se solicitan en una orden de compra y la cantidad de cada uno
-- Primero se debe crear una orden de compra y luego se pueden agregar lineas a esta orden
-- Luego actualizar el total de la orden de compra
-- Por ultimo se puede generar un comprobante de la orden de compra
CALL insertar_orden_compra(@id_orden_compra, @id_orden, 'Pendiente', NOW(), 0.0);
CALL insertar_linea_orden(@id_orden, 1, 100, 5.5*100); -- Arroz 100 gramos
CALL insertar_linea_orden(@id_orden, 2, 100, 3.5*100); -- Azucar 100 gramos
CALL insertar_linea_orden(@id_orden, 3, 100, 7.5*100); -- Aceite 100 mililitros
CALL actualizar_orden_compra(@id_orden_compra, @id_orden, 'Entregado', NOW(), 5.5*100 + 3.5*100 + 7.5*100);
CALL insertar_comprobante(@id_comprobante, @id_orden, 'Factura', NOW()); -- ID_COMP, ID_ORDEN, TIPO, FECHA

CALL insertar_orden_compra(@id_orden_compra, @id_orden, 'Pendiente', NOW(), 0.0);
CALL insertar_linea_orden(@id_orden, 4, 100, 10.5*100); -- Shampoo 100 mililitros
CALL insertar_linea_orden(@id_orden, 5, 100, 10.5*100); -- Acondicionador 100 mililitros
CALL insertar_linea_orden(@id_orden, 6, 100, 10.5*100); -- Crema 100 mililitros
CALL insertar_linea_orden(@id_orden, 7, 100, 10.5*100); -- Desodorante 100 mililitros
CALL actualizar_orden_compra(@id_orden_compra, @id_orden, 'Entregado', NOW(), 10.5*100 + 10.5*100 + 10.5*100 + 10.5*100);
CALL insertar_comprobante(@id_comprobante, @id_orden, 'BoletaSimple', NOW()); -- ID_COMP, ID_ORDEN, TIPO, FECHA

-- ==============================================
-- INSERTS DE ORDENVENTA, LINEAORDEN Y COMPROBANTE
-- ==============================================

-- Las ordenes de venta son registros sobre pedidos que realiza un cliente a la empresa son generadas por los empleados
-- Las lineas de orden son los productos que se solicitan en una orden de venta y la cantidad de cada uno
-- Primero se debe crear una orden de venta y luego se pueden agregar lineas a esta orden
-- Luego actualizar el total de la orden de venta
-- Por ultimo se puede generar un comprobante de la orden de venta

CALL insertar_orden_venta(@id_orden_venta, @id_orden, 1, 1, NULL, 'Pendiente', NOW(), 'Presencial', 'Efectivo', 0.0, 0.0);
CALL insertar_linea_orden(@id_orden, 1, 100, 5.5*100); -- Arroz 100 gramos
CALL insertar_linea_orden(@id_orden, 2, 100, 3.5*100); -- Azucar 100 gramos
CALL insertar_linea_orden(@id_orden, 3, 100, 7.5*100); -- Aceite 100 mililitros
CALL actualizar_orden_venta(@id_orden_venta, @id_orden, 1, 1, NULL, 'Entregado', NOW(), 'Presencial', 'Efectivo', 0.0, 5.5*100 + 3.5*100 + 7.5*100);
CALL insertar_comprobante(@id_comprobante, @id_orden, 'BoletaSimple', NOW()); -- ID_COMP, ID_ORDEN, TIPO, FECHA

CALL insertar_orden_venta(@id_orden_venta, @id_orden, 2, 2, NULL, 'Pendiente', NOW(), 'Delivery', 'Tarjeta', 10.0, 0.0);
CALL insertar_linea_orden(@id_orden, 4, 100, 10.5*100); -- Shampoo 100 mililitros
CALL insertar_linea_orden(@id_orden, 5, 100, 10.5*100); -- Acondicionador 100 mililitros
CALL insertar_linea_orden(@id_orden, 6, 100, 10.5*100); -- Crema 100 mililitros
CALL insertar_linea_orden(@id_orden, 7, 100, 10.5*100); -- Desodorante 100 mililitros

CALL actualizar_orden_venta(@id_orden_venta, @id_orden, 2, 2, NULL, 'Entregado', NOW(), 'Delivery', 'Tarjeta', 10.0, 10.5*100 + 10.5*100 + 10.5*100 + 10.5*100);
CALL insertar_comprobante(@id_comprobante, @id_orden, 'Factura', NOW()); -- ID_COMP, ID_ORDEN, TIPO, FECHA