<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrdenCompra.aspx.cs" Inherits="DxnSisventas.Views.OrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/CustomScripts/Documentos.js"></script>
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
    <div class="container row">
      <div class="container row">
        <div class="col-md-6">
          <div class="input-group mb-3">
            <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control" placeholder="Buscar"></asp:TextBox>
            <asp:LinkButton ID="BtnBuscar" runat="server" Text="<i class='fas fa-search'> </i>"
              CssClass="btn btn-secondary" OnClick="BtnBuscar_Click" />
          </div>
        </div>
      </div>
    </div>
    <div class="container row">
      <!-- Panel de filtros -->
      <label class="col-sm-2 col-form-label">Inicio</label>
      <div class="col-sm-3">
        <asp:TextBox ID="FechaInicio" runat="server" CssClass="form-control" type="date" AutoPostBack="true" OnTextChanged="FechaInicio_TextChanged"></asp:TextBox>
      </div>
      <!-- Seleccionar el tipo de producto -->
      <label class="col-sm-2 col-form-label">Fin</label>
      <div class="col-sm-3">
        <asp:TextBox ID="FechaFin" runat="server" CssClass="form-control" type="date" AutoPostBack="true" OnTextChanged="FechaFin_TextChanged"></asp:TextBox>
      </div>
      <div class="p-3">
        <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar"
          OnClick="BtnAgregar_Click" CssClass="btn btn-success" />
      </div>
    </div>
    <!-- Gridview para mostrar los registros de la tabla Alumnos -->
    <div class="container row">
      <!-- PageSize para modificar cuantos registros se muestran por pagina -->
      <asp:GridView ID="GridCompras" runat="server" AutoGenerateColumns="false"
        AllowPaging="true" PageSize="5" OnPageIndexChanging="GridCompras_PageIndexChanging"
        CssClass="table table-hover table-responsive table-striped" OnSelectedIndexChanged="GridCompras_SelectedIndexChanged">
        <Columns>
          <asp:BoundField DataField="idOrdenCompraCadena" HeaderText="Id" />
          <asp:BoundField DataField="fechaCreacion" HeaderText="Fecha" />
          <asp:BoundField DataField="estado" HeaderText="Estado" />
          <asp:BoundField DataField="total" HeaderText="Total" />
          <asp:TemplateField HeaderText="">
            <ItemTemplate>
              <asp:LinkButton ID="BtnEditar" runat="server" Text="<i class='fas fa-edit ps-2'>  </i>"
                OnClick="BtnEditar_Click" CommandArgument='<%# Eval("idOrdenCompraNumerico") %>' />
              <asp:LinkButton ID="BtnEliminar" runat="server" Text="<i class='fas fa-trash ps-2'>  </i>"
                OnClick="BtnEliminar_Click" CommandArgument='<%# Eval("idOrdenCompraNumerico") %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="100px" />
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div>
  </div>


</asp:Content>
