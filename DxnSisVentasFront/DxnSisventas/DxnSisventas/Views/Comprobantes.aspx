<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Comprobantes.aspx.cs" Inherits="DxnSisventas.Views.Comprobantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio > Documentos > Comprobantes</p>
    <hr>
  </div>
  <div class="container">
    <div class="container row">
      <h1>Registro de Comprobantes</h1>
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
      <div class="container row">
        <div class="text p-3">
          <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar"
            OnClick="BtnAgregar_Click" CssClass="btn btn-success" />
        </div>
      </div>
      <div class="container row ">
        <asp:GridView ID="GridComprobantes" runat="server" AutoGenerateColumns="false"
          AllowPaging="true" PageSize="7" OnPageIndexChanging="GridComprobantes_PageIndexChanging"
          OnRowDataBound="GridComprobantes_RowDataBound"
          CssClass="table table-hover table-responsive table-striped">
          <Columns>
            <asp:BoundField DataField="idComprobanteCadena" HeaderText="ID Comprobante" />
            <asp:BoundField DataField="fechaEmision" HeaderText="Fecha Emisión" />
            <asp:BoundField DataField="tipoComprobante" HeaderText="Tipo de comprobante" />
            <asp:BoundField DataField="ordenAsociada.idOrden" HeaderText="idOrdenAsociada" />
            <asp:TemplateField HeaderText="idOrdenVenta/Compra" >
              <ItemTemplate>
                <asp:Label ID="LblOrdenVentaCompra" runat="server" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ordenAsociada.total" HeaderText="Total" />
            <asp:TemplateField HeaderText="">
              <ItemTemplate>
                <asp:LinkButton ID="BtnEditar" runat="server" Text="<i class='fas fa-edit ps-2'>  </i>"
                  OnClick="BtnEditar_Click" CommandArgument='<%# Eval("idComprobanteNumerico") %>' />
                <asp:LinkButton ID="BtnEliminar" runat="server" Text="<i class='fas fa-trash ps-2'>  </i>"
                  OnClick="BtnEliminar_Click" CommandArgument='<%# Eval("idComprobanteNumerico") %>'
                  OnClientClick="return confirm('¿Esta seguro de eliminar este registro?');" />
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
          </Columns>
        </asp:GridView>
      </div>
    </div>
  </div>

</asp:Content>
