<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="StatisticInitialLoading.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.StatisticInitialLoading" %>

<%@ Import Namespace="rt.atl.model.dto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />
  <div style="padding: 5px; height: 800px">

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
      <ContentTemplate>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>

            <div class="admCaption">
              <div style="float: left">
                <asp:Label ID="Label9" runat="server" Text="Статистика первичной загрузки" />
              </div>
              <div style="float: right">
                <asp:Button ID="btnRefresh" runat="server" Text="Обновить" OnClick="btnRefresh_Click" />
              </div>
              <div style="clear: both" />
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick" />

        <asp:UpdatePanel ID="gridPanel" runat="server">
          <ContentTemplate>
            <div style="padding-top: 5px; padding-bottom: 5px">
              <div style="min-height: 50px; overflow: auto; border: 1px solid black; padding: 5px;">
                <asp:GridView Style="width: 100%;" ID="grid" runat="server" EnableModelValidation="True"
                  AllowSorting="True" AutoGenerateColumns="False"
                  DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both">
                  <HeaderStyle CssClass="admGridHeader" />
                  <RowStyle CssClass="admGridRowStyle" />
                  <SelectedRowStyle CssClass="admGridSelectedRowStyle" />
                  <Columns>
                    <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                      <HeaderStyle CssClass="HideButton" />
                      <ItemStyle CssClass="HideButton" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Экспортирован" SortExpression="">
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                      <ItemTemplate>
                        <asp:CheckBox ID="cbExported" runat="server" Checked='<%# ((StatisticInitialLoading)Container.DataItem).IsExported %>' Enabled="false" />
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ошибка экспорта" SortExpression="">
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemTemplate>
                        <asp:Label ID="lb2" runat="server" Text='<%# ((StatisticInitialLoading)Container.DataItem).ExportError %>' Enabled="false"></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Количество" SortExpression="">
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                      <ItemTemplate>
                        <asp:Label ID="lb3" runat="server" Text='<%# ((StatisticInitialLoading)Container.DataItem).Count %>' Enabled="false"></asp:Label>
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
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
          </Triggers>
        </asp:UpdatePanel>

      </ContentTemplate>
    </asp:UpdatePanel>

  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
