﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="OrdenVentaForm.aspx.cs" Inherits="DxnSisventas.Views.OrdenVentaForm" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../CustomScripts/OrdenVenta.js"></script>

    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2 class="card-title">Orden de Venta</h2>
            </div>
            <div class="body">
                <div class="card mb-3">
                    <div class="card-header">
                        <h4>Información de la Orden de Venta
                        </h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="row mb-3">

                                <label for="TxtId" class="col-sm-2 col-form-label">ID Orden Venta:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TxtIdOrdenVenta" runat="server" Enabled="false"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                                <label for="ddlEstado" class="col-sm-2 col-form-label">Estado:</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select"
                                        SelectionMode="Single">
                                        <asp:ListItem Text="Pendiente" Enabled="true" Selected="True"
                                            Value="Pendiente"></asp:ListItem>
                                        <asp:ListItem Text="Cancelado" Enabled="true" Value="Cancelado">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Entregado" Enabled="true" Value="Entregado">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label for="ddlMetodoDePago" class="col-sm-2 col-form-label">Metodo Pago:</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlMetodoDePago" runat="server" CssClass="form-select"
                                        SelectionMode="Single">
                                        <asp:ListItem Text="Efectivo" Enabled="true" Selected="True"
                                            Value="Efectivo"></asp:ListItem>
                                        <asp:ListItem Text="Tarjeta" Enabled="true" Value="Tarjeta"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <label for="ddlTipoVenta" class="col-sm-2 col-form-label">Tipo de Venta:</label>

                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlTipoVenta" runat="server" CssClass="form-select"
                                        SelectionMode="Single">
                                        <asp:ListItem Text="Presencial" Selected="True" Value="Presencial">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row mb-3">

                                <label for="TxtFechaCreacion" class="col-sm-2 col-form-label">
                                    Fecha de
                                        creacion</label>

                                <div class="col-sm-4">
                                    <asp:TextBox ID="TxtFechaCreacion" runat="server" CssClass="form-control"
                                        type="date"></asp:TextBox>
                                </div>
                                <label for="TxtFechaEntrega" class="col-sm-2 col-form-label">
                                    Fecha de
                                        entrega</label>

                                <div class="col-sm-4">
                                    <asp:TextBox ID="TxtFechaEntrega" runat="server" CssClass="form-control"
                                        type="date"></asp:TextBox>
                                </div>
                            </div>

                             <div class="row mb-3">
                            <label for="TxtDescuento" class="col-sm-2 col-form-label">Descuento:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TxtDescuento" runat="server" 
                                    type="number" 
                                    step="0.01" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>

                        </div>

                    </div>

                </div>
                <div class="card mb-3">
                    <div class="card-header">
                        <h4>Información del Cliente
                        </h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <label for="TxtIDCliente" class="col-sm-2 col-form-label">ID Cliente:</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TxtIDCliente" runat="server" Enabled="false"
                                    CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                            <label for="TxtNombreCompletoCliente" class="col-sm-2 col-form-label">
                                Nombre
                                    completo:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TxtNombreCompletoCliente" runat="server" Enabled="false"
                                    CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:LinkButton ID="lbBuscarCliente" runat="server" CssClass="btn btn-warning"
                                    OnClick="lbBuscarCliente_Click">Buscar
                                </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-3">
                    <div class="card-header">
                        <h4>Información del Repartidor
                        </h4>
                    </div>
                    <div class="card-body">

                        <div class="row">
                            <label for="TxtIDRepartidor" class="col-sm-2 col-form-label">ID Repartidor:</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TxtIDRepartidor" runat="server" Enabled="false"
                                    CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                            <label for="TxtNombreCompletoRepartidor" class="col-sm-2 col-form-label">
                                Nombre
                                    completo:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TxtNombreCompletoRepartidor" runat="server" Enabled="false"
                                    CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:LinkButton ID="lbBuscarRepartidor" runat="server" CssClass="btn btn-warning"
                                    OnClick="lbBuscarRepartidor_Click">
                                        Buscar</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card mb-3">
                    <div class="card-header mb-3">
                        <h4>Detalle de la Orden de Venta
                        </h4>
                    </div>
                    <div class="card-body">
                        <div class="row mb-3">
                            <label for="TxtIdProducto" class="col-sm-2 col-form-label">ID Producto:</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TxtIdProducto" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btnBuscarProducto" runat="server" Text="Buscar producto"
                                    OnClick="btnBuscarProducto_Click"
                                    CssClass="btn btn-warning" />
                            </div>

                        </div>
                        <div class="row mb-3">
                            <label for="TxtNombreProducto" class="col-sm-2 col-form-label">Nombre producto:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TxtNombreProducto" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <label for="TxtPrecio" class="col-sm-1 col-form-label">Precio:</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TxtPrecio" runat="server" CssClass="form-control" Enabled="false" type="number"
                                    step="1"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label for="TxtCantidad" class="col-sm-2 col-form-label">Cantidad:</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TxtCantidad" runat="server" CssClass="form-control" type="number"
                                    step="1"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar producto"
                                    CssClass="btn btn-success" OnClick="btnAgregarProducto_Click"/>
                            </div>
                        </div>
                        <div class="row">
                            <asp:GridView ID="gvLineasOrdenVenta" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="producto.idProductoCadena" HeaderText="ID Producto" />
                                    <asp:BoundField DataField="producto.nombre" HeaderText="Producto" />
                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="producto.precioUnitario" HeaderText="Precio" />
                                    <asp:BoundField DataField="subtotal" HeaderText="Subtotal" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                                OnClick="BtnEliminar_Click" CommandArgument='<%# Eval("producto.idProductoNumerico") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="row align-items-center justify-content-end">
                            <asp:Label ID="lblTotal" runat="server" Text="Total:" CssClass="col-form-label col-sm-2 text-end" />
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtTotal" CssClass="form-control col-sm-2" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card-footer">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"
                        CssClass="float-end btn btn-primary" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                        CssClass="float-start btn btn-danger" />
                </div>
            </div>
        </div>

        
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <div id="modalFormBuscarEmpleado" class="modal" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Busqueda de Repartidor</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="container row pb-3 pt-3">
                                    <div class="row align-items-center">
                                        <div class="col-sm-8">
                                            <asp:TextBox CssClass="form-control" ID="TxtPatronBusquedaEmpleado"
                                                runat="server" placeholder="Buscar"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:LinkButton ID="lbBuscarEmpleadoModal" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar"
                                                OnClick="lbBuscarEmpleadoModal_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="container row">
                                    <asp:GridView ID="gvEmpleados" runat="server" AllowPaging="true"
                                        PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped"
                                        OnPageIndexChanging="gvEmpleados_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idEmpleadoCadena" HeaderText="Id" />
                                            <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:TemplateField HeaderText="Apellidos">
                                                <ItemTemplate>
                                                    <%# Eval("apellidoPaterno") + " " + Eval("apellidoMaterno") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton class="btn btn-success"
                                                        runat="server"
                                                        Text="<i class='fa-solid fa-check ps-2'></i> Seleccionar"
                                                        ID="btnSeleccionarModalEmpleado"
                                                        OnClick="btnSeleccionarModalEmpleado_Click"
                                                        CommandArgument='<%# Eval("idEmpleadoNumerico") %>' />
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

        <div id="modalFormBuscarCliente" class="modal" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Busqueda de Clientes</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="container row pb-3 pt-3">
                                    <div class="row align-items-center">
                                        <div class="col-sm-8">
                                            <asp:TextBox CssClass="form-control" ID="TxtPatronBusquedaCliente"
                                                runat="server" placeholder="Buscar"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:LinkButton ID="lbBuscarClienteModal" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar"
                                                OnClick="lbBuscarClienteModal_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="container row">
                                    <asp:GridView ID="gvClientes" runat="server" AllowPaging="true"
                                        PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped"
                                        OnPageIndexChanging="gvClientes_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idCadena" HeaderText="ID Cliente" />
                                            <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:TemplateField HeaderText="Apellidos">
                                                <ItemTemplate>
                                                    <%# Eval("apellidoPaterno") + " " + Eval("apellidoMaterno") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton class="btn btn-success"
                                                        runat="server"
                                                        Text="<i class='fa-solid fa-check ps-2'></i> Seleccionar"
                                                        ID="btnSeleccionarModalCliente"
                                                        OnClick="btnSeleccionarModalCliente_Click"
                                                        CommandArgument='<%# Eval("idNumerico") %>' />
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





        <div id="modalFormBuscarProducto" class="modal" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Busqueda de Productos</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="container row pb-3 pt-3">
                                    <div class="row align-items-center">
                                        <div class="col-sm-8">
                                            <asp:TextBox CssClass="form-control" ID="TxtPatronBusquedaProducto"
                                                runat="server" placeholder="Buscar"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:LinkButton ID="lbBuscarProductos" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar"
                                                OnClick="lbBuscarProductos_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="container row">
                                    <asp:GridView ID="gvProductos" runat="server" AllowPaging="true"
                                        PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped"
                                        OnPageIndexChanging="gvProductos_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idProductoCadena" HeaderText="ID Producto" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="precioUnitario" HeaderText="Precio Unitario" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton class="btn btn-success"
                                                        runat="server"
                                                        Text="<i class='fa-solid fa-check ps-2'></i> Seleccionar"
                                                        ID="btnSeleccionarModalProducto"
                                                        OnClick="btnSeleccionarModalProducto_Click"
                                                        CommandArgument='<%# Eval("idProductoNumerico") %>' />
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

    </div>
</asp:Content>