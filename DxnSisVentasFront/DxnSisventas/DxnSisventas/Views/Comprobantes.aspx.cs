using DxnSisventas.DxnWebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
  public partial class Comprobantes : System.Web.UI.Page
  {
    private DocumentosAPIClient documentosAPIClient;
    private BindingList<comprobante> BlComprobantes;

    protected void Page_Init(object sender, EventArgs e)
    {
      Page.Title = "Comprobantes";
      documentosAPIClient = new DocumentosAPIClient();
      CargarTabla("");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
      bool flag = CargarTabla(TxtBuscar.Text);
      if (flag)
      {
        MostrarMensaje($"Se encontraron {BlComprobantes.Count} comprobantes", flag);
      }
      else
      {
        MostrarMensaje("No se encontraron comprobantes", flag);
      }
    }

    protected void BtnAgregar_Click(object sender, EventArgs e)
    {

    }

    protected void BtnEditar_Click(object sender, EventArgs e)
    {

    }

    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
      int idComprobante = int.Parse(((LinkButton)sender).CommandArgument);
      int res = documentosAPIClient.eliminarComprobante(idComprobante);
      if (res == 1)
      {
        MostrarMensaje("Comprobante eliminado", true);
      }
      else
      {
        MostrarMensaje("No se pudo eliminar el comprobante", false);
      }
      CargarTabla("");
    }

    protected void GridComprobantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      GridComprobantes.PageIndex = e.NewPageIndex;
      GridBind();
    }

    private void GridBind()
    {
      GridComprobantes.DataSource = BlComprobantes;
      GridComprobantes.DataBind();
    }

    private bool CargarTabla(string search)
    {
      comprobante[] lista = documentosAPIClient.listarComprobante(search);
      if (lista == null)
      {
        return false;
      }
      BlComprobantes = new BindingList<comprobante>(lista.ToList());
      GridBind();
      return true;
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