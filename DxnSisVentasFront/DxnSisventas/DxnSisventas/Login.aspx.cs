using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxnSisventas.DxnWebService;

namespace DxnSisventas
{
  public partial class Login : System.Web.UI.Page
  {
    private CuentasAPIClient cuentasAPIClient;

    protected void Page_Init(object sender, EventArgs e)
    {
      cuentasAPIClient = new CuentasAPIClient();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      ErrorPanel.Visible = false;
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
      string usuario = username.Text;
      string pass = password.Text;

      empleado Emp = cuentasAPIClient.iniciarSesionEmpleado(usuario, pass);

      if (Emp != null)
      {
        cuentaEmpleado cuenta = new cuentaEmpleado
        {
          usuario = usuario,
          contrasena = pass
        };

        Session["datosCuenta"] = cuenta;
        Session["empleado"] = Emp;
        Response.Redirect("Home.aspx");
      }
      else
      {
        password.Text = "";
        ErrorPanel.Visible = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "showErrorPanel", "showErrorPanel();", true);
    }
    }
  }
}