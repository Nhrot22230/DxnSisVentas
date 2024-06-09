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

import pe.edu.pucp.dxnsisventas.cuentas.controller.dao.CuentaEmpleadoDAO;
import pe.edu.pucp.dxnsisventas.cuentas.model.CuentaEmpleado;
import pe.edu.pucp.dxnsisventas.personas.model.Empleado;
import pe.edu.pucp.dxnsisventas.personas.model.Rol;
import pe.edu.pucp.dxnsisventas.utils.database.DBManager;

/**
 *
 * @author Candi
 */
public class CuentaEmpleadoMySQL implements CuentaEmpleadoDAO {

  private Connection con;
  private CallableStatement cs;
  private String sql;
  private ResultSet rs;

  /*
   * CREATE PROCEDURE iniciar_sesion_empleado(
   * IN p_usuario VARCHAR(30),
   * IN p_contrasena VARCHAR(30)
   * )
   * BEGIN
   * DECLARE v_id_cuenta INT;
   * DECLARE v_id_empleado INT;
   * SELECT ce.id_cuenta, ce.id_empleado INTO v_id_cuenta, v_id_empleado
   * FROM Cuenta_Empleado ce JOIN Cuenta c ON ce.id_cuenta = c.id_cuenta
   * WHERE c.usuario = p_usuario AND c.contrasena = p_contrasena;
   * SELECT e.id_empleado, e.dni, e.nombre, e.apellido_paterno,
   * e.apellido_materno, e.sueldo, e.rol
   * FROM Empleado e
   * WHERE e.id_empleado = v_id_empleado
   * AND e.activo = 1;
   * END$$
   */
  @Override
  public Empleado iniciar_sesion(String usuario, String contrasena) {
    Empleado empleado = null;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL iniciar_sesion_empleado(?, ?)}";
      cs = con.prepareCall(sql);
      cs.setString("p_usuario", usuario);
      cs.setString("p_contrasena", contrasena);
      rs = cs.executeQuery();
      if (rs.next()) {
        empleado = new Empleado();
        empleado.setDNI(rs.getString("dni"));
        empleado.setNombre(rs.getString("nombre"));
        empleado.setApellidoPaterno(rs.getString("apellido_paterno"));
        empleado.setApellidoMaterno(rs.getString("apellido_materno"));
        empleado.setSueldo(rs.getDouble("sueldo"));
        empleado.setRol(Rol.valueOf(rs.getString("rol")));

        empleado.setIdEmpleadoNumerico(rs.getInt("id_empleado"));
        empleado.setIdEmpleadoCadena("EMP" + String.format("%05d", empleado.getIdEmpleadoNumerico()));
        empleado.setEmpleadoActivo(true);
      }
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        con.close();
      } catch (SQLException ex) {
        System.out.println(ex.getMessage());
      }
    }

    return empleado;
  }

  /*
   * CREATE PROCEDURE insertar_cuenta_empleado(
   * OUT p_id_cuenta INT,
   * IN p_usuario VARCHAR(30),
   * IN p_contrasena VARCHAR(30),
   * IN p_id_empleado INT
   * )
   * BEGIN
   * CALL insertar_cuenta(p_id_cuenta, p_usuario, p_contrasena);
   * INSERT INTO Cuenta_Empleado(id_cuenta, id_empleado)
   * VALUES(p_id_cuenta, p_id_empleado);
   * END$$
   */
  @Override
  public int insertar_Cuenta_Empleado(CuentaEmpleado cuenta) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL insertar_cuenta_empleado(?, ?, ?, ?)}";
      cs = con.prepareCall(sql);
      cs.registerOutParameter("p_id_cuenta", java.sql.Types.INTEGER);
      cs.setString("p_usuario", cuenta.getUsuario());
      cs.setString("p_contrasena", cuenta.getContrasena());
      cs.setInt("p_id_empleado", cuenta.getFid_Empleado());
      cs.executeUpdate();
      resultado = cs.getInt("p_id_cuenta");
      cuenta.setIdCuenta(resultado);
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        con.close();
      } catch (SQLException ex) {
        System.out.println(ex.getMessage());
      }
    }
    return resultado;
  }

  /*
   * CREATE PROCEDURE actualizar_cuenta_empleado(
   * IN p_id_empleado INT,
   * IN p_usuario VARCHAR(30),
   * IN p_contrasena VARCHAR(30)
   * )
   * BEGIN
   * DECLARE v_id_cuenta INT;
   * SELECT id_cuenta INTO v_id_cuenta
   * FROM Cuenta_Empleado
   * WHERE id_empleado = p_id_empleado;
   * CALL actualizar_cuenta(v_id_cuenta, p_usuario, p_contrasena);
   * END$$
   */
  @Override
  public int actualizar_Cuenta_Empleado(CuentaEmpleado cuenta) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL actualizar_cuenta_empleado(?, ?, ?)}";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_empleado", cuenta.getFid_Empleado());
      cs.setString("p_usuario", cuenta.getUsuario());
      cs.setString("p_contrasena", cuenta.getContrasena());
      cs.executeUpdate();
      resultado = 1;
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        con.close();
      } catch (SQLException ex) {
        System.out.println(ex.getMessage());
      }
    }
    return resultado;
  }

  /*
   * CREATE PROCEDURE eliminar_cuenta_empleado(
   * IN p_id_empleado INT
   * )
   * BEGIN
   * DECLARE v_id_cuenta INT;
   * SELECT id_cuenta INTO v_id_cuenta
   * FROM Cuenta_Empleado
   * WHERE id_empleado = p_id_empleado;
   * CALL eliminar_cuenta(v_id_cuenta);
   * END$$
   */
  @Override
  public int eliminar_Cuenta_Empleado(int idEmpleado) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL eliminar_cuenta_empleado(?)}";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_empleado", idEmpleado);
      cs.executeUpdate();
      resultado = 1;
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        con.close();
      } catch (SQLException ex) {
        System.out.println(ex.getMessage());
      }
    }
    return resultado;
  }

  /*
   * CREATE PROCEDURE listar_cuentas_empleado()
   * BEGIN
   * SELECT ce.id_cuenta, ce.id_empleado, c.usuario, c.contrasena
   * FROM Cuenta_Empleado ce JOIN Cuenta c ON ce.id_cuenta = c.id_cuenta;
   * END$$
   */
  @Override
  public ArrayList<CuentaEmpleado> listarCuentas_Empleados() {
    ArrayList<CuentaEmpleado> cuentas = new ArrayList<>();
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL listar_cuentas_empleado()}";
      cs = con.prepareCall(sql);
      rs = cs.executeQuery();
      while (rs.next()) {
        CuentaEmpleado cuenta = new CuentaEmpleado();
        cuenta.setIdCuenta(rs.getInt("id_cuenta"));
        cuenta.setFid_Empleado(rs.getInt("id_empleado"));
        cuenta.setUsuario(rs.getString("usuario"));
        cuenta.setContrasena(rs.getString("contrasena"));
        cuentas.add(cuenta);
      }
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        con.close();
      } catch (SQLException ex) {
        System.out.println(ex.getMessage());
      }
    }
    return cuentas;
  }

}