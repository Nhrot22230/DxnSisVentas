<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DxnSisventas.Views.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div>
    <div><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></div>
    <asp:Button ID="btnShowPDF" runat="server" Text="Mostrar PDF" OnClick="btnShowPDF_Click" />
    <iframe id="pdfFrame" runat="server" width="100%" height="800px" style="display: none;"></iframe>
  </div>
</asp:Content>
