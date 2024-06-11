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
        private BindingList<lineaOrden> lineasOrden;

        private static string accion;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            apiDocumentos = new DocumentosAPIClient();
            apiPersonas = new PersonasAPIClient();
            apiProductos = new ProductosAPIClient();

            accion = Request.QueryString["accion"];
            if (accion.Equals("new"))
            {
                ordenVenta = new ordenVenta();
                if (!IsPostBack)
                {
                    Session["lineasDeOrden"] = null;
                    Session["empleadoSeleccionado"] = null;
                    Session["clienteSeleccionado"] = null;
                    Session["productoSeleccionado"] = null;

                }

    
            }
            else if (accion.Equals("editar"))
            {
                ordenVenta = (ordenVenta)Session["ordenSeleccionada"];
                mostrarDatos();            
            }
            llenarGridEmpleados("");
            llenarGridProductos("");
            llenarGridClientes("");
            llenarGridLineas();
                        
        }

        private void mostrarDatos()
        {
            // Información de la orden de venta
            TxtIdOrdenVenta.Text = ordenVenta.idOrdenVentaCadena;
            ddlEstado.SelectedValue = ordenVenta.estado.ToString();
            ddlMetodoDePago.SelectedValue = ordenVenta.metodoPago.ToString();
            ddlTipoVenta.SelectedValue = ordenVenta.tipoVenta.ToString();
            TxtFechaCreacion.Text = ordenVenta.fechaCreacion.ToShortDateString();
            TxtFechaEntrega.Text = ordenVenta.fechaEntrega.ToShortDateString();
            TxtDescuento.Text = ordenVenta.porcentajeDescuento.ToString();
            
            // información del cliente
            cliente cliente = ordenVenta.cliente;
            Session["clienteSeleccionado"] = cliente;
            TxtIDCliente.Text = ordenVenta.cliente.idCadena;
            TxtNombreCompletoCliente.Text = cliente.nombre + " " +
                cliente.apellidoPaterno + " " + cliente.apellidoMaterno;

            // información del repartidor

            empleado empleado = ordenVenta.repartidor;
            Session["empleadoSeleccionado"] = empleado;
            TxtIDRepartidor.Text = empleado.idEmpleadoCadena;
            TxtNombreCompletoRepartidor.Text = empleado.nombre + " " +
                empleado.apellidoPaterno + " " + empleado.apellidoMaterno;

            // detalle de la orden de venta
            Session["lineasDeOrden"] = new BindingList<lineaOrden>(ordenVenta.lineasOrden.ToList());
        }

        private void llenarGridLineas()
        {
            if (Session["lineasDeOrden"] == null)
            {
                lineasOrden = new BindingList<lineaOrden>();
            }
            else
            {
                lineasOrden = (BindingList<lineaOrden>)Session["lineasDeOrden"];
            }
            gvLineasOrdenVenta.DataSource = lineasOrden;
            gvLineasOrdenVenta.DataBind();
        }


        private void llenarGridEmpleados(String busqueda)
        {  
            empleado[] empleadosAux = apiPersonas.listarEmpleados(busqueda).Where(e => e.rol == rol.Repartidor).ToArray();
            
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
            int idProducto = Int32.Parse(((Button)sender).CommandArgument);
            lineaOrden linea = lineasOrden.Where(lo => lo.producto.idProductoNumerico == idProducto).FirstOrDefault();
            lineasOrden.Remove(linea);
            Session["lineasDeOrden"] = lineasOrden;
            txtTotal.Text = lineasOrden.Sum(lo => lo.subtotal).ToString("N2");
            llenarGridLineas();
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

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // Verificar selección de producto
            if (Session["productoSeleccionado"] == null)
            {
                MostrarMensaje("Debe seleccionar un producto", false);
                return;
            }

            producto productoSeleccionado = (producto)Session["productoSeleccionado"];

            // Validar cantidad ingresada
            if (!int.TryParse(TxtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MostrarMensaje("Ingrese una cantidad válida", false);
                return;
            }

            // Verificar stock disponible
            if (productoSeleccionado.stock < cantidad)
            {
                MostrarMensaje("La cantidad supera el stock disponible", false);
                return;
            }

            // Buscar si el producto ya está en la lista
            lineaOrden lineaExistente = lineasOrden.FirstOrDefault(lo => lo.producto.idProductoNumerico == productoSeleccionado.idProductoNumerico);
            if (lineaExistente != null)
            {
                // verificar si la cantidad supera el stock
                if (lineaExistente.cantidad + cantidad > productoSeleccionado.stock)
                {
                    MostrarMensaje("La cantidad supera el stock disponible", false);
                    return;
                }
                // Si el producto ya existe, actualiza la cantidad y subtotal
                lineaExistente.cantidad += cantidad;
                lineaExistente.subtotal = CalcularSubtotal(lineaExistente.cantidad, lineaExistente.producto.precioUnitario);
            }
            else
            {
                // Si el producto no existe, añade una nueva línea
                lineaOrden nuevaLinea = CrearNuevaLineaOrden(productoSeleccionado, cantidad);
                lineasOrden.Add(nuevaLinea);
                
            }
            
            Session["lineasDeOrden"] = lineasOrden;
            txtTotal.Text = lineasOrden.Sum(lo => lo.subtotal).ToString("N2");
            llenarGridLineas();
            // Limpiar campos del formulario
            LimpiarCamposProducto();
        }

        // Método para calcular subtotal
        private double CalcularSubtotal(int cantidad, double precioUnitario)
        {
            return cantidad * precioUnitario;
        }

        // Método para crear una nueva línea de orden
        private lineaOrden CrearNuevaLineaOrden(producto productoSeleccionado, int cantidad)
        {
            return new lineaOrden
            {
                producto = productoSeleccionado,
                cantidad = cantidad,
                subtotal = CalcularSubtotal(cantidad, productoSeleccionado.precioUnitario)
            };
        }

        // Método para limpiar campos del formulario
        private void LimpiarCamposProducto()
        {
            TxtIdProducto.Text = string.Empty;
            TxtNombreProducto.Text = string.Empty;
            TxtPrecio.Text = string.Empty;
            TxtCantidad.Text = string.Empty;
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ordenVenta.estadoSpecified = true;
            ordenVenta.estado = (estadoOrden)Enum.Parse(typeof(estadoOrden), 
                ddlEstado.SelectedValue.ToString());
            ordenVenta.metodoPagoSpecified = true;
            ordenVenta.metodoPago = (metodoPago)Enum.Parse(typeof(metodoPago), 
                ddlMetodoDePago.SelectedValue.ToString());
            ordenVenta.tipoVentaSpecified = true;
            ordenVenta.tipoVenta = (tipoVenta)Enum.Parse(typeof(tipoVenta),
                ddlTipoVenta.SelectedValue.ToString());
            ordenVenta.fechaCreacionSpecified = true;
            ordenVenta.fechaCreacion = DateTime.Parse(TxtFechaCreacion.Text);
            ordenVenta.fechaEntregaSpecified = true;
            if (!string.IsNullOrWhiteSpace(TxtFechaEntrega.Text))
            {
                ordenVenta.fechaEntrega = DateTime.Parse(TxtFechaEntrega.Text);
            }
            ordenVenta.porcentajeDescuento = Double.Parse(TxtDescuento.Text);
            ordenVenta.cliente = (cliente)Session["clienteSeleccionado"];
            ordenVenta.repartidor = (empleado)Session["empleadoSeleccionado"];
            ordenVenta.encargadoVenta = (empleado)Session["empleado"];
            ordenVenta.lineasOrden = ((BindingList<lineaOrden>)Session["lineasDeOrden"]).ToArray();
            if(accion.Equals("new"))
            {
                int res = apiDocumentos.insertarOrdenVenta(ordenVenta);
                string mensaje = res > 0 ? "Orden de venta guardada correctamente" : "Error al guardar la orden de venta";
                MostrarMensaje(mensaje, res > 0);
            }
            else if(accion.Equals("editar"))
            {
                int res = apiDocumentos.actualizarOrdenVenta(ordenVenta);
                string mensaje = res > 0 ? "Orden de venta actualizada correctamente" : "Error al actualizar la orden de venta";
                MostrarMensaje(mensaje, res > 0);
            }
            Response.Redirect("OrdenVenta.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrdenVenta.aspx");
        }
    }
}