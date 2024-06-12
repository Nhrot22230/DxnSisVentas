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
    public partial class OrdenCompraForm : System.Web.UI.Page
    {
        private ProductosAPIClient apiProducto;
        private DocumentosAPIClient apiOrdenCompra;
        private CorreoAPIClient apiCorreo;

        // listas
        private BindingList<producto> listaProductos = null;
        private ordenCompra ordenCompra;
        private static List<lineaOrden> lineasOrden;
   
        static OrdenCompraForm()
        {
            lineasOrden = new List<lineaOrden>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            apiProducto = new ProductosAPIClient();
            apiOrdenCompra = new DocumentosAPIClient();
            apiCorreo = new CorreoAPIClient();
            if (apiProducto.listarProductos(txtNombreProductoModal.Text) != null)
            {
                listaProductos = new BindingList<producto>(apiProducto.listarProductos(txtNombreProductoModal.Text).ToList());
                gvProductos.DataSource = listaProductos;
                gvProductos.DataBind();
            }

            String accion = Request.QueryString["accion"];
            if (accion != null && accion == "ver" && Session["idOrdenVenta"] != null)
            {
                int idOrdenVenta = (int)Session["idOrdenVenta"];
                ordenCompra = apiOrdenCompra.listarOrdenCompra(idOrdenVenta.ToString()).FirstOrDefault();
                //denCompra.lineasOrden = apiOrdenCompra.listarLineaOrden(ordenCompra.idOrden);
                Session["lineasOrdenVenta"] = ordenCompra.lineasOrden;
                lineasOrden = ordenCompra.lineasOrden.ToList();
                mostrarDatos();
            }
            else
            {
                
                ordenCompra = new ordenCompra();
                if (!IsPostBack)
                {
                    Session["idOrdenVenta"] = null;
                    Session["lineasOrdenVenta"] = null;
                    Session["producto"] = null;
                    Session["cliente"] = null;
                }
            }
            if (Session["lineasOrdenVenta"] == null) { }

            //ordenCompra.lineasOrden = new BindingList<lineaOrden>();
            else
                gvLineasOrdenVenta.DataSource = lineasOrden;
            gvLineasOrdenVenta.DataBind();
            calcularTotal();
            gvLineasOrdenVenta.DataSource = lineasOrden;
            gvLineasOrdenVenta.DataBind();
        }
        public void mostrarDatos()
        {
            txtEstado.Text = ordenCompra.estado.ToString();
            txtID.Text = ordenCompra.idOrden.ToString();
            txtFecha.Text = ordenCompra.fechaCreacion.ToShortDateString();
            btnBuscarProducto.Enabled = false;
            btnGuardar.Enabled = false;
            lbAgregarLOV.Enabled = false;
            txtCantidadUnidades.Enabled = false;

        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
          
            Response.Redirect("/Views/OrdenesCompra.aspx");

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

            Response.Redirect("/Views/OrdenesCompra.aspx");
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showModalFormProducto() };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);


        }

        protected void lbBusquedaProductoModal_Click(object sender, EventArgs e)
        {
            if (apiProducto.listarProductos(txtNombreProductoModal.Text) != null)
            {
                listaProductos = new BindingList<producto>(apiProducto.listarProductos(txtNombreProductoModal.Text).ToList());
                gvProductos.DataSource = listaProductos;
                gvProductos.DataBind();
            }
        }

        protected void btnSeleccionarProductoModal_Click(object sender, EventArgs e)
        {
            int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
            producto productoSeleccionado = listaProductos.SingleOrDefault(x => x.idProductoNumerico == idProducto);
            Session["producto"] = productoSeleccionado;
            txtNombreProducto.Text = productoSeleccionado.nombre;
            txtPrecioUnitProducto.Text = productoSeleccionado.precioUnitario.ToString("N2");
            txtIDProducto.Text = productoSeleccionado.idProductoCadena.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }


        protected void gvOrdenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[1].Text = (DataBinder.Eval(e.Row.DataItem, "idOrdenVen")).ToString();
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = ((DateTime)DataBinder.Eval(e.Row.DataItem, "fechaCreacion")).ToString();
            }
        }

        protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductos.PageIndex = e.NewPageIndex;
            gvProductos.DataSource = listaProductos;
            gvProductos.DataBind();
        }
        public void calcularTotal()
        {
            ordenCompra.total = 0;
            foreach (lineaOrden lovb in lineasOrden)
                ordenCompra.total += lovb.subtotal;
            txtTotal.Text = ordenCompra.total.ToString("N2");
        }
        protected void lbAgregarLOV_Click(object sender, EventArgs e)
        {
            if (Session["producto"] == null)
            {
                Response.Write("No puede añadir sin haber seleccionado un producto...");
                return;
            }
            if (txtCantidadUnidades.Text.Trim().Equals(""))
            {
                Response.Write("Debe ingresar una cantidad de unidades...");
                return;
            }

            lineaOrden lov = new lineaOrden();
            lov.producto = (producto)Session["producto"];
            lov.cantidad = Int32.Parse(txtCantidadUnidades.Text);
            lov.subtotal = lov.producto.precioUnitario * lov.cantidad;
            lineasOrden.Add(lov);

            Session["lineasOrdenVenta"] = lineasOrden;

            gvLineasOrdenVenta.DataSource = lineasOrden;
            gvLineasOrdenVenta.DataBind();

            calcularTotal();

            txtIDProducto.Text = "";
            txtNombreProducto.Text = "";
            txtPrecioUnitProducto.Text = "";
            txtCantidadUnidades.Text = "";
        }


        protected void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument);
            lineaOrden linea = lineasOrden.Find(l => l.producto.idProductoNumerico == id);
            lineasOrden.Remove(linea);
            calcularTotal();
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ordenCompra orden = new ordenCompra
            {
                total = double.Parse(txtTotal.Text),
                fechaCreacionSpecified = true,
                estadoSpecified = true,
                fechaCreacion = DateTime.Now,
                estado = estadoOrden.Pendiente,
            };
            orden.lineasOrden = new ArraySegment<lineaOrden>().ToArray();
            for (int i = 0; i < lineasOrden.Count; i++)
            {
                orden.lineasOrden.Append(lineasOrden[i]);
            }
            orden.lineasOrden = lineasOrden.ToArray();


            //Validacion de que exista por lo menos 1 linea de orden de venta
            if (lineasOrden == null || lineasOrden.Count() <= 0 || orden.lineasOrden.Count() == 0)
            {
                Response.Write("Debe agregar un producto...");
                return;
            }


            int resultado = apiOrdenCompra.insertarOrdenCompra(orden);
            lineasOrden.Clear();
            Response.Redirect("/Views/OrdenCompra.aspx");
        }

        protected void btnRegresar_Click1(object sender, EventArgs e)
        {
            lineasOrden.Clear();
            Response.Redirect("/Views/OrdenCompra.aspx");
        }

        protected void txtNombreProductoModal_TextChanged(object sender, EventArgs e)
        {
            if (apiProducto.listarProductos(txtNombreProductoModal.Text) != null)
            {
                listaProductos = new BindingList<producto>(apiProducto.listarProductos(txtNombreProductoModal.Text).ToList());
                gvProductos.DataSource = listaProductos;
                gvProductos.DataBind();
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showModalFormEnviar() };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected string CrearContenido()
        {
            string contenido = "<html><body>";
            contenido += "<h2>Saludos Estimado(a),</h2>";
            contenido += "<p>Adjunto encontrará la orden de compra Nro: " + ordenCompra.idOrdenCompraCadena.ToString() + ".</p>";
            contenido += "<p>A continuación, detallamos los productos:</p>";
            contenido += "<table border='1'>";
            contenido += "<tr><th>ID</th><th>Nombre</th><th>Cantidad</th></tr>";
            foreach (lineaOrden x in ordenCompra.lineasOrden)
            {
                contenido += "<tr>";
                contenido += "<td style='text-align:center'>" + x.producto.idProductoCadena.ToString() + "</td>";
                contenido += "<td>" + x.producto.nombre.ToString() + "</td>";
                contenido += "<td style='text-align:center'>" + x.cantidad.ToString() + "</td>";
                contenido += "</tr>";
            }
            contenido += "</table>";
            contenido += "<p>Quedamos a disposición para cualquier consulta.</p>";
            contenido += "<p>Atentamente,<br>Tu Nombre</p>";
            contenido += "</body></html>";

            return contenido;
        }
        protected void lbEnviaroModal_Click(object sender, EventArgs e)
        {

            string asunto = "Orden de compra Nro: " + ordenCompra.idOrdenCompraCadena.ToString();
            string contenido = CrearContenido();


            string correo = txtCorreo.Text.ToString();
            apiCorreo.enviarCorreoWeb(asunto, contenido, correo);
            Response.Redirect("/Views/OrdenCompra.aspx");
        }  
    }
}