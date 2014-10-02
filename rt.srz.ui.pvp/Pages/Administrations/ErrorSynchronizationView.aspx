<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="ErrorSynchronizationView.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.ErrorSynchronizationView" %>

<%@ Import Namespace="rt.atl.model.dto" %>
<%@ Import Namespace="rt.srz.model.srz" %>

<%@ Register Src="~/Controls/Common/SearchByDatesControl.ascx" TagName="SearchByDatesControl" TagPrefix="uc" %>

<%@ Register Src="~/Controls/CustomPager/Pager.ascx" TagPrefix="uc" TagName="Pager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />

  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/DateTextBox.js") %>"></script>

  <div class="formTitle">
    <asp:Label ID="Label59" runat="server" Text="Журнал ошибок синхронизации из СРЗ" />
  </div>

  <uc:SearchByDatesControl ID="searchByDatesControl" runat="server" />

  <asp:UpdatePanel ID="gridPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <div style="overflow: auto;">
        <asp:GridView Style="" ID="grid" runat="server" EnableModelValidation="True"
          AllowSorting="True" AutoGenerateColumns="false"
          DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%">
          <HeaderStyle CssClass="GridHeader" />
          <RowStyle CssClass="GridRowStyle" />
          <SelectedRowStyle CssClass="GridSelectedRowStyle" />
          <Columns>
            <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
              <HeaderStyle CssClass="HideButton" />
              <ItemStyle CssClass="HideButton" />
            </asp:CommandField>

            <asp:TemplateField HeaderText="Ошибка">
              <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# ((ErrorSinchronizationInfoResult)Container.DataItem).Error %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Фамилия">
              <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# ((ErrorSinchronizationInfoResult)Container.DataItem).Fam %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Имя">
              <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# ((ErrorSinchronizationInfoResult)Container.DataItem).Im %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Отчество">
              <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# ((ErrorSinchronizationInfoResult)Container.DataItem).Ot %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Дата рождения">
              <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# ((ErrorSinchronizationInfoResult)Container.DataItem).Dr.HasValue ? 
                      ((ErrorSinchronizationInfoResult)Container.DataItem).Dr.Value.ToString("dd.MM.yyyy") : null %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Пол">
              <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# ((ErrorSinchronizationInfoResult)Container.DataItem).W.HasValue ?
                        (((ErrorSinchronizationInfoResult)Container.DataItem).W.Value == 1 ? "мужской" : "женский") : null %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

            <%--                    <asp:TemplateField HeaderText="СНИЛС">
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                      <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# ((ErrorSinchronizationInfoResult)Container.DataItem).Przbuf.Snils %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Место рождения">
              <ItemTemplate>
                <asp:Label ID="Label9" runat="server" Text='<%# ((ErrorSinchronizationInfoResult)Container.DataItem).Mr %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ЕНП">
              <ItemTemplate>
                <asp:Label ID="Label10" runat="server" Text='<%# ((ErrorSinchronizationInfoResult)Container.DataItem).Enp %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Документ удл">
              <ItemTemplate>
                <asp:Label ID="Label7" runat="server" Text='<%# string.IsNullOrEmpty(((ErrorSinchronizationInfoResult)Container.DataItem).Docs) ? 
                                        ((ErrorSinchronizationInfoResult)Container.DataItem).Docn :
                                        ((ErrorSinchronizationInfoResult)Container.DataItem).Docs + " № " +((ErrorSinchronizationInfoResult)Container.DataItem).Docn %>'></asp:Label>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
