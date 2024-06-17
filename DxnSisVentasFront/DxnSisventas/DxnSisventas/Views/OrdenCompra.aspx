<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrdenCompra.aspx.cs" Inherits="DxnSisventas.Views.OrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/CustomScripts/Documentos.js"></script>
    <link href="../CustomStyles/OrdenCompra.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-path">
        <p><i class="fa fa-home"></i>> Inicio > Documentos > Ordenes > OrdenCompra</p>
        <hr>
    </div>
    <div class="container">
        <div class="container row">
            <h1>Registro de Ordenes de Compra</h1>
        </div>
       <div class="search-container">
    <!-- Búsqueda por Número de Orden -->
    <div >
        <label class="col-form-label">Búsqueda por Número de Orden</label>
        <div class="input-group">
            <asp:TextBox ID="TxtBuscar" runat="server" type="number" AutoPostBack="true" CssClass="form-control no-arrows text-left" placeholder="Nro. de Orden" OnTextChanged="TxtBuscar_TextChanged"></asp:TextBox>
            <asp:LinkButton ID="BtnBuscar" runat="server" Text="<i class='fas fa-search'></i>" CssClass="btn btn-secondary" OnClick="BtnBuscar_Click" />
        </div>
    </div>

    <!-- Estado -->
    <div class="search-item">
        <label class="col-form-label">Estado</label>
        <div class="d-flex ">
            <div class="dropdown">
                <asp:DropDownList ID="Estado" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Estado_SelectedIndexChanged" Style="width: 150px;">
                    <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
                    <asp:ListItem Text="Entregado" Value="Entregado"></asp:ListItem>
                    <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>

    <!-- Filtro por Montos -->
    <div class="search-item align-content-md-start">
        <label class="col-form-label">Filtro por Montos</label>
        <div class="input-group">
            <asp:TextBox runat="server" type="number" class="form-control rounded-start no-arrows" id="TxtMontoMin" AutoPostBack="true" placeholder="Min" maxlength="10" OnTextChanged="TxtMontoMin_TextChanged"></asp:TextBox>
            <span class="input-group-text">-</span>
           <asp:TextBox runat="server" type="number" class="form-control rounded-end no-arrows" id="TxtMontoMax" AutoPostBack="true" placeholder="Max" maxlength="10" OnTextChanged="TxtMontoMax_TextChanged"></asp:TextBox>
        </div>
    </div>

</div>



        <div class="container row">
            <!-- Panel de filtros -->
            <div class="row mb-3 align-items-center">
                <!-- Filtro Fecha Inicio -->
                <div class="search-item">
                    <label class="col-form-label" for="FechaInicio">Desde</label>
               
                <div class="col-auto ms-3">
                    <asp:TextBox ID="FechaInicio" runat="server" CssClass="form-control" Style="width: 150px;" type="date" AutoPostBack="true" OnTextChanged="FechaInicio_TextChanged"></asp:TextBox>
                </div>
                    </div>

                <!-- Filtro Fecha Fin -->
                <div class="search-item">
                    <label class="col-form-label" for="FechaFin">Hasta</label>

                    <div class="col-auto">
                        <asp:TextBox ID="FechaFin" runat="server" CssClass="form-control" Style="width: 150px;" type="date" AutoPostBack="true" OnTextChanged="FechaFin_TextChanged"></asp:TextBox>
                    </div>
                </div>

                <!-- Filtro fecha -->

                <div class="search-item">
                    <label class="col-form-label">Ordenar por Fecha</label>
                    <div class="dropdown">
                        <asp:DropDownList ID="OrdenarPorFecha" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="OrdenarPorFecha_SelectedIndexChanged">
                            <asp:ListItem Text="Ninguno" Value="todos"></asp:ListItem>
                            <asp:ListItem Text="Mas antiguos primero" Value="asc"></asp:ListItem>
                            <asp:ListItem Text="Mas recientes primero" Value="desc"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <!-- Filtro Monto -->
                <div class="search-item">
                    <label class="col-form-label">Ordenar por Monto</label>
                    <div class="dropdown">
                        <asp:DropDownList ID="OrdenarPorMonto" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="OrdenarPorMonto_SelectedIndexChanged">
                            <asp:ListItem Text="Ninguno" Value="todos"></asp:ListItem>
                            <asp:ListItem Text="Menor a Mayor" Value="asc"></asp:ListItem>
                            <asp:ListItem Text="Mayor a Menor" Value="desc"></asp:ListItem>

                        </asp:DropDownList>
                    </div>
                </div>


                <!-- Botón Limpiar Filtros -->
                <div class="col-auto ms-3">
                    <asp:LinkButton ID="BtnLimpiar" runat="server" OnClick="BtnLimpiar_Click" CssClass="btn btn-link text-decoration-none small" Style="color: #000;">
                <i class="fas fa-times-circle"></i> Limpiar Filtros
                    </asp:LinkButton>
                </div>
            </div>

            <!-- Botón Nuevo -->
            <div class="p-3">
                <asp:LinkButton ID="BtnAgregar" runat="server" OnClick="BtnAgregar_Click" CssClass="btn btn-primary btn-lg">
            <i class="fas fa-plus pe-2"></i> Nuevo
        </asp:LinkButton>
            </div>
        </div>

        <!-- Gridview para mostrar los registros de la tabla  -->
        <div class="container row">
            <!-- PageSize para modificar cuantos registros se muestran por pagina -->
            <asp:GridView ID="GridCompras" runat="server" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="5" OnPageIndexChanging="GridCompras_PageIndexChanging"
                CssClass="gridview-custom" OnSelectedIndexChanged="GridCompras_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="idOrdenCompraCadena" HeaderText="Id" />
                    <asp:BoundField DataField="fechaCreacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:C}" HtmlEncode="false" />
                    <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnEditar" runat="server"
                                Text='<%# Eval("estado").ToString() == "Entregado" ? "Ver <i class=\"fas fa-eye ps-2\"></i>" : "Editar <i class=\"fas fa-edit ps-2\"></i>" %>'
                                OnClick="BtnEditar_Click" CommandArgument='<%# Eval("idOrdenCompraNumerico") %>' CssClass="link-button" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>


</asp:Content>
