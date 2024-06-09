/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.documentos.controller.mysql;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.ArrayList;

import pe.edu.pucp.dxnsisventas.documentos.controller.dao.LineaOrdenDAO;
import pe.edu.pucp.dxnsisventas.documentos.controller.dao.OrdenCompraDAO;
import pe.edu.pucp.dxnsisventas.documentos.model.EstadoOrden;
import pe.edu.pucp.dxnsisventas.utils.database.DBManager;
import pe.edu.pucp.dxnsisventas.documentos.model.LineaOrden;
import pe.edu.pucp.dxnsisventas.documentos.model.OrdenCompra;
import pe.edu.pucp.dxnsisventas.productos.model.Producto;
import pe.edu.pucp.dxnsisventas.productos.model.TipoProducto;
import pe.edu.pucp.dxnsisventas.productos.model.UnidadMedida;

public class OrdenCompraMySQL implements OrdenCompraDAO {

  private Connection con;
  private CallableStatement cs;
  private String sql;
  private ResultSet rs;
  private LineaOrdenDAO lineaOrdenDAO;

  public OrdenCompraMySQL() {
    lineaOrdenDAO = new LineaOrdenMySQL();
  }

  /*
   * CREATE PROCEDURE listar_ordenes_compra(
   * IN p_cadena VARCHAR(30)
   * )
   * BEGIN
   * SELECT o.id_orden, o.estado, o.fecha_creacion, o.total,
   * oc.id_orden_compra, oc.fecha_recepcion,
   * p.id_producto, p.nombre, p.precio_unitario, p.stock, p.capacidad,
   * p.unidad_medida, p.tipo, p.puntos,
   * lo.cantidad, lo.subtotal
   * FROM Orden o
   * JOIN Orden_Compra oc ON o.id_orden = oc.id_orden
   * JOIN LineaOrden lo ON o.id_orden = lo.id_orden
   * JOIN Producto p ON lo.id_producto = p.id_producto
   * WHERE (o.id_orden LIKE CONCAT('%', p_cadena, '%') OR
   * oc.id_orden_compra LIKE CONCAT('%', p_cadena, '%'));
   * END$$
   */
  @Override
  public ArrayList<OrdenCompra> listar(String cadena) {
    ArrayList<OrdenCompra> ordenCompras = new ArrayList<>();

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL listar_ordenes_compra(?)}";
      cs = con.prepareCall(sql);
      cs.setString(1, cadena);
      rs = cs.executeQuery();
      OrdenCompra prevORC = null;
      while (rs.next()) {
        OrdenCompra ordenCompra = new OrdenCompra();
        ordenCompra.setIdOrden(rs.getInt("id_orden"));
        ordenCompra.setEstado(EstadoOrden.valueOf(rs.getString("estado")));
        ordenCompra.setFechaCreacion(rs.getTimestamp("fecha_creacion"));
        ordenCompra.setTotal(rs.getDouble("total"));
        ordenCompra.setIdOrdenCompraNumerico(rs.getInt("id_orden_compra"));
        ordenCompra.setIdOrdenCompraCadena("ORC" + String.format("%05d", ordenCompra.getIdOrdenCompraNumerico()));
        try {
          Timestamp fechaRecepcion = rs.getTimestamp("fecha_recepcion");
          ordenCompra.setFechaRecepcion(fechaRecepcion != null ? fechaRecepcion : null);
        } catch (Exception ex) {
          ordenCompra.setFechaRecepcion(null);
        }
        Producto producto = new Producto();
        producto.setIdProductoNumerico(rs.getInt("id_producto"));
        producto.setIdProductoCadena("PRO" + String.format("%05d", producto.getIdProductoNumerico()));
        producto.setNombre(rs.getString("nombre"));
        producto.setPrecioUnitario(rs.getDouble("precio_unitario"));
        producto.setStock(rs.getInt("stock"));
        producto.setCapacidad(rs.getDouble("capacidad"));
        producto.setUnidadDeMedida(UnidadMedida.valueOf(rs.getString("unidad_medida")));
        producto.setTipo(TipoProducto.valueOf(rs.getString("tipo")));
        producto.setPuntos(rs.getInt("puntos"));
        LineaOrden lineaOrden = new LineaOrden();
        lineaOrden.setProducto(producto);
        lineaOrden.setCantidad(rs.getInt("cantidad"));
        lineaOrden.setSubtotal(rs.getDouble("subtotal"));

        if (prevORC != null && prevORC.getIdOrden() == ordenCompra.getIdOrden()) {
          prevORC.agregarLineaOrden(lineaOrden);
        } else {
          if (prevORC != null) {
            ordenCompras.add(prevORC);
          }
          prevORC = ordenCompra;
          prevORC.agregarLineaOrden(lineaOrden);
        }
      }
      if (prevORC != null) {
        ordenCompras.add(prevORC);
      }
    } catch (Exception ex) {
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

    return ordenCompras;
  }

  /*
   * CREATE PROCEDURE insertar_orden_compra(
   * OUT p_id_orden_compra INT,
   * OUT p_id_orden INT,
   * IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
   * IN p_fecha_creacion DATETIME,
   * IN p_total DECIMAL(10, 2)
   * )
   * BEGIN
   * CALL insertar_orden(p_id_orden, p_estado, p_fecha_creacion, p_total);
   * INSERT INTO Orden_Compra(id_orden, fecha_recepcion)
   * VALUES(p_id_orden, NULL);
   * SET p_id_orden_compra = LAST_INSERT_ID();
   * END$$
   */
  @Override
  public int insertar(OrdenCompra ordenCompra) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL insertar_orden_compra(?,?,?,?,?)}";
      cs = con.prepareCall(sql);
      cs.registerOutParameter("p_id_orden_compra", java.sql.Types.INTEGER);
      cs.registerOutParameter("p_id_orden", java.sql.Types.INTEGER);
      cs.setString("p_estado", ordenCompra.getEstado().toString());
      cs.setTimestamp("p_fecha_creacion", new java.sql.Timestamp(ordenCompra.getFechaCreacion().getTime()));
      cs.setDouble("p_total", ordenCompra.getTotal());
      cs.executeUpdate();
      ordenCompra.setIdOrdenCompraNumerico(cs.getInt("p_id_orden_compra"));
      ordenCompra.setIdOrdenCompraCadena("OC" + String.format("%05d", ordenCompra.getIdOrdenCompraNumerico()));
      ordenCompra.setIdOrden(cs.getInt("p_id_orden"));
      resultado = ordenCompra.getIdOrdenCompraNumerico();
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

    for (LineaOrden lineaOrden : ordenCompra.getLineasOrden()) {
      lineaOrdenDAO.insertar(lineaOrden, ordenCompra.getIdOrden());
    }

    return resultado;
  }

  /*
   * CREATE PROCEDURE actualizar_orden_compra(
   * IN p_id_orden_compra INT,
   * IN p_id_orden INT,
   * IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
   * IN p_fecha_recepcion DATETIME,
   * IN p_total DECIMAL(10, 2)
   * )
   * BEGIN
   * CALL actualizar_orden(p_id_orden, p_estado, p_total);
   * UPDATE Orden_Compra
   * SET id_orden = p_id_orden,
   * fecha_recepcion = p_fecha_recepcion
   * WHERE id_orden_compra = p_id_orden_compra;
   * END$$
   */
  @Override
  public int modificar(OrdenCompra ordenCompra) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL actualizar_orden_compra(?,?,?,?,?)}";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_orden_compra", ordenCompra.getIdOrdenCompraNumerico());
      cs.setInt("p_id_orden", ordenCompra.getIdOrden());
      cs.setString("p_estado", ordenCompra.getEstado().toString());
      cs.setTimestamp("p_fecha_recepcion", new java.sql.Timestamp(ordenCompra.getFechaRecepcion().getTime()));
      cs.setDouble("p_total", ordenCompra.getTotal());
      cs.executeUpdate();
      resultado = 1;
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
