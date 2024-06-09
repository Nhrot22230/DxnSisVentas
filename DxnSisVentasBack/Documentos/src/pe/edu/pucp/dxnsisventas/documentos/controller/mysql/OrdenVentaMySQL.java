/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.documentos.controller.mysql;

import java.sql.Timestamp;
import java.sql.Connection;
import java.sql.CallableStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;

import pe.edu.pucp.dxnsisventas.documentos.controller.dao.OrdenVentaDAO;
import pe.edu.pucp.dxnsisventas.documentos.model.EstadoOrden;
import pe.edu.pucp.dxnsisventas.documentos.model.OrdenVenta;
import pe.edu.pucp.dxnsisventas.documentos.model.TipoVenta;
import pe.edu.pucp.dxnsisventas.documentos.model.MetodoPago;
import pe.edu.pucp.dxnsisventas.documentos.model.LineaOrden;
import pe.edu.pucp.dxnsisventas.personas.model.Cliente;
import pe.edu.pucp.dxnsisventas.personas.model.Empleado;
import pe.edu.pucp.dxnsisventas.personas.model.Rol;
import pe.edu.pucp.dxnsisventas.productos.model.Producto;
import pe.edu.pucp.dxnsisventas.productos.model.TipoProducto;
import pe.edu.pucp.dxnsisventas.productos.model.UnidadMedida;
import pe.edu.pucp.dxnsisventas.utils.database.DBManager;

/**
 *
 * @author Candi
 */
public class OrdenVentaMySQL implements OrdenVentaDAO {
  private Connection con;
  private CallableStatement cs;
  private String sql;
  private ResultSet rs;

  private LineaOrdenMySQL lineaOrdenMySQL;

  public OrdenVentaMySQL() {
    lineaOrdenMySQL = new LineaOrdenMySQL();
  }

  /*
   * CREATE PROCEDURE listar_ordenes_venta(
   * IN p_cadena VARCHAR(30)
   * )
   * BEGIN
   * SELECT o.id_orden, o.estado, o.fecha_creacion, o.total,
   * ov.id_orden_venta, ov.fecha_entrega, ov.tipo_venta, ov.metodo_pago,
   * ov.porcentaje_descuento,
   * ov.id_cliente, c.dni AS dni_cliente, c.nombre AS nombre_cliente,
   * c.apellido_paterno AS apellido_paterno_cliente, c.apellido_materno AS
   * apellido_materno_cliente,
   * c.puntos, c.puntos_retenidos, c.ruc, c.razon_social, c.direccion,
   * ov.id_empleado, e.dni AS dni_empleado, e.nombre AS nombre_empleado,
   * e.apellido_paterno AS apellido_paterno_empleado, e.apellido_materno AS
   * apellido_materno_empleado,
   * e.sueldo, e.rol,
   * ov.fecha_entrega, ov.tipo_venta, ov.metodo_pago,
   * p.id_producto, p.nombre AS nombre_producto, p.precio_unitario, p.stock,
   * p.capacidad, p.unidad_medida, p.tipo, p.puntos,
   * lo.cantidad, lo.subtotal
   * FROM Orden o
   * JOIN Orden_Venta ov ON o.id_orden = ov.id_orden
   * JOIN Cliente c ON ov.id_cliente = c.id_cliente
   * JOIN Empleado e ON ov.id_empleado = e.id_empleado
   * LEFT JOIN LineaOrden lo ON o.id_orden = lo.id_orden
   * LEFT JOIN Producto p ON lo.id_producto = p.id_producto
   * WHERE (o.id_orden LIKE CONCAT('%', p_cadena, '%') OR
   * ov.id_orden_venta LIKE CONCAT('%', p_cadena, '%'));
   * END$$
   */
  @Override
  public ArrayList<OrdenVenta> listar(String cadena) {
    ArrayList<OrdenVenta> ordenes = new ArrayList<>();
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL listar_ordenes_venta(?)}";
      cs = con.prepareCall(sql);
      cs.setString("p_cadena", cadena);
      rs = cs.executeQuery();

