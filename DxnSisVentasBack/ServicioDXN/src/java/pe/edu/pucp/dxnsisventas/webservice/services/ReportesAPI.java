/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.webservice.services;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import pe.edu.pucp.dxnsisventas.reportes.manager.ReporteManager;

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
  public byte[] generarReporteAlmacen(@WebParam(name = "template_path") String template_path) {
    byte [] file = null;
    
    try {
      file = ReporteManager.generarReporteAlmacen(template_path);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return file;
  }
}
