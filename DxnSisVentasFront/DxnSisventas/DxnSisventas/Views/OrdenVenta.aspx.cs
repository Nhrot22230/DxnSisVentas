using DxnSisventas.DxnWebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
  public partial class OrdenVenta : System.Web.UI.Page
  {
    private DocumentosAPIClient documentosAPIClient;
    private BindingList<ordenVenta> Blordenes;
    private BindingList<ordenVenta> BlordenesFiltradas;

    protected void Page_Init(object sender, EventArgs e)
    {
      Page.Title = "Ordenes de Venta";
      documentosAPIClient = new DocumentosAPIClient();
      CargarTabla("");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    private void AplicarFiltro()
    {
      BlordenesFiltradas = Blordenes;
      string fechaIni = FechaInicio.Text;
      string fechaFin = FechaFin.Text;

      if (fechaIni != "")
      {
        DateTime dateIni = Convert.ToDateTime(fechaIni);
        BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.Where(x => x.fechaCreacion >= dateIni).ToList());
      }
      if (fechaFin != "")
      {
        DateTime dateFin = Convert.ToDateTime(fechaFin);
        BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.Where(x => x.fechaCreacion <= dateFin).ToList());
      }
    }

    private void GridBind()
    {
      GridVentas.DataSource = BlordenesFiltradas;
      GridVentas.DataBind();
    }

    private bool CargarTabla(string search)
    {
      ordenVenta[] lista = documentosAPIClient.listarOrdenVenta(search);
      if (lista == null)
      {
        return false;
      }
      Blordenes = new BindingList<ordenVenta>(lista.ToList());
      BlordenesFiltradas = Blordenes;
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
    protected void FechaInicio_TextChanged(object sender, EventArgs e)
    {
      AplicarFiltro();
      GridBind();
      MostrarMensaje("Se aplico el filtro", true);
    }

    protected void FechaFin_TextChanged(object sender, EventArgs e)
    {
      AplicarFiltro();
      GridBind();
      MostrarMensaje("Se aplico el filtro", true);
    }

    protected void GridVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      GridVentas.PageIndex = e.NewPageIndex;
      GridBind();
    }

    protected void BtnAgregar_Click(object sender, EventArgs e)
    {

    }

    protected void BtnEditar_Click(object sender, EventArgs e)
    {

    }

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
      bool flag = CargarTabla(TxtBuscar.Text);
      if (flag)
      {
        MostrarMensaje($"Se encontraron {Blordenes.Count} ordenes de venta", flag);
      }
      else
      {
        MostrarMensaje("No se encontraron ordenes de venta", flag);
      }
    }
  }
}