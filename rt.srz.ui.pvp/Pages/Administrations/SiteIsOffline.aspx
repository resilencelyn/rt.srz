<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/AuthentificationPage.Master" CodeBehind="SiteIsOffline.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.SiteIsOffline" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="false" ScriptMode="Release"/>

    <asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick" />

    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
      <ContentTemplate>
        <div style="height: 100%; font-size: x-large; background-color: InactiveBorder; margin-left: auto; margin-right: auto;">
          <table>
            <tr>
              <td>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/settings.png" />
              </td>
              <td>
                <asp:Label ID="lb" runat="server" Text="Сайт находится на тех. обслуживании. Доступ будет возобновлён с {0}"></asp:Label>
              </td>
            </tr>
          </table>
        </div>
      </ContentTemplate>
      <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
      </Triggers>
    </asp:UpdatePanel>

</asp:Content>