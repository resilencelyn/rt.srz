<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="PfrStatistic.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.PfrStatistic" %>

<%@ Import Namespace="rt.srz.model.srz" %>
<%@ Import Namespace="rt.srz.model.srz.concepts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />

  <div class="formTitle">
    <asp:Label ID="Label9" runat="server" Text="Статистика ПФР" />
  </div>

  <asp:UpdatePanel ID="gridPanel" runat="server">
    <ContentTemplate>
      <div style="padding-top: 5px; padding-bottom: 5px">
        <div style="height: 200px; overflow: auto; padding: 5px;">
          <asp:GridView Style="width: 100%;" ID="grid" runat="server" EnableModelValidation="True"
            AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both" OnSelectedIndexChanged="grid_SelectedIndexChanged" Caption="Пакеты">
            <HeaderStyle CssClass="GridHeader" />
            <RowStyle CssClass="GridRowStyle" />
            <SelectedRowStyle CssClass="GridSelectedRowStyle" />
            <Columns>
              <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                <HeaderStyle CssClass="HideButton" />
                <ItemStyle CssClass="HideButton" />
              </asp:CommandField>
              <asp:TemplateField HeaderText="Файл" SortExpression="">
                <ItemTemplate>
                  <asp:Label ID="lb1" runat="server" Text='<%# ((Batch)Container.DataItem).FileName %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Статус обработки" SortExpression="">
                <ItemTemplate>
                  <asp:Label ID="lb2" runat="server" Text='<%# ((Batch)Container.DataItem).StatusDescription %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Период" SortExpression="">
                <ItemTemplate>
                  <asp:Label ID="lb3" runat="server" Text='<%# ((Batch)Container.DataItem).Period.Description %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>
        </div>
      </div>

      <%--            <div class="partPadding">
              <div style="height: 20px">
                <uc:Pager ID="custPager" runat="server" OnPageIndexChanged="custPager_PageIndexChanged"
                  OnPageSizeChanged="custPager_PageSizeChanged" />
              </div>
            </div>--%>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div style="padding-bottom: 5px">
        <div style="height: 200px; overflow: auto; padding: 5px;">
          <asp:GridView Style="width: 100%;" ID="gridP" runat="server" EnableModelValidation="True"
            AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both" OnSelectedIndexChanged="gridP_SelectedIndexChanged" Caption="Периоды">
            <HeaderStyle CssClass="GridHeader" />
            <RowStyle CssClass="GridRowStyle" />
            <SelectedRowStyle CssClass="GridSelectedRowStyle" />
            <Columns>
              <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                <HeaderStyle CssClass="HideButton" />
                <ItemStyle CssClass="HideButton" />
              </asp:CommandField>
              <asp:TemplateField HeaderText="Название периода" SortExpression="">
                <ItemTemplate>
                  <asp:Label ID="lb1" runat="server" Text='<%# ((Period)Container.DataItem).Description %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>
        </div>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <div>
        <div style="clear: both">
          <div class="labelControl">
            <div class="twinControl">
              <asp:Label ID="lbTitle" runat="Server" Text="Статистика по " />
            </div>
          </div>
          <div class="valueControl">
            <div class="twinControl">
              <asp:Label ID="lbStatisticBy" runat="Server" Width="100%" Text="" />
            </div>
          </div>
        </div>

        <div style="clear: both">
          <div class="separator">
          </div>
        </div>

        <div style="clear: both;">
          <div class="labelControl">
            <div class="twinControl">
              <asp:Label ID="lblLastName" runat="Server" Text="Ненайденных записей" />
            </div>
          </div>
          <div class="valueControl">
            <div class="twinControl">
              <asp:TextBox ID="tbNotFoundRecordCount" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="labelControl">
            <div class="twinControl">
              <asp:Label ID="Label2" runat="Server" Text="Всего записей" />
            </div>
          </div>
          <div class="valueControl">
            <div class="twinControl">
              <asp:TextBox ID="tbTotalRecordCount" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="labelControl">
            <div class="twinControl">
              <asp:Label ID="Label1" runat="Server" Text="Застрахованных в СРЗ" />
            </div>
          </div>
          <div class="valueControl">
            <div class="twinControl">
              <asp:TextBox ID="tbInsuredRecordCount" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="labelControl">
            <div class="twinControl">
              <asp:Label ID="Label3" runat="Server" Text="Работающих" />
            </div>
          </div>
          <div class="valueControl">
            <div class="twinControl">
              <asp:TextBox ID="tbEmployedRecordCount" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="labelControl">
            <div class="twinControl">
              <asp:Label ID="Label4" runat="Server" Text="Найдено по СНИЛС" />
            </div>
          </div>
          <div class="valueControl">
            <div class="twinControl">
              <asp:TextBox ID="tbFoundBySnilsRecordCount" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="labelControl">
            <div class="twinControl">
              <asp:Label ID="Label5" runat="Server" Text="Найдено по данным" />
            </div>
          </div>
          <div class="valueControl">
            <div class="twinControl">
              <asp:TextBox ID="tbFoundByDataRecordCount" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
            </div>
          </div>
        </div>

        <div style="clear: both" />
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