      OrdenVenta prevORV = null;
      while (rs.next()) {
        OrdenVenta orden = new OrdenVenta();
        orden.setIdOrden(rs.getInt("id_orden"));
        orden.setEstado(EstadoOrden.valueOf(rs.getString("estado")));
        orden.setFechaCreacion(rs.getTimestamp("fecha_creacion"));
        orden.setTotal(rs.getDouble("total"));
        orden.setIdOrdenVentaNumerico(rs.getInt("id_orden_venta"));
        try {
          Timestamp fechaEntrega = rs.getTimestamp("fecha_entrega");
          orden.setFechaEntrega(fechaEntrega != null ? new Date(fechaEntrega.getTime()) : null);
        } catch (Exception ex) {
          orden.setFechaEntrega(null);
        }
        orden.setTipoVenta(TipoVenta.valueOf(rs.getString("tipo_venta")));
        orden.setMetodoPago(MetodoPago.valueOf(rs.getString("metodo_pago")));
        orden.setPorcentajeDescuento(rs.getDouble("porcentaje_descuento"));

        Cliente cliente = new Cliente();
        cliente.setIdNumerico(rs.getInt("id_cliente"));
        cliente.setDNI(rs.getString("dni_cliente"));
        cliente.setNombre(rs.getString("nombre_cliente"));
        cliente.setApellidoPaterno(rs.getString("apellido_paterno_cliente"));
        cliente.setApellidoMaterno(rs.getString("apellido_materno_cliente"));
        cliente.setPuntos(rs.getInt("puntos"));
        cliente.setPuntosRetenidos(rs.getInt("puntos_retenidos"));
        cliente.setRUC(rs.getString("ruc"));
        cliente.setRazonSocial(rs.getString("razon_social"));
        cliente.setDireccion(rs.getString("direccion"));

        orden.setCliente(cliente);

        Empleado empleado = new Empleado();
        empleado.setIdEmpleadoActual(rs.getInt("id_empleado"));
        empleado.setDNI(rs.getString("dni_empleado"));
        empleado.setNombre(rs.getString("nombre_empleado"));
        empleado.setApellidoPaterno(rs.getString("apellido_paterno_empleado"));
        empleado.setApellidoMaterno(rs.getString("apellido_materno_empleado"));
        empleado.setSueldo(rs.getDouble("sueldo"));

        empleado.setRol(Rol.EncargadoVentas);
        orden.setEncargadoVenta(empleado);

        LineaOrden lineaOrden = new LineaOrden();
        Producto producto = new Producto();
        producto.setIdProductoNumerico(rs.getInt("id_producto"));
        producto.setNombre(rs.getString("nombre_producto"));
        producto.setPrecioUnitario(rs.getDouble("precio_unitario"));
        producto.setStock(rs.getInt("stock"));
        producto.setCapacidad(rs.getDouble("capacidad"));
        producto.setUnidadDeMedida(UnidadMedida.valueOf(rs.getString("unidad_medida")));
        producto.setTipo(TipoProducto.valueOf(rs.getString("tipo")));
        producto.setPuntos(rs.getInt("puntos"));
        lineaOrden.setProducto(producto);
        lineaOrden.setCantidad(rs.getInt("cantidad"));
        lineaOrden.setSubtotal(rs.getDouble("subtotal"));

        if (prevORV != null && prevORV.getIdOrdenVentaNumerico()== orden.getIdOrdenVentaNumerico()) {
          prevORV.agregarLineaOrden(lineaOrden);
        } else {
          if (prevORV != null) {
            ordenes.add(prevORV);
          }
          prevORV = orden;
          prevORV.agregarLineaOrden(lineaOrden);
        }
      }
      if (prevORV != null) {
        ordenes.add(prevORV);
      }
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        if (rs != null) {
          rs.close();
        }
        if (cs != null) {
          cs.close();
        }
        if (con != null) {
          con.close();
        }
      } catch (SQLException ex) {
        System.err.println(ex.getMessage());
      }
    }

    return ordenes;
  }

  /*
   * CREATE PROCEDURE insertar_orden_venta(
   * OUT p_id_orden_venta INT,
   * OUT p_id_orden INT,
   * IN p_id_cliente INT,
   * IN p_id_empleado INT,
   * IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
   * IN p_fecha_creacion DATETIME,
   * IN p_tipo_venta ENUM('Presencial', 'Delivery'),
   * IN p_metodo_pago ENUM('Efectivo', 'Tarjeta'),
   * IN p_porcentaje_descuento DECIMAL(4,2),
   * IN p_total DECIMAL(10, 2)
   * )
   * BEGIN
   * CALL insertar_orden(p_id_orden, p_estado, p_fecha_creacion, p_total);
   * INSERT INTO Orden_Venta(id_orden, id_cliente, id_empleado, tipo_venta,
   * metodo_pago, porcentaje_descuento)
   * VALUES(p_id_orden, p_id_cliente, p_id_empleado, p_tipo_venta, p_metodo_pago,
   * p_porcentaje_descuento);
   * SET p_id_orden_venta = LAST_INSERT_ID();
   * END$$
   */
  @Override
  public int insertar(OrdenVenta ordenVenta) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL insertar_orden_venta(?, ?, ?, ?, ?, ?, ?, ?, ?, ?)}";
      cs = con.prepareCall(sql);
      cs.registerOutParameter("p_id_orden_venta", java.sql.Types.INTEGER);
      cs.registerOutParameter("p_id_orden", java.sql.Types.INTEGER);
      cs.setInt("p_id_cliente", ordenVenta.getCliente().getIdNumerico());
      cs.setInt("p_id_empleado", ordenVenta.getEncargadoVenta().getIdEmpleadoActual());
      cs.setString("p_estado", ordenVenta.getEstado().toString());
      cs.setTimestamp("p_fecha_creacion", new Timestamp(ordenVenta.getFechaCreacion().getTime()));
      cs.setString("p_tipo_venta", ordenVenta.getTipoVenta().toString());
      cs.setString("p_metodo_pago", ordenVenta.getMetodoPago().toString());
      cs.setDouble("p_porcentaje_descuento", ordenVenta.getPorcentajeDescuento());
      cs.setDouble("p_total", ordenVenta.getTotal());
      cs.executeUpdate();
      ordenVenta.setIdOrdenVentaNumerico(cs.getInt("p_id_orden_venta"));
      ordenVenta.setIdOrdenVentaCadena("OV" + String.format("%05d", ordenVenta.getIdOrdenVentaNumerico()));
      ordenVenta.setIdOrden(cs.getInt("p_id_orden"));
      resultado = ordenVenta.getIdOrdenVentaNumerico();
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        if (rs != null) {
          rs.close();
        }
        if (cs != null) {
          cs.close();
        }
        if (con != null) {
          con.close();
        }
      } catch (SQLException ex) {
        System.err.println(ex.getMessage());
      }
    }

    for (LineaOrden lineaOrden : ordenVenta.getLineasOrden()) {
      lineaOrdenMySQL.insertar(lineaOrden, ordenVenta.getIdOrden());
    }
    return resultado;
  }

  /*
   * CREATE PROCEDURE actualizar_orden_venta(
   * IN p_id_orden_venta INT,
   * IN p_id_orden INT,
   * IN p_id_cliente INT,
   * IN p_id_empleado INT,
   * IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
   * IN p_fecha_entrega DATETIME,
   * IN p_tipo_venta ENUM('Presencial', 'Delivery'),
   * IN p_metodo_pago ENUM('Efectivo', 'Tarjeta'),
   * IN p_porcentaje_descuento DECIMAL(4,2),
   * IN p_total DECIMAL(10, 2)
   * )
   * BEGIN
   * CALL actualizar_orden(p_id_orden, p_estado);
   * UPDATE Orden_Venta
   * SET id_orden = p_id_orden,
   * id_cliente = p_id_cliente,
   * id_empleado = p_id_empleado,
   * fecha_entrega = p_fecha_entrega,
   * tipo_venta = p_tipo_venta,
   * metodo_pago = p_metodo_pago,
   * porcentaje_descuento = p_porcentaje_descuento,
   * total = p_total
   * WHERE id_orden_venta = p_id_orden_venta;
   * END$$
   */
  @Override
  public int modificar(OrdenVenta ordenVenta) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL actualizar_orden_venta(?, ?, ?, ?, ?, ?, ?, ?, ?, ?)}";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_orden_venta", ordenVenta.getIdOrdenVentaNumerico());
      cs.setInt("p_id_orden", ordenVenta.getIdOrden());
      cs.setInt("p_id_cliente", ordenVenta.getCliente().getIdNumerico());
      cs.setInt("p_id_empleado", ordenVenta.getEncargadoVenta().getIdEmpleadoActual());
      cs.setString("p_estado", ordenVenta.getEstado().toString());
      cs.setTimestamp("p_fecha_entrega", new Timestamp(ordenVenta.getFechaEntrega().getTime()));
      cs.setString("p_tipo_venta", ordenVenta.getTipoVenta().toString());
      cs.setString("p_metodo_pago", ordenVenta.getMetodoPago().toString());
      cs.setDouble("p_porcentaje_descuento", ordenVenta.getPorcentajeDescuento());
      cs.setDouble("p_total", ordenVenta.getTotal());
      cs.executeUpdate();
      resultado = cs.getInt("p_id_orden");
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        if (rs != null) {
          rs.close();
        }
        if (cs != null) {
          cs.close();
        }
        if (con != null) {
          con.close();
        }
      } catch (SQLException ex) {
        System.err.println(ex.getMessage());
      }
    }
    return resultado;
  }
}

