/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.documentos.controller.dao;

import java.util.ArrayList;
import pe.edu.pucp.dxnsisventas.documentos.model.Comprobante;

/**
 *
 * @author Candi
 */
public interface ComprobanteDAO {
    ArrayList<Comprobante> listar(String cadena);
    int insertar(Comprobante comprobante);
    int eliminar(int idComprobante);
    int modificar(Comprobante comprobante);
}

