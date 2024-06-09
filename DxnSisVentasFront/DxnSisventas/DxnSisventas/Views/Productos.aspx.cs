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
      CargarTabla();
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

    private void CargarTabla()
    {
      if (TxtBuscar.Text == null) TxtBuscar.Text = " ";
      producto[] productos = productosAPIClient.listarProductos(TxtBuscar.Text);
      if (productos == null)
      {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se encontraron productos');", true);
        return;
      }

      BlProductos = new BindingList<producto>(productos.ToList());
      GvProductos.DataSource = BlProductos;
      GvProductos.DataBind();
    }
    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
      CargarTabla();
    }
    protected void BtnAgregar_Click(object sender, EventArgs e)
    {

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
      //CallJavascritp("showModalForm()");
      ScriptManager.RegisterStartupScript(this, GetType(), "showModalForm", "showModalForm();", true);
    }
    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
      int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
      productosAPIClient.eliminarProducto(idProducto);
      CargarTabla();
    }
    protected void ButGuardar_Click(object sender, EventArgs e)
    {
      producto p = new producto();
      p.nombre = TxtNombre.Text;
      p.stock = Int32.Parse(TxtStock.Text);
      p.precioUnitario = Math.Round(Double.Parse(TxtPrecio.Text), 2);
      p.puntos = Int32.Parse(TxtPuntos.Text);
      p.capacidad = Double.Parse(TxtCapacidad.Text);
      p.tipo = (tipoProducto)Enum.Parse(typeof(tipoProducto), ddlTipoProducto.SelectedValue);
      p.tipoSpecified = true;
      p.unidadDeMedida = (unidadMedida)Enum.Parse(typeof(unidadMedida), ddlUnidadMedida.SelectedValue);
      p.unidadDeMedidaSpecified = true;

      if (Session["idProducto"] != null)
        p.idProductoNumerico = (int)Session["idProducto"];
      else 
        p.idProductoNumerico = 0;

      string mensaje;
      if (p.idProductoNumerico > 0)
      {
        int error = productosAPIClient.actualizarProducto(p);
        if (error != 0) mensaje = "Producto actualizado";
        else mensaje = "Error al actualizar producto";
      }
      else
      {
        int error = productosAPIClient.insertarProducto(p);
        if (error != 0) mensaje = "Producto insertado";
        else mensaje = "Error al insertar producto";
      }
      ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "');", true);
      
      CargarTabla();

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
    }
  }
}