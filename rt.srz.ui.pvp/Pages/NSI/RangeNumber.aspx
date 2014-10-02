<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="RangeNumber.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.NSI.RangeNumber" %>

<%@ Register TagPrefix="uc" TagName="RangeNumberControl" Src="~/Controls/NSI/RangeNumberControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
  <uc:RangeNumberControl runat="server" ID="rangeNumberControl" />
</asp:Content>
