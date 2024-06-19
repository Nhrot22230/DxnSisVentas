/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.webservice.services;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.sql.Connection;
import java.util.HashMap;
import java.util.Map;
import net.sf.jasperreports.engine.JasperCompileManager;
import net.sf.jasperreports.engine.JasperExportManager;
import net.sf.jasperreports.engine.JasperFillManager;
import net.sf.jasperreports.engine.JasperPrint;
import net.sf.jasperreports.engine.JasperReport;
import pe.edu.pucp.dxnsisventas.reportes.manager.ReporteManager;
import pe.edu.pucp.dxnsisventas.utils.database.DBManager;

/**
 *
 * @author Candi
 */
@WebService(serviceName = "ReportesAPI", targetNamespace="http://services.webservice.dxnsisventas.pucp.edu.pe/")
public class ReportesAPI {

  /**
   * This is a sample web service operation
   * @param txt
   * @return 
   */
  @WebMethod(operationName = "hello")
  public String hello(@WebParam(name = "name") String txt) {
    return "Hello " + txt + " !";
  }
  
  @WebMethod(operationName = "generarReporteAlmacen")
  public byte[] generarReporteAlmacen() {
    byte [] file = null;
    
    String template_path = ReportesAPI.class.getResource("/pe/edu/pucp/dxnsisventas/webservice/templates/Simple_Blue.jrxml").getPath();
    template_path = template_path.replace("%20", " ");
    
    try {
      file = ReporteManager.generarReporteAlmacen(template_path);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return file;
  }
  
  @WebMethod(operationName = "imprimirComprobante")
  public byte[] imprimirComprobante(int idComprobanteNumerico) {
    byte [] file = null;
    
    String template_path = ReportesAPI.class.getResource("/pe/edu/pucp/dxnsisventas/webservice/templates/Comprobante.jrxml").getPath();
    template_path = template_path.replace("%20", " ");
    
    try {
      file = ReporteManager.imprimirComprobante(template_path, idComprobanteNumerico);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    
    return file;
  }
   @WebMethod(operationName = "generarReporteOrdenCompra")
  public byte[] generarReporteOrdenCompra(@WebParam(name = "id") int id) {
    byte [] file = null;
    
    String template_path = ReportesAPI.class.getResource("/pe/edu/pucp/dxnsisventas/webservice/templates/OrdenCompraReport.jrxml").getPath();
    template_path = template_path.replace("%20", " ");
    
    try {
      file = ReporteManager.imprimirComprobante(template_path,id);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return file;
  }
  
    @WebMethod(operationName = "reporteOrdenVentaPDF")
    public byte[] reporteOrdenVenta() throws Exception {
        
        byte [] file = null;
    
        String template_path = ReportesAPI.class.getResource("/pe/edu/pucp/dxnsisventas/webservice/templates/OrdenesVentas.jrxml").getPath();
        template_path = template_path.replace("%20", " ");

        try {
          file = ReporteManager.generarReporteAlmacen(template_path);
        } catch (Exception ex){
          System.err.println(ex.getMessage());
        }
        return file;
   
    }
    
  
}
