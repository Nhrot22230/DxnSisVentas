/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.webservice.services;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import pe.edu.pucp.dxnsisventas.UtilCorreo.correoAccess.EnvioCorreos;
import pe.edu.pucp.dxnsisventas.UtilCorreo.correoDAO.CorreoDAO;

/**
 *
 * @author GianLuka
 */
@WebService(serviceName = "CorreoAPI")
public class CorreoAPI {
        private CorreoDAO daoCorreo;
    /**
     * This is a sample web service operation
     */
        
    public CorreoAPI(){
        daoCorreo = new EnvioCorreos();
    }
    @WebMethod(operationName = "enviarCorreoWeb")
    public int enviarCorreoWeb(@WebParam(name = "asunto") String asunto,
            @WebParam(name = "contenido") String contenido,@WebParam(name = "correo") String correo) {
        int resultado=0;
        
         try {
                daoCorreo.enviarCorreo(asunto,contenido,correo);
                   resultado = 1;
        } catch (Exception ex){
                System.err.println(ex.getMessage());
        }
        
        return resultado;
    }
}
