<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssueOfPolicy.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.IssueOfPolicy" %>

<%@ Register Assembly="rt.srz.ui.pvp" Namespace="rt.srz.ui.pvp.Controls" TagPrefix="uc" %>

<script type="text/javascript">
    function checkKeys() {
        switch (event.keyCode) {
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

        if (String.fromCharCode(event.keyCode).match(/^\d+$/))
            return true;

        return false;
    }
    
    function policyTypeChanged() {
      if (window.$get('<%=ddlPolicyType.ClientID%>').value == 322) {
        window.$get('<%=tbPolicyCertificateNumber.ClientID%>').maxLength = 14;
      } else {
        window.$get('<%=tbPolicyCertificateNumber.ClientID%>').maxLength = 11;
      }
    }
</script>

<div>
    <%--Тип полиса--%>
    <div style="clear: both;">
        <div class="admLabelControl1_4">
            <div class="admControlPadding">
                <asp:Label ID="lbPolicyType" runat="server" Text="Тип выданного полиса:"></asp:Label>
            </div>
        </div>
        <div class="admValueControl1_4">
            <div class="admControlPadding">
                <asp:DropDownList ID="ddlPolicyType" runat="server" Enabled="false" MaxLength="14" onChange="policyTypeChanged();" Width="100%" CssClass="controlBoxes"></asp:DropDownList>
            </div>
        </div>
        <div class="errorMessage">
            <asp:CustomValidator ID="cvPolicyType" runat="server" EnableClientScript="false" Text="" />
        </div>
    </div>

    <%--Номер ЕНП--%>
    <div style="clear: both;">
        <div class="admLabelControl1_4">
            <div class="admControlPadding">
                <asp:Label ID="lbEnpNumber" runat="server" Text="Номер ЕНП:"></asp:Label>
            </div>
        </div>
        <div class="admValueControl1_4">
            <div class="admControlPadding">
                <asp:TextBox ID="tbEnpNumber" runat="server" MaxLength="16" Width="100%" CssClass="controlBoxes" onkeydown="return checkKeys()"></asp:TextBox>
            </div>
        </div>
        <div class="errorMessage">
            <asp:CustomValidator ID="cvEnpNumber" runat="server" EnableClientScript="false" Text=""/>
        </div>
    </div>

    <%--Номер бланка--%>
    <div style="clear: both;">
        <div class="admLabelControl1_4">
            <div class="admControlPadding">
                <asp:Label ID="lbPolicyCertificateNumber" runat="server" Text="Номер бланка:"></asp:Label>
            </div>
        </div>
        <div class="admValueControl1_4">
            <div class="admControlPadding">
                <asp:TextBox ID="tbPolicyCertificateNumber" runat="server" MaxLength="14" Width="100%" CssClass="controlBoxes" onkeydown="return checkKeys()"></asp:TextBox>
            </div>
        </div>
        <div class="errorMessage">
            <asp:CustomValidator ID="cvPolicyCertificateNumber" runat="server" EnableClientScript="false" Text=""/>
        </div>
    </div>

    <%--Дата выдачи--%>
    <div style="clear: both;">
        <div class="admLabelControl1_4">
            <div class="admControlPadding">
                <asp:Label ID="lbPolicyDateIssue" runat="server" Text="Дата выдачи полиса:"></asp:Label>
            </div>
        </div>
        <div class="admValueControl1_4">
            <div class="admControlPadding">
                <uc:DateBox runat="server" ID="tbPolicyDateIssue" Width="150px" CssClass="controlBoxes" />
            </div>
        </div>
        <div class="errorMessage">
            <asp:CustomValidator ID="cvPolicyDateIssue" runat="server" EnableClientScript="false" Text=""/>
        </div>
    </div>

    <%--Дата окончания действия--%>
    <div style="clear: both;">
        <div class="admLabelControl1_4">
            <div class="admControlPadding">
                <asp:Label ID="lbPolicyDateEnd" runat="server" Text="Дата окончания действия полиса:"></asp:Label>
            </div>
        </div>
        <div class="admValueControl1_4">
            <div class="admControlPadding">
                <uc:DateBox runat="server" ID="tbPolicyDateEnd" Width="150px" Enabled="false" CssClass="controlBoxes" />
            </div>
        </div>
    </div>
</div>
