<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KladrIntellisenseUserControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.KladrIntellisenseUserControl" %>

<script type="text/javascript">
    //Подлключение обработчика ввода допустимых символов
    $(document).ready(function() {
        limitKeyPressForRoomNumber();
    });

    //Добавляет новый элемент в список текущей иерархии
    var symbolsAfterCommaCounter_<%= UniqueKey %> = 0;

    function KLADR_AddHierarchyId_<%= UniqueKey %>(id) {
        var hierarchyField = $get('<%= hfKladrHierarchy.ClientID %>');
        if (hierarchyField.value.length > 0)
            hierarchyField.value += ";";
        hierarchyField.value += id;
    }

    //Удаляет последний добавленный элемент из текущей иерархии
    function KLADR_RemoveLastHierarchyId_<%= UniqueKey %>() {

        var hierarchyField = $get('<%= hfKladrHierarchy.ClientID %>');
        var spittedHierarchy = hierarchyField.value.split(';');
        hierarchyField.value = "";
        for (var i = 0; i < spittedHierarchy.length - 1; i++)
            hierarchyField.value += spittedHierarchy[i] + ";";
        if (hierarchyField.value[hierarchyField.value.length - 1] == ";")
            hierarchyField.value = hierarchyField.value.substring(0, hierarchyField.value.length - 1);
    }

    //Возвращает последний добавленный элемент из текущей иерархии
    function KLADR_GetCurrentHierarchyId_<%= UniqueKey %>() {
        var hierarchyField = $get('<%= hfKladrHierarchy.ClientID %>');
        var spittedHierarchy = hierarchyField.value.split(';');
        if (spittedHierarchy.length > 0)
            return spittedHierarchy[spittedHierarchy.length - 1];
        return null;
    }

    function KLADR_itemSelected_<%= UniqueKey %>(sender, e) {
        var key = e.get_value();
        $find('<%= aceKLADRIntellisense.ClientID %>').set_contextKey(key);
        KLADR_AddHierarchyId_<%= UniqueKey %>(e.get_value());
        var textBox = $get('<%= tbKLADRIntellisense.ClientID %>');
        textBox.value += ",";
        symbolsAfterCommaCounter_<%= UniqueKey %> = 0;

        //Принудительный Postback для обновления индекса
        var kladrId = KLADR_GetCurrentHierarchyId_<%= UniqueKey %>();
        PageMethods.GetPostcodeByKladrId(kladrId, onSuccessGet_<%= UniqueKey %>, onErrorGet_<%= UniqueKey %>);
    }

    function onSuccessGet_<%= UniqueKey %>(result) {
        var tbPostcode = $get('<%= tbPostcode.ClientID %>');
        tbPostcode.value = result;
    }

    function onErrorGet_<%= UniqueKey %>(result) {
    }

    var maxChars = 1000;

    function KLADR_KeyPress_<%= UniqueKey %>(e) {
        var evt = e || window.event;
        if (evt) {
            var keyCode = evt.charCode || evt.keyCode;
            if (symbolsAfterCommaCounter_<%= UniqueKey %> >= maxChars) { //Количество символов больше 3, запрещаем ввод
                evt.returnValue = false;
            } else {
                symbolsAfterCommaCounter_<%= UniqueKey %>++; //Увеличиваем счетчик введенных символов
            }
        }
    }

    function KLADR_KeyDown_<%= UniqueKey %>(e) {
        var evt = e || window.event;
        if (evt) {
            var keyCode = evt.charCode || evt.keyCode;

            switch (keyCode) {
            case 33: //Page Up
            case 34: //Page Down
            case 35: //Home
            case 36: //End
            case 37: //Влево
            case 38: //Вправо
                evt.returnValue = false;
                break;
            case 8: //Backspace
                //Удаление категории
                var tbKLADRIntellisense = $get('<%= tbKLADRIntellisense.ClientID %>');

                var value = tbKLADRIntellisense.value;
                if (value.charAt(value.length - 1) == ",")
                    value = value.substring(0, value.length - 1);
                var spittedValue = value.split(',');
                var newValue = "";
                for (var i = 0; i < spittedValue.length - 1; i++) {
                    newValue += spittedValue[i] + ",";
                }

                tbKLADRIntellisense.value = newValue;
                var hierarchyField = $get('<%= hfKladrHierarchy.ClientID %>');
                var spittedHierarchy = hierarchyField.value.split(';');
                if (spittedValue.length <= spittedHierarchy.length) {
                    KLADR_RemoveLastHierarchyId_<%= UniqueKey %>();
                    var key = KLADR_GetCurrentHierarchyId_<%= UniqueKey %>();
                    $find('<%= aceKLADRIntellisense.ClientID %>').set_contextKey(key);
                }
                evt.returnValue = false;

                //Принудительный Postback для обновления индекса
                var kladrId = KLADR_GetCurrentHierarchyId_<%= UniqueKey %>();
                PageMethods.GetPostcodeByKladrId( kladrId, onSuccessGet_<%= UniqueKey %>, onErrorGet_<%= UniqueKey %>);
            }
        }
    }

    function KLADR_DisableSelection_<%= UniqueKey %>() {
        var tbKLADRIntellisesne = $get('<%= tbKLADRIntellisense.ClientID %>');
        if (typeof tbKLADRIntellisesne.onselectstart != "undefined") //For IE 
            tbKLADRIntellisesne.onselectstart = function() {
                return false;
            };
        else if (typeof tbKLADRIntellisesne.style.MozUserSelect != "undefined") //For Firefox
            tbKLADRIntellisesne.style.MozUserSelect = "none";
        else //All other route (For Opera)
            tbKLADRIntellisesne.onmousedown = function() {
                return false;
            };
        tbKLADRIntellisesne.style.cursor = "default";
    }


    function KLADR_ListPopulating_<%= UniqueKey %>(source, e) {
        var textboxControl = $get('<%= tbKLADRIntellisense.ClientID %>'); // Get the textbox control.
        textboxControl.style.backgroundImage = 'url(~/Resources/ajax-loader.gif)';
        textboxControl.style.backgroundRepeat = 'no-repeat';
        textboxControl.style.backgroundPosition = 'right';
    }

    function KLADR_ListPopulated_<%= UniqueKey %>(sender, e) {
        var textboxControl = $get('<%= tbKLADRIntellisense.ClientID %>'); // Get the textbox control.
        textboxControl.style.backgroundImage = 'none';
    }

    function KLADR_ListShowing_<%= UniqueKey %>(sender, e) {
        var searchList = sender.get_completionList().childNodes;
        if (searchList.length == 1) {
            sender._setText(searchList[0]);
            e.set_cancel(true);
        }
    }

    function KLADR_MouseUp_<%= UniqueKey %>() {
        var tbKLADRIntellisesne = $get('<%= tbKLADRIntellisense.ClientID %>');
        if (tbKLADRIntellisesne.createTextRange) {
            //IE  
            var FieldRange = tbKLADRIntellisesne.createTextRange();
            FieldRange.moveStart('character', tbKLADRIntellisesne.value.length);
            FieldRange.collapse();
            FieldRange.select();
        } else {
            //Firefox and Opera  
            tbKLADRIntellisesne.focus();
            var length = tbKLADRIntellisesne.value.length;
            tbKLADRIntellisesne.setSelectionRange(length, length);
        }
    }

    function KLADR_OnFocus_<%= UniqueKey %>() {
        var tbKLADRIntellisesne = $get('<%= tbKLADRIntellisense.ClientID %>');
        if (tbKLADRIntellisesne.createTextRange) {
            //IE  
            var FieldRange = tbKLADRIntellisesne.createTextRange();
            FieldRange.moveStart('character', tbKLADRIntellisesne.value.length);
            FieldRange.collapse();
            FieldRange.select();
        } else {
            //Firefox and Opera  
            tbKLADRIntellisesne.focus();
            var length = tbKLADRIntellisesne.value.length;
            tbKLADRIntellisesne.setSelectionRange(length, length);
        }
    }
