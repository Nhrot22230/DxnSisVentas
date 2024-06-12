<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ComprobantesForm.aspx.cs" Inherits="DxnSisventas.Views.ComprobantesForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2>Comprobante
                </h2>
            </div>
            <div class="card-body">
                <div class="card mb-3">
                    <div class="card-header">
                        <h5>Información del Comprobante
                        </h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3 row">
                            <label for="TxtId" class="col-sm-2 col-form-label">Id Comprobante</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TxtId" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>

                            <label for="TxtFecha" class="col-sm-2 col-form-label">Fecha</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TxtFechaComprobante" runat="server" type="date" Enabled="true" CssClass="form-control" onblur="validateFechaComprobante()"></asp:TextBox>
                                <div id="fechaComprobanteErrorMessage" style="display: none; color: red;"></div>
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="TxtTipoComprobante" class="col-sm-2 col-form-label">Tipo Comprobante</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="DropDownListTipoComprobante" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Boleta" Value="BoletaSimple"></asp:ListItem>
                                    <asp:ListItem Text="Factura" Value="Factura"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label for="TxtTotal" class="col-sm-2 col-form-label">Total</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TxtTotal" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-3">
                    <div class="card-header">
                        <h5>Información del Orden Asociada
                        </h5>
                    </div>
                    <div class="card-body">
                        <div class="row align-items-center pb-3">
                            <div class="col-sm-1">
                                <asp:Label ID="LbOrden" CssClass="form-label" runat="server" Text="Orden:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TxtIdOrden" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:LinkButton ID="BtnBuscar" runat="server"
                                    CssClass="btn btn-primary" Text="<i class='fas fa-solid fa-search pe-2'></i> Buscar" 
                                    OnClick="BtnBuscar_Click" ></asp:LinkButton>
                            </div>
                            <label for="LbFecha" class="col-sm-2 col-form-label">Fecha Orden</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TxtFechaOrden" runat="server" Enabled="false" CssClass="form-control" onblur="validateOrdenAsociada()"></asp:TextBox>
                            </div>
                        </div>
                        <div id="ordenAsociadaErrorMessage" style="display: none; color: red;"></div>
                    </div>
                </div>
            <div class="card-footer clearfix">
                <asp:Button ID="BtnRegresar" runat="server" Text="Regresar"
                    CssClass="float-start btn btn-secondary" OnClick="BtnRegresar_Click" />
                <asp:Button ID="BtnGuardar" runat="server" Text="Guardar"
                    CssClass="float-end btn btn-primary" OnClick="BtnGuardar_Click" OnClientClick="return validarFormularioComprobante();"/>
            </div>
        </div>
    </div>

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    </div>
    <div id="form-modal-ordenes" class="modal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Ordenes</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container row pb-3 pt-3">
                        <div class="row align-items-center">
                            <div class="col-auto">
                                <asp:Label CssClass="form-label" runat="server" Text="Ingresar código de la orden:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" ID="txtCodOrdenModal" runat="server" OnTextChanged="txtCodOrdenModal_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:LinkButton ID="BtnBuscarModal" runat="server" CssClass="btn btn-info" Text="<i class='fas fa-solid fa-search pe-2'></i> Buscar" OnClick="BtnBuscarModal_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="container row">
                        <asp:GridView ID="gvOrdenes" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="gvOrdenes_PageIndexChanging" OnRowDataBound="gvOrdenes_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="IdOrden" DataField="idOrden" />
                                <%-- Estamos enlazando de otra manera a traves del evento OnRowDataBound --%>
                                <asp:BoundField HeaderText="IdOrdenCompra/Venta"/>
                                <asp:BoundField HeaderText="Fecha Creacion"/>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton class="btn btn-success" runat="server" Text="<i class='fas fa-solid fa-check ps-2'></i> Seleccionar" OnClick="BtnSeleccionarOrdenModal_Click" CommandArgument='<%# Eval("idOrden") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript" src="/CustomScripts/Documentos.js"></script>
</asp:Content>
