<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrdenCompraForm.aspx.cs" Inherits="DxnSisventas.Views.OrdenCompraForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript" src="/CustomScripts/Documentos.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <div class="container">
        <div class="card">
            <div class="card-header">
                <h2>Registrar Orden de Compra</h2>
            </div>
            <div class="card-body">
                <div class="card border">
                    <div class="card-header bg-light">
                        <h5 class="card-title mb-0">Información del Orden</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3 row">
                            <asp:Label ID="lblID" runat="server" Text="ID: " CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtID" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                            <asp:Label ID="lblEstado" runat="server" Text="Estado: " CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtEstado" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <asp:Label ID="lblFecha" runat="server" Text="Fecha:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtFecha"  runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                            
                        </div>
                    </div>
                </div>
                <div class="card border">
                    <div class="card-header bg-light">
                        <h5 class="card-title mb-0">Detalle de la Orden de Compra</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3 row">
                            <asp:Label ID="lblIDProducto" runat="server" Text="ID del Producto:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtIDProducto" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                            <asp:Button ID="btnBuscarProducto" CssClass="btn btn-primary col-sm-2" runat="server" Text="Buscar Producto" OnClick="btnBuscarProducto_Click" />
                        </div>
                        <div class="mb-3 row">
                            <asp:Label ID="lblNombreProducto" runat="server" Text="Nombre:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtNombreProducto" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <asp:Label ID="lblPrecioUnitProducto" runat="server" Text="Precio Unitario:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtPrecioUnitProducto" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <asp:Label ID="lblCantidadUnidades" type="number" runat="server" Text="Cantidad:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCantidadUnidades" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-3">
                                <asp:LinkButton ID="lbAgregarLOV" CssClass="btn btn-success" runat="server" Text=" Agregar" OnClick="lbAgregarLOV_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <asp:GridView ID="gvLineasOrdenVenta" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped">
                                <Columns>
                                    <asp:BoundField HeaderText="Nombre Producto" DataField="producto.nombre" />
                                    <asp:BoundField HeaderText="Precio Unit." DataField="producto.precioUnitario" />
                                    <asp:BoundField HeaderText="Cant" DataField="cantidad" />
                                    <asp:BoundField HeaderText="Subtotal" DataField="subtotal" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash ps-2'></i>" OnClick="btnEliminarProducto_Click" CommandArgument='<%# Eval("producto.idProductoNumerico") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="row align-items-center justify-content-end">
                            <asp:Label ID="lblTotal" runat="server" Text="Total:" CssClass="col-form-label col-sm-2 text-end"/>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtTotal" CssClass="form-control col-sm-2" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer clearfix">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar"
                    CssClass="float-start btn btn-secondary" OnClick="btnRegresar_Click1" />
                <asp:Button ID="btnEnviar" runat="server" Text="Enviar"
                 CssClass="float-end btn btn-primary" OnClick="btnEnviar_Click" />
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                    CssClass="float-end btn btn-primary" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>

    <asp:ScriptManager runat="server"></asp:ScriptManager>



    <div class="modal" id="form-modal-producto">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Búsqueda de Productos</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="container row pb-3 pt-3">
                                <div class="row align-items-center">
                                    <div class="col-auto">
                                        <asp:Label CssClass="form-label" runat="server" Text="Ingresar nombre del producto:"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" ID="txtNombreProductoModal" runat="server" OnTextChanged="txtNombreProductoModal_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBusquedaProductoModal_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="container row">
                                <asp:GridView ID="gvProductos" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="gvProductos_PageIndexChanging" OnRowDataBound="gvProductos_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Nombre del Producto" DataField="nombre" />
                                        <%-- Estamos enlazando de otra manera el precio unitario, a traves del evento OnRowDataBound --%>
                                        <asp:BoundField HeaderText="Precio Unit" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton class="btn btn-success" runat="server" Text="<i class='fa-solid fa-check ps-2'></i> Seleccionar" OnClick="btnSeleccionarProductoModal_Click" CommandArgument='<%# Eval("idProductoNumerico") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>


     <%-- Modal para enviar a un destinatario adicional --%>




<div class="modal" id="form-modal-enviar">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Agregar destinatario</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container row pb-3 pt-3">
                            <div class="row align-items-center">
                                <div class="col-auto">
                                    <asp:Label CssClass="form-label" runat="server" Text="Ingresar destinatario:"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox CssClass="form-control" ID="txtCorreo" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="EnviarModal" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Enviar" OnClick="lbEnviaroModal_Click" />
                                </div>
                            </div>
                        </div>
  
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
</div>


    <script type="text/javascript" src="/CustomScripts/Master.js"></script>

</asp:Content>
