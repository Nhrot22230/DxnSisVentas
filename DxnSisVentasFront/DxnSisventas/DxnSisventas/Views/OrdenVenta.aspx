﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrdenVenta.aspx.cs" Inherits="DxnSisventas.Views.OrdenVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio > Documentos > Ordenes > OrdenVenta</p>
    <hr>
  </div>
  <div class="container">
    <div class="container row">
      <h1>Registro de Ordenes de Venta</h1>
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
      <label class="col-sm-2 col-form-label">Inicio</label>
      <div class="col-sm-3">
        <asp:TextBox ID="FechaInicio" runat="server" CssClass="form-control"
            type="date" AutoPostBack="true" OnTextChanged="FechaInicio_TextChanged"></asp:TextBox>
      </div>
      <label class="col-sm-2 col-form-label">Fin</label>
      <div class="col-sm-3">
        <asp:TextBox ID="FechaFin" runat="server"
            CssClass="form-control" type="date" AutoPostBack="true"
            OnTextChanged="FechaFin_TextChanged"></asp:TextBox>
      </div>
      <div class="p-3">
        <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar"
          OnClick="BtnAgregar_Click" CssClass="btn btn-success" />
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
              <asp:LinkButton ID="EliminarORV" runat="server" Text="<i class='fas fa-trash ps-2'>  </i>" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div>
  </div>
</asp:Content>
