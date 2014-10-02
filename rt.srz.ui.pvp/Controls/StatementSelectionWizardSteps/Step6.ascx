<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step6.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps.Step6" %>

<%@ Import Namespace="rt.srz.model.srz.concepts" %>
<%@ Register Assembly="rt.srz.ui.pvp" Namespace="rt.srz.ui.pvp.Controls" TagPrefix="uc" %>

<script type="text/javascript">

    function сheckKeys(keypressed) {
        switch (keypressed) {
            case 8:
            case 9:
            case 13:
            case 17:
            case 18:
            case 35:
            case 36:
            case 37:
            case 39:
            case 46:
            case 96:
            case 97:
            case 98:
            case 99:
            case 100:
            case 101:
            case 102:
            case 103:
            case 104:
            case 105:
                return true;
        }
        return false;
    }

    function temporaryCertificateNumberChanged() {
        var validator = $get('<%=cvTemporaryCertificateNumber.ClientID%>');
        if (validator != null)
            validator.style.visibility = 'hidden';

        //проверка чтобы номер содержал только цифры и нельзя было ввести символы
        if (сheckKeys(event.keyCode))
            return true;

        if (!String.fromCharCode(event.keyCode).match(/^\d+$/)) {
            return false;
        }
        return true;
    }
    
    function temporaryCertificateDateIssueChanged() {
        var validator = $get('<%=rfvTemporaryCertificateDateIssue.ClientID%>');
        if (validator != null)
            validator.style.visibility = 'hidden';
    }

    function policyIsIssuedChecked() {
        var requestedPolicyType = $get('<%=hfRequestedPolicyType.ClientID%>');
        var policyType = $get('<%=ddlPolicyType.ClientID%>');
        var enpNumber = $get('<%=tbEnpNumber.ClientID%>');
        var policyNumber = $get('<%=tbPolicyNumber.ClientID%>');
        var policyDateIssue = $get('<%=tbPolicyDateIssue.ClientID%>');
        if ($get('<%=chbPolicyIsIssued.ClientID%>').checked == false) {
            policyType.disabled = true;
            policyType.value = requestedPolicyType.value;
            enpNumber.disabled = true;
            enpNumber.value = "";
            policyNumber.disabled = true;
            policyNumber.value = "";
            policyDateIssue.disabled = true;
            policyDateIssue.value = "";
        }
        else {
            if (requestedPolicyType.value == '<%=PolisType.К%>') {
                policyType.disabled = true;
                policyType.value = '<%=PolisType.П%>';
            }
            enpNumber.disabled = false;
            policyNumber.disabled = false;
            policyDateIssue.disabled = false;
            policyDateIssue.value = getCurrentDate();
        }
    }

    function policyNumberChanged() {
        var validator = $get('<%=cvPolicyNumber.ClientID%>');
        if (validator != null)
            validator.style.visibility = 'hidden';

        //проверка чтобы номер содержал только цифры и нельзя было ввести символы
        if (сheckKeys(event.keyCode))
            return true;

        if (!String.fromCharCode(event.keyCode).match(/^\d+$/))
            return false;

        //проверяем длину
        var policyNumber = $get('<%=tbPolicyNumber.ClientID%>');
        var policyType = $get('<%=ddlPolicyType.ClientID%>');
        var maxLength = policyType.value == '<%=PolisType.К%>' ? 14 : 11;
        if (policyNumber.value.length > maxLength)
            policyNumber.value = policyNumber.value.substr(0, maxLength);

        return true;
    }

    function enpNumberChanged() {
        var validator = $get('<%=cvEnpNumber.ClientID%>');
        if (validator != null)
            validator.style.visibility = 'hidden';

        //проверка чтобы номер содержал только цифры и нельзя было ввести символы
        if (сheckKeys(event.keyCode))
            return true;

        if (!String.fromCharCode(event.keyCode).match(/^\d+$/))
            return false;

        return true;
    }

    function policyDateIssueChanged() {
        var validator = $get('<%=cvPolicyDateIssue.ClientID%>');
        if (validator != null)
            validator.style.visibility = 'hidden';
    }

    function getCurrentDate() {
        var currentDate = new Date();
        var day = currentDate.getDate();
        var month = currentDate.getMonth() + 1;
        var fullMonth = "";
        if (month < 10)
            fullMonth = '0' + month.toString();
        else
            fullMonth = month.toString();

        var year = currentDate.getFullYear();
        return day + "." + fullMonth + "." + year;
    }
    
    function policyTypeChanged() {
      if (window.$get('<%=ddlPolicyType.ClientID%>').value == 322) {
         window.$get('<%=tbPolicyNumber.ClientID%>').maxLength = 14;
      } else {
        window.$get('<%=tbPolicyNumber.ClientID%>').maxLength = 11;
      }
    }
