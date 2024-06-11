/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.cuentas.controller.dao;

import java.util.ArrayList;
import pe.edu.pucp.dxnsisventas.cuentas.model.CuentaCliente;
import pe.edu.pucp.dxnsisventas.cuentas.model.PersonaCuenta;
import pe.edu.pucp.dxnsisventas.personas.model.Cliente;

/**
 *
 * @author Candi
 */
public interface CuentaClienteDAO {

  Cliente iniciar_sesion(String usuario, String contrasena);

  ArrayList<CuentaCliente> listarCuentas_Clientes();

  int insertar_Cuenta_Cliente(CuentaCliente cuenta);

  int actualizar_Cuenta_Cliente(CuentaCliente cuenta);

  int eliminar_Cuenta_Cliente(int idCuenta);

  ArrayList<PersonaCuenta> listar_clientes_cuentas(String cadena);
}
