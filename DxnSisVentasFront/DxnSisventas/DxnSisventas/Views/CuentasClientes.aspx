﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CuentasClientes.aspx.cs" Inherits="DxnSisventas.Views.CuentasClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio > Cuentas > Clientes</p>
    <hr>
  </div>
  <div class="container">
    <div class="container row">
      <h1>Registro de Cuentas de Clientes</h1>
    </div>
    <div class="container row">
      <div class="col-md-6">
        <div class="input-group mb-3">
          <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control" placeholder="Buscar"></asp:TextBox>
          <asp:LinkButton ID="BtnBuscar" runat="server" Text="<i class='fas fa-search'> </i>"
            CssClass="btn btn-secondary" OnClick="BtnBuscar_Click" />
        </div>
      </div>
    </div>
    <div class="container row pt-3">
      <asp:GridView ID="GridClienteCuenta" AutoGenerateColumns="false" PageSize="8" AllowPaging="true"
        CssClass="table table-hover table-responsive table-striped" runat="server"
        OnPageIndexChanging="GridClienteCuenta_PageIndexChanged"
        OnRowDataBound="GridClienteCuenta_RowDataBound">
        <Columns>
          <asp:TemplateField HeaderText="Id Cliente">
            <ItemTemplate>
              <asp:Label ID="LblIdCliente" runat="server"></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField DataField="persona.nombre" HeaderText="Nombre" />
          <asp:BoundField DataField="persona.apellidoPaterno" HeaderText="Apellido Paterno" />
          <asp:BoundField DataField="persona.apellidoMaterno" HeaderText="Apellido Materno" />
          <asp:BoundField DataField="cuenta.idCuenta" HeaderText="Id Cuenta" />
          <asp:BoundField DataField="cuenta.usuario" HeaderText="Usuario" />
          <asp:TemplateField HeaderText="Opciones">
            <ItemTemplate>
              <asp:LinkButton ID="BtnEditar" runat="server" Text="<i class='fas fa-edit ps-2'>  </i>"
                OnClick="BtnEditar_Click" />
              <asp:LinkButton ID="BtnEliminar" runat="server" Text="<i class='fas fa-trash ps-2'>  </i>"
                OnClick="BtnEliminar_Click"
                OnClientClick="return confirm('¿Esta seguro de eliminar este registro?');" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div>
  </div>

  <div id="modalCuenta" class="modal">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <asp:Label ID="LblTitulo" runat="server" Text="Registro de Cuenta" CssClass="modal-title"></asp:Label>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="row">
            <div class="col-md-6 mb-3">
              <asp:Label ID="LblIdEmpleado" runat="server" Text="Id Empleado:" CssClass="form-label"></asp:Label>
              <asp:TextBox ID="TxtIdEmpleado" runat="server" CssClass="form-control" Enabled="false" />
            </div>
            <div class="col-md-6 mb-3">
              <asp:Label ID="LblIdCuenta" runat="server" Text="Id Cuenta:" CssClass="form-label"></asp:Label>
              <asp:TextBox ID="TxtIdCuenta" runat="server" CssClass="form-control" Enabled="false" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 mb-3">
              <asp:Label ID="LblUsuario" runat="server" Text="Usuario:" CssClass="form-label"></asp:Label>
              <asp:TextBox ID="TxtUsuario" runat="server" CssClass="form-control" />
              <p id="errMsgUsuario" style="color: red; display: none;" />
            </div>
            <div class="col-md-6 mb-3">
              <asp:Label ID="LblContrasena" runat="server" Text="Contraseña:" CssClass="form-label"></asp:Label>
              <asp:TextBox ID="TxtContrasena" runat="server" CssClass="form-control" />
              <p id="errMsgContrasena" style="color: red; display: none;" />
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BtnGuardar_Click" OnClientClick="return validateForm();" />
          <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" data-bs-dismiss="modal" OnClientClick="closeModalCuenta(); return true;" />
        </div>
      </div>
    </div>
  </div>
  <script>
    let modalCuenta = new bootstrap.Modal(document.getElementById('modalCuenta'));

    function openModalCuenta() {
      modalCuenta.show();
    }

    function closeModalCuenta() {
      modalCuenta.hide();
    }

    function validateUsuario() {
      let usuario = document.getElementById('<%= TxtUsuario.ClientID %>').value;
      let errMsgUsuario = document.getElementById('errMsgUsuario');
      if (usuario === '') {
        errMsgUsuario.style.display = 'block';
        errMsgUsuario.innerHTML = 'El usuario no puede estar vacío.';
        return false;
      }
      errMsgUsuario.style.display = 'none';
      return true;
    }

    function validateContrasena() {
      let contrasena = document.getElementById('<%= TxtContrasena.ClientID %>').value;
      let errMsgContrasena = document.getElementById('errMsgContrasena');
      if (contrasena === '') {
        errMsgContrasena.style.display = 'block';
        errMsgContrasena.innerHTML = 'La contraseña no puede estar vacía.';
        return false;
      }
      errMsgContrasena.style.display = 'none';
      return true;
    }

    function validateForm() {
      let validUsuario = validateUsuario();
      let validContrasena = validateContrasena();
      return validUsuario && validContrasena;
    }

  </script>
</asp:Content>
