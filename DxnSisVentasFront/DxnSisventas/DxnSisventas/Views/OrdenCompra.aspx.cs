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
            Session["lineasOrdenVenta"] = null;
            CargarTabla("");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnBuscar_Click(object sender, EventArgs e)

    {
            if (TxtBuscar.Text.ToString() != "")
            {
                if (int.Parse(TxtBuscar.Text) <= 0)
                {
                    MostrarMensaje("Ingrese un Nro de orden correcto", false);
                    return;
                }
            }
            
            bool flag = CargarTabla(TxtBuscar.Text);
            AplicarFiltro();
      if (flag)
      {
        MostrarMensaje($"Se encontraron {BlordenesFiltradas.Count} ordenes de compra", flag);
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
      string min = TxtMontoMin.Text;
      string max = TxtMontoMax.Text;

            if (TxtBuscar.Text.ToString()!= "")
      {
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.idOrdenCompraNumerico == (int.Parse(TxtBuscar.Text))).ToList());
      }
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
      if(Estado.SelectedValue != "Todos")
      {
         BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.estado.ToString() == Estado.SelectedValue).ToList());
            
        }

            if (min != "")
            {
                double montomin = double.Parse(min);
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.total >= montomin).ToList());
            }
            if (max != "")
            {
                double montomax = double.Parse(max);
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.total <= montomax).ToList());
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
            AplicarFiltro();
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

    protected bool verificarFechas()
    {
       string fechaIni = FechaInicio.Text;
       string fechaFin = FechaFin.Text;
       
       if (fechaIni != "" && fechaFin != "")
       {
          DateTime dateIni = Convert.ToDateTime(fechaIni);
          DateTime dateFin = Convert.ToDateTime(fechaFin);
          if (dateIni > dateFin)
          {
                    return false;
          }
       }
            return true;
    }

    protected void FechaInicio_TextChanged(object sender, EventArgs e)
    {
      if (!verificarFechas())
      {
            MostrarMensaje("Ingrese un rango de fechas correcto", verificarFechas());
                return ;
      }
            GridCompras.PageIndex = 0;
            AplicarFiltro();
      GridBind();
      MostrarMensaje("Se aplico el filtro", true);
    }

    protected void FechaFin_TextChanged(object sender, EventArgs e)
    {
            if (!verificarFechas())
            {
                MostrarMensaje("Ingrese un rango de fechas correcto", verificarFechas());
                return;
            }
            GridCompras.PageIndex = 0;
            AplicarFiltro();
      GridBind();
      MostrarMensaje("Se aplico el filtro", true);
    }

    protected void GridCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      GridCompras.PageIndex = e.NewPageIndex;
      AplicarFiltro();
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

        protected void Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            GridCompras.PageIndex = 0;
            GridBind();
            MostrarMensaje("Se aplico el filtro", true);

        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            // Lógica para limpiar todos los filtros
            TxtBuscar.Text = string.Empty;
            FechaInicio.Text = string.Empty;
            FechaFin.Text = string.Empty;
            Estado.SelectedIndex = 0;
            TxtMontoMax.Text = string.Empty;
            TxtMontoMin.Text = string.Empty;
            // Selecciona "Todos"
            GridCompras.PageIndex = 0;
            GridCompras.DataSource = Blordenes;
            GridBind();
        }
        protected bool verificarMontos()
        {
            string min = TxtMontoMin.Text;
            string max = TxtMontoMax.Text;

            if (min != "" && max != "")
            {
                double montomin = double.Parse(min);
                double montomax = double.Parse(max);
                if (montomin > montomax)
                {
                    return false;
                }
            }
            return true;
        }
        protected void TxtMontoMin_TextChanged(object sender, EventArgs e)
        {
            if (!verificarMontos())
            {
                MostrarMensaje("Ingrese un rango de montos correcto", verificarMontos());
                return;
            }
            GridCompras.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            MostrarMensaje("Se aplico el filtro", true);
        }

        protected void TxtMontoMax_TextChanged(object sender, EventArgs e) 
        {

            if (!verificarMontos())
            {
                MostrarMensaje("Ingrese un rango de montos correcto", verificarMontos());
                return;
            }
            GridCompras.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            MostrarMensaje("Se aplico el filtro", true);
        }

        protected void TxtBuscar_TextChanged(object sender, EventArgs e)
        {

            if (TxtBuscar.Text.ToString() != "")
            {
                if (int.Parse(TxtBuscar.Text) <= 0)
                {
                    MostrarMensaje("Ingrese un Nro de orden correcto", false);
                    return;
                }
            }

            GridCompras.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            MostrarMensaje("Se aplico el filtro", true);
        }
    }
}