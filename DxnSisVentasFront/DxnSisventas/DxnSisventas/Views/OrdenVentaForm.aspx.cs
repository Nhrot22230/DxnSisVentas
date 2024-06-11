using DxnSisventas.DxnWebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
    public partial class OrdenVentaForm : System.Web.UI.Page
    {
        // apis
        private DocumentosAPIClient apiDocumentos;
        private PersonasAPIClient apiPersonas;
        private ProductosAPIClient apiProductos;

        // listas
        private BindingList<empleado> empleados;
        private BindingList<producto> productos;
        private BindingList<cliente> clientes;

        //
        private ordenVenta ordenVenta;

        protected void Page_Load(object sender, EventArgs e)
        {
            apiDocumentos = new DocumentosAPIClient();
            apiPersonas = new PersonasAPIClient();
            apiProductos = new ProductosAPIClient();

            llenarGridEmpleados("");
            llenarGridProductos("");
            llenarGridClientes("");

        }

        private void llenarGridEmpleados(String busqueda)
        {
            empleado[] empleadosAux = apiPersonas.listarEmpleados(busqueda);
            
            if(empleadosAux == null)
            {
                empleados = new BindingList<empleado>();
            }
            else
            {
                empleados = new BindingList<empleado>(empleadosAux.ToList());
            }
            gvEmpleados.DataSource = empleados;
            gvEmpleados.DataBind();
        }


        private void llenarGridProductos(String busqueda)
        {
            producto[] productosAxu = apiProductos.listarProductos(busqueda);
            if (productosAxu == null)
            {
                productos = new BindingList<producto>();
            }
            else
            {
                productos = new BindingList<producto>(productosAxu.ToList());
            }
            gvProductos.DataSource = productos;
            gvProductos.DataBind();
        }

        private void llenarGridClientes(String busqueda)
        {
            cliente[] clientesAux = apiPersonas.listarClientes(busqueda);
            if (clientesAux == null)
            {
                clientes = new BindingList<cliente>();
            }
            else
            {
                clientes = new BindingList<cliente>(clientesAux.ToList());
            }
            gvClientes.DataSource = clientes;
            gvClientes.DataBind();
        }

        private void CallJavascript(string function, string modalId)
        {
            string script = $"window.onload = function() {{ {function}('{modalId}'); }};";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }



        protected void BtnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void gvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmpleados.PageIndex = e.NewPageIndex;
            gvEmpleados.DataBind();
        }

        protected void btnSeleccionarModalEmpleado_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            empleado empleado = empleados.Where(c => c.idEmpleadoNumerico == idEmpleado).FirstOrDefault();
            Session["empleadoSeleccionado"] = empleado;
            TxtIDRepartidor.Text = empleado.idEmpleadoCadena;
            TxtNombreCompletoRepartidor.Text = empleado.nombre + " " +
                empleado.apellidoPaterno + " " + empleado.apellidoMaterno;
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }

        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClientes.PageIndex = e.NewPageIndex;
            gvClientes.DataBind();
        }

        protected void btnSeleccionarModalCliente_Click(object sender, EventArgs e)
        {
            int idCliente = Int32.Parse(((LinkButton)sender).CommandArgument);
            cliente cliente = clientes.Where(c => c.idNumerico == idCliente).FirstOrDefault();
            Session["clienteSeleccionado"] = cliente;
            TxtIDCliente.Text = cliente.idCadena.ToString();
            TxtNombreCompletoCliente.Text = cliente.nombre + " " +
                cliente.apellidoPaterno + " " + cliente.apellidoMaterno;
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }

        protected void lbBuscarClienteModal_Click(object sender, EventArgs e)
        {
            string busqueda = TxtPatronBusquedaCliente.Text;
            llenarGridClientes(busqueda);
        }

        protected void lbBuscarProductos_Click(object sender, EventArgs e)
        {
            string busqueda = TxtPatronBusquedaProducto.Text;
            llenarGridProductos(busqueda);
        }

        protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductos.PageIndex = e.NewPageIndex;
            gvProductos.DataBind();
        }

        protected void btnSeleccionarModalProducto_Click(object sender, EventArgs e)
        {
            int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
            producto producto = productos.Where(c => c.idProductoNumerico == idProducto).FirstOrDefault();
            Session["productoSeleccionado"] = producto;
            TxtIdProducto.Text = producto.idProductoCadena;
            TxtNombreProducto.Text = producto.nombre;
            TxtPrecio.Text = producto.precioUnitario.ToString("N2");
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }

        protected void lbBuscarEmpleadoModal_Click(object sender, EventArgs e)
        {
            string busqueda = TxtPatronBusquedaEmpleado.Text;
            llenarGridEmpleados(busqueda);
        }

        protected void lbBuscarCliente_Click(object sender, EventArgs e)
        {
            CallJavascript("showModalForm", "modalFormBuscarCliente");
        }

        protected void lbBuscarRepartidor_Click(object sender, EventArgs e)
        {
            CallJavascript("showModalForm", "modalFormBuscarEmpleado");
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            CallJavascript("showModalForm", "modalFormBuscarProducto");
        }
    }
}