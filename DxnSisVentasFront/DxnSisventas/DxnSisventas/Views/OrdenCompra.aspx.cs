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
  public partial class OrdenCompra : System.Web.UI.Page
  {
    private DocumentosAPIClient documentosAPIClient;
    private BindingList<ordenCompra> Blordenes;
    private BindingList<ordenCompra> BlordenesFiltradas;

    protected void Page_Init(object sender, EventArgs e)
    {
      Page.Title = "Ordenes de Compra";
      documentosAPIClient = new DocumentosAPIClient();
            Session["idOrdenVenta"] = null;
            Session["ordenCompra"] = null;
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
        MostrarMensaje($"Se encontraron {Blordenes.Count} ordenes de compra", flag);
      }
      else
      {
        MostrarMensaje("No se encontraron ordenes de compra", flag);
      }
    }

    private void AplicarFiltro()
    {
      BlordenesFiltradas = Blordenes;
      string fechaIni = FechaInicio.Text;
      string fechaFin = FechaFin.Text;

      if (fechaIni != "")
      {
        DateTime dateIni = Convert.ToDateTime(fechaIni);
        BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.fechaCreacion >= dateIni).ToList());
      }
      if (fechaFin != "")
      {
        DateTime dateFin = Convert.ToDateTime(fechaFin);
        BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.fechaCreacion <= dateFin).ToList());
      }
    }

    private void GridBind()
    {
      GridCompras.DataSource = BlordenesFiltradas;
      GridCompras.DataBind();
    }

    private bool CargarTabla(string search)
    {
      ordenCompra[] lista = documentosAPIClient.listarOrdenCompra(search);
      if (lista == null)
      {
        return false;
      }
      Blordenes = new BindingList<ordenCompra>(lista.ToList());
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

    protected void GridCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      GridCompras.PageIndex = e.NewPageIndex;
      GridBind();
    }

    protected void GridCompras_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void BtnAgregar_Click(object sender, EventArgs e)
    {
            Response.Redirect("/Views/OrdenCompraForm.aspx?accion=new");
    }

    protected void BtnEditar_Click(object sender, EventArgs e)
    {
            int idOrdenVenta = int.Parse(((LinkButton)sender).CommandArgument);
            Session["idOrdenVenta"] = idOrdenVenta;
            Response.Redirect("OrdenCompraForm.aspx?accion=ver");
        }

    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
      MostrarMensaje("Las ordenes de compra y venta solo pueden ser editadas", false);
    }
  }
}