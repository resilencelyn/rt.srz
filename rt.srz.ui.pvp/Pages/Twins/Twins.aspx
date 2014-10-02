<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="Twins.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Twins.Twins" %>

<%@ Import Namespace="rt.srz.model.srz" %>
<%@ Register Src="~/Controls/Twins/TwinPersonControl.ascx" TagName="personControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/CustomPager/Pager.ascx" TagPrefix="uc" TagName="Pager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />
  <script type="text/javascript">
  </script>

  <div class="formTitle">
    <asp:Label ID="Label1" runat="server" Text="Дубликаты"></asp:Label>
  </div>

  <div style="clear: both">

    <div style="float: left; width: 25%">

      <asp:UpdatePanel ID="ddlPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <div class="labelControl">
            <div class="padding5">
              <asp:Label ID="Label18" runat="Server" Text="Тип ключа" />
            </div>
          </div>
          <div class="valueControl">
            <div style="padding-left: 5px; padding-top: 5px; padding-bottom: 5px">
              <asp:DropDownList ID="ddlKeyTypes" runat="server" Width="100%" OnSelectedIndexChanged="ddlKeyTypes_SelectedIndexChanged" AutoPostBack="True" CssClass="dropDowns">
                <asp:ListItem Value="All">Все</asp:ListItem>
                <asp:ListItem Value="Standard">Стандартные</asp:ListItem>
              </asp:DropDownList>
              <div style="padding-top: 2px">
                <asp:Button ID="btnDeleteTwins" runat="server" Text="Удалить дубликаты по ключу" Enabled="false" Width="100%" OnClick="btnDeleteTwins_Click" CssClass="buttons" />
              </div>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
      <div style="clear: both">
        <div class="twin1GridPart">
          <asp:UpdatePanel ID="gridPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <div class="twinGrid">
                <asp:GridView Style="width: 100%; table-layout: fixed;" ID="grid" runat="server" EnableModelValidation="True"
                  AllowSorting="True" AutoGenerateColumns="False" OnRowDeleting="grid_Deleting"
                  DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both" OnSelectedIndexChanged="grid_SelectedIndexChanged" CssClass="ItemGrid">
                  <HeaderStyle CssClass="GridHeader" />
                  <RowStyle CssClass="GridRowStyle" />
                  <SelectedRowStyle CssClass="GridSelectedRowStyle" />
                  <Columns>
                    <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                      <HeaderStyle CssClass="HideButton" />
                      <ItemStyle CssClass="HideButton" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Человек 1" SortExpression="">
                      <HeaderStyle HorizontalAlign="Center" Width="50%" />
                      <ItemStyle HorizontalAlign="Center" />
                      <ItemTemplate>
                        <asp:Label ID="lb1" runat="server" Text='<%# ((Twin)Container.DataItem).FirstInsuredPerson.Id %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Человек 2" SortExpression="">
                      <HeaderStyle HorizontalAlign="Center" Width="50%" />
                      <ItemStyle HorizontalAlign="Center" />
                      <ItemTemplate>
                        <asp:Label ID="lb2" runat="server" Text='<%# ((Twin)Container.DataItem).SecondInsuredPerson.Id %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                  </Columns>
                </asp:GridView>
              </div>

              <div class="pagerPadding">
                <uc:Pager ID="custPager" runat="server" OnPageIndexChanged="custPager_PageIndexChanged"
                  OnPageSizeChanged="custPager_PageSizeChanged" />
              </div>

            </ContentTemplate>
          </asp:UpdatePanel>
        </div>
      </div>

      <div class="buttonDeletePartPadding">
        <div class="buttonDeletePart">
          <div class="separator">
          </div>
          <div>
            <asp:UpdatePanel ID="buttonPanel" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
                <asp:Button ID="btnDelete" runat="server" Text="Удалить связь" OnClick="btnDelete_Click" CssClass="buttons" />
              </ContentTemplate>
            </asp:UpdatePanel>
          </div>
        </div>
      </div>
    </div>


    <div style="float: left; width: 75%">
      <asp:UpdatePanel ID="dataPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

          <div style="clear: both; padding: 5px">
            <div style="float: left; text-align: right; width: 20%">
              <div>
                <div style="padding-left: 5px;">
                  <asp:Label ID="Label19" runat="Server" Text="Ключи поиска" />
                </div>
              </div>
            </div>
            <div style="float: left; width: 80%">
              <div>
                <div style="padding-left: 5px;">
                  <asp:TextBox ID="tbTwinKeys" runat="Server" Width="100%" ReadOnly="True" TextMode="MultiLine" CssClass="controlBoxes" />
                </div>
              </div>
            </div>
          </div>

          <div style="clear: both">
            <div style="float: left; width: 50%">
              <div style="padding-left: 5px; padding-top: 5px">
                <uc:personControl ID="twinPerson1" runat="server" />
              </div>
            </div>
            <div style="float: left; width: 50%">
              <div style="padding-left: 5px; padding-top: 5px">
                <uc:personControl ID="twinPerson2" runat="server" />
              </div>
            </div>
          </div>

        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>

  <div style="clear: both">
  </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
