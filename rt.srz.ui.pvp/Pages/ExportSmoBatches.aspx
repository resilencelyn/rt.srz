<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="ExportSmoBatches.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.ExportSmoBatches" %>

<%@ Import Namespace="rt.srz.model.dto" %>
<%@ Register Src="~/Controls/CustomPager/Pager.ascx" TagPrefix="uc" TagName="Pager" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-2.1.0.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-migrate-1.2.1.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.limitkeypress.js") %>"></script>
  <script type="text/javascript">

    //Подлключение обработчика для поля "Номер батча"
    $(document).ready(function () {
      $('.batchNumber').limitkeypress({ rexp: /^[0-9]*$/ });
    });

  </script>


  <%--Заголовок--%>
  <div class="formTitle">
    <asp:Label ID="lbTitle" runat="server" Text="Пакетные операции экспорта для СМО"></asp:Label>
  </div>

  <%--Панель поиска--%>
  <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

      <div>
        <div style="float: left">
          <div class="padding5">
            <asp:Label ID="lbPeriod" runat="server" Text="Период"></asp:Label>
          </div>
        </div>
        <div style="float: left; width: 20%">
          <div class="padding5">
            <asp:DropDownList ID="ddlPeriod" runat="server" Width="100%" CssClass="dropDowns"></asp:DropDownList>
          </div>
        </div>
        <div style="float: left">
          <div class="padding5">
            <asp:Label ID="lbBatchNumber" runat="server" Text="Номер пакетной операции"></asp:Label>
          </div>
        </div>
        <div style="float: left">
          <div class="padding5">
            <asp:TextBox ID="tbBatchNumber" runat="server" MaxLength="15" CssClass="batchNumber"></asp:TextBox>
          </div>
        </div>
        <div style="float: left;">
          <div class="padding5">
            <asp:Button ID="btnClear" runat="server" Text="Очистить" OnClick="btnClear_Click" UseSubmitBehavior="False" CssClass="buttons" />
          </div>
        </div>
        <div style="float: left">
          <div class="padding5">
            <asp:Button ID="btnSearch" runat="server" Text="Поиск" OnClick="btnSearch_Click" CssClass="buttons" />
          </div>
        </div>
      </div>

    </ContentTemplate>
  </asp:UpdatePanel>

  <%--Таблица Батчей--%>
  <asp:UpdatePanel ID="upBatchGrid" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <div>
        <asp:GridView Style="width: 100%" ID="batchGrid" runat="server" EnableModelValidation="True"
          AllowSorting="True" AutoGenerateColumns="False"
          DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both">
          <HeaderStyle CssClass="GridHeader" />
          <RowStyle CssClass="GridRowStyle" />
          <SelectedRowStyle CssClass="GridSelectedRowStyle" />
          <Columns>
            <asp:TemplateField HeaderText="Отправитель">
              <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# ((SearchBatchResult) Container.DataItem).SenderName != null ?
                                    ((SearchBatchResult) Container.DataItem).SenderName : null %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Получатель">
              <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# ((SearchBatchResult) Container.DataItem).ReceiverName != null ?
                                    ((SearchBatchResult) Container.DataItem).ReceiverName : null %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Год периода">
              <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# ((SearchBatchResult) Container.DataItem).PeriodYear.Year.ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Месяц периода">
              <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# ((SearchBatchResult) Container.DataItem).PeriodMonth != null ?
                                    ((SearchBatchResult) Container.DataItem).PeriodMonth : null %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Номер">
              <HeaderStyle HorizontalAlign="Center" />
              <ItemStyle HorizontalAlign="Center" />
              <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# ((SearchBatchResult) Container.DataItem).Number %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Количество записей">
              <ItemTemplate>
                <asp:Label ID="Label6" runat="server" Text='<%# ((SearchBatchResult) Container.DataItem).RecordCount %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Имя файла">
              <ItemTemplate>
                <asp:Label ID="Label7" runat="server" Text='<%# ((SearchBatchResult) Container.DataItem).FileName != null ?
                                    ((SearchBatchResult) Container.DataItem).FileName + ".xml" : null %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Код состояния">
              <ItemTemplate>
                <asp:Label ID="Label8" runat="server" Text='<%# ((SearchBatchResult) Container.DataItem).CodeConfirm != null ?
                                    ((SearchBatchResult) Container.DataItem).CodeConfirm: null %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Операции">
              <ItemTemplate>
                <asp:Button ID="btnExport" runat="server" Text="Пометить как не выгруженный" OnClick="btnExport_Click" CssClass="buttons"></asp:Button>
                <asp:HiddenField runat="server" ID="hfId" Value='<%# ((SearchBatchResult)Container.DataItem).Id %>' />
              </ItemTemplate>
            </asp:TemplateField>
          </Columns>
        </asp:GridView>
      </div>
      <%--Пэйджер--%>
      <div class="pagerPadding">
        <uc:Pager ID="custPager" runat="server" OnPageIndexChanged="custPager_PageIndexChanged"
          OnPageSizeChanged="custPager_PageSizeChanged" />
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>


</asp:Content>

