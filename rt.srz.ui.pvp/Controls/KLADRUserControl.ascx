<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KladrUserControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.KladrUserControl" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<script type="text/javascript">
    //Подлключение обработчика ввода допустимых символов
    $(document).ready(function () {
        limitKeyPressForRoomNumber();
    });

    // Create the event handlers for PageRequestManager
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(PageRequestManager_beginRequest_<%=UniqueKey %>);
    prm.add_endRequest(PageRequestManager_endRequest_<%=UniqueKey %>);
    
    var focusElementId_<%=UniqueKey %> = 0;
    function PageRequestManager_beginRequest_<%=UniqueKey %>(sender, args) {
        var ddlSubject = $get('<%= ddlSubject.ClientID %>');
        var ddlArea = $get('<%= ddlArea.ClientID %>');
        var ddlCity = $get('<%= ddlCity.ClientID %>');
        var ddlTown = $get('<%= ddlTown.ClientID %>');
        var ddlStreet = $get('<%= ddlStreet.ClientID %>');
        
        postbackElem = args.get_postBackElement();
        if (postbackElem == ddlSubject) {
            focusElementid_<%=UniqueKey %> = 1;
        }
        else
        if (postbackElem == ddlArea) {
            focusElementid_<%=UniqueKey %> = 2;
        }
        else
        if (postbackElem == ddlCity) {
            focusElementid_<%=UniqueKey %> = 3;
        }
        else
        if (postbackElem == ddlTown) {
            focusElementid_<%=UniqueKey %> = 4;
        }
        else
        if (postbackElem == ddlStreet) {
            focusElementid_<%=UniqueKey %> = 5;
        }
    }

    function PageRequestManager_endRequest_<%=UniqueKey %>(sender, args) {
        if (focusElementid_<%=UniqueKey %> == 1 && $get('<%= ddlSubject.ClientID %>') != null) {
            $get('<%= ddlSubject.ClientID %>').focus();
        }
        else
        if (focusElementid_<%=UniqueKey %> == 2 && $get('<%= ddlArea.ClientID %>') != null) {
            $get('<%= ddlArea.ClientID %>').focus();
        }
        else
        if (focusElementid_<%=UniqueKey %> == 3 && $get('<%= ddlCity.ClientID %>') != null) {
            $get('<%= ddlCity.ClientID %>').focus();
        }
        else
        if (focusElementid_<%=UniqueKey %> == 4 && $get('<%= ddlTown.ClientID %>') != null) {
            $get('<%= ddlTown.ClientID %>').focus();
        }
        else
        if (focusElementid_<%=UniqueKey %> == 5 && $get('<%= ddlStreet.ClientID %>') != null) {
            $get('<%= ddlStreet.ClientID %>').focus();
        }
    }

    function disableControls_<%=UniqueKey %>(disabled) {
        var ddlSubject = $get('<%= ddlSubject.ClientID %>');
        var ddlArea = $get('<%= ddlArea.ClientID %>');
        var ddlCity = $get('<%= ddlCity.ClientID %>');
        var ddlTown = $get('<%= ddlTown.ClientID %>');
        var ddlStreet = $get('<%= ddlStreet.ClientID %>');

        ddlSubject.disabled = disabled;
        if (ddlArea != null)
            ddlArea.disabled = disabled;
        if (ddlCity != null)
            ddlCity.disabled = disabled;
        if (ddlTown != null)
            ddlTown.disabled = disabled;
        if (ddlStreet != null)
            ddlStreet.disabled = disabled;
    }
</script>

<div>
    <asp:UpdatePanel ID="UpdatePanelKLADR" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr> <%--Регион--%>
                    <td class="regLeftColumn"><asp:Label id="lblSubject" runat="Server" Text="Регион РФ*:" /></td>
                    <td class="regRightColumn">
                        <asp:DropDownList id="ddlSubject" runat="Server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DdlSubjectSelectedIndexChanged" CssClass="dropDowns">
                            <asp:ListItem Text="Выберите регион" Value="-1" />
                        </asp:DropDownList>
                    </td>
                    <td class="errorMessage">
                        <asp:CustomValidator ID="cvSubject" runat="server" EnableClientScript="false" Text="Укажите регион!" />
                    </td>
                </tr>
                <tr> <%--Район--%>
                    <td class="regLeftColumn"><asp:Label id="lblArea" runat="Server" Text="Район:" /></td>
                    <td class="regRightColumn"><asp:DropDownList id="ddlArea"  runat="Server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DdlAreaSelectedIndexChanged" CssClass="dropDowns" /></td>
                </tr>
                <tr> <%--Город--%>
                    <td class="regLeftColumn"><asp:Label id="lblCity" runat="Server" Text="Город:" /></td>
                    <td class="regRightColumn"><asp:DropDownList id="ddlCity" runat="Server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DdlCitySelectedIndexChanged" CssClass="dropDowns"/></td>
                </tr>
                <tr> <%--Населенный пункт--%>
                    <td class="regLeftColumn"><asp:Label id="lblTown" runat="Server" Text="Населенный пункт:" /></td>
                    <td class="regRightColumn">
                        <asp:DropDownList id="ddlTown" runat="Server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DdlTownSelectedIndexChanged" CssClass="dropDowns"/>
                        <asp:TextBox id="tbTown" runat="Server" Width="100%" Visible="false" CssClass="controlBoxes"/>
                     </td>
                </tr>
                <tr> <%--Улица--%>
                    <td class="regLeftColumn"><asp:Label id="lblStreet" runat="Server" Text="Улица:" /></td>
                    <td class="regRightColumn">
                        <asp:DropDownList id="ddlStreet" runat="Server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DdlStreetSelectedIndexChanged" CssClass="dropDowns"/>
                        <asp:TextBox id="tbStreet" runat="Server" Width="100%" Visible="false" CssClass="controlBoxes"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanelPostcode" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr> <%--Индекс--%>
                    <td class="regLeftColumn"><asp:Label ID="lblPostcode" runat="Server" Text="Индекс" /></td>
                    <td class="regRightColumn"><asp:TextBox ID="tbPostcode" runat="Server" Width="100px" MaxLength="6" CssClass="room"/></td>
                    <td class="errorMessage">
                        <asp:CustomValidator ID="cvPostcode" runat="server" EnableClientScript="false" Text="Укажите индекс!" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table>
        <tr> <%--Номер дома--%>
            <td class="regLeftColumn"><asp:Label ID="lblHouse" runat="Server" Text="Номер дома (владения):" /></td>
            <td class="regRightColumn"><asp:TextBox ID="tbHouse" runat="Server" Width="100px" MaxLength="7" CssClass="controlBoxes"/></td>
        </tr>
        <tr> <%--Корпус--%>
            <td class="regLeftColumn"><asp:Label ID="lblHousing" runat="Server" Text="Корпус (строение)" /></td>
            <td class="regRightColumn"><asp:TextBox ID="tbHousing" runat="Server" Width="100px" MaxLength="5" CssClass="controlBoxes"/></td>
        </tr>
        <tr> <%--Квартира--%>
            <td class="regLeftColumn"><asp:Label ID="lblRoom" runat="Server" Text="Квартира"/></td>
            <td class="regRightColumn"><asp:TextBox ID="tbRoom" runat="Server" Width="100px" MaxLength="4" CssClass="room"/></td>
        </tr>
    </table>
</div>
