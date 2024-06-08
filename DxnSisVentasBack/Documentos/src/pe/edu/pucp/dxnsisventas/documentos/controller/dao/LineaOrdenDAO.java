/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.dxnsisventas.documentos.controller.dao;

import java.util.ArrayList;
import pe.edu.pucp.dxnsisventas.documentos.model.LineaOrden;

/**
 *
 * @author Candi
 */
public interface LineaOrdenDAO {
    int insertar(LineaOrden lineaOrden, int id_orden);
    ArrayList<LineaOrden> listar(int id_orden);
}