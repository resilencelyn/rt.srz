<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="JobsProgress.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administration.JobsProgress" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="false" ScriptMode="Release" />

  <script type="text/javascript">

    var downed = false;

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function (s, e) {
      var h = document.getElementById("<%=hfScrollPosition.ClientID%>");
      var el = document.getElementById("<%=scrollArea.ClientID%>");
      if (el != null) {
        el.scrollTop = h.value;
      }
    });

    window.onload = function () {
      var h = document.getElementById("<%=hfScrollPosition.ClientID%>");
      var el = document.getElementById("<%=scrollArea.ClientID%>");
      if (el != null) {
        el.scrollTop = h.value;
      }
    }

    function SetDivPosition() {
      var intY = document.getElementById("<%=scrollArea.ClientID%>").scrollTop;
      var h = document.getElementById("<%=hfScrollPosition.ClientID%>");
      h.value = intY;
    }

  </script>

  <div class="formTitle">
    <asp:Label ID="Label9" runat="server" Text="Текущие задачи" />
  </div>

  <asp:Timer ID="Timer1" runat="server" Interval="2000" OnTick="Timer1_Tick" />

  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

      <asp:UpdatePanel ID="noDataPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <div class="headerTitles">
            <div style="text-align: center;">
              <asp:Label ID="lbNoDataText" runat="server" Text="Нет данных для отображения" Width="100%" Visible="False"></asp:Label>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>

      <div runat="server" id="griddiv">
        <asp:UpdatePanel ID="gridPanel" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

            <asp:HiddenField ID="hfScrollPosition" runat="server" Value="0" />

            <div runat="server" id="scrollArea" onscroll="SetDivPosition()" style="overflow: auto; height: 700px">

              <asp:GridView Style="width: 100%" ID="grid" runat="server" EnableModelValidation="True"
                AllowSorting="True" AutoGenerateColumns="False" EnableViewState="false"
                CellPadding="4" ForeColor="#333333" GridLines="Both" OnRowDataBound="grid_RowDataBound">
                <HeaderStyle CssClass="GridHeader" />
                <RowStyle CssClass="GridRowStyle" />
                <SelectedRowStyle CssClass="GridSelectedRowStyle" />
                <Columns>
                  <asp:BoundField DataField="RecordNumber" HeaderText="№" />
                  <asp:BoundField DataField="Name" HeaderText="Название" />
                  <asp:BoundField DataField="Datails" HeaderText="Описание" />
                  <asp:TemplateField HeaderText="Прогресс">
                    <ItemTemplate>
                      <asp:HiddenField ID="progressValue" runat="server" Value='<%# ((JobData)Container.DataItem).Position.ToString() %>' />
                      <div style="border: solid 1px; height: 20px">
                        <div style="text-align: center;">
                          <div id="progressdiv" runat="server" style="width: 1%; background-color: lightgreen">
                          </div>
                          <div style="position: relative; top: -17px; width: 0px; left: 50%">
                            <asp:Label ID="lbProgress" runat="server" Text='<%# ((JobData)Container.DataItem).Position.ToString() + "%" %>' Width="100%"></asp:Label>
                          </div>
                        </div>
                      </div>
                    </ItemTemplate>
                  </asp:TemplateField>
                </Columns>
              </asp:GridView>
            </div>
          </ContentTemplate>
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
          </Triggers>
        </asp:UpdatePanel>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
