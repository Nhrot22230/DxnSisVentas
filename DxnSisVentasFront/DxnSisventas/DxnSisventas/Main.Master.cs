using System;
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

    }

    protected void BtnCerrarSesion_Click(object sender, EventArgs e)
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
  }
}
