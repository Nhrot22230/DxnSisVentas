/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.personas.model;

/**
 *
 * @author Candi
 */
public class Empleado extends Persona {

  private int idEmpleadoActual;
  private String idEmpleado;
  private double sueldo;
  private boolean empleadoActual;
  private Rol rol;

  // Constructores
  public Empleado() {
  }

  public int getIdEmpleadoActual() {
    return idEmpleadoActual;
  }

  public void setIdEmpleadoActual(int idEmpleadoActual) {
    this.idEmpleadoActual = idEmpleadoActual;
  }

  public String getIdEmpleado() {
    return idEmpleado;
  }

  public void setIdEmpleado(String idEmpleado) {
    this.idEmpleado = idEmpleado;
  }

  public double getSueldo() {
    return sueldo;
  }

  public void setSueldo(double sueldo) {
    this.sueldo = sueldo;
  }

  public boolean isEmpleadoActual() {
    return empleadoActual;
  }

  public void setEmpleadoActual(boolean empleadoActual) {
    this.empleadoActual = empleadoActual;
  }

  public Rol getRol() {
    return rol;
  }

  public void setRol(Rol rol) {
    this.rol = rol;
  }
}
