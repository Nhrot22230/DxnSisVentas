/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package tester;

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

  /**
   * @param args the command line arguments
   */
  public static void main(String[] args) {
    // TODO code application logic here
    testProductos();
  }
}
