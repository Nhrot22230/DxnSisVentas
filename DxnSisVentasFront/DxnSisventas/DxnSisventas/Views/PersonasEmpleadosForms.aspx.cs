using DxnSisventas.DxnWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
  public partial class PersonasEmpleadosForms : System.Web.UI.Page
  {
    private empleado empTemporal;
    private PersonasAPIClient personasAPIClient;

    protected void Page_Init(object sender, EventArgs e)
    {
      Page.Title = "Agregar Empleado";
      personasAPIClient = new PersonasAPIClient();
      if (Session["empleadoEditar"] != null)
      {
        empTemporal = (empleado)Session["empleadoEditar"];
        TxtId.Text = empTemporal.idEmpleadoCadena;
        TxtNombre.Text = empTemporal.nombre;
        TxtApellidoPat.Text = empTemporal.apellidoPaterno;
        TxtApellidoMat.Text = empTemporal.apellidoMaterno;
        TxtDNI.Text = empTemporal.DNI;
        TxtSueldo.Text = empTemporal.sueldo.ToString();
        DropDownListRoles.Enabled = ((empleado)Session["empleado"]).rol == rol.Administrador;
        DropDownListRoles.SelectedValue = empTemporal.rol.ToString();
      }
      else
      {
        empTemporal = new empleado();
        TxtId.Text = "";
        TxtNombre.Text = "";
        TxtApellidoPat.Text = "";
        TxtApellidoMat.Text = "";
        TxtDNI.Text = "";
        TxtSueldo.Text = "";
        DropDownListRoles.Enabled = true;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnRegresar_Click(object sender, EventArgs e)
    {
      TxtId.Text = "";
      TxtNombre.Text = "";
      TxtApellidoPat.Text = "";
      TxtApellidoMat.Text = "";
      TxtDNI.Text = "";
      TxtSueldo.Text = "";
      DropDownListRoles.Enabled = true;
      Response.Redirect("~/Views/PersonasEmpleados.aspx");
    }

    protected void BtnGuardar_Click(object sender, EventArgs e)
    {
      empTemporal.nombre = TxtNombre.Text;
      empTemporal.apellidoPaterno = TxtApellidoPat.Text;
      empTemporal.apellidoMaterno = TxtApellidoMat.Text;
      empTemporal.DNI = TxtDNI.Text;
      empTemporal.sueldo = Double.Parse(TxtSueldo.Text);
      empTemporal.rol = (rol)Enum.Parse(typeof(rol), DropDownListRoles.SelectedValue);

      if (empTemporal.idEmpleadoCadena == null)
      {
        personasAPIClient.insertarEmpleado(empTemporal);
      }
      else
      {
        personasAPIClient.actualizarEmpleado(empTemporal);
      }
      Response.Redirect("~/Views/PersonasEmpleados.aspx");
    }
  }
}