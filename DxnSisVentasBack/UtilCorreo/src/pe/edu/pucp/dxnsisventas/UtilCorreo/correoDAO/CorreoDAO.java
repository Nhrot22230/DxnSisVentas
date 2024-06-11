/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.UtilCorreo.correoDAO;

/**
 *
 * @author GianLuka
 */
public interface CorreoDAO {
    
    
    int enviarCorreo(String asunto, String contenido, String correo);
}
