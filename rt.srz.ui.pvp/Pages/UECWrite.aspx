<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="UECWrite.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.UECWrite" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="False" ScriptMode="Release"/>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-2.1.0.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-migrate-1.2.1.js") %>"></script>
    <script type="text/javascript">

    //Вывод окна для ввода пина для УЭК карты
    function showUECPinPad() {
        $find('<%= mpeUECPinCode.ClientID%>').show();
    }

    //Закрытия окна для ввода пина по ESC
    $(document).keyup(function (event) {
        if (event.keyCode == 27) {//ESC
            var mpe = $find('<%= mpeUECPinCode.ClientID%>');
            if (mpe != null && mpe != undefined) {
                mpe.hide();
            }
        }
    });

    //Непосредственно запись
    function writeUEC() {
        var lastName = $get('<%= hfLastName.ClientID%>').value;
        var firstName = $get('<%= hfFirstName.ClientID%>').value;
        var midleName = $get('<%= hfMiddleName.ClientID%>').value;
        var birthDate = $get('<%= hfBirthdate.ClientID%>').value;
        var ogrn = $get('<%= hfOGRN.ClientID%>').value;
        var okato = $get('<%= hfOKATO.ClientID%>').value;
        var dateFrom = $get('<%= hfDateFrom.ClientID%>').value;
        var dateTo = $get('<%= hfDateTo.ClientID%>').value;

        //Вызов ActiveX
        var resdata = window.uecReader.WriteOmsData(lastName, firstName, midleName, birthDate, ogrn, okato, dateFrom, dateTo);
        if (resdata.Result != 0) {
            alert(resdata.ErrorString);
            return;
        }

        window.location = '<%= ResolveUrl("~/Pages/Main.aspx") %>';
    }

    //Обработчик ввода пин для УЭК карты
    function UECPinEntered() {
        var tbUECPinCode = $get('<%= tbUECPinCode.ClientID%>');
        var resa = window.uecReader.Authorize(tbUECPinCode.value); //попытка авторизации
        tbUECPinCode.value = '';
        if (resa.Result == 0) {
            writeUEC(); //запись
            window.uecReader.CloseCard();  //закрытие карты
        }
        else {
            alert(resa.ErrorString);
            if (resa.PinRestTriesOut > 0)
                showUEKPinPad(); //повторный вывод окна для ввода пина
            else
                window.uecReader.CloseCard();  //закрытие карты
        }
    }

    //сохранение на карту
    function writeUECHandler() {
        try {
            //открытие карты
            var res = window.uecReader.OpenCard(<%= string.Format("'{0}'", authService.GetAuthToken().Signature) %>); //открытие карты
            if (res.Result == 0) {
                //Вывод окна для ввода пина для УЭК карты
                showUECPinPad();
            } else {
                alert(res.ErrorString);
            }
        } catch (e) {
            alert(e);
        }
    }

    //отмена сохранения
    function cancelUECHandler() {
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
        <ajaxToolkit:ModalPopupExtender ID="mpeUECPinCode" runat="server" TargetControlID="btnFake" PopupControlID="pnUECPinCode" OkControlID="btnUECConfirmPin"
            CancelControlID="btnUECCancelPin" BackgroundCssClass="modalBackground" OnOkScript="UECPinEntered()" OnCancelScript="UECPinCanceled()">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Button ID="btnFake" runat="server" CssClass="HideButton" />
        <asp:Panel ID="pnUECPinCode" runat="server" CssClass="modalPopup" DefaultButton="btnUECConfirmPin">
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbUECPinCode" runat="server" Text="Введите Pin:" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbUECPinCode" runat="server" TextMode="Password" MaxLength="4" />
                        </td>
                     </tr>   
                    <tr>
                        <td>
                            <asp:Button ID="btnUECConfirmPin" Text="Подтвердить" runat="server"/>
                        </td>
                        <td>
                            <asp:Button ID="btnUECCancelPin" Text="Отменить" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblQuestion" runat="server" Text="Сохранить данные на УЭК?" />
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" id="btnUECWrite" value="Да" onclick="writeUECHandler()" autofocus="autofocus"/>
                </td>
                <td>
                    <input type="button" id="btnUECCancel" value="Нет" onclick="cancelUECHandler()"/>
                </td>
            </tr>
        </table>
	</div>
</asp:Content>

