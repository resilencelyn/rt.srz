<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="InsuranceHistory.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.InsuranceHistory" %>

<%@ Import Namespace="rt.srz.model.srz" %>
<%@ Register Src="~/Controls/Twins/TwinPersonControl.ascx" TagName="personControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />


  <div class="formTitle">
    <asp:Label ID="Label59" runat="server" Text="История страхования" />
  </div>


  <div style="float: left; width: 55%">
    <asp:UpdatePanel ID="gridPanel" runat="server" UpdateMode="Conditional">
      <ContentTemplate>
        <div style="height: 100%; overflow: auto;">
          <asp:GridView Style="" ID="grid" runat="server" EnableModelValidation="True"
            AllowSorting="True" AutoGenerateColumns="false"
            DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both" OnSelectedIndexChanged="grid_SelectedIndexChanged">
            <HeaderStyle CssClass="GridHeader" />
            <RowStyle CssClass="GridRowStyle" />
            <SelectedRowStyle CssClass="GridSelectedRowStyle" />
            <Columns>
              <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                <HeaderStyle CssClass="HideButton" />
                <ItemStyle CssClass="HideButton" />
              </asp:CommandField>

              <asp:TemplateField HeaderText="Дата подачи">
                <ItemTemplate>
                  <asp:Label ID="Label1" runat="server" Text='<%# ((DateTime)((Statement)Container.DataItem).DateFiling).ToString("dd.MM.yyyy") %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Статус заявления">
                <ItemTemplate>
                  <asp:Label ID="Label2" runat="server" Text='<%# ((Statement) Container.DataItem).Status != null ?
                      ((Statement) Container.DataItem).Status.Name : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Тип заявления">
                <ItemTemplate>
                  <asp:Label ID="Label3" runat="server" Text='<%# ((Statement) Container.DataItem).ModeFiling != null ?
                      ((Statement) Container.DataItem).ModeFiling.Name : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Причина подачи">
                <ItemTemplate>
                  <asp:Label ID="Label4" runat="server" Text='<%# ((Statement)Container.DataItem).CauseFiling != null ?
                      ((Statement)Container.DataItem).CauseFiling.Name : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="ЕНП">
                <ItemTemplate>
                  <asp:Label ID="Label5" runat="server" Text='<%# ((Statement)Container.DataItem).NumberPolicy %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>


              <asp:TemplateField HeaderText="Фамилия">
                <ItemTemplate>
                  <asp:Label ID="Label6" runat="server" Text='<%# ((Statement)Container.DataItem).InsuredPersonData.LastName %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Имя">
                <ItemTemplate>
                  <asp:Label ID="Label7" runat="server" Text='<%# ((Statement)Container.DataItem).InsuredPersonData.FirstName %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Отчество">
                <ItemTemplate>
                  <asp:Label ID="Label8" runat="server" Text='<%# ((Statement)Container.DataItem).InsuredPersonData.MiddleName %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Дата рождения">
                <ItemTemplate>
                  <asp:Label ID="Label9" runat="server" Text='<%# ((Statement)Container.DataItem).InsuredPersonData.Birthday.HasValue ? 
                      ((Statement)Container.DataItem).InsuredPersonData.Birthday.Value.ToString("dd.MM.yyyy") : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Пол">
                <ItemTemplate>
                  <asp:Label ID="Label10" runat="server" Text='<%# ((Statement)Container.DataItem).InsuredPersonData.Gender != null ?
                      ((Statement)Container.DataItem).InsuredPersonData.Gender.Name : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="СНИЛС">
                <ItemTemplate>
                  <asp:Label ID="Label11" runat="server" Text='<%# ((Statement)Container.DataItem).InsuredPersonData.Snils %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Место рождения">
                <ItemTemplate>
                  <asp:Label ID="Label12" runat="server" Text='<%# ((Statement)Container.DataItem).InsuredPersonData.Birthplace %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Гражданство">
                <ItemTemplate>
                  <asp:Label ID="Label13" runat="server" Text='<%# ((Statement)Container.DataItem).InsuredPersonData.Citizenship != null ?
                      ((Statement)Container.DataItem).InsuredPersonData.Citizenship.Name : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Без гражданства">
                <ItemTemplate>
                  <asp:CheckBox ID="CheckBox1" runat="server" Checked="<%# ((Statement)Container.DataItem).InsuredPersonData.IsNotCitizenship %>" />
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Беженец">
                <ItemTemplate>
                  <asp:CheckBox ID="CheckBox2" runat="server" Checked="<%# ((Statement)Container.DataItem).InsuredPersonData.IsRefugee %>" />
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Категория">
                <ItemTemplate>
                  <asp:Label ID="Label14" runat="server" Text='<%# ((Statement)Container.DataItem).InsuredPersonData.Category != null ?
                      ((Statement)Container.DataItem).InsuredPersonData.Category.Name : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Документ удл">
                <ItemTemplate>
                  <asp:Label ID="Label15" runat="server" Text='<%# ((Statement)Container.DataItem).DocumentUdl != null ?
                                            ((Statement)Container.DataItem).DocumentUdl.SeriesNumber : null
                                              %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Временное свидетельство">
                <ItemTemplate>
                  <asp:Label ID="Label16" runat="server" Text='<%# ((Statement)Container.DataItem).NumberTemporaryCertificate %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Полис">
                <ItemTemplate>
                  <asp:Label ID="Label17" runat="server" Text='<%# ((Statement)Container.DataItem).NumberPolisCertificate %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Адрес регистрации">
                <ItemTemplate>
                  <asp:Label ID="Label18" runat="server" Text='<%# ((Statement)Container.DataItem).AddressRegistrationStr %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Адрес проживания">
                <ItemTemplate>
                  <asp:Label ID="Label19" runat="server" Text='<%# ((Statement)Container.DataItem).AddressLiveStr %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Документ подтверждающий регистрацию">
                <ItemTemplate>
                  <asp:Label ID="Label20" runat="server" Text='<%# ((Statement)Container.DataItem).DocumentRegistration != null ?
                                         ((Statement)Container.DataItem).DocumentRegistration.SeriesNumber : null %>'></asp:Label>

                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Телефон домашний">
                <ItemTemplate>
                  <asp:Label ID="Label21" runat="server" Text='<%# ((Statement)Container.DataItem).ContactInfo != null ?
                      ((Statement)Container.DataItem).ContactInfo.HomePhone : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Телефон рабочий">
                <ItemTemplate>
                  <asp:Label ID="Label22" runat="server" Text='<%# ((Statement)Container.DataItem).ContactInfo != null ?
                      ((Statement)Container.DataItem).ContactInfo.WorkPhone : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Электронная почта">
                <ItemTemplate>
                  <asp:Label ID="Label23" runat="server" Text='<%# ((Statement)Container.DataItem).ContactInfo != null ?
                      ((Statement)Container.DataItem).ContactInfo.Email : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Фамилия представителя">
                <ItemTemplate>
                  <asp:Label ID="Label24" runat="server" Text='<%# ((Statement)Container.DataItem).Representative == null ? 
                      null : ((Statement)Container.DataItem).Representative.LastName %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Имя представителя">
                <ItemTemplate>
                  <asp:Label ID="Label25" runat="server" Text='<%# ((Statement)Container.DataItem).Representative == null ? 
                      null : ((Statement)Container.DataItem).Representative.FirstName %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Отчество представителя">
                <ItemTemplate>
                  <asp:Label ID="Label26" runat="server" Text='<%# ((Statement)Container.DataItem).Representative == null ? 
                      null : ((Statement)Container.DataItem).Representative.MiddleName %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Документ удл представителя">
                <ItemTemplate>
                  <asp:Label ID="Label27" runat="server" Text='<%# ((Statement)Container.DataItem).Representative == null || 
                      ((Statement)Container.DataItem).Representative.Document == null? 
                        null : ((Statement)Container.DataItem).Representative.Document.SeriesNumber %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Телефон домашний представителя">
                <ItemTemplate>
                  <asp:Label ID="Label28" runat="server" Text='<%# ((Statement)Container.DataItem).Representative == null ? 
                      null : ((Statement)Container.DataItem).Representative.HomePhone %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Телефон рабочий представителя">
                <ItemTemplate>
                  <asp:Label ID="Label29" runat="server" Text='<%# ((Statement)Container.DataItem).Representative == null ? 
                      null : ((Statement)Container.DataItem).Representative.WorkPhone %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>

            </Columns>
          </asp:GridView>
        </div>

      </ContentTemplate>
    </asp:UpdatePanel>
  </div>

  <div style="float: left; width: 45%">
    <div style="padding-left: 5px">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <uc:personControl ID="person1" runat="server" />
        </ContentTemplate>
      </asp:UpdatePanel>

    </div>
  </div>

  <div style="clear: both">
  </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
