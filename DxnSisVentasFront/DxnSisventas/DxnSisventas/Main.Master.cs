﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas
{
  public partial class Main : System.Web.UI.MasterPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      ClearError();
    }

    protected void LbCerrarSesion_Click(object sender, EventArgs e)
    {
      Session["empleado"] = null;
      Session["datosCuenta"] = null;

      Response.Redirect("~/Login.aspx");
    }

    protected void LbProductos_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/Productos.aspx");
    }

    protected void LbInicio_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Home.aspx");
    }

    public void ClearError()
    {
      ErrorPanel.Visible = false;
      ErrorLabel.Text = "";
    }

    public void MostrarError(string mensaje)
    {
      ErrorPanel.CssClass = "notification-panel error-panel";
      ErrorLabel.CssClass = "error-message";
      ErrorPanel.Visible = true;
      ErrorLabel.Text = mensaje;
      ScriptManager.RegisterStartupScript(this, GetType(), "showPanel", "showErrorPanel();", true);
    }

    public void MostrarExito(string mensaje)
    {
      ErrorPanel.CssClass = "notification-panel success-panel";
      ErrorLabel.CssClass = "success-message";
      ErrorPanel.Visible = true;
      ErrorLabel.Text = mensaje;
      ScriptManager.RegisterStartupScript(this, GetType(), "showPanel", "showErrorPanel();", true);
    }

    protected void LbUsuariosEmpleados_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/PersonasEmpleados.aspx");
    }

    protected void LbUsuariosClientes_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/PersonasClientes.aspx");
    }

    protected void LbOrdenCompra_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/OrdenCompra.aspx");
    }

    protected void LbOrdenVenta_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/OrdenVenta.aspx");
    }

    protected void LbComprobante_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/Comprobantes.aspx");
    }

    protected void LbCuentasEmpleado_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/CuentasEmpleados.aspx");
    }

    protected void LbCuentasClientes_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/CuentasClientes.aspx");
    }
  }
}
