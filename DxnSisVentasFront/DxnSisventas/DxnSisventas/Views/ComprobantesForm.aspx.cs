using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxnSisventas.DxnWebService;

namespace DxnSisventas.Views
{
    public partial class ComprobantesForm : System.Web.UI.Page
    {
        public string opcion = "";
        //
        private DocumentosAPIClient apiDocumentos;

        private BindingList<orden> listaOrdenes = null;
        private BindingList<ordenCompra> listaOrdenesCompra = null;
        private BindingList<ordenVenta> listaOrdenesVenta = null;
        private orden ord;
        private comprobante comprobante;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Session["Usuario"] == null)
                Response.Redirect("/Login.aspx");*/


            if (!IsPostBack)
            {
                //opcion = Request.QueryString["op"];
                /*if (opcion.Equals("upd"))
                {
                    CargarDatos();
                }*/
            }
        }

        protected void Page_Init()
        {
            apiDocumentos = new DocumentosAPIClient();
        }
        protected void CargarDatos()
        {
            if(comprobante!=null)
            {
                TxtId.Text = comprobante.idComprobanteCadena;
                TxtFecha.Text = comprobante.fechaEmision.ToString("dd-MM-yyyy");
                //TxtTotal.Text = comprobante.total.ToString("N2");
                DropDownListTipoComprobante.SelectedValue = comprobante.tipoComprobante.ToString();
                if(comprobante.ordenAsociada!=null)
                {
                    TxtIdOrden.Text = comprobante.ordenAsociada.idOrden.ToString();
                    TxtFechaOrden.Text = comprobante.ordenAsociada.fechaCreacion.ToString("dd-MM-yyyy");
                }
            }
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Comprobantes.aspx");
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            comprobante comprobante = new comprobante();
            comprobante.ordenAsociada = (orden)Session["OrdenSeleccionada"];
            comprobante.fechaEmisionSpecified = true;
            comprobante.fechaEmision = DateTime.Parse(TxtFecha.Text);
            comprobante.tipoComprobanteSpecified = true;
            comprobante.tipoComprobante = (tipoComprobante)Enum.Parse(typeof(tipoComprobante),DropDownListTipoComprobante.SelectedValue);
            //comprobante.idComprobanteNumerico = apiDocumentos.insertarComprobante(comprobante);

            int res = comprobante.idComprobanteNumerico > 0 ? apiDocumentos.actualizarComprobante(comprobante) : apiDocumentos.insertarComprobante(comprobante);
            if(res>0) Response.Redirect("/Views/Comprobantes.aspx");
            string mensaje = res > 0 ? "Comprobante guardado correctamente" : "Error al guardar el comprobante";
            MostrarMensaje(mensaje, res > 0);
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() {showModalFormOrdenes()};";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void BtnSeleccionarOrdenModal_Click(object sender, EventArgs e)
        {
            int idOrden = Int32.Parse(((LinkButton)sender).CommandArgument);
            listaOrdenes = new BindingList<orden>(apiDocumentos.listarOrden(txtCodOrdenModal.Text).ToList());
            orden ordenSeleccionada = listaOrdenes.SingleOrDefault(x => x.idOrden == idOrden);
            Session["OrdenSeleccionada"] = ordenSeleccionada;
            TxtIdOrden.Text = ordenSeleccionada.idOrden + "";
            TxtFechaOrden.Text = ordenSeleccionada.fechaCreacion.ToString("dd-MM-yyyy");
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }

        protected void txtCodOrdenModal_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void BtnBuscarModal_Click(object sender, EventArgs e)
        {
            if (apiDocumentos.listarOrdenCompra(txtCodOrdenModal.Text) != null || apiDocumentos.listarOrdenVenta(txtCodOrdenModal.Text) != null)
            {
                //listaOrdenes = new BindingList<orden>();
                listaOrdenesCompra = new BindingList<ordenCompra>(apiDocumentos.listarOrdenCompra(txtCodOrdenModal.Text).ToList());
                listaOrdenesVenta = new BindingList<ordenVenta>(apiDocumentos.listarOrdenVenta(txtCodOrdenModal.Text).ToList());
                //listaOrdenes = listaOrdenesCompra.Concat(listaOrdenesVenta);
                listaOrdenes = new BindingList<orden>(apiDocumentos.listarOrden(txtCodOrdenModal.Text).ToList());
                GridBindOrdenes();
            }
        }

        protected void gvOrdenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrdenes.PageIndex = e.NewPageIndex;
            GridBindOrdenes();
        }
        private void GridBindOrdenes()
        {
            gvOrdenes.DataSource = listaOrdenes;
            gvOrdenes.DataBind();
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
    }
}