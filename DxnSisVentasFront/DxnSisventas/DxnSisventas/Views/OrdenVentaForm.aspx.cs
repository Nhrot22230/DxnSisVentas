﻿using DxnSisventas.DxnWebService;
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

        protected void Page_Init(object sender, EventArgs e)
        {
            Page.Title = "Orden de Venta";
            apiDocumentos = new DocumentosAPIClient();
            apiPersonas = new PersonasAPIClient();
            apiProductos = new ProductosAPIClient();
            accion = Request.QueryString["accion"];
            TxtFechaCreacion.Enabled = false;

            if (accion.Equals("editar"))
            {
               
                ordenVenta = (ordenVenta)Session["ordenSeleccionada"];
                mostrarDatos();
            }
            else if(accion.Equals("new"))
            {
                ordenVenta = new ordenVenta();
                TxtFechaCreacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TxtDescuento.Text = "0";
                if (!IsPostBack)
                {
                    Session["lineasDeOrden"] = null;
                    Session["empleadoSeleccionado"] = null;
                    Session["clienteSeleccionado"] = null;
                    Session["productoSeleccionado"] = null;
                }
               
            }
            llenarGridLineas();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        private void mostrarDatos()
        {
            // campos desabilitados
            TxtDescuento.Enabled = false;
            TxtCantidad.Enabled = false;

            // Información de la orden de venta
            TxtIdOrdenVenta.Text = ordenVenta.idOrdenVentaCadena;
            ddlEstado.SelectedValue = ordenVenta.estado.ToString();
            ddlMetodoDePago.SelectedValue = ordenVenta.metodoPago.ToString();
            ddlTipoVenta.SelectedValue = ordenVenta.tipoVenta.ToString();
         
 
            TxtFechaCreacion.Text = ordenVenta.fechaCreacion.ToString("yyyy-MM-dd");
            TxtFechaEntrega.Text = ordenVenta.fechaEntrega.ToString("yyyy-MM-dd");
            TxtDescuento.Text = ordenVenta.porcentajeDescuento.ToString();
            
            // información del cliente
            cliente cliente = ordenVenta.cliente;
            Session["clienteSeleccionado"] = cliente;
            TxtIDCliente.Text = ordenVenta.cliente.idCadena;
            TxtNombreCompletoCliente.Text = cliente.nombre + " " +
                cliente.apellidoPaterno + " " + cliente.apellidoMaterno;

            // información del repartidor en caso exista
            if(ordenVenta.repartidor != null)
            {
                empleado empleado = ordenVenta.repartidor;
                Session["empleadoSeleccionado"] = empleado;
                TxtIDRepartidor.Text = empleado.idEmpleadoCadena;
                TxtNombreCompletoRepartidor.Text = empleado.nombre + " " +
                    empleado.apellidoPaterno + " " + empleado.apellidoMaterno;
            }
            else
            {
                TxtIDRepartidor.Text = "";
                TxtNombreCompletoRepartidor.Text = "";
            }
          
            // detalle de la orden de venta
            Session["lineasDeOrden"] = new BindingList<lineaOrden>(ordenVenta.lineasOrden.ToList());
            txtTotal.Text = ordenVenta.total.ToString("N2");
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
            empleado[] empleadosAux = apiPersonas.listarEmpleados(busqueda);
            if(empleadosAux != null)
            {
                empleadosAux = empleadosAux.Where(e => e.rol == rol.Repartidor).ToArray();
            }    
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
            llenarGridEmpleados(TxtPatronBusquedaEmpleado.Text);
            gvEmpleados.PageIndex = e.NewPageIndex;
            gvEmpleados.DataBind();
        }
        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            llenarGridClientes(TxtPatronBusquedaCliente.Text);
            gvClientes.PageIndex = e.NewPageIndex;
            gvClientes.DataBind();
        }
        protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            llenarGridProductos(TxtPatronBusquedaProducto.Text);
            gvProductos.PageIndex = e.NewPageIndex;
            gvProductos.DataBind();
        }
        protected void btnSeleccionarModalEmpleado_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);         
            llenarGridEmpleados("");
            empleado empleado = empleados.Where(c => c.idEmpleadoNumerico == idEmpleado).FirstOrDefault();
            Session["empleadoSeleccionado"] = empleado;
            TxtIDRepartidor.Text = empleado.idEmpleadoCadena;
            TxtNombreCompletoRepartidor.Text = empleado.nombre + " " +
                empleado.apellidoPaterno + " " + empleado.apellidoMaterno;
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }

       

        protected void btnSeleccionarModalCliente_Click(object sender, EventArgs e)
        {
            int idCliente = Int32.Parse(((LinkButton)sender).CommandArgument);
            llenarGridClientes("");
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

        protected void btnSeleccionarModalProducto_Click(object sender, EventArgs e)
        {
            int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
            llenarGridProductos("");
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
            llenarGridClientes("");
            CallJavascript("showModalForm", "modalFormBuscarCliente");
        }

        protected void lbBuscarRepartidor_Click(object sender, EventArgs e)
        {
            llenarGridEmpleados("");
            CallJavascript("showModalForm", "modalFormBuscarEmpleado");
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            llenarGridProductos("");
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
            double totalSinDescuento = lineasOrden.Sum(lo => lo.subtotal);

            double descuento = totalSinDescuento * (Double.Parse(TxtDescuento.Text) / 100);
            txtTotal.Text = (totalSinDescuento - descuento).ToString("N2");

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
            if(double.TryParse(TxtDescuento.Text, out double descuento) && descuento >= 0 && descuento < 100)
            {
                ordenVenta.porcentajeDescuento = descuento;
            }
            else
            {
                MostrarMensaje("Ingrese un descuento válido", false);
                return;
            }

            // Validar el cliente
            if (Session["clienteSeleccionado"] is cliente clienteSeleccionado)
            {
                ordenVenta.cliente = clienteSeleccionado;
            }
            else
            {
                MostrarMensaje("Seleccione un cliente válido.", false);
                return;
            }

            // Validar el repartidor
            if (Session["empleadoSeleccionado"] is empleado empleadoRepartidorSeleccionado)
            {
                ordenVenta.repartidor = empleadoRepartidorSeleccionado;
            }
            else if(!ddlTipoVenta.SelectedValue.Equals("Delivery"))
            {
                MostrarMensaje("Seleccione un repartidor válido.", false);
                return;
            }

            // Validar el encargado de venta
            if (Session["empleado"] is empleado empleadoEncargado)
            {
                ordenVenta.encargadoVenta = empleadoEncargado;
            }
            else
            {
                MostrarMensaje("El empleado encargado de la venta no es válido.", false);
                return;
            }

            // Validar las líneas de la orden
            if (Session["lineasDeOrden"] is BindingList<lineaOrden> lineasDeOrden && lineasDeOrden.Any())
            {
                ordenVenta.lineasOrden = lineasDeOrden.ToArray();
            }
            else
            {
                MostrarMensaje("Agregue al menos una línea a la orden.", false);
                return;
            }
            ordenVenta.total = Double.Parse(txtTotal.Text);
            int res = 0;
            string mensaje = "";
            if(accion.Equals("new"))
            {
                res = apiDocumentos.insertarOrdenVenta(ordenVenta);
                mensaje = res > 0 ? "Orden de venta registrada correctamente" : "Error al registrar la orden de venta";
            }
            else if(accion.Equals("editar"))
            {
                res = apiDocumentos.actualizarOrdenVenta(ordenVenta);
                mensaje = res > 0 ? "Orden de venta actualizada correctamente" : "Error al actualizar la orden de venta";

            }
            if (res == 0)
            {
                MostrarMensaje(mensaje, false);
                return;
            }
            Response.Redirect("OrdenVenta.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrdenVenta.aspx");
        }

        protected void ddlTipoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTipoVenta.SelectedValue.Equals("Delivery"))
            {
                panelRepartidor.Visible = true;
            }
            else
            {
                panelRepartidor.Visible = false;
                Session["empleadoSeleccionado"] = null;
                TxtIDRepartidor.Text = "";
                TxtNombreCompletoRepartidor.Text = "";

            }
        }
    }
}