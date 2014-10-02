<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="EncryptionConnections.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.EncryptionConnections" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <asp:Button ID="btnEncrypt" runat="server" Text="Зашифровать строку соединения с базой данных" OnClick="btnEncrypt_Click" />
  <asp:Button ID="btnDecrypt" runat="server" Text="Расшифровать строку соединения с базой данных" OnClick="btnDecrypt_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
