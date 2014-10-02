<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="SeparateOptions.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.SeparateOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <asp:Label ID="Label1" runat="server" Text="Найдено более одной записи для разъединения. Выберите дальнейшие действия"></asp:Label>
  <asp:RadioButtonList ID="rbl" runat="server">
    <asp:ListItem Selected="True" Value="one">Разъединить указанные</asp:ListItem>
    <asp:ListItem Value="all">Разъединить все</asp:ListItem>
  </asp:RadioButtonList>
  <asp:Button ID="btnSeparate" runat="server" Text="Разъединить" OnClick="btnSeparate_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