</script>

<div>
    <asp:HiddenField ID="hfKladrHierarchy" runat="server" />
    <table>
        <tr>
            <%--Адрес строкой--%>
            <td class="regLeftColumn">
                <asp:Label ID="lblAddress" runat="Server" Text="Адрес:" /></td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbKLADRIntellisense" runat="server" Width="100%" oncopy="return false" onpaste="return false" oncut="return false" CssClass="controlBoxes" />
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvSubject" runat="server" EnableClientScript="false" Text="Укажите регион!" />
            </td>
            <ajaxToolkit:AutoCompleteExtender
                runat="server"
                ID="aceKLADRIntellisense"
                TargetControlID="tbKLADRIntellisense"
                ServiceMethod="GetKladrList"
                FirstRowSelected="true"
                MinimumPrefixLength="3"
                CompletionInterval="100"
                EnableCaching="false"
                CompletionSetCount="20"
                DelimiterCharacters=","
                UseContextKey="true"
                CompletionListElementID="">
            </ajaxToolkit:AutoCompleteExtender>
        </tr>
    </table>
    <table>
        <tr>
            <%--Индекс--%>
            <td class="regLeftColumn">
                <asp:Label ID="lblPostcode" runat="Server" Text="Индекс" /></td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbPostcode" runat="Server" Width="100px" MaxLength="6" CssClass="room" /></td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvPostcode" runat="server" EnableClientScript="false" Text="Укажите индекс!" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <%--Номер дома--%>
            <td class="regLeftColumn">
                <asp:Label ID="lblHouse" runat="Server" Text="Номер дома (владения):" /></td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbHouse" runat="Server" Width="100px" MaxLength="7" CssClass="controlBoxes" /></td>
        </tr>
        <tr>
            <%--Корпус--%>
            <td class="regLeftColumn">
                <asp:Label ID="lblHousing" runat="Server" Text="Корпус (строение)" /></td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbHousing" runat="Server" Width="100px" MaxLength="5" CssClass="controlBoxes" /></td>
        </tr>
        <tr>
            <%--Квартира--%>
            <td class="regLeftColumn">
                <asp:Label ID="lblRoom" runat="Server" Text="Квартира" /></td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbRoom" runat="Server" Width="100px" MaxLength="4" CssClass="room" /></td>
        </tr>
    </table>
</div>