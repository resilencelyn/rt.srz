<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.CustomPager.Pager" %>
<div style="font-size: 8pt; font-family: Verdana;">

  <div style="clear: both">
    <div id="left" style="float: left;">
      <span>Показать страницу </span>
      <asp:DropDownList ID="ddlPageNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageNumber_SelectedIndexChanged" CssClass="dropDowns">
      </asp:DropDownList>
      <span>из</span>
      <asp:Label ID="lblShowRecords" runat="server"></asp:Label>
      <span>страниц </span>
    </div>
    <div id="right" style="float: right;">
      <span>На странице: </span>
      <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" CssClass="dropDowns">
        <asp:ListItem Text="1" Value="1"></asp:ListItem>
        <asp:ListItem Text="5" Value="5" Selected="true"></asp:ListItem>
        <asp:ListItem Text="10" Value="10"></asp:ListItem>
        <asp:ListItem Text="20" Value="20"></asp:ListItem>
        <asp:ListItem Text="25" Value="25"></asp:ListItem>
        <asp:ListItem Text="50" Value="50"></asp:ListItem>
      </asp:DropDownList>
    </div>
  </div>

<div style="clear: both">
</div>

</div>
