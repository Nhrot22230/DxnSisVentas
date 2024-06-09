﻿using DxnSisventas.DxnWebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
  public partial class PersonasClientes : System.Web.UI.Page
  {
    private PersonasAPIClient personasAPIClient;
    private BindingList<cliente> clientes;

    private bool CargarTabla(String filtro)
    {
      cliente[] lista = personasAPIClient.listarClientes(filtro);
      if (lista == null) return false;

      clientes = new BindingList<cliente>(lista.ToList());
      GridCliente.DataSource = clientes;
      GridCliente.DataBind();
      return true;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      personasAPIClient = new PersonasAPIClient();
      CargarTabla("");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      GridCliente.PageIndex = e.NewPageIndex;
      GridCliente.DataBind();
    }
    protected void BtnAgregar_Click(object sender, EventArgs e)
    {

    }

    protected void BtnEditar_Click(object sender, EventArgs e)
    {

    }

    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
      int idCliente = Int32.Parse(((LinkButton)sender).CommandArgument);
      int res = personasAPIClient.eliminarCliente(idCliente);
      string mensaje = res > 0 ? "Cliente eliminado correctamente" : "Error al eliminar al Cliente";
      MostrarMensaje(mensaje, res > 0);

      CargarTabla("");
    }

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
      bool flag = CargarTabla(TxtBuscar.Text);
      if (flag)
      {
        MostrarMensaje($"Se encontraron {clientes.Count} empleados", flag);
      }
      else
      {
        MostrarMensaje("No se encontraron empleados", flag);
      }
    }

    private void MostrarMensaje(string mensaje, bool exito)
    {
      if (this.Master is Main master)
      {
        if (exito)
        {
          master.MostrarExito(mensaje);
        }
        else
        {
          master.MostrarError(mensaje);
        }
      }
    }
  }
}