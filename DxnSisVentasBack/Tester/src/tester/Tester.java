/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package tester;

import java.util.ArrayList;
import java.util.Date;
import pe.edu.pucp.dxnsisventas.documentos.controller.dao.OrdenVentaDAO;
import pe.edu.pucp.dxnsisventas.documentos.controller.mysql.OrdenVentaMySQL;
import pe.edu.pucp.dxnsisventas.documentos.model.EstadoOrden;
import pe.edu.pucp.dxnsisventas.documentos.model.LineaOrden;
import pe.edu.pucp.dxnsisventas.documentos.model.MetodoPago;
import pe.edu.pucp.dxnsisventas.documentos.model.OrdenVenta;
import pe.edu.pucp.dxnsisventas.documentos.model.TipoVenta;
import pe.edu.pucp.dxnsisventas.personas.controller.dao.ClienteDAO;
import pe.edu.pucp.dxnsisventas.personas.controller.dao.EmpleadoDAO;
import pe.edu.pucp.dxnsisventas.personas.controller.mysql.ClienteMySQL;
import pe.edu.pucp.dxnsisventas.personas.controller.mysql.EmpleadoMySQL;
import pe.edu.pucp.dxnsisventas.personas.model.Cliente;
import pe.edu.pucp.dxnsisventas.personas.model.Empleado;
import pe.edu.pucp.dxnsisventas.personas.model.Rol;
import pe.edu.pucp.dxnsisventas.productos.controller.dao.ProductoDAO;
import pe.edu.pucp.dxnsisventas.productos.controller.mysql.ProductoMySQL;
import pe.edu.pucp.dxnsisventas.productos.model.Producto;
import pe.edu.pucp.dxnsisventas.productos.model.TipoProducto;
import pe.edu.pucp.dxnsisventas.productos.model.UnidadMedida;

/**
 *
 * @author Candi
 */
public class Tester {
  
  /** 
   */
  public static void testProductos(){
    ProductoDAO productoDAO = new ProductoMySQL();
    
    System.out.println("Listado de productos");
    productoDAO.listar("").forEach(producto -> {
      System.out.println("ID: " + producto.getIdProductoNumerico() + " - " + producto.getNombre() + " - " + producto.getCapacidad() + " " + producto.getUnidadDeMedida());
    });

    System.out.println("Insertar producto");
    Producto p1 = new Producto();
    p1.setNombre("Galletas de avena");
    p1.setPrecioUnitario(100);
    p1.setPuntos(12);
    p1.setStock(10);
    p1.setCapacidad(100);
    p1.setTipo(TipoProducto.Comestible);
    p1.setUnidadDeMedida(UnidadMedida.gramos);    
    int resultado = productoDAO.insertar(p1);
    System.out.println("Producto insertado con ID: " + resultado);

    System.out.println("Listado de productos");
    productoDAO.listar("").forEach(producto -> {
      System.out.println("ID: " + producto.getIdProductoNumerico() + " - " + producto.getNombre() + " - " + producto.getCapacidad() + " " + producto.getUnidadDeMedida());
    });

    System.out.println("Actualizar producto");
    p1.setCapacidad(10);

    productoDAO.modificar(p1);

    System.out.println("Listado de productos");
    productoDAO.listar("").forEach(producto -> {
      System.out.println("ID: " + producto.getIdProductoNumerico() + " - " + producto.getNombre() + " - " + producto.getCapacidad() + " " + producto.getUnidadDeMedida());
    });

    System.out.println("Eliminar producto");

    productoDAO.eliminar(p1.getIdProductoNumerico());

    System.out.println("Listado de productos");
    productoDAO.listar("").forEach(producto -> {
      System.out.println("ID: " + producto.getIdProductoNumerico() + " - " + producto.getNombre() + " - " + producto.getCapacidad() + " " + producto.getUnidadDeMedida());
    });
  }

    public static void testOrdenVenta(){
        
        
        
        // DAOS
        OrdenVentaDAO daoOrdenVenta = new OrdenVentaMySQL();
        ProductoDAO daoProducto = new ProductoMySQL();
        EmpleadoDAO daoEmpleado = new EmpleadoMySQL();
        ClienteDAO daoCliente = new ClienteMySQL();

        // LISTAS
        ArrayList<Producto> productos = daoProducto.listar("");
        ArrayList<Cliente> clientes = daoCliente.listar("");
        ArrayList<Empleado> empleados = daoEmpleado.listar("");

        OrdenVenta ordenVenta = new OrdenVenta();

        ordenVenta.setCliente(clientes.get(0));
        // quiero un empleado que tenga como rol encargado de ventas
        for (Empleado empleado : empleados) {
            if (empleado.getRol().equals(Rol.EncargadoVentas)) {
                ordenVenta.setEncargadoVenta(empleado);
                break;
            }
        }
        for (Empleado empleado : empleados) {
            if (empleado.getRol().equals(Rol.Repartidor)) {
                ordenVenta.setRepartidor(empleado);
                break;
            }
        }
        ordenVenta.setCliente(clientes.get(0));
        
        
        ordenVenta.setEstado(EstadoOrden.Pendiente);
        ordenVenta.setFechaCreacion(new Date());
        ordenVenta.setTipoVenta(TipoVenta.Delivery);
        ordenVenta.setMetodoPago(MetodoPago.Efectivo);
        ordenVenta.setPorcentajeDescuento(0);
        
        ArrayList<LineaOrden> lineasOrden = new ArrayList<>();
        for(int i=0; i<1; i++){
            LineaOrden lineaOrden = new LineaOrden();
            lineaOrden.setProducto(productos.get(i));
            lineaOrden.setCantidad(2);
            lineaOrden.setSubtotal(lineaOrden.getCantidad() + lineaOrden.getProducto().getPrecioUnitario());
            lineasOrden.add(lineaOrden);
        }
                
        ordenVenta.setLineasOrden(lineasOrden);
        // calculamos el total
        double total = 0;
        for (LineaOrden lineaOrden : lineasOrden) {
            total += lineaOrden.getSubtotal();
        }
        ordenVenta.setTotal(total);
        int resultado = daoOrdenVenta.insertar(ordenVenta);
        if(resultado > 0){
            System.out.println("Orden de venta creada");
        }else{
            System.out.println("Error al insertar orden de venta");
        }
        
        // ACTUALIZAR ODRDEN DE VENTA
        ordenVenta.setEstado(EstadoOrden.Entregado);
        ordenVenta.setFechaEntrega(new Date());
        resultado = daoOrdenVenta.modificar(ordenVenta);
        if(resultado > 0){
            System.out.println("Orden de venta actualizada correctamente");
        }else{
            System.out.println("Error al actualizar la orden de venta");
        }
        
    }

    /**
     * @param args the command line arguments
   */
  public static void main(String[] args) {
    //testProductos();
    testOrdenVenta();
  }
}
