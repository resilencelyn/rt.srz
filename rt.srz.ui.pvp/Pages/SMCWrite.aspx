<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="SMCWrite.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.SMCWrite" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="False" ScriptMode="Release"/>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-2.1.0.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-migrate-1.2.1.js") %>"></script>
    <script type="text/javascript">

        //Вывод окна для ввода пина для модуля безопасности
        function showSecurityModulePinPad() {
            $find('<%= mpeSecutiryModulePinCode.ClientID%>').show();
        }

        //Вывод окна для ввода пина для карты
        function showPinPad() {
            $find('<%= mpePinCode.ClientID%>').show();
        }

        //Закрытия окон для ввода пина по ESC
        $(document).keyup(function (event) {
            if (event.keyCode == 27) {//ESC
                var mpeSM = $find('<%= mpeSecutiryModulePinCode.ClientID%>');
                if (mpeSM != null && mpeSM != undefined) {
                    mpeSM.hide();
                }

                var mpe = $find('<%= mpePinCode.ClientID%>');
                if (mpe != null && mpe != undefined) {
                    mpe.hide();
                }
            }
        });

        //Непосредственно запись
        function writeSMC() {
            var ogrn = $get('<%= hfOGRN.ClientID%>').value;
            var okato = $get('<%= hfOKATO.ClientID%>').value;
            var dateFrom = $get('<%= hfDateFrom.ClientID%>').value;
            var dateTo = $get('<%= hfDateTo.ClientID%>').value;
            var securityModulePinCode = $get('<%= tbSecutiryModulePinCode.ClientID%>').value;
            $get('<%= tbSecutiryModulePinCode.ClientID%>').value = '';
            var pinCode = $get('<%= tbPinCode.ClientID%>').value;
            $get('<%= tbPinCode.ClientID%>').value = '';

            //Вызов ActiveX
            if (!window.smardcardReader.ChangeSmo(ogrn, okato, dateFrom, dateTo, securityModulePinCode, pinCode)) {
                alert('Ошибка записи');
                return;
            }
            window.location = '<%= ResolveUrl("~/Pages/Main.aspx") %>';
        }

        // обработчик ввода пин для для модуля безопасности
        function securityModulePinEntered() {
            // открываем окно для ввода пина карты
            showPinPad();
        }

        // обрабочтик ввода пин для карты
        function pinEntered() {
            // пишем данные на полис
            writeSMC();
        }

        //сохранение на карту
        function writeSMCHandler() {
            try {
                //открытие карты
                //window.smardcardReader.OpenConnection(<%= string.Format("'{0}'", authService.GetAuthToken().Signature) %>);
                //window.smardcardReader.SetCardReader();
                //Вывод окна для ввода пина для Smart карты
                showSecurityModulePinPad();
            } catch (e) {
                alert(e);
            }
        }

        //отмена сохранения
        function cancelSMCHandler() {
            window.location = '<%= ResolveUrl("~/Pages/Main.aspx") %>';
        }
    </script>
    
    <div>
        <asp:HiddenField ID="hfLastName" runat="server"/>
        <asp:HiddenField ID="hfFirstName" runat="server"/>
        <asp:HiddenField ID="hfMiddleName" runat="server"/>
        <asp:HiddenField ID="hfBirthdate" runat="server"/>
        <asp:HiddenField ID="hfOGRN" runat="server"/>
        <asp:HiddenField ID="hfOKATO" runat="server"/>
        <asp:HiddenField ID="hfDateFrom" runat="server"/>
        <asp:HiddenField ID="hfDateTo" runat="server"/>
        <asp:Label ID="lbLastName" runat="server"/>
        <asp:Label ID="lbFirstName" runat="server"/>
        <asp:Label ID="lbMiddleName" runat="server"/>
        <asp:Label ID="lbBirthdate" runat="server"/>
        <asp:Label ID="lbOGRN" runat="server"/>
        <asp:Label ID="lbOKATO" runat="server"/>
        <asp:Label ID="lbDateFrom" runat="server"/>
        <asp:Label ID="lbDateTo" runat="server"/>
        
        <%--Popup ввода пина для модуля безопасности--%>
        <ajaxToolkit:ModalPopupExtender ID="mpeSecutiryModulePinCode" runat="server" TargetControlID="btnFake" PopupControlID="pnSecutiryModulePinCode" OkControlID="btnSecutiryModuleConfirmPin"
            CancelControlID="btnSecutiryModuleCancelPin" BackgroundCssClass="modalBackground" OnOkScript="securityModulePinEntered()" OnCancelScript="securityModulePinCanceled()">
        </ajaxToolkit:ModalPopupExtender>
         
        <%--Фейковая кнопка--%>
        <asp:Button ID="btnFake" runat="server" CssClass="HideButton" />
        
        <%--Панель ввода пина для модуля безопасности--%>
        <asp:Panel ID="pnSecutiryModulePinCode" runat="server" CssClass="modalPopup" DefaultButton="btnSecutiryModuleConfirmPin">
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbSecutiryModulePinCode" runat="server" Text="Введите Пин-код модуля безопасности:" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbSecutiryModulePinCode" runat="server" TextMode="Password" MaxLength="8" />
                        </td>
                     </tr>   
                    <tr>
                        <td>
                            <asp:Button ID="btnSecutiryModuleConfirmPin" Text="Подтвердить" runat="server"/>
                        </td>
                        <td>
                            <asp:Button ID="btnSecutiryModuleCancelPin" Text="Отменить" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>

        <%--Popup ввода пина для карты--%>
        <ajaxToolkit:ModalPopupExtender ID="mpePinCode" runat="server" TargetControlID="btnFake" PopupControlID="pnPinCode" OkControlID="btnConfirmPin"
            CancelControlID="btnCancelPin" BackgroundCssClass="modalBackground" OnOkScript="pinEntered()" OnCancelScript="pinCanceled()">
        </ajaxToolkit:ModalPopupExtender>

        <%--Панель ввода пина для карты--%>
        <asp:Panel ID="pnPinCode" runat="server" CssClass="modalPopup" DefaultButton="btnConfirmPin">
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbPinCode" runat="server" Text="Введите Пин-код:" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbPinCode" runat="server" TextMode="Password" MaxLength="4" />
                        </td>
                     </tr>   
                    <tr>
                        <td>
                            <asp:Button ID="btnConfirmPin" Text="Подтвердить" runat="server"/>
                        </td>
                        <td>
                            <asp:Button ID="btnCancelPin" Text="Отменить" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblQuestion" runat="server" Text="Сохранить данные на электронный полис?" />
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" id="btnSMCWrite" value="Да" onclick="writeSMCHandler()" autofocus="autofocus"/>
                </td>
                <td>
                    <input type="button" id="btnSMCCancel" value="Нет" onclick="cancelSMCHandler()"/>
                </td>
            </tr>
        </table>
	</div>
</asp:Content>
