/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.cuentas.controller.mysql;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import pe.edu.pucp.dxnsisventas.cuentas.controller.dao.CuentaClienteDAO;
import pe.edu.pucp.dxnsisventas.cuentas.model.CuentaCliente;
import pe.edu.pucp.dxnsisventas.personas.model.Cliente;
import pe.edu.pucp.dxnsisventas.utils.database.DBManager;

/**
 *
 * @author Candi
 */
public class CuentaClienteMySQL implements CuentaClienteDAO {

  private Connection con;
  private CallableStatement cs;
  private String sql;
  private ResultSet rs;

  /*
  CREATE PROCEDURE iniciar_sesion_cliente(
    IN p_usuario VARCHAR(30),
    IN p_contrasena VARCHAR(30)
  )
  BEGIN
    DECLARE v_id_cuenta INT;
    SELECT id_cuenta INTO v_id_cuenta
    FROM Cuenta
    WHERE usuario = p_usuario AND contrasena = p_contrasena;

    IF v_id_cuenta IS NOT NULL THEN
      SELECT c.id_cliente, c.dni, c.nombre, c.apellido_paterno, c.apellido_materno, c.puntos, c.puntos_retenidos, c.ruc, c.razon_social, c.direccion,
      c.id_patrocinador, p.dni as dni_patrocinador, p.nombre as nombre_patrocinador, p.apellido_paterno as apellido_paterno_patrocinador, p.apellido_materno as apellido_materno_patrocinador, p.puntos as puntos_patrocinador, p.puntos_retenidos as puntos_retenidos_patrocinador, p.ruc as ruc_patrocinador, p.razon_social as razon_social_patrocinador, p.direccion as direccion_patrocinador
      FROM Cliente c
      LEFT JOIN Cliente p ON c.id_patrocinador = p.id_cliente
      WHERE c.id_cuenta = v_id_cuenta;
    END IF;
  END$$
   */
  @Override
  public Cliente iniciar_sesion(String usuario, String contrasena) {
    Cliente res = null;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL iniciar_sesion_cliente(?, ?) }";
      cs = con.prepareCall(sql);
      cs.setString("p_usuario", usuario);
      cs.setString("p_contrasena", contrasena);
      cs.executeUpdate();
      if (rs.next()) {
        res = new Cliente();
        res.setIdNumerico(rs.getInt("id_cliente"));
        res.setDNI(rs.getString("dni"));
        res.setNombre(rs.getString("nombre"));
        res.setApellidoPaterno(rs.getString("apellido_paterno"));
        res.setApellidoMaterno(rs.getString("apellido_materno"));
        res.setPuntos(rs.getInt("puntos"));
        res.setPuntosRetenidos(rs.getInt("puntos_retenidos"));
        res.setRUC(rs.getString("ruc"));
        res.setRazonSocial(rs.getString("razon_social"));
        res.setDireccion(rs.getString("direccion"));

        Cliente patrocinador = new Cliente();
        patrocinador.setIdNumerico(rs.getInt("id_patrocinador"));
        patrocinador.setDNI(rs.getString("dni_patrocinador"));
        patrocinador.setNombre(rs.getString("nombre_patrocinador"));
        patrocinador.setApellidoPaterno(rs.getString("apellido_paterno_patrocinador"));
        patrocinador.setApellidoMaterno(rs.getString("apellido_materno_patrocinador"));
        patrocinador.setPuntos(rs.getInt("puntos_patrocinador"));
        patrocinador.setPuntosRetenidos(rs.getInt("puntos_retenidos_patrocinador"));
        patrocinador.setRUC(rs.getString("ruc_patrocinador"));
        patrocinador.setRazonSocial(rs.getString("razon_social_patrocinador"));
        patrocinador.setDireccion(rs.getString("direccion_patrocinador"));

        res.setPatrocinador(patrocinador);
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
    return res;
  }

  /*
   * CREATE PROCEDURE insertar_cuenta_cliente(
   * OUT p_id_cuenta INT,
   * IN p_usuario VARCHAR(30),
   * IN p_contrasena VARCHAR(30),
   * IN p_id_cliente INT
   * )
   * BEGIN
   * CALL insertar_cuenta(p_id_cuenta, p_usuario, p_contrasena);
   * INSERT INTO Cuenta_Cliente(id_cuenta, id_cliente)
   * VALUES(p_id_cuenta, p_id_cliente);
   * END$$
   */
  @Override
  public int insertar_Cuenta_Cliente(CuentaCliente cuenta) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL insertar_cuenta_cliente(?, ?, ?, ?) }";
      cs = con.prepareCall(sql);
      cs.registerOutParameter("p_id_cuenta", java.sql.Types.INTEGER);
      cs.setString("p_usuario", cuenta.getUsuario());
      cs.setString("p_contrasena", cuenta.getContrasena());
      cs.setInt("p_id_cliente", cuenta.getFid_Cliente());
      cs.executeUpdate();
      resultado = cs.getInt("p_id_cuenta");
      cuenta.setIdCuenta(resultado);
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

  /*
   * CREATE PROCEDURE actualizar_cuenta_cliente(
   * IN p_id_cliente INT,
   * IN p_usuario VARCHAR(30),
   * IN p_contrasena VARCHAR(30)
   * )
   * BEGIN
   * DECLARE v_id_cuenta INT;
   * SELECT id_cuenta INTO v_id_cuenta
   * FROM Cuenta_Cliente
   * WHERE id_cliente = p_id_cliente;
   * CALL actualizar_cuenta(v_id_cuenta, p_usuario, p_contrasena);
   * END$$
   */
  @Override
  public int actualizar_Cuenta_Cliente(CuentaCliente cuenta) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL actualizar_cuenta_cliente(?, ?, ?) }";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_cliente", cuenta.getFid_Cliente());
      cs.setString("p_usuario", cuenta.getUsuario());
      cs.setString("p_contrasena", cuenta.getContrasena());
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

  /*
   * CREATE PROCEDURE eliminar_cuenta_cliente(
   * IN p_id_cliente INT
   * )
   * BEGIN
   * DECLARE v_id_cuenta INT;
   * SELECT id_cuenta INTO v_id_cuenta
   * FROM Cuenta_Cliente
   * WHERE id_cliente = p_id_cliente;
   * CALL eliminar_cuenta(v_id_cuenta);
   * END$$
   */
  @Override
  public int eliminar_Cuenta_Cliente(int idCliente) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL eliminar_cuenta_cliente(?) }";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_cliente", idCliente);
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

  /*
   * CREATE PROCEDURE listar_cuentas_cliente()
   * BEGIN
   * SELECT cc.id_cuenta, cc.id_cliente, c.usuario, c.contrasena
   * FROM Cuenta_Cliente cc JOIN Cuenta c ON cc.id_cuenta = c.id_cuenta;
   * END$$
   */
  @Override
  public ArrayList<CuentaCliente> listarCuentas_Clientes() {
    ArrayList<CuentaCliente> cuentas = new ArrayList<>();
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL listar_cuentas_cliente() }";
      cs = con.prepareCall(sql);
      rs = cs.executeQuery();
      while (rs.next()) {
        CuentaCliente cuenta = new CuentaCliente();
        cuenta.setFid_Cliente(rs.getInt("id_cliente"));
        cuenta.setUsuario(rs.getString("usuario"));
        cuenta.setContrasena(rs.getString("contrasena"));

        cuenta.setIdCuenta(rs.getInt("id_cuenta"));
        cuentas.add(cuenta);
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
    return cuentas;
  }
}
