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
  public partial class Productos : System.Web.UI.Page
  {
    private ProductosAPIClient productosAPIClient;
    private BindingList<producto> BlProductos;

    protected void Page_Init(object sender, EventArgs e)
    {
      Page.Title = "Productos";
      productosAPIClient = new ProductosAPIClient();
      CargarTabla("");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      GvProductos.PageIndex = e.NewPageIndex;
      GvProductos.DataSource = BlProductos;
      GvProductos.DataBind();
    }

    private void CargarTabla(string search)
    {
      producto[] productos = productosAPIClient.listarProductos(search);
      if (productos == null)
      {
        string mensaje = "No se encontraron productos";
        if (this.Master is Main master) master.MostrarError(mensaje);
        return;
      }

      BlProductos = new BindingList<producto>(productos.ToList());
      GvProductos.DataSource = BlProductos;
      GvProductos.DataBind();
    }
    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
      if(TxtBuscar.Text == null)
      {
        TxtBuscar.Text = "";
      }
      CargarTabla(TxtBuscar.Text);
    }
    protected void BtnAgregar_Click(object sender, EventArgs e)
    {
      // Clean modal form
      TxtNombre.Text = "";
      TxtStock.Text = "";
      TxtPrecio.Text = "";
      TxtPuntos.Text = "";
      TxtCapacidad.Text = "";
      ddlTipoProducto.SelectedValue = "0";
      ddlUnidadMedida.SelectedValue = "0";
      TxtId.Text = "";
      Session["idProducto"] = null;

      ScriptManager.RegisterStartupScript(this, GetType(), "showModalForm", "showModalForm();", true);
    }
    protected void BtnEditar_Click(object sender, EventArgs e)
    {
      int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
      producto p = BlProductos.SingleOrDefault(x => x.idProductoNumerico == idProducto);
      Session["idProducto"] = p.idProductoNumerico;
      TxtNombre.Text = p.nombre;
      TxtStock.Text = p.stock.ToString();
      TxtPrecio.Text = p.precioUnitario.ToString("N2");
      TxtPuntos.Text = p.puntos.ToString();
      TxtCapacidad.Text = p.capacidad.ToString();
      ddlTipoProducto.SelectedValue = p.tipo.ToString();
      ddlUnidadMedida.SelectedValue = p.unidadDeMedida.ToString();
      TxtId.Text = p.idProductoCadena;
      TxtId.Enabled = false;

      ScriptManager.RegisterStartupScript(this, GetType(), "showModalForm", "showModalForm();", true);
    }
    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
      int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
      productosAPIClient.eliminarProducto(idProducto);
      CargarTabla("");
    }
    protected void ButGuardar_Click(object sender, EventArgs e)
    {
      // Crear y asignar valores al objeto producto
      producto p = new producto
      {
        nombre = TxtNombre.Text,
        stock = Int32.Parse(TxtStock.Text),
        precioUnitario = Math.Round(Double.Parse(TxtPrecio.Text), 2),
        puntos = Int32.Parse(TxtPuntos.Text),
        capacidad = Double.Parse(TxtCapacidad.Text),
        tipo = (tipoProducto)Enum.Parse(typeof(tipoProducto), ddlTipoProducto.SelectedValue),
        tipoSpecified = true,
        unidadDeMedida = (unidadMedida)Enum.Parse(typeof(unidadMedida), ddlUnidadMedida.SelectedValue),
        unidadDeMedidaSpecified = true,
        idProductoNumerico = Session["idProducto"] != null ? (int)Session["idProducto"] : 0
      };

      // Insertar o actualizar producto
      int res = p.idProductoNumerico > 0 ? productosAPIClient.actualizarProducto(p) : productosAPIClient.insertarProducto(p);
      string mensaje = res > 0 ? "Producto guardado correctamente" : "Error al guardar el producto";

      // Mostrar mensaje en el panel de la página maestra
      if (this.Master is Main master)
      {
        if (res > 0)
        {
          master.MostrarExito(mensaje);
        }
        else
        {
          master.MostrarError(mensaje);
        }
      }

      // Recargar tabla de productos
      CargarTabla("");
    }

  }
}