</script>

<div class="wizardDiv">
    <asp:CustomValidator ID="cvSteps" runat="server" EnableClientScript="false" CssClass="errorMessage" /><br />
    <br />
    <div class="headerSubTitles">
        <asp:Label ID="lbTemporaryCertificate" runat="server" Text="Укажите сведения о выданном временном свидетельстве"></asp:Label>
    </div>
    <table>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="lblTemporaryCertificateNumber" runat="Server" Text="Номер:" />
            </td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbTemporaryCertificateNumber" runat="Server" Width="100%" MaxLength="9" CssClass="controlBoxes" onkeydown="return temporaryCertificateNumberChanged()" />

            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvTemporaryCertificateNumber" runat="server" EnableClientScript="false" Text=""
                    OnServerValidate="ValidateTemporaryCertificateNumber" />
            </td>
        </tr>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="lblTemporaryCertificateDateIssue" runat="Server" Text="Дата выдачи:" />

            </td>
            <td class="regRightColumn">
                <uc:DateBox runat="server" ID="tbTemporaryCertificateDateIssue" Width="150px" OnChange="temporaryCertificateDateIssueChanged()" CssClass="controlBoxes" />
                <asp:RequiredFieldValidator ID="rfvTemporaryCertificateDateIssue" runat="server" EnableClientScript="false"
                    Text="Не указана дата выдачи ВС" ControlToValidate="tbTemporaryCertificateDateIssue" CssClass="errorMessage" />
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvTemporaryCertificateDateIssue" runat="server" EnableClientScript="false" Text=""
                    OnServerValidate="ValidateTemporaryCertificateDateIssue" />
            </td>
        </tr>
    </table>
    <br />
    <div class="headerSubTitles">
        <asp:Label ID="Label1" runat="server" Text="Укажите сведения о выданном полисе"></asp:Label>
    </div>
    
    <%--Тип заказанного полиса--%>
    <asp:HiddenField runat="server" ID="hfRequestedPolicyType"/>
    
    <table>
        <tr>
            <td class="regLeftColumn"></td>
            <td class="regRightColumn">
                <asp:CheckBox ID="chbPolicyIsIssued" runat="server" Text="Выдан полис" Checked="False" OnClick="policyIsIssuedChecked()" />
            </td>
        </tr>
        
        <%--Тип полиса--%>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="lbPolicyType" runat="server" Text="Тип выданного полиса:"></asp:Label>
            </td>
            <td class="regRightColumn">
                <asp:DropDownList ID="ddlPolicyType" runat="server" Enabled="false" MaxLength="14" Width="100%" onChange="policyTypeChanged();" CssClass="controlBoxes"></asp:DropDownList>
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvPolicyType" runat="server" EnableClientScript="false" Text="" OnServerValidate="ValidatePolicyType" />    
            </td>
        </tr>

        <%--Номер ЕНП--%>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="lbEnpNumber" runat="server" Text="Номер ЕНП:"></asp:Label>
            </td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbEnpNumber" runat="server" MaxLength="16" Width="100%" OnKeyUp="return enpNumberChanged()" CssClass="controlBoxes"></asp:TextBox>
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvEnpNumber" runat="server" EnableClientScript="false" Text="" OnServerValidate="ValidateEnpNumber"/>
            </td>
        </tr>

        <%--Номер бланка--%>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="lbPolicyNumber" runat="server" Text="Номер бланка:"></asp:Label>
            </td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbPolicyNumber" runat="server" MaxLength="14" Width="100%" OnKeyUp="return policyNumberChanged()" CssClass="controlBoxes"></asp:TextBox>
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvPolicyNumber" runat="server" EnableClientScript="false" Text="" OnServerValidate="ValidatePolicyNumber"/>
            </td>
        </tr>

        <%--Дата выдачи--%>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="lbPolicyDateIssue" runat="server" Text="Дата выдачи полиса:"></asp:Label>
            </td>
            <td class="regRightColumn">
                <uc:DateBox runat="server" ID="tbPolicyDateIssue" Width="150px" OnChange="policyDateIssueChanged" CssClass="controlBoxes" />
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvPolicyDateIssue" runat="server" EnableClientScript="false" Text="" onchange="policyDateIssueChanged()" OnServerValidate="ValidatePolicyDateIssue"/>
            </td>
        </tr>

        <%--Дата окончания действия--%>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="lbPolicyDateEnd" runat="server" Text="Дата окончания действия полиса:"></asp:Label>
            </td>
            <td class="regRightColumn">
                <uc:DateBox runat="server" ID="tbPolicyDateEnd" Width="150px" Enabled="false" CssClass="controlBoxes" />
            </td>
        </tr>
    </table>
</div>
