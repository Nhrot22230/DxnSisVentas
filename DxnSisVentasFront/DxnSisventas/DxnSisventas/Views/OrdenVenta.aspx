<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrdenVenta.aspx.cs" Inherits="DxnSisventas.Views.OrdenVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CustomStyles/OrdenVenta.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-path">
        <p><i class="fa fa-home"></i>> Inicio > Documentos > Ordenes > OrdenVenta</p>
        <hr>
    </div>
    <div class="container">
        <div class="row mb-3">
            <h1>Registro de Ordenes de Venta</h1>
        </div>
        <div class="row mb-3">
            <div class="col-md-7">
                <div class="input-group">
                    <asp:TextBox ID="TxtBuscar" runat="server" AutoPostBack="true" 
                        CssClass="form-control" placeholder="Buscar" OnTextChanged="TxtBuscar_TextChanged"></asp:TextBox>
                    <asp:LinkButton ID="BtnBuscar" runat="server" Text="<i class='fas fa-search'> </i>"
                        CssClass="btn btn-secondary" OnClick="BtnBuscar_Click" />
                </div>
            </div>
        </div>
        <div class="search-row mb-3">
            <!-- Filtro por Fecha -->
            <div class="search-item col-sm-3">
                <label class="col-form-label">Filtro por Fecha</label>
                <div class="input-group">
                    <asp:TextBox ID="FechaInicio" runat="server" CssClass="form-control" type="date" AutoPostBack="true" OnTextChanged="FechaInicio_TextChanged"></asp:TextBox>
                    <span class="input-group-text">-</span>
                    <asp:TextBox ID="FechaFin" runat="server" CssClass="form-control" type="date" AutoPostBack="true" OnTextChanged="FechaFin_TextChanged"></asp:TextBox>
                </div>
            </div>

            <!-- Ordenar por Fecha -->
            <div class="search-item col-sm-3">
                <label class="col-form-label">Ordenar por Fecha</label>
                <asp:DropDownList ID="OrdenarPorFecha" runat="server" CssClass="form-select" AutoPostBack="true"
                    OnSelectedIndexChanged="OrdenarPorFecha_SelectedIndexChanged">
                    <asp:ListItem Text="Ninguno" Value="todos"></asp:ListItem>
                    <asp:ListItem Text="Mas antiguos primero" Value="asc"></asp:ListItem>
                    <asp:ListItem Text="Mas recientes primero" Value="desc"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <!-- Estado -->
            <div class="search-item col-sm-3">
                <label class="col-form-label">Estado</label>
                <asp:DropDownList ID="Estado" runat="server" CssClass="form-select" AutoPostBack="true"
                    OnSelectedIndexChanged="Estado_SelectedIndexChanged">
                    <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
                    <asp:ListItem Text="Entregado" Value="Entregado"></asp:ListItem>
                    <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="search-row mb-3">
            <!-- Filtro por Montos -->
            <div class="search-item col-sm-3">
                <label class="col-form-label">Filtro por Montos</label>
                <div class="input-group">
                    <asp:TextBox runat="server" type="number" class="form-control rounded-start no-arrows" ID="TxtMontoMin" AutoPostBack="true"
                        placeholder="Min" MaxLength="10" OnTextChanged="TxtMontoMin_TextChanged"></asp:TextBox>
                    <span class="input-group-text">-</span>
                    <asp:TextBox runat="server" type="number" class="form-control rounded-end no-arrows" ID="TxtMontoMax" AutoPostBack="true"
                        placeholder="Max" MaxLength="10" OnTextChanged="TxtMontoMax_TextChanged"></asp:TextBox>
                </div>
            </div>
            <!-- Ordenar por Monto -->
            <div class="search-item col-sm-3">
                <label class="col-form-label">Ordenar por Monto</label>
                <asp:DropDownList ID="OrdenarPorMonto" runat="server" CssClass="form-select" AutoPostBack="true"
                    OnSelectedIndexChanged="OrdenarPorMonto_SelectedIndexChanged">
                    <asp:ListItem Text="Ninguno" Value="todos"></asp:ListItem>
                    <asp:ListItem Text="Menor a Mayor" Value="asc"></asp:ListItem>
                    <asp:ListItem Text="Mayor a Menor" Value="desc"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="row mb-3">
            <div class="search-item col-sm-2">
                <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar"
                    OnClick="BtnAgregar_Click" CssClass="btn btn-primary btn-sm" />
            </div>
        </div>
        <div class="container row ">
            <asp:GridView ID="GridVentas" runat="server" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="5" OnPageIndexChanging="GridVentas_PageIndexChanging"
                CssClass="table table-hover table-responsive table-striped">
                <Columns>
                    <asp:BoundField DataField="idOrdenVentaCadena" HeaderText="Id" />
                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <%# Eval("fechaEntrega") == null ? "Sin Fecha" : String.Format("{0:dd/MM/yyyy}", Eval("fechaEntrega")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cliente.nombre" HeaderText="Cliente" />
                    <asp:BoundField DataField="encargadoVenta.nombre" HeaderText="EncargadoVenta" />
                    <asp:BoundField DataField="repartidor.nombre" HeaderText="Repartidor" />
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnEditar" runat="server" Text="<i class='fas fa-edit ps-2'>  </i>"
                                OnClick="BtnEditar_Click" CommandArgument='<%# Eval("idOrdenVentaNumerico") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
